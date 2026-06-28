using Crypto_Website.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions{ get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioAsset> PortfolioAssets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Favorites>(e =>
            {
                e.HasOne(x => x.User)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Favorites>(e =>
            {
                e.HasOne(x => x.Crypto)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.CryptoId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Transaction>(e =>
            {
                e.HasOne(x => x.Crypto)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CryptoId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Transaction>(e =>
            {
                e.HasOne(x => x.User)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<Wallet>(e =>
            {
                e.HasOne(x => x.User)
                .WithOne(x => x.Wallet)
                .HasForeignKey<Wallet>(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            });
            modelBuilder.Entity<WalletTransaction>(e =>
            {
                e.HasOne(x => x.Wallet)
                .WithMany(x => x.WalletTransactions)
                .HasForeignKey(x => x.WalletId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Portfolio>(e =>
            {
                e.HasOne(x => x.User)
                .WithOne(x => x.Portfolio)
                .HasForeignKey<Portfolio>(p => p.Userid)
                .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<PortfolioAsset>(e =>
            {
                e.HasOne(x => x.Portfolio)
                .WithMany(x => x.PortfolioAssets)
                .HasForeignKey(x => x.PortfolioId)
                .OnDelete(DeleteBehavior.Restrict);

            });
            modelBuilder.Entity<PortfolioAsset>(e =>
            {
                e.HasOne(x => x.Crypto)
                .WithMany(x => x.PortfolioAssets)
                .HasForeignKey(x => x.CryptoId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
