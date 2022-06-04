using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        public async Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            var data = await Database
                    .Table<T>()
                    .Where(predicate)
                    .ToListAsync()
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<IEnumerable<T>> Find<T>() where T : new()
        {
            var data = await Database
                    .Table<T>()
                    .ToListAsync()
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<int> FindCount<T>() where T : new()
        {
            var data = await Database
                    .Table<T>()
                    .CountAsync()
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<T> FindSingle<T>() where T : new()
        {
            var data = await Database
                    .FindWithQueryAsync<T>($"SELECT * FROM {typeof(T).Name}")
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<T> FindSingle<T>(string s) where T : new()
        {
            var data = await Database
                    .FindWithQueryAsync<T>(s)
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<T> FindSingle<T>(string s, params object[] parameters) where T : new()
        {
            var data = await Database
                    .FindWithQueryAsync<T>(s, parameters)
                    .ConfigureAwait(false);
            return data;
        }
    }
}