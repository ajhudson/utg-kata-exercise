using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtgKata.Lib.CsvReader.CustomExceptions;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Lib.CsvReader
{
    public class CsvReader<TMappedModel> : ICsvReader<TMappedModel> where TMappedModel : CsvReaderModelBase, new()
    {
        private readonly string csvAbsolutePath;

        private readonly bool includesHeader;

        private readonly ICsvRowMapper<TMappedModel> csvRowMapper;

        public CsvReader(ICsvRowMapper<TMappedModel> rowMapper, string csvAbsolutePath, bool includesHeader = true)
        {
            this.csvAbsolutePath = csvAbsolutePath;
            this.includesHeader = includesHeader;
            this.csvRowMapper = rowMapper;
        }

        /// <summary>
        /// Read the CSV file and return a list of models
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TMappedModel>> ReadCsvAsync()
        {
            this.AssertFileExists();
            this.AssertHeaderExists();

            var models = await this.ParseCsvAsync();

            return models;
        }

        /// <summary>
        /// Check the file can be found and throw an exception if it cannot
        /// </summary>
        private void AssertFileExists()
        {
            if (!File.Exists(this.csvAbsolutePath))
            {
                throw new FileNotFoundException(nameof(this.csvAbsolutePath));
            }
        }

        /// <summary>
        /// Only CSV files with headers are supported at the moment
        /// </summary>
        private void AssertHeaderExists()
        {
            if (!this.includesHeader)
            {
                throw new CsvWithoutHeaderException(this.csvAbsolutePath);
            }
        }

        /// <summary>
        /// Parse the CSV and return an enumerable of the models in the required type
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

        /// <summary>
        /// Parse the row into an array of strings
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static string[] ParseRow(string row)
        {
            var csvParsePattern = new Regex("(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            string[] columnsData = csvParsePattern.Matches(row).OfType<Match>().Select(m => Regex.Replace(m.Value, @"(^\""|\""$)", string.Empty)).ToArray();

            return columnsData;
        }
    }
}
