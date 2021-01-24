// <copyright file="GeneralResponseViewModel.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Models
{
    /// <summary>
    /// Common API response.
    /// </summary>
    public class GeneralResponseViewModel
    {
        /// <summary>Gets or sets the error details.</summary>
        /// <value>The error details.</value>
        public ErrorMessageViewModel ErrorDetails { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance has errors.</summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.</value>
        public bool HasErrors { get; set; }

        /// <summary>Gets or sets the response.</summary>
        /// <value>The response.</value>
        public object Response { get; set; }
    }
}
