using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using Empresa_X_API.Repository.Models;

namespace Empresa_X_API.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) 
        { 
        }
            public DbSet<Clients> clients { get; set; }
            public DbSet<Directions> directions { get; set; }
        }
    }


