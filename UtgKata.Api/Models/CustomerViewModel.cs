using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtgKata.Api.Models
{
    public class CustomerViewModel : AddCustomerViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
