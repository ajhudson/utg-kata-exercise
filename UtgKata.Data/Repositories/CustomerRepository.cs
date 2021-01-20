using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Data.Models;

namespace UtgKata.Data.Repositories
{
    public class CustomerRepository : GeneralRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(UtgKataDbContext ctx) : base(ctx)
        {
        }

        public Customer GetByCustomerReference(string reference)
        {
            var entity = this.dbSet.FirstOrDefault(c => c.CustomerRef == reference.ToUpper());

            return entity;
        }
    }
}
