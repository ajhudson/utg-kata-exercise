// <copyright file="EntityExtensions.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data.Extensions
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Entity yframework extensions.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>Clears the specified database set.</summary>
        /// <typeparam name="T">The db set type.</typeparam>
        /// <param name="dbSet">The database set.</param>
        public static void Clear<T>(this DbSet<T> dbSet)
            where T : class
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
