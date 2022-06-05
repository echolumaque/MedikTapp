using Microsoft.EntityFrameworkCore;

namespace MedikTappFunctionApp.Models
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<PatientModel> PatientData { get; set; }
    }
}