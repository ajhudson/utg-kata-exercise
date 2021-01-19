using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtgKata.Console.Events
{
    public class NumberModelsFoundEventArgs : EventArgs
    {
        public int NumberOfModelsFound { get; set; }

        public NumberModelsFoundEventArgs(int numberOfModelsFound)
        {
            this.NumberOfModelsFound = numberOfModelsFound;
        }
    }
}
