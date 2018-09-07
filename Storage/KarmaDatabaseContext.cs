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
        //INSERT INTO "main"."Channels"("Id", "Name") VALUES ('C724506744E47F2B50FED0AE55857449', '#abc');
        //INSERT INTO "main"."Karmas"("Id", "Name", "Score", "ChannelId") VALUES ('A61D35921C85AB2E1352FFFD806CBE09', 'amore', 3, 'C724506744E47F2B50FED0AE55857449');
        base.OnConfiguring(builder);
    }
}