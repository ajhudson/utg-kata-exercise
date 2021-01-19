using Polly;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UtgKata.Console.Events;
using UtgKata.Console.HttpClientRetryPolicies;
using UtgKata.Lib.CsvReader;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Console
{
    public class CsvImporter<TCsvRecordModel> : ICsvImporter<TCsvRecordModel> where TCsvRecordModel : CsvReaderModelBase, new()
    {
        public event EventHandler<CsvFileResolvedEventArgs> CsvFilePathResolved;

        public event EventHandler<NumberModelsFoundEventArgs> NumberModelsFound;

        public event EventHandler<SavedAtEndpointEventArgs<TCsvRecordModel>> SuccessfullySavedAtEndpoint;

        public event EventHandler<FailedSaveAtEndpointEventArgs<TCsvRecordModel>> FailedSaveAtEndpoint;

        private readonly IAsyncPolicy<HttpResponseMessage> retryPolicy;

        public CsvImporter()
        {
            this.retryPolicy = RetryPolicy.GetRetryPolicy();
        }

        public async Task ImportCsvToDbAsync(string csvPath, string importApiEndpoint)
        {
            // Get the path to the CSV file
            string csvAbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), csvPath);

            if (File.Exists(csvAbsolutePath))
            {
                OnCsvFilePathResolved(new CsvFileResolvedEventArgs(csvAbsolutePath));
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
            OnNumberModelsFound(new NumberModelsFoundEventArgs(models.Count()));

            await Task.Delay(5000); // wait for API project to load

            HttpClient httpClient = new HttpClient();

            foreach (var model in models)
            {
                await this.retryPolicy.ExecuteAsync(async () => await this.PostModelAsync(model));
            }
        }

        protected virtual void OnCsvFilePathResolved(CsvFileResolvedEventArgs e)
        {
            CsvFilePathResolved?.Invoke(this, e);
        }

        protected virtual void OnNumberModelsFound(NumberModelsFoundEventArgs e)
        {
            NumberModelsFound?.Invoke(this, e);
        }

        protected virtual void OnSuccessfullySavedOnEndpoint(SavedAtEndpointEventArgs<TCsvRecordModel> e)
        {
            SuccessfullySavedAtEndpoint?.Invoke(this, e);
        }

        protected virtual void OnFailedSaveOnEndpoint(FailedSaveAtEndpointEventArgs<TCsvRecordModel> e)
        {
            FailedSaveAtEndpoint?.Invoke(this, e);
        }

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
                OnSuccessfullySavedOnEndpoint(new SavedAtEndpointEventArgs<TCsvRecordModel>(model, httpResponse.StatusCode, responseText));
            }
            else
            {
                OnFailedSaveOnEndpoint(new FailedSaveAtEndpointEventArgs<TCsvRecordModel>(model, httpResponse.StatusCode, responseText));
            }

            return httpResponse;
        }
    }
}
