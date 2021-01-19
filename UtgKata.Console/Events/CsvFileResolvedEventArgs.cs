using System;
using System.Collections.Generic;
using System.Text;

namespace UtgKata.Console.Events
{
    public class CsvFileResolvedEventArgs : EventArgs
    {
        public string ResolvedFile { get; set; }

        public CsvFileResolvedEventArgs(string resolvedFile)
        {
            this.ResolvedFile = resolvedFile;
        }
    }
}
