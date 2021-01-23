using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtgKata.Data
{
    public class UtgKataDbContextFactory : IDesignTimeDbContextFactory<UtgKataDbContext>
    {
        public UtgKataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtgKataDbContext>();
            optionsBuilder.UseInMemoryDatabase(DbContextSettings.DatabaseName);

            return new UtgKataDbContext(optionsBuilder.Options);
        }
    }
}
