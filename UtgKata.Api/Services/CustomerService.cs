// <copyright file="CustomerService.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using UtgKata.Api.Models;
    using UtgKata.Data.Models;
    using UtgKata.Data.Repositories;

    /// <summary>
    /// The customer service.
    /// </summary>
    /// <seealso cref="UtgKata.Api.Services.ICustomerService" />
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> repository;

        private readonly IMapper mapper;

        /// <summary>Initializes a new instance of the <see cref="CustomerService" /> class.</summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CustomerService(IRepository<Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>Add a single customer.</summary>
        /// <param name="customerViewModel">The customer.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<CustomerViewModel> AddCustomerAsync(CustomerViewModel customerViewModel)
        {
            var customer = this.mapper.Map<Customer>(customerViewModel);
            var addedCustomer = await this.repository.AddAsync(customer);
            var customerModel = this.mapper.Map<CustomerViewModel>(addedCustomer);

            return customerModel;
        }

        /// <summary>Get all customers.</summary>
        /// <returns>
        ///   Customer view models.
        /// </returns>
        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync()
        {
            var customers = await this.repository.GetAllAsync();
            var models = this.mapper.Map<List<Customer>, List<CustomerViewModel>>(customers);

            return models;
        }

        /// <summary>Get customer by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
        {
            var customer = await this.repository.GetByIdAsync(id);
            var model = this.mapper.Map<Customer, CustomerViewModel>(customer);

            return model;
        }

        /// <summary>Get customer by reference.</summary>
        /// <param name="customerReference">The customer reference.</param>
        /// <returns>
        ///   The customer view model.
        /// </returns>
        public async Task<CustomerViewModel> GetCustomerByReferenceAsync(string customerReference)
        {
            var customer = await this.repository.GetFirstMatchAsync(c => c.CustomerRef.ToLower() == customerReference.ToLower());
            var model = this.mapper.Map<Customer, CustomerViewModel>(customer);

            return model;
        }
    }
}
