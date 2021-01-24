// <copyright file="CsvReader.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using UtgKata.Lib.CsvReader.CustomExceptions;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// The CSV reader.
    /// </summary>
    /// <typeparam name="TMappedModel">The type of the mapped model.</typeparam>
    /// <seealso cref="UtgKata.Lib.CsvReader.ICsvReader{TMappedModel}" />
    public class CsvReader<TMappedModel> : ICsvReader<TMappedModel>
        where TMappedModel : CsvReaderModelBase, new()
    {
        private readonly string csvAbsolutePath;

        private readonly bool includesHeader;

        private readonly ICsvRowMapper<TMappedModel> csvRowMapper;

        /// <summary>Initializes a new instance of the <see cref="CsvReader{TMappedModel}" /> class.</summary>
        /// <param name="rowMapper">The row mapper.</param>
        /// <param name="csvAbsolutePath">The CSV absolute path.</param>
        /// <param name="includesHeader">if set to <c>true</c> [includes header].</param>
        public CsvReader(ICsvRowMapper<TMappedModel> rowMapper, string csvAbsolutePath, bool includesHeader = true)
        {
            this.csvAbsolutePath = csvAbsolutePath;
            this.includesHeader = includesHeader;
            this.csvRowMapper = rowMapper;
        }

        /// <summary>Reads the CSV asynchronous.</summary>
        /// <returns>
        ///   A list of mapped models based on the configuration of the mapped model.
        /// </returns>
        public async Task<IEnumerable<TMappedModel>> ReadCsvAsync()
        {
            this.AssertFileExists();
            this.AssertHeaderExists();

            var models = await this.ParseCsvAsync();

            return models;
        }

        /// <summary>
        /// Parses the row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>The parsed row.</returns>
        private static string[] ParseRow(string row)
        {
            var csvParsePattern = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            string[] columnsData = csvParsePattern.Matches(row).OfType<Match>().Select(m => Regex.Replace(m.Value, @"(^\""|\""$)", string.Empty)).ToArray();

            return columnsData;
        }

        /// <summary>
        /// Asserts the file exists.
        /// </summary>
        /// <exception cref="FileNotFoundException">csvAbsolutePath.</exception>
        private void AssertFileExists()
        {
            if (!File.Exists(this.csvAbsolutePath))
            {
                throw new FileNotFoundException(nameof(this.csvAbsolutePath));
            }
        }

        /// <summary>
        /// Only CSV files with headers are supported at the moment.
        /// </summary>
        private void AssertHeaderExists()
        {
            if (!this.includesHeader)
            {
                throw new CsvWithoutHeaderException(this.csvAbsolutePath);
            }
        }

        /// <summary>
        /// Parse the CSV and return an enumerable of the models in the required type.
        /// </summary>
        private async Task<IEnumerable<TMappedModel>> ParseCsvAsync()
        {
            const char SplitChar = ',';

            string[] csvData = await File.ReadAllLinesAsync(this.csvAbsolutePath);

            // first get the header names
            string[] headers = csvData[0].Split(SplitChar);

            // now get the rest of the data starting at index 1
            var parsedRows = csvData.Select((row, i) => new { CurrentRow = row, Index = i })
                                .Where(rowInfo => rowInfo.Index > 0)
                                .Select(rowInfo => ParseRow(rowInfo.CurrentRow))
                                .ToArray();

            var mappedModels = this.csvRowMapper.MapToModels(headers, parsedRows);

            return mappedModels;
        }
    }
}
