// <copyright file="ICsvImporter.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console
{
    using System.Threading.Tasks;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="TCsvRecordModel">The type of the CSV record model.</typeparam>
    public interface ICsvImporter<TCsvRecordModel>
        where TCsvRecordModel : CsvReaderModelBase, new()
    {
        /// <summary>Imports the CSV to database asynchronous.</summary>
        /// <param name="csvPath">The CSV path.</param>
        /// <param name="importApiEndpoint">The import API endpoint.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task ImportCsvToDbAsync(string csvPath, string importApiEndpoint);
    }
}
