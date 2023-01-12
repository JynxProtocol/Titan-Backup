using Microsoft.EntityFrameworkCore;
using Titan.Models.AccessControl;
using Titan.Models.AccessControl.Roles;
using Titan.Models.DBUser;

#nullable disable

namespace Titan.Data
{
    public partial class TitanAuthContext : DbContext
    {
        public TitanAuthContext()
        {
        }

        public TitanAuthContext(DbContextOptions<TitanAuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleFeatureMapping> RoleFeatureMappings { get; set; }
        public virtual DbSet<DBUser> Users { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.FeatureArea)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FeatureName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleFeatureMapping>(entity =>
            {
                entity.ToTable("RoleFeatureMapping");
            });

            modelBuilder.Entity<DBUser>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UsrID);

                entity.Property(e => e.UsrDepartment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrDisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrEmailAddress).HasMaxLength(255);

                entity.Property(e => e.UsrFirstName).HasMaxLength(50);

                entity.Property(e => e.UsrLastLogin).HasColumnType("datetime");

                entity.Property(e => e.UsrLastName).HasMaxLength(50);

                entity.Property(e => e.UsrName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.ToTable("UserRoleMapping");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
