using Shouldly;
using System.Linq;
using System.Collections.Generic;
using UtgKata.Lib.CsvReader;
using UtgKata.Lib.CsvReader.CustomAttributes;
using UtgKata.Lib.CsvReader.Models;
using Xunit;

namespace UtgKata.Lib.Tests.CsvMapperTests
{
    public class CsvMapperTests
    {
        [Fact]
        public void ShouldMapRowsToModelCorrectly()
        {
            // Arrange 
            string[] cols = new[] { "record_id", "email_address", "full_name" };

            var rows = new []
            {
                new string[] { "1", "js@gmail.com", "Jim Smith" },
                new string[] { "2", "aj@msn.com", "Alice Jones" }
            };

            var rowMapper = new CsvRowMapper<TestModel>();

            // Act
            var mappedModels = rowMapper.MapToModels(cols, rows).ToList();

            // Assert
            mappedModels.ShouldBeOfType<List<TestModel>>();
            mappedModels[0].Id.ShouldBe(1);
            mappedModels[0].Name.ShouldBe("Jim Smith");
            mappedModels[0].EmailAddress.ShouldBe("js@gmail.com");
            mappedModels[1].Id.ShouldBe(2);
            mappedModels[1].Name.ShouldBe("Alice Jones");
            mappedModels[1].EmailAddress.ShouldBe("aj@msn.com");
        }
    }

    public class TestModel : CsvReaderModelBase
    {
        [CsvHeadingMapper(HeadingName = "record_id")]
        public int Id { get; set; }

        [CsvHeadingMapper(HeadingName = "email_address")]
        public string EmailAddress { get; set; }

        [CsvHeadingMapper(HeadingName = "full_name")]
        public string Name { get; set; }
    }
}
