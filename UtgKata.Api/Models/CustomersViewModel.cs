using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtgKata.Api.Models
{
    public class CustomersViewModel : ViewModelBase
    {
        public List<CustomerViewModel> Customers { get; set; }
    }
}
