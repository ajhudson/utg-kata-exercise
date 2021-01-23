using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtgKata.Api.Models;
using UtgKata.Data.Models;
using UtgKata.Data.Repositories;

namespace UtgKata.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> repository;

        private readonly IMapper mapper;

        public CustomerService(IRepository<Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Add a single customer
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public async Task<CustomerViewModel> AddCustomerAsync(CustomerViewModel customerViewModel)
        {
            var customer = this.mapper.Map<Customer>(customerViewModel);
            var addedCustomer = await this.repository.AddAsync(customer);
            var customerModel = this.mapper.Map<CustomerViewModel>(addedCustomer);

            return customerModel;
        }

        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync()
        {
            var customers = await this.repository.GetAllAsync();
            var models = this.mapper.Map<List<Customer>, List<CustomerViewModel>>(customers);

            return models;
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
        {
            var customer = await this.repository.GetByIdAsync(id);
            var model = this.mapper.Map<Customer, CustomerViewModel>(customer);

            return model;
        }

        /// <summary>
        /// Get customer by reference
        /// </summary>
        /// <param name="customerReference"></param>
        /// <returns></returns>
        public async Task<CustomerViewModel> GetCustomerByReferenceAsync(string customerReference)
        {
            var customer = await this.repository.GetFirstMatchAsync(c => c.CustomerRef.ToLower() == customerReference.ToLower());
            var model = this.mapper.Map<Customer, CustomerViewModel>(customer);

            return model;
        }
    }
}
