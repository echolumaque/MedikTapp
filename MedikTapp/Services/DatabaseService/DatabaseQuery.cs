using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        public async Task<IEnumerable<T>> Query<T>(string s) where T : new() //for read operations
        {
            var data = await Database
                   .QueryAsync<T>(s)
                   .ConfigureAwait(false);
            return data;
        }

        public async Task<IEnumerable<T>> Query<T>(string s, params object[] parameters) where T : new()
        {
            var data = await Database
                    .QueryAsync<T>(s, parameters)
                    .ConfigureAwait(false);
            return data;
        }
    }
}