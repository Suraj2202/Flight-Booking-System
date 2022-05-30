using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Airline.Models
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
        {
        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AirlinesDetails> AirlinesDetails { get; set; }
        public virtual DbSet<DiscountsDetails> DiscountsDetails { get; set; }
        public virtual DbSet<FlightsDetails> FlightsDetails { get; set; }
        public virtual DbSet<SchedulesDetails> SchedulesDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:inventory007.database.windows.net,1433;Initial Catalog=Inventory;Persist Security Info=False;User ID=gulu007;Password=Suraj@12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirlinesDetails>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BaseFare)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Blocked)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessRows)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessSeats)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InstrumentUsed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NonBusinessRows)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NonBusinessSeats)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DiscountsDetails>(entity =>
            {
                entity.HasKey(e => e.CouponCode);

                entity.Property(e => e.CouponCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MinimumAmount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FlightsDetails>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DetailsUpdated)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SchedulesDetails>(entity =>
            {
                entity.HasKey(e => e.ConfirmationNumber);

                entity.Property(e => e.ConfirmationNumber).IsUnicode(false);

                entity.Property(e => e.EndDateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Schedule).IsUnicode(false);

                entity.Property(e => e.StartDateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
