using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UtgKata.Api.Controllers;
using UtgKata.Api.Filters;
using UtgKata.Api.Models;
using UtgKata.Data.Models;
using UtgKata.Data.Repositories;
using Xunit;

namespace UtgKata.Api.Tests
{
    public class GeneralResponseViewResultFilterTests
    {
        [Fact]
        public async Task ShouldModifySuccessfulResponseCorrectly()
        {
            // Arrange
            var actionCtx = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var mockRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();
            var controller = new CustomerController(mockRepo.Object, mockMapper.Object);
            var actionResult = new OkObjectResult(new TestViewModel { TestId = 123 });
            var ctx = new ResultExecutingContext(actionCtx, new List<IFilterMetadata>(), actionResult, controller);
            var mockDelegate = new Mock<ResultExecutionDelegate>();

            var attrib = new GeneralResponseViewResultFilterAttribute();

            // Act
            await attrib.OnResultExecutionAsync(ctx, mockDelegate.Object);

            // Assert
            var result = ctx.Result as ObjectResult; 
            var response = result.Value as GeneralResponseViewModel;
            response.ShouldNotBeNull();
            response.HasErrors.ShouldBeFalse();
            response.Response.ShouldBeOfType<TestViewModel>();
            response.ErrorDetails.ShouldBeNull();

            var model = response.Response as TestViewModel;
            model.TestId.ShouldBe(123);

            mockDelegate.Verify(x => x(), Times.Once);
        }

        [Fact]
        public async Task ShouldModifyFailedResponseCorrectly()
        {
            // Arrange
            var actionCtx = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var mockRepo = new Mock<ICustomerRepository>();
            var mockMapper = new Mock<IMapper>();
            var controller = new CustomerController(mockRepo.Object, mockMapper.Object);
            var actionResult = new BadRequestObjectResult(new ErrorMessageViewModel("Something bad happened"));
            var ctx = new ResultExecutingContext(actionCtx, new List<IFilterMetadata>(), actionResult, controller);
            var mockDelegate = new Mock<ResultExecutionDelegate>();

            var attrib = new GeneralResponseViewResultFilterAttribute();

            // Act
            await attrib.OnResultExecutionAsync(ctx, mockDelegate.Object);

            // Assert
            var result = ctx.Result as ObjectResult;
            var response = result.Value as GeneralResponseViewModel;
            response.ShouldNotBeNull();
            response.HasErrors.ShouldBeTrue();
            response.ErrorDetails.ShouldBeOfType<ErrorMessageViewModel>();
            response.ErrorDetails.ErrorMessage.ShouldBe("Something bad happened");
            response.Response.ShouldBeNull();

            mockDelegate.Verify(x => x(), Times.Once);
        }
    }

    public class TestViewModel
    {
        public int TestId { get; set; }
    }
}
