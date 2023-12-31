using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext: DbContext
    {
        
        public DataContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; } // table naam = property naam
    }
}