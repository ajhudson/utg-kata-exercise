// <copyright file="UtgKataDbContextFactory.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    /// <summary>
    /// DB context factory.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory{UtgKata.Data.UtgKataDbContext}" />
    public class UtgKataDbContextFactory : IDesignTimeDbContextFactory<UtgKataDbContext>
    {
        /// <summary>Creates a new instance of a derived context.</summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>An instance of <span class="typeparameter">TContext</span>.</returns>
        public UtgKataDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtgKataDbContext>();
            optionsBuilder.UseInMemoryDatabase(DbContextSettings.DatabaseName);

            return new UtgKataDbContext(optionsBuilder.Options);
        }
    }
}
