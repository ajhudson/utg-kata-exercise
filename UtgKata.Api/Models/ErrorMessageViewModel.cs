// <copyright file="ErrorMessageViewModel.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Models
{
    /// <summary>
    /// The error message view model.
    /// </summary>
    public class ErrorMessageViewModel
    {
        /// <summary>Initializes a new instance of the <see cref="ErrorMessageViewModel" /> class.</summary>
        /// <param name="errorMessage">The error message.</param>
        public ErrorMessageViewModel(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
    }
}
