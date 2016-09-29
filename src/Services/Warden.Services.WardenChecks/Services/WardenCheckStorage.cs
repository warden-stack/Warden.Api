using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Net;
using Warden.Services.WardenChecks.Domain;
using Warden.Services.WardenChecks.Rethink;

namespace Warden.Services.WardenChecks.Services
{
    public class WardenCheckStorage : IWardenCheckStorage
    {
        private readonly RethinkDB _rethinkDb = RethinkDB.R;
        private readonly RethinkDbSettings _dbSettings;
        private readonly IConnection _connection;

        public WardenCheckStorage(RethinkDbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            _connection = Connect();
        }

        public async Task SaveAsync(WardenCheckResultStorage storage)
            => await WardenChecks
                .Insert(new StorageData(storage))
                .RunAsync(_connection);

        private Table WardenChecks => _rethinkDb.Db(_dbSettings.Database)
            .Table(_dbSettings.TableName);

        private IConnection Connect()
            => _rethinkDb.Connection()
                .Hostname(_dbSettings.Hostname)
                .User(_dbSettings.User, _dbSettings.Password)
                .Port(_dbSettings.Port)
                .Timeout(_dbSettings.TimeoutSeconds)
                .Connect();

        //Internal classes using the low amount of data for storing the JSON objects within a RethinkDB.
        private class StorageData
        {
            public string o { get; set; }
            public string w { get; set; }
            public long c { get; set; }
            public WardenCheckResultData r { get; set; }

            public StorageData()
            {
            }

            public StorageData(WardenCheckResultStorage storage)
            {
                o = storage.OrganizationId.ToString("N");
                w = storage.WardenId.ToString("N");
                c = storage.CreatedAt.Ticks;
                r = new WardenCheckResultData(storage.Result);
            }
        }

        private class WardenCheckResultData
        {
            public bool v { get; set; }
            public WatcherCheckResultData r { get; set; }
            public long s { get; set; }
            public long c { get; set; }
            public long t { get; set; }
            public ExceptionData e { get; set; }

            public WardenCheckResultData()
            {
            }

            public WardenCheckResultData(WardenCheckResult check)
            {
                v = check.IsValid;
                r = new WatcherCheckResultData(check.WatcherCheckResult);
                s = check.StartedAt.Ticks;
                c = check.CompletedAt.Ticks;
                t = check.ExecutionTime.Ticks;
                e = check.Exception != null ? new ExceptionData(check.Exception) : null;
            }
        }

        private class WatcherCheckResultData
        {
            public string n { get; set; }
            public string t { get; set; }
            public string d { get; set; }
            public bool v { get; set; }

            public WatcherCheckResultData()
            {
            }

            public WatcherCheckResultData(WatcherCheckResult check)
            {
                n = check.Watcher.Name;
                t = check.Watcher.Type;
                d = check.Description;
                v = check.IsValid;
            }
        }

        private class ExceptionData
        {
            public string m { get; set; }
            public string s { get; set; }
            public string t { get; set; }
            public ExceptionData i { get; set; }

            public ExceptionData()
            {
            }

            public ExceptionData(ExceptionInfo exception)
            {
                m = exception.Message;
                s = exception.Source;
                t = exception.StackTrace;
                i = exception.InnerException != null ? new ExceptionData(exception.InnerException) : null;
            }
        }
    }
}