using System;

namespace UtgKata.Lib.CsvReader.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CsvHeadingMapper : Attribute
    {
        public string HeadingName { get; set; }
    }
}
