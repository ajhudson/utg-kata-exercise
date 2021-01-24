// <copyright file="UtgKataDbContext.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data
{
    using Microsoft.EntityFrameworkCore;
    using UtgKata.Data.Models;

    /// <summary>
    /// Entity framework DB context.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class UtgKataDbContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="UtgKataDbContext" /> class.</summary>
        /// <param name="options">The options.</param>
        public UtgKataDbContext(DbContextOptions<UtgKataDbContext> options)
            : base(options)
        {
        }

        /// <summary>Gets or sets the customers.</summary>
        /// <value>The customers.</value>
        public DbSet<Customer> Customers { get; set; }
    }
}
