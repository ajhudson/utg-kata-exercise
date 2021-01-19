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

namespace UtgKata.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<Customer> customerRepo;

        private readonly IMapper mapper;


        public CustomerController(IRepository<Customer> customerRepo, IMapper mapper)
        {
            this.customerRepo = customerRepo;
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
            var customers = await this.customerRepo.GetAllAsync();

            if (customers == null || !customers.Any())
            {
                return this.NotFound(new ErrorMessageViewModel(ErrorResponseFactory.ErrorMessage_NothingFound));
            }

            var allCustomers = this.mapper.Map<List<Customer>, List<CustomerViewModel>>(customers);
            var model = new CustomersViewModel { Customers = allCustomers };

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
            var customer = await this.customerRepo.GetByIdAsync(id);

            if (customer == null)
            {
                return this.NotFound(new ErrorMessageViewModel(string.Format(ErrorResponseFactory.ErrorMessage_EntityNotFound, id)));
            }

            var model = this.mapper.Map<Customer, CustomerViewModel>(customer);

            return this.Ok(model);
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

            var entity = this.mapper.Map<CustomerViewModel, Customer>(customer);
            int id = await this.customerRepo.AddAsync(entity);
            var model = this.mapper.Map<Customer, CustomerViewModel>(entity);

            return this.CreatedAtAction(nameof(GetCustomerById), new { id }, model);
        }
    }
}
