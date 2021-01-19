using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtgKata.Lib;

namespace UtgKata.Api.Models
{
    public class AddCustomerViewModel : ViewModelBase
    {
        public string CustomerRef { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

    }
}
