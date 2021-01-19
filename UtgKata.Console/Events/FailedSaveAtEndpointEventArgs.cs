using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UtgKata.Lib.CsvReader.Models;

namespace UtgKata.Console.Events
{
    public class FailedSaveAtEndpointEventArgs<TCsvRecordModel> : EventArgs where TCsvRecordModel : CsvReaderModelBase
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ResponseText { get; set; }

        public TCsvRecordModel Model { get; set; }

        public FailedSaveAtEndpointEventArgs(TCsvRecordModel model, HttpStatusCode statusCode, string responseText)
        {
            this.Model = model;
            this.StatusCode = statusCode;
            this.ResponseText = responseText;
        }
    }
}
