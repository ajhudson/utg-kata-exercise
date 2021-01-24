// <copyright file="Program.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Entry point for the API program.
    /// </summary>
    public class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>Creates the host builder.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
