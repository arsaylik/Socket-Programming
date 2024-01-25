using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.SqlServer;


public class EntitieDbContext : DbContext
{
    public DbSet<Entitie> Entites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ArsDb;Trusted_Connection=True;");
    }

}


