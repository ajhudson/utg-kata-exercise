using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UtgKata.Lib.CsvReader.Models;
using UtgKata.Lib.CsvReader.CustomAttributes;

namespace UtgKata.Lib.CsvReader
{
    public class CsvRowMapper<TModel> : ICsvRowMapper<TModel> where TModel : CsvReaderModelBase, new()
    {
        private static readonly Dictionary<string, RowMapInfo> mappingInfo;

        /// <summary>
        /// Work out the mapping in a static constructor so that it is only done once
        /// </summary>
        static CsvRowMapper()
        {
            mappingInfo = GetMappingInfo();
        }

        /// <summary>
        /// Use reflection to work out how properties and columns should be mapped and store in dictionary
        /// </summary>
        /// <returns></returns>
        static Dictionary<string, RowMapInfo> GetMappingInfo()
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
                                                                .HeadingName
                                    }).ToDictionary(propInfo => propInfo.HeadingName,
                                                        propInfo => new RowMapInfo
                                                        {
                                                            PropertyName = propInfo.PropName,
                                                            PropertyType = propInfo.PropType
                                                        });

            return mappingProps;
        }

        /// <summary>
        /// Use reflection to dynamically map properties of the model using column headers as source
        /// </summary>
        /// <param name="columnHeaders"></param>
        /// <param name="rowData"></param>
        /// <returns></returns>
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

                    if (!mappingInfo.ContainsKey(currentCol))
                    {
                        continue;
                    }

                    var currentMappingInfo = mappingInfo[currentCol];
                    var currentProp = model.GetType().GetProperty(currentMappingInfo.PropertyName);
                    currentProp.SetValue(model, Convert.ChangeType(currentVal, currentProp.PropertyType));
                }

                yield return model;
            }
        }
    }
}
