using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebCiro.Core.Entities.Authentication;

namespace WebCiro.Repository
{

    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<NET_001_CIRO> NET_001_CIRO { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlConnection"));
        }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityreference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {

                                entityreference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                if (entityreference.IsDelete == true)
                                {
                                    Entry(entityreference).Property(x => x.CreatedDate).IsModified = false;
                                    Entry(entityreference).Property(x => x.UpdatedDate).IsModified = false;
                                    entityreference.DeletedDate = DateTime.Now;
                                }
                                else
                                {
                                    Entry(entityreference).Property(x => x.CreatedDate).IsModified = false;
                                    Entry(entityreference).Property(x => x.UpdatedDate).IsModified = false;
                                    entityreference.UpdatedDate = DateTime.Now;
                                }

                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
