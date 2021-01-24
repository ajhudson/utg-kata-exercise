// <copyright file="ErrorResponseFactory.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Utilities
{
    /// <summary>
    /// The error response factory.
    /// </summary>
    public static class ErrorResponseFactory
    {
        /// <summary>The error message entity not found.</summary>
        public const string ErrorMessageEntityNotFound = "The requested entity with an id of {0} was not found";

        /// <summary>The error message nothing found.</summary>
        public const string ErrorMessageNothingFound = "There were no records found";

        /// <summary>The error message validation errors found.</summary>
        public const string ErrorMessageValidationErrorsFound = "There were validation errors found:";
    }
}
