using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Ast;
using RethinkDb.Driver.Model;
using RethinkDb.Driver.Net;
using Warden.Api.Infrastructure.DTO.Wardens;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Rethink
{
    public class RethinkDbManager : IRethinkDbManager
    {
        private readonly RethinkDB _rethinkDb = RethinkDB.R;
        private readonly RethinkDbSettings _dbSettings;
        private readonly IConnection _connection;

        public RethinkDbManager(RethinkDbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            _connection = Connect();
        }

        public async Task SaveWardenCheckResultAsync(WardenCheckResultStorageDto check)
        {
            await WardenChecks
                .Insert(check)
                .RunAsync(_connection);
        }

        public async Task<Cursor<Change<WardenCheckResultStorageDto>>> StreamWardenCheckResultChangesAsync()
            => await WardenChecks.Changes().RunChangesAsync<WardenCheckResultStorageDto>(_connection);

        private Table WardenChecks => _rethinkDb.Db(_dbSettings.Database)
            .Table(_dbSettings.TableName);

        private IConnection Connect()
            => _rethinkDb.Connection()
                .Hostname(_dbSettings.Hostname)
                .User(_dbSettings.User, _dbSettings.Password)
                .Port(_dbSettings.Port)
                .Timeout(_dbSettings.TimeoutSeconds)
                .Connect();
    }
}