using Microsoft.EntityFrameworkCore;

namespace MedikTappFunctionApp.Models
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<AppointmentModel> AppointmentData { get; set; }
        public DbSet<PatientModel> PatientData { get; set; }
        public DbSet<PromoModel> PromoData { get; set; }
        public DbSet<ServiceModel> ServiceData { get; set; }
    }
}