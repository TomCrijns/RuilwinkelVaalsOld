using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RuilWinkelVaals.Models
{
    public partial class UserDataModel : DbContext
    {
        public UserDataModel()
            : base("name=UserDataModel")
        {
        }

        public virtual DbSet<AccountData> AccountData { get; set; }
        public virtual DbSet<AccountType_LT> AccountType_LT { get; set; }
        public virtual DbSet<Ledenpas_LT> Ledenpas_LT { get; set; }
        public virtual DbSet<ProfileData> ProfileData { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType_LT>()
                .HasMany(e => e.ProfileData)
                .WithOptional(e => e.AccountType_LT)
                .HasForeignKey(e => e.AccountType);

            modelBuilder.Entity<Ledenpas_LT>()
                .HasMany(e => e.ProfileData)
                .WithOptional(e => e.Ledenpas_LT)
                .HasForeignKey(e => e.Ledenpas);

            modelBuilder.Entity<ProfileData>()
                .HasMany(e => e.AccountData)
                .WithOptional(e => e.ProfileData)
                .HasForeignKey(e => e.ProfileId);
        }
    }
}
