// <copyright file="DbContextSettings.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Data
{
    /// <summary>
    ///   The Db Context settings.
    /// </summary>
    public static class DbContextSettings
    {
        /// <summary>The database name.</summary>
        public static string DatabaseName = "utgkata";

        /// <summary>The connection string.</summary>
        public static string ConnectionString = "Data Source=localhost;Initial Catalog=utgkata;Integrated Security=True";
    }
}
