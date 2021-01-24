// <copyright file="ICsvRowMapper.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader
{
    using System.Collections.Generic;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// Interface for CSV row mapper.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface ICsvRowMapper<TModel>
        where TModel : CsvReaderModelBase, new()
    {
        /// <summary>
        /// Maps to models.
        /// </summary>
        /// <param name="columnHeaders">The column headers.</param>
        /// <param name="rowData">The row data.</param>
        /// <returns>Enumerable of models.</returns>
        IEnumerable<TModel> MapToModels(string[] columnHeaders, string[][] rowData);
    }
}
