using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Data.Models;

namespace UtgKata.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByCustomerReference(string reference);
    }
}
