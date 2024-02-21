using Microsoft.EntityFrameworkCore;

namespace TestApp
{
    public class TestAppDbContext: DbContext
    {
        public DbSet<IpAddressGeoInfo> IpAddressGeoInfo { get; set; }
        public DbSet<IpBogon> IpBogons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                connectionString: Properties.Resources.DbConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IpAddressGeoInfo>(e => e.ToTable("ip_address"));
            modelBuilder.Entity<IpAddressGeoInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Ip).HasColumnName("ip");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.Region).HasColumnName("region");
                entity.Property(e => e.Country).HasColumnName("country");
                entity.Property(e => e.Loc).HasColumnName("loc");
                entity.Property(e => e.Org).HasColumnName("org");
                entity.Property(e => e.Postal).HasColumnName("postal");
                entity.Property(e => e.Timezone).HasColumnName("timezone");
                entity.Property(e => e.Readme).HasColumnName("readme");
            });

            modelBuilder.Entity<IpBogon>(e => e.ToTable("ip_bogon"));
            modelBuilder.Entity<IpBogon>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd(); ;
                entity.Property(e => e.Ip).HasColumnName("ip");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
