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

        public virtual DbSet<AirlineDetails> AirlineDetails { get; set; }
        public virtual DbSet<DiscountDetails> DiscountDetails { get; set; }
        public virtual DbSet<FlightDetails> FlightDetails { get; set; }
        public virtual DbSet<ScheduleDetails> ScheduleDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-OG93GD7N\\SQLEXPRESS;Database=Inventory;User ID=admin;Password=admin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirlineDetails>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Blocked).HasDefaultValueSql("((0))");

                entity.Property(e => e.ContactAddress).IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InstrumentUsed).IsUnicode(false);

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.HasOne(d => d.FlightNumberNavigation)
                    .WithOne(p => p.AirlineDetails)
                    .HasForeignKey<AirlineDetails>(d => d.FlightNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AirlineDetails_FlightDetails");
            });

            modelBuilder.Entity<DiscountDetails>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CouponCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FlightDetails>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ScheduleDetails>(entity =>
            {
                entity.HasKey(e => e.ConfirmationNumber);

                entity.Property(e => e.ConfirmationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Meal).IsUnicode(false);

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
