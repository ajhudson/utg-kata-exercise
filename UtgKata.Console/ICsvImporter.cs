using System.Threading.Tasks;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Console
{
    public interface ICsvImporter<TCsvRecordModel>  where TCsvRecordModel : CsvReaderModelBase, new()
    {
        Task ImportCsvToDbAsync(string csvPath, string importApiEndpoint);
    }
}
