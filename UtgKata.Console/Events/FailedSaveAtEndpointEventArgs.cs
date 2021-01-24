// <copyright file="FailedSaveAtEndpointEventArgs.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Console.Events
{
    using System;
    using System.Net;
    using UtgKata.Lib.CsvReader.Models;

    /// <summary>
    /// Event arguments for when there is a failure attempting to save at an API end point.
    /// </summary>
    /// <typeparam name="TCsvRecordModel">The type of the CSV record model.</typeparam>
    /// <seealso cref="System.EventArgs" />
    public class FailedSaveAtEndpointEventArgs<TCsvRecordModel> : EventArgs
                    where TCsvRecordModel : CsvReaderModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="FailedSaveAtEndpointEventArgs{TCsvRecordModel}" /> class.</summary>
        /// <param name="model">The model.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="responseText">The response text.</param>
        public FailedSaveAtEndpointEventArgs(TCsvRecordModel model, HttpStatusCode statusCode, string responseText)
        {
            this.Model = model;
            this.StatusCode = statusCode;
            this.ResponseText = responseText;
        }

        /// <summary>Gets or sets the status code.</summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>Gets or sets the response text.</summary>
        /// <value>The response text.</value>
        public string ResponseText { get; set; }

        /// <summary>Gets or sets the model.</summary>
        /// <value>The model.</value>
        public TCsvRecordModel Model { get; set; }
    }
}
