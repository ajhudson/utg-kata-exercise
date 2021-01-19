using System;
using System.Collections.Generic;
using System.Text;
using UtgKata.Lib.CsvReader.CustomAttributes;

namespace UtgKata.Lib.CsvReader.Models
{
    public class CustomerRecordModel : CsvReaderModelBase
    {
        public const string CustomerRefHeaderMapping = "customer_ref";

        public const string FirstNameHeaderMapping = "firstname";

        public const string LastNameHeaderMapping = "lastname";

        public const string Address1HeaderMapping = "address_1";

        public const string Address2HeaderMapping = "address_2";

        public const string TownHeaderMapping = "town";

        public const string CountyHeaderMapping = "county";

        public const string CountryHeaderMapping = "country";

        public const string PostCodeHeaderMapping = "post_code";


        [CsvHeadingMapper(HeadingName = CustomerRefHeaderMapping)]
        public string CustomerRef { get; set; }

        [CsvHeadingMapper(HeadingName = FirstNameHeaderMapping)]
        public string FirstName { get; set; }

        [CsvHeadingMapper(HeadingName = LastNameHeaderMapping)]
        public string LastName { get; set; }

        [CsvHeadingMapper(HeadingName = Address1HeaderMapping)]
        public string Address1 { get; set; }

        [CsvHeadingMapper(HeadingName = Address2HeaderMapping)]
        public string Address2 { get; set; }

        [CsvHeadingMapper(HeadingName = TownHeaderMapping)]
        public string Town { get; set; }

        [CsvHeadingMapper(HeadingName = CountyHeaderMapping)]
        public string County { get; set; }

        [CsvHeadingMapper(HeadingName = CountryHeaderMapping)]
        public string Country { get; set; }

        [CsvHeadingMapper(HeadingName = PostCodeHeaderMapping)]
        public string PostCode { get; set; }
    }
}
