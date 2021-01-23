using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Api.MappingProfiles;
using UtgKata.Api.Models;
using UtgKata.Api.Services;
using UtgKata.Data.Models;
using UtgKata.Data.Repositories;
using Xunit;

namespace UtgKata.Api.Tests.ServiceTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<IRepository<Customer>> repositoryMock;

        private readonly IMapper mapper;

        public CustomerServiceTests()
        {
            this.repositoryMock = new Mock<IRepository<Customer>>();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(typeof(CustomerAutoMapperProfile)));
            this.mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task ShouldAddCustomerViaRepository()
        {
            // Arrange
            var model = new CustomerViewModel { Id = 101, FirstName = "Jim", LastName = "Smith", PostCode = "PR4 3ED" };
            this.repositoryMock.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync(new Customer { Id = 101, FirstName = "Jim", LastName = "Smith", PostCode = "PR4 3ED" });
            var service = new CustomerService(this.repositoryMock.Object, this.mapper);

            // Act
            var result = await service.AddCustomerAsync(model);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<CustomerViewModel>();
            result.Id.ShouldBe(101);
        }

        [Fact]
        public async Task ShouldGetAllCustomers()
        {
            // Arrange
            var customer1 = new Customer { Id = 101, FirstName = "Jim", LastName = "Smith" };
            var customer2 = new Customer { Id = 102, FirstName = "Alice", LastName = "Jones" };
            this.repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Customer> { customer1, customer2 });

            var service = new CustomerService(this.repositoryMock.Object, this.mapper);

            // Act
            var result = await service.GetAllCustomersAsync();

            // Assert
            result.ShouldBeOfType<List<CustomerViewModel>>();
            
            var models = result as List<CustomerViewModel>;
            models.ShouldNotBeNull();
            models.Count().ShouldBe(2);
            models[0].Id.ShouldBe(101);
            models[1].Id.ShouldBe(102);
        }

        [Fact]
        public async Task ShouldGetCustomerById()
        {
            // Arrange
            var customer = new Customer { Id = 101, FirstName = "Jim", LastName = "Smith" };
            this.repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var service = new CustomerService(this.repositoryMock.Object, this.mapper);

            // Act
            var result = await service.GetCustomerByIdAsync(101);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<CustomerViewModel>();
            result.Id.ShouldBe(101);
        }

        [Fact]
        public async Task ShouldGetCustomerByReference()
        {
            // Arrange
            var customer = new Customer { Id = 101, FirstName = "Jim", LastName = "Smith", CustomerRef = "ABC123" };
            this.repositoryMock.Setup(x => x.GetFirstMatchAsync(It.IsAny<Func<Customer, bool>>())).ReturnsAsync(customer);

            var service = new CustomerService(this.repositoryMock.Object, this.mapper);

            // Act
            var result = await service.GetCustomerByReferenceAsync("ABC123");

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<CustomerViewModel>();
            result.Id.ShouldBe(101);
            result.CustomerRef.ShouldBe("ABC123");
        }

    }
}
