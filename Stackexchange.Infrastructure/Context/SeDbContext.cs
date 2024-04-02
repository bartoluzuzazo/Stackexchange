using Microsoft.EntityFrameworkCore;
using Stackexchange.Domain.Tags;
using Stackexchange.Domain.Users;

namespace Stackexchange.Infrastructure.Context;

public partial class SeDbContext : DbContext
{
    public SeDbContext()
    {
    }

    public SeDbContext(DbContextOptions<SeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SE_DB_CONNSTR"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tag_pk");

            entity.ToTable("Tag");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Percentage).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
