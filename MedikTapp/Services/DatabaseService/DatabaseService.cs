using SQLite;
using System;
using System.IO;
using Xamarin.Essentials;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        private static readonly string BasePath = FileSystem.AppDataDirectory;
        private readonly object _locker = new();

        public DatabaseService() //ctor for DB2
        {
            IsNewDatabase = !File.Exists(Path.Combine(BasePath, "Bookings.db3"));
            Database = new Lazy<SQLiteAsyncConnection>(()
                => new SQLiteAsyncConnection(Path.Combine(BasePath, "Bookings.db3"), Flags)).Value;
            Database.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
        }

        private SQLiteOpenFlags Flags => SQLiteOpenFlags.ReadWrite
            | SQLiteOpenFlags.SharedCache
            | SQLiteOpenFlags.Create
            | SQLiteOpenFlags.FullMutex;
    }
}