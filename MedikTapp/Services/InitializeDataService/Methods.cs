using MedikTapp.Models;
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
            if (_databaseService.IsNewDatabase)
            {
                await Task.WhenAll
                (
                    _databaseService.CreateTable<Bookings>(),
                    _databaseService.CreateTable<Schedules>()
                ).ConfigureAwait(false);
            }
        }
    }
}