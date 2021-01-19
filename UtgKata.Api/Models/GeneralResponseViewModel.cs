using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtgKata.Api.Models
{
    public class GeneralResponseViewModel
    {
        public ErrorMessageViewModel ErrorDetails { get; set; }

        public bool HasErrors { get; set; }

        public object Response { get; set; }
    }
}
