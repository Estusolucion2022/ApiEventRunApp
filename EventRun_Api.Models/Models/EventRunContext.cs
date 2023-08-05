using EventRun_Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.Serialization;

namespace EventRun_Api.Models;

public partial class EventRunContext : DbContext
{
    private readonly string DefaultConnection = string.Empty;
    public EventRunContext(string url)
    {
        DefaultConnection = url;
    }

    public EventRunContext(DbContextOptions<EventRunContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<InscriptionData> InscriptionData { get; set; }

    public virtual DbSet<Runner> Runners { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<RunnerResponse> RunnerResponse { get; set; }

    public virtual DbSet<InscriptionDataResponse> InscriptionDataResponse { get; set; }

    public virtual DbSet<ReportInscriptionData> ReportInscriptionData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(DefaultConnection);
    }


protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Observations)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodeCountry)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.ToTable("Races");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Year).HasColumnType("numeric(4, 0)");
        });

        modelBuilder.Entity<InscriptionData>(entity =>
        {
            entity.HasKey(e => new { e.IdRace, e.IdRunner });

            entity.Property(e => e.AirlineCityOrigin)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Club)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DepartureDate).HasColumnType("date");
            entity.Property(e => e.DetailsPayment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Observations)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProofPayment).HasMaxLength(20);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("date");
            entity.Property(e => e.TshirtSize)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("TShirtSize");
        });

        modelBuilder.Entity<Runner>(entity =>
        {
            entity.HasIndex(e => new { e.CodeDocumentType, e.DocumentNumber }, "Unique_Runners").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.BloodType)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CodeCity)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodeCountryNationality)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CodeDocumentType)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("date");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmergencyContactPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => new { e.UserPlataform, e.Password }, "Unique_Users").IsUnique();

            entity.Property(e => e.CreationDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPlataform)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}