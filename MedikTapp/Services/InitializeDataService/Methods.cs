using MedikTapp.Tables;
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
            await Task.WhenAll
            (
                _databaseService.CreateTable<AppConfig>(),
                _databaseService.CreateTable<MedikTapp.Models.Services>()
            ).ConfigureAwait(false);
        }
    }
}