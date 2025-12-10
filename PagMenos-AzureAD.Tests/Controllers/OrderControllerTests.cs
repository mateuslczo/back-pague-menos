using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.ExceptionsDTOs;
using PagMenos.Presentation.Filters;
using PagMenos_AzureAD.Tests.Helpers;
using System.Security.Claims;

namespace PagMenos_AzureAD.Tests.Controllers
{
	public class OrderControllerTests
	{
		[Fact]
		public async Task GetById_ShouldReturnOk_WhenOrderExists()
		{
			// Arrange
			var mockService = new Mock<IOrderService>();
			var mockMapper = new Mock<IMapper>();

			mockService.Setup(s => s.GetOrderByNumber("123"))
					   .ReturnsAsync(new Order { OrderNumber = "123" });

			mockMapper.Setup(x => x.Map<OrderDto>(It.IsAny<Order>()))
					  .Returns(new OrderDto { OrderNumber = "123" });

			var validator = new OrderValidator();

			var controller = ControllerTestHelper.BuildControllerWithUser(
				mockService.Object,
				validator,
				mockMapper.Object);

			// Act
			var result = await controller.GetById(123);

			// Assert
			var ok = Assert.IsType<OkObjectResult>(result);
			var dto = Assert.IsType<OrderDto>(ok.Value);
			Assert.Equal("123", dto.OrderNumber);
		}



		[Fact]
		public async Task GetById_ShouldReturnOk_WhenUserIsAuthenticated()
		{
			// Arrange
			var mockService = new Mock<IOrderService>();
			var mockMapper = new Mock<IMapper>();
			var validator = new OrderValidator();

			// Configura retorno do serviço
			mockService.Setup(s => s.GetOrderByNumber("123"))
					   .ReturnsAsync(new Order { OrderNumber = "123" });

			// Configura AutoMapper
			mockMapper.Setup(x => x.Map<OrderDto>(It.IsAny<Order>()))
					  .Returns(new OrderDto { OrderNumber = "123" });

			// Cria usuário com claims
			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, "100"),
		new Claim(ClaimTypes.Name, "Marcos"),
		new Claim(ClaimTypes.Role, "Admin")
	};

			var controller = ControllerTestHelper.BuildControllerWithUser(
				mockService.Object,
				validator,
				mockMapper.Object,
				claims
			);

			// Act
			var result = await controller.GetById(123);

			// Assert
			var ok = Assert.IsType<OkObjectResult>(result);
			var dto = Assert.IsType<OrderDto>(ok.Value);
			Assert.Equal("123", dto.OrderNumber);
		}

		/// <summary>
		/// Testar usuário não autenticado via filter pois anotações [Authorize] não detectado por testes unitarios
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task Filter_ShouldReturn401_WhenUserNotAuthenticated()
		{
			// Arrange
			var http = new DefaultHttpContext();

			// Usuário NÃO autenticado
			http.User = new ClaimsPrincipal(new ClaimsIdentity());

			var actionContext = new ActionContext(
				http,
				new RouteData(),
				new ControllerActionDescriptor()
			);

			var filterContext = new ActionExecutingContext(
				actionContext,
				new List<IFilterMetadata>(),
				new Dictionary<string, object>(),
				controller: null
			);

			var filter = new RequireAuthenticatedUserFilter();

			// Delegate "next" FAKE — necessário para testes
			// Ele NUNCA será chamado quando o filtro barrar o acesso (401)
			ActionExecutionDelegate next = () =>
			{
				var executed = new ActionExecutedContext(
					actionContext,
					new List<IFilterMetadata>(),
					controller: null
				);

				return Task.FromResult(executed);
			};

			// Act
			await filter.OnActionExecutionAsync(filterContext, next);

			// Assert
			Assert.IsType<UnauthorizedResult>(filterContext.Result);
		}



	}
}