using CleaningRequests.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleaningRequests.Data;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("request_pkey");

            entity.ToTable("Request");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Cabinets).HasMaxLength(200);
            entity.Property(e => e.Comment).HasMaxLength(300);
            entity.Property(e => e.Full_name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Feedback).HasMaxLength(300);

            entity.HasOne(d => d.Status)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.Status_id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(d => d.Services).WithMany(p => p.Requests)
                           .UsingEntity<Dictionary<string, object>>(
                               "RequestServices",
                               r => r.HasOne<Service>().WithMany()
                                   .HasForeignKey("ServiceId"),
                               l => l.HasOne<Request>().WithMany()
                                   .HasForeignKey("RequestId"));
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("Service");

            entity.HasIndex(e => e.Name, "service_name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("Status");

            entity.HasIndex(e => e.Name, "status_name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
