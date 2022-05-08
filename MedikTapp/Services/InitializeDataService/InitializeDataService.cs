using MedikTapp.Services.DatabaseService;

namespace XF.Services.InitializeDataService
{
    public partial class InitializeDataService
    {
        private readonly DatabaseService _databaseService;

        public InitializeDataService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
    }
}