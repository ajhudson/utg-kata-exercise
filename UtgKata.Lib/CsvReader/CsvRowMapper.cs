// <copyright file="CsvRowMapper.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UtgKata.Lib.CsvReader.CustomAttributes;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// Class responsible for how a row from a CSV file is mapped to a model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="UtgKata.Lib.CsvReader.ICsvRowMapper{TModel}" />
    public class CsvRowMapper<TModel> : ICsvRowMapper<TModel>
        where TModel : CsvReaderModelBase, new()
    {
        /// <summary>The mapping information.</summary>
        private static readonly Dictionary<string, RowMapInfo> MappingInfo;

        /// <summary>
        /// Initializes static members of the <see cref="CsvRowMapper{TModel}"/> class.Initializes the <see cref="CsvRowMapper{TModel}" /> class.
        /// </summary>
        static CsvRowMapper()
        {
            MappingInfo = GetMappingInfo();
        }

        /// <summary>Gets the mapping information.</summary>
        /// <returns>
        ///   Mapping info for each property/column.
        /// </returns>
        public static Dictionary<string, RowMapInfo> GetMappingInfo()
        {
            var mappingProps = typeof(TModel).GetProperties()
                                    .Where(prop => Attribute.IsDefined(prop, typeof(CsvHeadingMapper)))
                                    .Select(prop => new
                                    {
                                        PropName = prop.Name,
                                        PropType = prop.PropertyType,
                                        HeadingName = prop.GetCustomAttributes(false)
                                                                .OfType<CsvHeadingMapper>()
                                                                .First()
                                                                .HeadingName,
                                    }).ToDictionary(
                                        propInfo => propInfo.HeadingName,
                                        propInfo => new RowMapInfo
                                        {
                                            PropertyName = propInfo.PropName,
                                            PropertyType = propInfo.PropType,
                                        });

            return mappingProps;
        }

        /// <summary>
        /// Maps to models.
        /// </summary>
        /// <param name="columnHeaders">The column headers.</param>
        /// <param name="rowData">The row data.</param>
        /// <returns>An enumerable of models.</returns>
        public IEnumerable<TModel> MapToModels(string[] columnHeaders, string[][] rowData)
        {
            for (int i = 0; i < rowData.Length; i++)
            {
                string[] currentRow = rowData[i];

                var model = new TModel();

                for (int j = 0; j < currentRow.Length; j++)
                {
                    string currentVal = currentRow[j];
                    string currentCol = columnHeaders[j];

                    if (!MappingInfo.ContainsKey(currentCol))
                    {
                        continue;
                    }

                    var currentMappingInfo = MappingInfo[currentCol];
                    var currentProp = model.GetType().GetProperty(currentMappingInfo.PropertyName);
                    currentProp.SetValue(model, Convert.ChangeType(currentVal, currentProp.PropertyType));
                }

                yield return model;
            }
        }
    }
}
