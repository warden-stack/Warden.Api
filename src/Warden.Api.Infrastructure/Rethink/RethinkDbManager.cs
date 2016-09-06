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
        private readonly IConnection _connection;

        public RethinkDbManager(RethinkDbSettings dbSettings)
        {
            _connection = Connect(dbSettings);
        }

        public async Task SaveWardenCheckResultAsync(WardenCheckResultDto check)
        {
            await WardenChecks
                .Insert(check)
                .RunAsync(_connection);
        }

        public async Task<Cursor<Change<WardenCheckResultDto>>> StreamWardenCheckResultChangesAsync()
            => await WardenChecks.Changes().RunChangesAsync<WardenCheckResultDto>(_connection);

        private Table WardenChecks => _rethinkDb.Db("Warden")
            .Table("Checks");

        private IConnection Connect(RethinkDbSettings settings)
            => _rethinkDb.Connection()
                .Hostname(settings.Hostname)
                .Port(settings.Port)
                .Timeout(settings.TimeoutSeconds)
                .Connect();
    }
}