using DigitalLibrary.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibrary.Server.Data.DatabaseContext
{
    public class DbDigitalLibraryContext : DbContext
    {
        public DbDigitalLibraryContext(DbContextOptions<DbDigitalLibraryContext> options) : base(options) { }

        public DbSet<Authors> authors { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<DocumentAuthors> documentAuthors { get; set; }
        public DbSet<DocumentCategories> documentCategorises { get; set; }
        public DbSet<Documents> documents { get; set; }
        public DbSet<DocumentSubject> documentSubjects { get; set; }
        public DbSet<Nations> nations { get; set; }
        public DbSet<Reviews> reviews { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Statistics> statistics { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<SubscriptionPlans> subscriptionPlans { get; set; }
        public DbSet<UserPermissions> userPermissions { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<UserSubcriptions> userSubcriptions { get; set; }
        public DbSet<PaymentHistory> paymentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentAuthors>()
                .HasKey(da => new { da.documentid, da.authorid });

            modelBuilder.Entity<DocumentCategories>()
                .HasKey(dc => new { dc.documentid, dc.categoryid });

            modelBuilder.Entity<DocumentSubject>()
                .HasKey(ds => new { ds.documentid, ds.subjectid });
        }

    }
}
