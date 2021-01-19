using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtgKata.Api.Models;

namespace UtgKata.Api.Utilities
{
    public static class ErrorResponseFactory
    {
        public static string ErrorMessage_EntityNotFound = "The requested entity with an id of {0} was not found";

        public static string ErrorMessage_NothingFound = "There were no records found";

        public static string ErrorMessage_ValidationErrorsFound = "There were validation errors found:";
    }
}
