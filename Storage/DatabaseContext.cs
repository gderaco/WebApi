using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class DatabaseContext : DbContext
{
    public DbSet<Karma> Karmas { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=blogging.db");
    }
}