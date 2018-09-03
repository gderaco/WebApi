using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

public class KarmaDatabaseContext : DbContext
{
    public DbSet<Karma> Karmas { get; set; }
    public DbSet<Channel> Channels { get; set; }

    public KarmaDatabaseContext(DbContextOptions<KarmaDatabaseContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder
            .Entity<Channel>()
            .Property(c => c.Id)
            .HasDefaultValueSql("uuid_generate_v4()");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }
}