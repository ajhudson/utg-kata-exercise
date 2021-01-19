using Shouldly;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UtgKata.Lib.CsvReader;
using Xunit;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Lib.Tests.CsvReaderTests
{
    public class CsvReaderIntegrationTests : CsvReaderTestsBase
    {
        [Fact]
        public void ShouldThrowFileNotFoundExceptionIfFileDoesNotExist()
        {
            // Arrange 
            string csvFile = Path.Combine(Directory.GetCurrentDirectory(), CsvFolder, "imaginary-file.csv");
            var csvMapper = new CsvRowMapper<CustomerRecordModel>();
            var csvReader = new CsvReader<CustomerRecordModel>(csvMapper, csvFile);

            // Act and Assert
            Should.ThrowAsync<FileNotFoundException>(() => csvReader.ReadCsvAsync());

        }

        [Fact]
        public async Task ShouldReadCsvFileCorrectly()
        {
            // Arrange
            var csvMapper = new CsvRowMapper<CustomerRecordModel>();
            var csvReader = new CsvReader<CustomerRecordModel>(csvMapper, GetCsvFilePath());

            // Act
            var result = await csvReader.ReadCsvAsync();
            var models = result.ToList();
                
            // Assert
            models.ShouldBeOfType<List<CustomerRecordModel>>();
            models.Count().ShouldBe(3);
            models[0].CustomerRef.ShouldBe("A1");
            models[1].CustomerRef.ShouldBe("B2");
            models[2].CustomerRef.ShouldBe("C3");
        }

        private static string GetCsvFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), CsvFolder, CsvFileName);
        }
    }
}
