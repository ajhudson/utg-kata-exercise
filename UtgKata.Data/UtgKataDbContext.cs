using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UtgKata.Data.Models;

namespace UtgKata.Data
{
    public class UtgKataDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public UtgKataDbContext(DbContextOptions<UtgKataDbContext> options) : base(options)
        {
        }
    }
}
