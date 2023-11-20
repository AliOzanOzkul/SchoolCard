using Microsoft.EntityFrameworkCore;
using StudentIdCard.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Context
{
    public class StudentIdCardContext : DbContext
    {
        public StudentIdCardContext(DbContextOptions options) : base(options)
        {
        }

        public StudentIdCardContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = 104.247.167.130\\MSSQLSERVER2022; Initial Catalog = okulkart_DBSet; User ID = okulkart_Admin; Password = cc2#y24N0;");
            }
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BaseCard> BaseCardsTable { get; set; }
        public DbSet<AktiveCard> AktiveCard { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Card sınıfının BaseCard navigasyonu
            modelBuilder.Entity<Card>()
                .HasOne(c => c.BaseCard)
                .WithOne(bc => bc.Card)
                .HasForeignKey<BaseCard>(bc => bc.CardId);

            // BaseCard sınıfının Card navigasyonu
            modelBuilder.Entity<BaseCard>()
                .HasOne(bc => bc.Card)
                .WithOne(c => c.BaseCard)
                .HasForeignKey<Card>(c => c.BaseCardId);
        }

    }
}
