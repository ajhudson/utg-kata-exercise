// <copyright file="CustomerController.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using UtgKata.Api.Models;
    using UtgKata.Api.Models.Validators;
    using UtgKata.Api.Services;
    using UtgKata.Api.Utilities;

    /// <summary>
    ///   The customer controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        private readonly IMapper mapper;

        /// <summary>Initializes a new instance of the <see cref="CustomerController" /> class.</summary>
        /// <param name="customerService">The customer service.</param>
        /// <param name="mapper">The mapper.</param>
        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        /// <summary>Gets the customers.</summary>
        /// <returns>
        ///   A view model containing all the customers.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await this.customerService.GetAllCustomersAsync();

            if (customers == null || !customers.Any())
            {
                return this.NotFound(new ErrorMessageViewModel(ErrorResponseFactory.ErrorMessageNothingFound));
            }

            var model = new CustomersViewModel { Customers = customers.ToList() };

            return this.Ok(model);
        }

        /// <summary>Gets the customer by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await this.customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return this.NotFound(new ErrorMessageViewModel(string.Format(ErrorResponseFactory.ErrorMessageEntityNotFound, id)));
            }

            return this.Ok(customer);
        }

        /// <summary>Gets the customer by reference.</summary>
        /// <param name="reference">The reference.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("ref/{reference}")]
        public async Task<IActionResult> GetCustomerByReference(string reference)
        {
            var customer = await this.customerService.GetCustomerByReferenceAsync(reference);

            if (customer == null)
            {
                return this.NotFound(new ErrorMessageViewModel($"No customer could be found with the reference '{reference}'"));
            }

            return this.Ok(customer);
        }

        /// <summary>Adds the customer.</summary>
        /// <param name="customer">The customer.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCustomer(CustomerViewModel customer)
        {
            var validator = new CustomerValidator();
            var validationResult = await validator.ValidateAsync(customer);

            if (!validationResult.IsValid)
            {
                var errorsList = string.Join<FluentValidation.Results.ValidationFailure>(',', validationResult.Errors.ToArray());

                return this.BadRequest(new ErrorMessageViewModel($"{ErrorResponseFactory.ErrorMessageValidationErrorsFound} {errorsList}"));
            }

            var addedCustomer = await this.customerService.AddCustomerAsync(customer);

            return this.CreatedAtAction(nameof(this.GetCustomerById), new { addedCustomer.Id }, addedCustomer);
        }
    }
}
