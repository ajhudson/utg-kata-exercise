﻿// <copyright file="Program.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UtgKata.Data;
    using UtgKata.Data.Extensions;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// Main program.
    /// </summary>
    public class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            // Check the database is created
            var optsBuilder = new DbContextOptionsBuilder<UtgKataDbContext>();
            optsBuilder.UseSqlServer(DbContextSettings.ConnectionString);

            using (var ctx = new UtgKataDbContext(optsBuilder.Options))
            {
                await ctx.Database.EnsureCreatedAsync();
                ctx.Customers.Clear();
                await ctx.SaveChangesAsync();
            }

            var csvImporter = new CsvImporter<CustomerRecordModel>();

            csvImporter.CsvFilePathResolved += (obj, args) => System.Console.WriteLine($"File resolved at: {args.ResolvedFile}" + Environment.NewLine);
            csvImporter.NumberModelsFound += (obj, args) => System.Console.WriteLine($"Number of models found is: {args.NumberOfModelsFound}" + Environment.NewLine);
            csvImporter.SuccessfullySavedAtEndpoint += (obj, args) => System.Console.WriteLine($"Customer {args.Model.CustomerRef} saved via API end point with status code {(int)args.StatusCode}, {args.ResponseText}" + Environment.NewLine);
            csvImporter.FailedSaveAtEndpoint += (obj, args) => System.Console.WriteLine($"Customer {args.Model.CustomerRef} failed to save at endpoint. {args.ResponseText}" + Environment.NewLine);

            string csvPath = Path.Combine(ConsoleAppSettings.CsvFolder, ConsoleAppSettings.CsvFileName);

            try
            {
                await csvImporter.ImportCsvToDbAsync(csvPath, ConsoleAppSettings.AddCustomerApiEndpoint);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error importing CSV file: {e.Message}");
            }

            System.Console.WriteLine("Press a key to finish");
            System.Console.ReadKey();
        }
    }
}
