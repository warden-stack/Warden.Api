//using System;
//using System.Collections.Concurrent;
//using System.Threading.Tasks;
//using RethinkDb.Driver;
//using RethinkDb.Driver.Ast;
//using RethinkDb.Driver.Net;

//namespace Warden.Services.WardenChecks.Rethink
//{
//    public class RethinkDbWardenCheckStorage : IWardenCheckStorage
//    {
//        private readonly RethinkDB _rethinkDb = RethinkDB.R;
//        private readonly RethinkDbSettings _dbSettings;
//        private readonly IConnection _connection;
//        private bool _streamEnabled = false;
//        private readonly ConcurrentDictionary<object, Action<object>> _subscribers =
//            new ConcurrentDictionary<object, Action<object>>();

//        public RethinkDbWardenCheckStorage(RethinkDbSettings dbSettings)
//        {
//            _dbSettings = dbSettings;
//            _connection = Connect();
//        }

//        public async Task SaveAsync(object storage)
//            => await WardenChecks
//                .Insert(new StorageData(storage))
//                .RunAsync(_connection);

//        public async Task EnableStreamAsync()
//        {
//            _streamEnabled = true;
//            var stream = await WardenChecks.Changes().RunChangesAsync<StorageData>(_connection);
//            while (stream.IsOpen && _streamEnabled)
//            {
//                foreach (var value in stream)
//                {
//                    var storage = value.NewValue;
//                    var dto = MapToDto(storage);
//                    foreach (var subscriber in _subscribers)
//                    {
//                        subscriber.Value(dto);
//                    }
//                }
//                await stream.MoveNextAsync();
//            }
//        }

//        public void DisableStream()
//        {
//            _streamEnabled = false;
//        }

//        public void SubscribeToStream(object subscriber, Action<object> action)
//        {
//            UnsubscribeFromStream(subscriber);
//            _subscribers.TryAdd(subscriber, action);
//        }

//        public void UnsubscribeFromStream(object subscriber)
//        {
//            if(!_subscribers.ContainsKey(subscriber))
//                return;

//            Action<object> action;
//            _subscribers.TryRemove(subscriber, out action);
//        }

//        public void RemoveAllStreamSubscribers()
//        {
//            _subscribers.Clear();
//        }

//        private Table WardenChecks => _rethinkDb.Db(_dbSettings.Database)
//            .Table(_dbSettings.TableName);

//        private IConnection Connect()
//            => _rethinkDb.Connection()
//                .Hostname(_dbSettings.Hostname)
//                .User(_dbSettings.User, _dbSettings.Password)
//                .Port(_dbSettings.Port)
//                .Timeout(_dbSettings.TimeoutSeconds)
//                .Connect();

//        private WardenCheckResultStorageDto MapToDto(StorageData data)
//            => new WardenCheckResultStorageDto
//            {
//                OrganizationId = Guid.Parse(data.o),
//                WardenId = Guid.Parse(data.w),
//                CreatedAt = new DateTime(data.c),
//                Result = new WardenCheckResultDto
//                {
//                    CompletedAt = new DateTime(data.r.c),
//                    ExecutionTime = new TimeSpan(data.r.t),
//                    StartedAt = new DateTime(data.r.s),
//                    IsValid = data.r.v,
//                    WatcherCheckResult = new WatcherCheckResultDto
//                    {
//                        WatcherName = data.r.r.n,
//                        WatcherType = data.r.r.t,
//                        Description = data.r.r.d,
//                        IsValid = data.r.r.v
//                    },
//                    Exception = data.r.e != null ? MapExceptionDto(data.r.e) : null
//                }
//            };

//        private ExceptionDto MapExceptionDto(ExceptionData data) =>
//            new ExceptionDto
//            {
//                Message = data.m,
//                Source = data.s,
//                StackTrace = data.t,
//                InnerException = data.i != null ? MapExceptionDto(data.i) : null
//            };

//        //Internal classes using the low amount of data for storing the JSON objects within a RethinkDB.
//        private class StorageData
//        {
//            public string o { get; set; }
//            public string w { get; set; }
//            public long c { get; set; }
//            public WardenCheckResultData r { get; set; }

//            public StorageData()
//            {
//            }

//            public StorageData(WardenCheckResultStorageDto storage)
//            {
//                o = storage.OrganizationId.ToString("N");
//                w = storage.WardenId.ToString("N");
//                c = storage.CreatedAt.Ticks;
//                r = new WardenCheckResultData(storage.Result);
//            }
//        }

//        private class WardenCheckResultData
//        {
//            public bool v { get; set; }
//            public WatcherCheckResultData r { get; set; }
//            public long s { get; set; }
//            public long c { get; set; }
//            public long t { get; set; }
//            public ExceptionData e { get; set; }

//            public WardenCheckResultData()
//            {
//            }

//            public WardenCheckResultData(WardenCheckResultDto check)
//            {
//                v = check.IsValid;
//                r = new WatcherCheckResultData(check.WatcherCheckResult);
//                s = check.StartedAt.Ticks;
//                c = check.CompletedAt.Ticks;
//                t = check.ExecutionTime.Ticks;
//                e = check.Exception != null ? new ExceptionData(check.Exception) : null;
//            }
//        }

//        private class WatcherCheckResultData
//        {
//            public string n { get; set; }
//            public string t { get; set; }
//            public string d { get; set; }
//            public bool v { get; set; }

//            public WatcherCheckResultData()
//            {
//            }

//            public WatcherCheckResultData(WatcherCheckResultDto check)
//            {
//                n = check.WatcherName;
//                t = check.WatcherName;
//                d = check.Description;
//                v = check.IsValid;
//            }
//        }

//        private class ExceptionData
//        {
//            public string m { get; set; }
//            public string s { get; set; }
//            public string t { get; set; }
//            public ExceptionData i { get; set; }

//            public ExceptionData()
//            {
//            }

//            public ExceptionData(ExceptionDto exception)
//            {
//                m = exception.Message;
//                s = exception.Source;
//                t = exception.StackTrace;
//                i = exception.InnerException != null ? new ExceptionData(exception.InnerException) : null;
//            }
//        }
//    }
//}