using MedikTapp.Tables;
using System.Threading.Tasks;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        public async Task<int> Insert<T>(T entity) where T : DatabaseTable
        {
            var data = await Database
                    .InsertAsync(entity)
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<int> Delete<T>(int id) where T : DatabaseTable
        {
            var data = await Database.DeleteAsync<T>(id).ConfigureAwait(false);
            return data;
        }

        public async Task<int> Delete<T>(string id) where T : DatabaseTable
        {
            var data = await Database.DeleteAsync<T>(id).ConfigureAwait(false);
            return data;
        }

        public async Task<int> Delete<T>(T entity) where T : DatabaseTable
        {
            var data = await Database.DeleteAsync(entity).ConfigureAwait(false);
            return data;
        }

        public async Task<int> DeleteAll<T>() where T : DatabaseTable
        {
            var data = await Database.DeleteAllAsync<T>().ConfigureAwait(false);
            return data;
        }

        public async Task<int> Update<T>(T entity) where T : DatabaseTable
        {
            var data = await Database
                    .UpdateAsync(entity)
                    .ConfigureAwait(false);
            return data;
        }
    }
}