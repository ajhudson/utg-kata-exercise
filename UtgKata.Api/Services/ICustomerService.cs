// <copyright file="ICustomerService.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UtgKata.Api.Models;

    /// <summary>
    /// Interface for customer service.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Add a single customer.
        /// </summary>
        /// <param name="customerViewModel">The customer view model to save.</param>
        /// <returns>A customer view model.</returns>
        public Task<CustomerViewModel> AddCustomerAsync(CustomerViewModel customerViewModel);

        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <returns>List of customer view models.</returns>
        public Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync();

        /// <summary>
        /// Get customer by id.
        /// </summary>
        /// <param name="id">The customer id.</param>
        /// <returns>The customer view model which matches the id.</returns>
        public Task<CustomerViewModel> GetCustomerByIdAsync(int id);

        /// <summary>
        /// Get customer by reference.
        /// </summary>
        /// <param name="customerReference">The customer reference.</param>
        /// <returns>The customer matching the reference.</returns>
        public Task<CustomerViewModel> GetCustomerByReferenceAsync(string customerReference);
    }
}
