using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MameToppleApi.Models
{
    public partial class ToppleDBContext : DbContext
    {
        public ToppleDBContext()
        {
        }

        public ToppleDBContext(DbContextOptions<ToppleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Doll> Dolls { get; set; }
        public virtual DbSet<Phrase> Pharses { get; set; }
        public virtual DbSet<Sticker> Stickers { get; set; }
        public virtual DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Account);

                entity.ToTable("User");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(50)
                    .HasColumnName("avatar");

                entity.Property(e => e.Lose).HasColumnName("lose");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nickName");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("password");

                entity.Property(e => e.Win).HasColumnName("win");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
