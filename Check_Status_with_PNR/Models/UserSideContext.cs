using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Check_Status_with_PNR.Models
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

        public virtual DbSet<BookedTicketDetails> BookedTicketDetails { get; set; }
        public virtual DbSet<FlightsSchedules> FlightsSchedules { get; set; }
        public virtual DbSet<LoginsDetails> LoginsDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:inventory007.database.windows.net,1433;Initial Catalog=UserSide;Persist Security Info=False;User ID=gulu007;Password=Suraj@12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookedTicketDetails>(entity =>
            {
                entity.HasKey(e => e.UniqueBookingId);

                entity.Property(e => e.UniqueBookingId).IsUnicode(false);

                entity.Property(e => e.ArrivalTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalTo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CanCancel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CancelStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureFrom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fare)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightIid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasangerAge)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasangerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pnrnumber).IsUnicode(false);

                entity.Property(e => e.SeatNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FlightsSchedules>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BaseFare)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessRows)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessSeats)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClassSelected)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmationNumber).IsUnicode(false);

                entity.Property(e => e.ContactAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntryId)
                    .HasMaxLength(900)
                    .IsUnicode(false);

                entity.Property(e => e.FlightName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlightNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.From)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InstrumentUsed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Meal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NonBusinessRows)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NonBusinessSeats)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Schedule)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueKey).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoginsDetails>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Token).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
