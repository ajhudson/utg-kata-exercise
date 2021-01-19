using System.Collections.Generic;
using System.Threading.Tasks;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Lib.CsvReader
{
    public interface ICsvReader<TMappedModel> where TMappedModel : CsvReaderModelBase, new()
    {
        Task<IEnumerable<TMappedModel>> ReadCsvAsync();
    }
}
