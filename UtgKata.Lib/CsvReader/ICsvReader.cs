// <copyright file="ICsvReader.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// CSV reader interface.
    /// </summary>
    /// <typeparam name="TMappedModel">The type of the mapped model.</typeparam>
    public interface ICsvReader<TMappedModel>
        where TMappedModel : CsvReaderModelBase, new()
    {
        /// <summary>
        /// Reads the CSV asynchronous.
        /// </summary>
        /// <returns>Enumerable of mapped models.</returns>
        Task<IEnumerable<TMappedModel>> ReadCsvAsync();
    }
}
