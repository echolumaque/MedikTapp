using MedikTapp.Tables;
using SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        public async Task<CreateTableResult> CreateTable<T>() where T : DatabaseTable
        {
            var data = await Database
                    .CreateTableAsync(typeof(T), CreateFlags.None)
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<bool> TableColumnExists(string table, string column)
        {
            var tableInfo = await Database
                    .GetTableInfoAsync(table)
                    .ConfigureAwait(false);
            return tableInfo.Any(x => x.Name.Equals(column));
        }

        public async Task<bool> TableColumnMissing(string table, string column)
        {
            var tableInfo = await Database
                    .GetTableInfoAsync(table)
                    .ConfigureAwait(false);
            return !tableInfo.Any(x => x.Name.Equals(column));
        }

        public async Task<bool> TableExists<T>() where T : DatabaseTable
        {
            var data = !string.IsNullOrWhiteSpace(await Database
                    .ExecuteScalarAsync<string>("SELECT name FROM sqlite_master WHERE type = 'table' AND name = ?", typeof(T).Name)
                    .ConfigureAwait(false));
            return data;
        }

        public async Task<bool> TableMissing<T>() where T : DatabaseTable
        {
            var data = string.IsNullOrWhiteSpace(await Database
                    .ExecuteScalarAsync<string>("SELECT name FROM sqlite_master WHERE type = 'table' AND name = ?", typeof(T).Name)
                    .ConfigureAwait(false));
            return data;
        }
    }
}