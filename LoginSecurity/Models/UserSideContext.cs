using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LoginSecurity.Models
{
    public partial class UserSideContext : DbContext
    {
        public UserSideContext()
        {
        }

        public UserSideContext(DbContextOptions<UserSideContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FlightSchedules> FlightSchedules { get; set; }
        public virtual DbSet<LoginDetails> LoginDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-OG93GD7N\\SQLEXPRESS;Database=UserSide;User ID=admin;Password=admin;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightSchedules>(entity =>
            {
                entity.HasKey(e => e.EntryId);

                entity.Property(e => e.EntryId).IsUnicode(false);

                entity.Property(e => e.ConfirmationNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndDateTime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Schedule)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueKey).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoginDetails>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
