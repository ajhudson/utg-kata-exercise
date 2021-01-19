using System;
using System.IO;
using System.Runtime.Serialization;

namespace UtgKata.Lib.CsvReader.CustomExceptions
{
    [Serializable]
    public class CsvWithoutHeaderException : Exception
    {
        private readonly string filename;

        public CsvWithoutHeaderException() : base()
        {
        }

        public CsvWithoutHeaderException(string absolutePath) : base($"Header does not exist in CSV file {absolutePath}")
        {
            this.filename = Path.GetFileName(absolutePath);
        }

        public CsvWithoutHeaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CsvWithoutHeaderException(System.Runtime.Serialization.SerializationInfo info, StreamingContext ctx) : base(info, ctx)
        {
        }
    }
}
