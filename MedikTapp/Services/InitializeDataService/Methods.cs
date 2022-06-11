using System.Threading.Tasks;

namespace XF.Services.InitializeDataService
{
    public partial class InitializeDataService
    {
        public async Task Init()
        {
            await CreateDB2Tables();
        }

        private async Task CreateDB2Tables()
        {
            if (_databaseService.IsNewDatabase)
            {
                await Task.WhenAll
                (
                    _databaseService.CreateTable<MedikTapp.Models.Services>()
                //,
                // _databaseService.CreateTable<Schedules>()
                ).ConfigureAwait(false);
            }
        }
    }
}