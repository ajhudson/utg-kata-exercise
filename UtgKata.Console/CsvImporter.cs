// <copyright file="CsvImporter.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Polly;
    using UtgKata.Console.Events;
    using UtgKata.Console.HttpClientRetryPolicies;
    using UtgKata.Lib.CsvReader;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// The CSV importer.
    /// </summary>
    /// <typeparam name="TCsvRecordModel">The type of the CSV record model.</typeparam>
    /// <seealso cref="UtgKata.Console.ICsvImporter{TCsvRecordModel}" />
    public class CsvImporter<TCsvRecordModel> : ICsvImporter<TCsvRecordModel>
        where TCsvRecordModel : CsvReaderModelBase, new()
    {
        /// <summary>The retry policy.</summary>
        private readonly IAsyncPolicy<HttpResponseMessage> retryPolicy;

        /// <summary>Initializes a new instance of the <see cref="CsvImporter{TCsvRecordModel}" /> class.</summary>
        public CsvImporter()
        {
            this.retryPolicy = RetryPolicy.GetRetryPolicy();
        }

        /// <summary>Occurs when [CSV file path resolved].</summary>
        public event EventHandler<CsvFileResolvedEventArgs> CsvFilePathResolved;

        /// <summary>Occurs when [number models found].</summary>
        public event EventHandler<NumberModelsFoundEventArgs> NumberModelsFound;

        /// <summary>Occurs when [successfully saved at endpoint].</summary>
        public event EventHandler<SavedAtEndpointEventArgs<TCsvRecordModel>> SuccessfullySavedAtEndpoint;

        /// <summary>Occurs when [failed save at endpoint].</summary>
        public event EventHandler<FailedSaveAtEndpointEventArgs<TCsvRecordModel>> FailedSaveAtEndpoint;

        /// <summary>Imports the CSV to database asynchronous.</summary>
        /// <param name="csvPath">The CSV path.</param>
        /// <param name="importApiEndpoint">The import API endpoint.</param>
        /// <exception cref="FileNotFoundException">The file which isn't found.</exception>
        /// <returns>Asynchronous task when completed.</returns>
        public async Task ImportCsvToDbAsync(string csvPath, string importApiEndpoint)
        {
            // Get the path to the CSV file
            string csvAbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), csvPath);

            if (File.Exists(csvAbsolutePath))
            {
                this.OnCsvFilePathResolved(new CsvFileResolvedEventArgs(csvAbsolutePath));
            }
            else
            {
                throw new FileNotFoundException(csvAbsolutePath);
            }

            // Initialise the CSV mapper and the CSV reader
            var csvMapper = new CsvRowMapper<TCsvRecordModel>();
            var csvParser = new CsvReader<TCsvRecordModel>(csvMapper, csvAbsolutePath);

            // Read the CSV file and return the mapped models
            var csvModels = await csvParser.ReadCsvAsync();
            var models = csvModels.ToList();

            // Let the user know how many models were found
            this.OnNumberModelsFound(new NumberModelsFoundEventArgs(models.Count()));

            await Task.Delay(5000); // wait for API project to load

            HttpClient httpClient = new HttpClient();

            foreach (var model in models)
            {
                await this.retryPolicy.ExecuteAsync(async () => await this.PostModelAsync(model));
            }
        }

        /// <summary>Raises the <see cref="E:CsvFilePathResolved" /> event.</summary>
        /// <param name="e">The <see cref="CsvFileResolvedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCsvFilePathResolved(CsvFileResolvedEventArgs e)
        {
            this.CsvFilePathResolved?.Invoke(this, e);
        }

        /// <summary>Raises the <see cref="E:NumberModelsFound" /> event.</summary>
        /// <param name="e">The <see cref="NumberModelsFoundEventArgs" /> instance containing the event data.</param>
        protected virtual void OnNumberModelsFound(NumberModelsFoundEventArgs e)
        {
            this.NumberModelsFound?.Invoke(this, e);
        }

        /// <summary>Raises the <see cref="E:SuccessfullySavedOnEndpoint" /> event.</summary>
        /// <param name="e">The <see cref="SavedAtEndpointEventArgs{TCsvRecordModel}" /> instance containing the event data.</param>
        protected virtual void OnSuccessfullySavedOnEndpoint(SavedAtEndpointEventArgs<TCsvRecordModel> e)
        {
            this.SuccessfullySavedAtEndpoint?.Invoke(this, e);
        }

        /// <summary>Raises the <see cref="E:FailedSaveOnEndpoint" /> event.</summary>
        /// <param name="e">The <see cref="FailedSaveAtEndpointEventArgs{TCsvRecordModel}" /> instance containing the event data.</param>
        protected virtual void OnFailedSaveOnEndpoint(FailedSaveAtEndpointEventArgs<TCsvRecordModel> e)
        {
            this.FailedSaveAtEndpoint?.Invoke(this, e);
        }

        /// <summary>
        /// Posts the model asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The http response.</returns>
        private async Task<HttpResponseMessage> PostModelAsync(TCsvRecordModel model)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            string payload = JsonSerializer.Serialize(model);
            var httpContent = new StringContent(payload, Encoding.UTF8, "text/json");
            var httpClient = new HttpClient(httpClientHandler);
            var httpResponse = await httpClient.PostAsync(ConsoleAppSettings.AddCustomerApiEndpoint, httpContent);
            string responseText = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                this.OnSuccessfullySavedOnEndpoint(new SavedAtEndpointEventArgs<TCsvRecordModel>(model, httpResponse.StatusCode, responseText));
            }
            else
            {
                this.OnFailedSaveOnEndpoint(new FailedSaveAtEndpointEventArgs<TCsvRecordModel>(model, httpResponse.StatusCode, responseText));
            }

            return httpResponse;
        }
    }
}
