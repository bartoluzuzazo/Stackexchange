using Microsoft.EntityFrameworkCore;
using Stackexchange.Domain.Tags;

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
