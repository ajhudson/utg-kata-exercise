// <copyright file="ConsoleAppSettings.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console
{
    /// <summary>
    /// The console app settings.
    /// </summary>
    public static class ConsoleAppSettings
    {
        /// <summary>The CSV folder.</summary>
        public const string CsvFolder = "CsvFiles";

        /// <summary>The CSV file name.</summary>
        public const string CsvFileName = "utg-sample.csv";

        /// <summary>The add customer API endpoint.</summary>
        public const string AddCustomerApiEndpoint = "http://localhost:55563/api/customer";

        /// <summary>The add customer HTTP client.</summary>
        public const string AddCustomerHttpClient = "AddCustomerHttpClient";
    }
}
