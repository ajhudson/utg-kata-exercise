using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Api.Controllers;
using UtgKata.Api.MappingProfiles;
using UtgKata.Api.Models;
using UtgKata.Data.Models;
using UtgKata.Data.Repositories;
using Xunit;

namespace UtgKata.Api.Tests.ControllerTests
{
    public class ControllerTests
    {
        private readonly Mock<ICustomerRepository> customerRepoMock;

        private readonly IMapper mapper;

        public ControllerTests()
        {
            this.customerRepoMock = new Mock<ICustomerRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerAutoMapperProfile>();
            });

            this.mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetByIdShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 101, FirstName = "Jim", LastName = "Smith" };
            this.customerRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = await controller.GetCustomerById(101) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var model = result.Value as CustomerViewModel;
            model.ShouldNotBeNull();
            model.Id.ShouldBe(101);
            model.FirstName.ShouldBe("Jim");
            model.LastName.ShouldBe("Smith");
        }

        [Fact]
        public async Task GetByIdShouldReturnErrorIfCustomerNotFound()
        {
            // Arrange
            this.customerRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);
            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = await controller.GetCustomerById(101) as NotFoundObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);

            var model = result.Value as ErrorMessageViewModel;
            model.ShouldNotBeNull();
            model.ErrorMessage.ShouldBe("The requested entity with an id of 101 was not found");
        }

        [Fact]
        public void ShouldFindCustomerByReference()
        {
            // Arrange
            var customer = new Customer { Id = 101, CustomerRef = "ABC123", FirstName = "Jim", LastName = "Smith" };
            this.customerRepoMock.Setup(x => x.GetByCustomerReference(It.IsAny<string>())).Returns(customer);
            
            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = controller.GetCustomerByReference("ABC123") as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.Value.ShouldBeOfType<CustomerViewModel>();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var model = result.Value as CustomerViewModel;
            model.CustomerRef.ShouldBe("ABC123");
            model.Id.ShouldBe(101);
            model.FirstName.ShouldBe("Jim");
            model.LastName.ShouldBe("Smith");
        }

        [Fact]
        public void ShouldReturnNotFoundWhenCannotFindCustomerByReference()
        {
            // Arrange
            this.customerRepoMock.Setup(x => x.GetByCustomerReference(It.IsAny<string>())).Returns((Customer)null);

            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = controller.GetCustomerByReference("ABC123") as NotFoundObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.Value.ShouldBeOfType<ErrorMessageViewModel>();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);

            var errorMessageViewModel = result.Value as ErrorMessageViewModel;
            errorMessageViewModel.ErrorMessage.ShouldBe("No customer could be found with the reference 'ABC123'");
        }

        [Fact]
        public async Task GetAllShouldReturnAllCustomers()
        {
            // Arrange
            var customer1 = new Customer { Id = 101, FirstName = "Jim", LastName = "Smith" };
            var customer2 = new Customer { Id = 102, FirstName = "Alice", LastName = "Jones" };
            this.customerRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Customer> { customer1, customer2 });
            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = await controller.GetCustomers() as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);

            var model = result.Value as CustomersViewModel;
            model.ShouldNotBeNull();
            model.ShouldBeOfType<CustomersViewModel>();
            model.Customers[0].Id.ShouldBe(101);
            model.Customers[0].FirstName.ShouldBe("Jim");
            model.Customers[0].LastName.ShouldBe("Smith");
            model.Customers[1].Id.ShouldBe(102);
            model.Customers[1].FirstName.ShouldBe("Alice");
            model.Customers[1].LastName.ShouldBe("Jones");
        }

        [Fact]
        public async Task GetAllShouldReturnErrorIfNoCustomers()
        {
            // Arrange
            this.customerRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync((List<Customer>)null);
            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = await controller.GetCustomers() as NotFoundObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);

            var model = result.Value as ErrorMessageViewModel;
            model.ShouldNotBeNull();
            model.ErrorMessage.ShouldBe("There were no records found");
        }

        [Fact]
        public async Task ShouldCreateCustomerWhenAddCustomerIsCalled()
        {
            // Arrange
            var customer = new CustomerViewModel
            {
                CustomerRef = "A1",
                FirstName = "Jim",
                LastName = "Smith",
                PostCode = "PR6 3ED"
            };

            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);
            this.customerRepoMock.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync(101);

            // Act
            var result = await controller.AddCustomer(customer) as CreatedAtActionResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status201Created);

            var model = result.Value as CustomerViewModel;
            model.ShouldNotBeNull();
            model.CustomerRef.ShouldBe("A1");
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfCustomerIsNotValid()
        {
            // Arrange
            this.customerRepoMock.Setup(x => x.AddAsync(It.IsAny<Customer>())).Verifiable();
            var customer = new CustomerViewModel 
            { 
                CustomerRef = "A1",
                FirstName = "Jim", 
                LastName = "Smith", 
                PostCode = "xxx" 
            };

            var controller = new CustomerController(this.customerRepoMock.Object, this.mapper);

            // Act
            var result = await controller.AddCustomer(customer) as BadRequestObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);

            var model = result.Value as ErrorMessageViewModel;
            model.ShouldNotBeNull();
            model.ErrorMessage.ShouldBe("There were validation errors found: Post code must be a valid UK postal code");
        }
    }
}
