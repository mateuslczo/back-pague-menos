
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Presentation.Controllers;
using System.Security.Claims;

namespace PagMenos_AzureAD.Tests.Helpers
{
	public static class ControllerTestHelper
	{
		// ====================================
		// 1) USUÁRIO AUTENTICADO PADRÃO (FAKE B2C)
		// ====================================
		public static OrderController BuildControllerWithUser(
			IOrderService serviceMock,
			OrderValidator validator,
			IMapper mapper)
		{
			var logger = Mock.Of<ILogger<OrderController>>();

			var controller = new OrderController(
				serviceMock,
				logger,
				validator,
				mapper);

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = FakeUserRolesClaimsHelper.CreateUserFakeB2C()
				}
			};

			return controller;
		}


		// ====================================
		// 2) USUÁRIO AUTENTICADO COM CLAIMS PERSONALIZADAS
		// ====================================
		public static OrderController BuildControllerWithUser(
		   IOrderService service,
		   OrderValidator validator,
		   IMapper mapper,
		   IEnumerable<Claim> claims)
		{
			var logger = Mock.Of<ILogger<OrderController>>();

			var controller = new OrderController(
				service,
				logger,
				validator,
				mapper
			);

			// 👈 ERRO CORRIGIDO: agora usa o `user` corretamente
			var identity = new ClaimsIdentity(claims, "TestAuthScheme");
			var user = new ClaimsPrincipal(identity);

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = user
				}
			};

			return controller;
		}


		// ====================================
		// 3) USUÁRIO NÃO AUTENTICADO (401)
		// ====================================
		public static OrderController BuildControllerWithoutUser(
			IOrderService service,
			OrderValidator validator,
			IMapper mapper)
		{
			var logger = Mock.Of<ILogger<OrderController>>();

			var controller = new OrderController(
				service,
				logger,
				validator,
				mapper
			);

			// Usuário sem identidade autenticada → IsAuthenticated = false
			var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = anonymous
				}
			};

			return controller;
		}
	}


	
}