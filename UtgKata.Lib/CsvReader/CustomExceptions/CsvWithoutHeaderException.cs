// <copyright file="CsvWithoutHeaderException.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib.CsvReader.CustomExceptions
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;

    /// <summary>
    /// CSV without header exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class CsvWithoutHeaderException : Exception
    {
        private readonly string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWithoutHeaderException"/> class.
        /// </summary>
        public CsvWithoutHeaderException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWithoutHeaderException"/> class.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        public CsvWithoutHeaderException(string absolutePath)
            : base($"Header does not exist in CSV file {absolutePath}")
        {
            this.filename = Path.GetFileName(absolutePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWithoutHeaderException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public CsvWithoutHeaderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvWithoutHeaderException"/> class.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="ctx">The CTX.</param>
        protected CsvWithoutHeaderException(System.Runtime.Serialization.SerializationInfo info, StreamingContext ctx)
            : base(info, ctx)
        {
        }
    }
}
