using SQLite;

namespace MedikTapp.Services.DatabaseService
{
    public partial class DatabaseService
    {
        public SQLiteAsyncConnection Database { get; private set; }
        public bool IsNewDatabase { get; set; }
    }
}