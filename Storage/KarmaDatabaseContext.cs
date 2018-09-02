using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

public class KarmaDatabaseContext : DbContext
{
    private string _connectionString;

    public KarmaDatabaseContext(string connectionString) {  
        _connectionString = connectionString;  
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //TOOD: Access to connection string
        builder.UseNpgsql(_connectionString);
        base.OnConfiguring(builder);
    }

    public DbSet<Karma> Karmas { get; set; }
}