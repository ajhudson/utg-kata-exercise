using System.Collections.Generic;
using System.Threading.Tasks;
using UtgKata.Api.Models;

namespace UtgKata.Api.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Add a single customer 
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public Task<CustomerViewModel> AddCustomerAsync(CustomerViewModel customerViewModel);

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync();

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<CustomerViewModel> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Get customer by reference
        /// </summary>
        /// <param name="customerReference"></param>
        /// <returns></returns>
        public Task<CustomerViewModel> GetCustomerByReferenceAsync(string customerReference);
    }
}
