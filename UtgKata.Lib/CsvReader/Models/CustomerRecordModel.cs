// <copyright file="CustomerRecordModel.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader.Models
{
    using UtgKata.Lib.CsvReader.CustomAttributes;

    /// <summary>
    /// The customer record view model.
    /// </summary>
    /// <seealso cref="UtgKata.Lib.CsvReader.Models.CsvReaderModelBase" />
    public class CustomerRecordModel : CsvReaderModelBase
    {
        /// <summary>
        /// The customer reference header mapping.
        /// </summary>
        public const string CustomerRefHeaderMapping = "customer_ref";

        /// <summary>
        /// The first name header mapping.
        /// </summary>
        public const string FirstNameHeaderMapping = "firstname";

        /// <summary>
        /// The last name header mapping.
        /// </summary>
        public const string LastNameHeaderMapping = "lastname";

        /// <summary>
        /// The address1 header mapping.
        /// </summary>
        public const string Address1HeaderMapping = "address_1";

        /// <summary>
        /// The address2 header mapping.
        /// </summary>
        public const string Address2HeaderMapping = "address_2";

        /// <summary>
        /// The town header mapping.
        /// </summary>
        public const string TownHeaderMapping = "town";

        /// <summary>
        /// The county header mapping.
        /// </summary>
        public const string CountyHeaderMapping = "county";

        /// <summary>
        /// The country header mapping.
        /// </summary>
        public const string CountryHeaderMapping = "country";

        /// <summary>
        /// The post code header mapping.
        /// </summary>
        public const string PostCodeHeaderMapping = "post_code";

        /// <summary>
        /// Gets or sets the customer reference.
        /// </summary>
        /// <value>
        /// The customer reference.
        /// </value>
        [CsvHeadingMapper(HeadingName = CustomerRefHeaderMapping)]
        public string CustomerRef { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [CsvHeadingMapper(HeadingName = FirstNameHeaderMapping)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [CsvHeadingMapper(HeadingName = LastNameHeaderMapping)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        [CsvHeadingMapper(HeadingName = Address1HeaderMapping)]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        [CsvHeadingMapper(HeadingName = Address2HeaderMapping)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        [CsvHeadingMapper(HeadingName = TownHeaderMapping)]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>
        /// The county.
        /// </value>
        [CsvHeadingMapper(HeadingName = CountyHeaderMapping)]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [CsvHeadingMapper(HeadingName = CountryHeaderMapping)]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [CsvHeadingMapper(HeadingName = PostCodeHeaderMapping)]
        public string PostCode { get; set; }
    }
}
