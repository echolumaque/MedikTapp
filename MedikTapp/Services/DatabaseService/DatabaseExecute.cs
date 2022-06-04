using System.Threading.Tasks;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        public async Task<int> Execute(string s, params object[] parameters)
        {
            var data = await Database
                    .ExecuteAsync(s, parameters)
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<T> ExecuteScalar<T>(string s, params object[] parameters)
        {
            var data = await Database
                    .ExecuteScalarAsync<T>(s, parameters)
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<int> RunExecute(string s)
        {
            var data = await Database
                    .ExecuteAsync(s)
                    .ConfigureAwait(false);
            return data;
        }

        public async Task<(int, int)> RunExecute(string s1, string s2)
        {
            int rc1 = 0, rc2 = 0;
            await Database.RunInTransactionAsync(async transaction =>
            {
                lock (_locker)
                {
                    rc1 = transaction.Execute(s1);
                    rc2 = transaction.Execute(s2);
                    transaction.Commit();
                }
            }).ConfigureAwait(false);

            return (rc1, rc2);
        }

        public async Task<(int, int, int)> RunExecute(string s1, string s2, string s3)
        {
            int rc1 = 0, rc2 = 0, rc3 = 0;
            await Database.RunInTransactionAsync(async transaction =>
            {
                lock (_locker)
                {
                    rc1 = transaction.Execute(s1);
                    rc2 = transaction.Execute(s2);
                    rc3 = transaction.Execute(s3);
                    transaction.Commit();
                }
            }).ConfigureAwait(false);

            return (rc1, rc2, rc3);
        }

        public async Task<(int, int, int, int)> RunExecute(string s1, string s2, string s3, string s4)
        {
            int rc1 = 0, rc2 = 0, rc3 = 0, rc4 = 0;
            await Database.RunInTransactionAsync(async transaction =>
            {
                lock (_locker)
                {
                    rc1 = transaction.Execute(s1);
                    rc2 = transaction.Execute(s2);
                    rc3 = transaction.Execute(s3);
                    rc4 = transaction.Execute(s4);
                    transaction.Commit();
                }
            }).ConfigureAwait(false);

            return (rc1, rc2, rc3, rc4);
        }

        public async Task<(int, int, int, int, int)> RunExecute(string s1, string s2, string s3, string s4, string s5)
        {
            int rc1 = 0, rc2 = 0, rc3 = 0, rc4 = 0, rc5 = 0;
            await Database.RunInTransactionAsync(async transaction =>
            {
                lock (_locker)
                {
                    rc1 = transaction.Execute(s1);
                    rc2 = transaction.Execute(s2);
                    rc3 = transaction.Execute(s3);
                    rc4 = transaction.Execute(s4);
                    rc5 = transaction.Execute(s5);
                    transaction.Commit();
                }
            }).ConfigureAwait(false);

            return (rc1, rc2, rc3, rc4, rc5);
        }
    }
}