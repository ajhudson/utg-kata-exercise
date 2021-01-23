using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtgKata.Api.Models;
using UtgKata.Api.Models.Validators;
using UtgKata.Api.Utilities;
using UtgKata.Data.Models;
using UtgKata.Data.Repositories;
using UtgKata.Api.Filters;
using UtgKata.Api.Services;

namespace UtgKata.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        private readonly IMapper mapper;


        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await this.customerService.GetAllCustomersAsync();

            if (customers == null || !customers.Any())
            {
                return this.NotFound(new ErrorMessageViewModel(ErrorResponseFactory.ErrorMessage_NothingFound));
            }

            var model = new CustomersViewModel { Customers = customers.ToList() };

            return this.Ok(model);
        }

        /// <summary>
        /// Get the customers by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await this.customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return this.NotFound(new ErrorMessageViewModel(string.Format(ErrorResponseFactory.ErrorMessage_EntityNotFound, id)));
            }

            return this.Ok(customer);
        }

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

        /// <summary>
        /// Add a new customer to the database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCustomer(CustomerViewModel customer)
        {
            var validator = new CustomerValidator();
            var validationResult = await validator.ValidateAsync(customer);

            if (!validationResult.IsValid)
            {
                var errorsList = string.Join<FluentValidation.Results.ValidationFailure>(',',validationResult.Errors.ToArray());

                return this.BadRequest(new ErrorMessageViewModel($"{ErrorResponseFactory.ErrorMessage_ValidationErrorsFound} {errorsList}"));
            }

            var addedCustomer = await this.customerService.AddCustomerAsync(customer);

            return this.CreatedAtAction(nameof(GetCustomerById), new { addedCustomer.Id }, addedCustomer);
        }
    }
}
