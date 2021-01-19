using System;
using System.Collections.Generic;
using System.Text;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Lib.CsvReader
{
    public interface ICsvRowMapper<TModel> where TModel : CsvReaderModelBase, new()
    {
        IEnumerable<TModel> MapToModels(string[] columnHeaders, string[][] rowData);
    }
}
