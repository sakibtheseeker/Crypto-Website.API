using Crypto_Website.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)  :base(options) { }

        public DbSet<Portfolio> Portfolios { get; set; }

        public DbSet<Crypto> Cryptos { get; set; }

        public DbSet<Favourite> Favourites => Set<Favourite>();

        public DbSet<User> Users { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<WalletTransaction> WalletTransactions { get; set; }

        public DbSet<TransactionsHistory> TransactionsHistories { get; set; }

        public DbSet<PortfolioAsset> PortfolioAssets { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>()
            .ToTable("Wallet")
            .HasIndex(w => w.Uid)
            .IsUnique();

            modelBuilder.Entity<PortfolioAsset>(entity =>
            {
                entity.ToTable("PortfolioAssets");

                entity.HasKey(pa => pa.Paid);

                entity.HasIndex(pa => new { pa.Pid, pa.Cid })
                      .IsUnique();

                entity.HasOne(pa => pa.Portfolio)
                      .WithMany(p => p.Assets)
                      .HasForeignKey(pa => pa.Pid)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(pa => pa.Crypto)
                      .WithMany()
                      .HasForeignKey(pa => pa.Cid)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<WalletTransaction>(entity =>
            {
                entity.ToTable("WalletTransactions");

                entity.HasKey(t => t.Wtid);

    
                entity.Property(t => t.Amount)
                      .HasPrecision(18, 2);

                entity.Property(t => t.TransactionType)
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(t => t.TransactionStatus)
                      .HasMaxLength(10);

                entity.Property(t => t.PaymentMethod)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.HasOne(t => t.Wallet)
                      .WithMany()
                      .HasForeignKey(t => t.Wid)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TransactionsHistory>(entity =>
            {
                entity.ToTable("TransactionsHistory");
                entity.HasKey(t => t.Tid);

                entity.Property(t => t.Price)
                      .HasPrecision(18, 2);

                entity.Property(t => t.Quantity)
                      .HasPrecision(18, 8);

                entity.HasOne(t => t.Crypto)
                      .WithMany()
                      .HasForeignKey(t => t.Cid)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.Uid)
                      .OnDelete(DeleteBehavior.Restrict);
            });



            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Crypto)
                .WithMany()
                .HasForeignKey(f => f.Cid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.Uid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favourite>()
                .HasIndex(f => new { f.Uid, f.Cid })
                .IsUnique();

            modelBuilder.Entity<Crypto>(entity =>
            {
                entity.ToTable("Crypto");

                entity.Property(c => c.CurrentPrice)
                      .HasPrecision(18, 8);
            });
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");

                entity.HasKey(rt => rt.RtId);

                entity.Property(rt => rt.Token)
                      .IsRequired();

                entity.Property(rt => rt.IsActive)
                      .HasDefaultValue(true);

                entity.HasOne(rt => rt.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(rt => rt.Uid)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Uid);

                entity.Property(u => u.Uname)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.Uemail)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(u => u.Uemail).IsUnique();

                entity.Property(u => u.Upassword)
                      .HasMaxLength(255)
                      .IsRequired();
            });




        }

    }
}


