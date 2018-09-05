using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
        modelBuilder
            .Entity<Channel>()
            .Property(c => c.Id)
            .HasDefaultValueSql("hex(randomblob(16))");

        modelBuilder
            .Entity<Karma>()
            .Property(c => c.Id)
            .HasDefaultValueSql("hex(randomblob(16))");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //insert into Channels("Name") values ("#abc");
        //insert into Channels("Name") values ("#cba")
        base.OnConfiguring(builder);
    }
}