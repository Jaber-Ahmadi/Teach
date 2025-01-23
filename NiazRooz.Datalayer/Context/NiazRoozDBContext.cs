using System;
using NiazRooz.DomainClasses;
using NiazRooz.DomainClasses.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NiazRooz.Datalayer
{
    public partial class NiazRoozDBContext : DbContext
    {
        public NiazRoozDBContext()
        {

        }

        public NiazRoozDBContext(DbContextOptions<NiazRoozDBContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// table for project
        /// </summary>
        /// 
        #region NiazRooz 


        #endregion 

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NiazRoozDB;Persist Security Info=True;Encrypt=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Filter for user table

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDelete);
            base.OnModelCreating(modelBuilder);

            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
