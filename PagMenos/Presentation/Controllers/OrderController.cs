using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.Exceptions;
using PagMenos.Application.Shared.ExceptionsDTOs;
using PagMenos.Application.Shared.ExceptionsExtensions;

namespace PagMenos.Presentation.Controllers
{

	/// <summary>
	/// Gerencia operações relacionadas aos colaboradores.
	/// Autorização integrada com Azure AD B2C. [somente Authorize é necessário]
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController :ControllerBase
	{
		private readonly IOrderService service;
		private readonly ILogger<OrderController> logger;
		private readonly OrderValidator validator;
		private readonly IMapper mapper;

		public OrderController(
			IOrderService _service,
			ILogger<OrderController> _logger,
			OrderValidator _validator,
			IMapper _mapper)
		{
			service = _service;
			logger = _logger;
			validator = _validator;
			mapper = _mapper;
		}

		/// <summary>
		/// Retorna lista de pedidos
		/// </summary>
		/// <returns>Lista paginada de pedidos</returns>
		[HttpGet("List")]
		public async Task<IActionResult> getAllOrder()
		{

			try
			{

				var listOrder = await service.ListPagedOrder();

				if (listOrder.orderItems == null)
				{
					logger.LogInformation("Nenhum pedido encontrado {TotalItens}", listOrder.TotalItems);
					return new CustomHttpResponseException("ORDER_NOTFOUND", "Nenhum pedido encontrado").ToActionResult();

				}

				if (listOrder.orderItems.Count() == 0)
				{
					logger.LogInformation("Nenhum pedido encontrado {TotalItens}", listOrder.TotalItems);
					return new CustomHttpResponseException("ORDER_NOTFOUND", "Nenhum pedido encontrado").ToActionResult();
				}

				return Ok(listOrder);

			}
			catch (Exception ex)
			{
				// erros genericos application
				logger.LogError(ex, ex.Message);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", ex.Message).ToActionResult();
			}
		}

		/// <summary>
		/// Obtém um pedido pelo numero.
		/// </summary>
		/// <param name="orderNumber">Numero do pedido.</param>
		/// <returns>O pedido encontrado.</returns>
		/// <response code="200">Colaborador encontrado.</response>
		/// <response code="404">Nenhum pedido encontrado com o ID informado.</response>
		[HttpGet("{orderNumber}")]
		public async Task<IActionResult> GetOrderByNumber(string orderNumber)
		{
			logger.LogInformation("Buscando pedido numero {orderNumber}", orderNumber);

			var order = await service.GetOrderByNumber(orderNumber);
			if (order == null)
			{
				logger.LogWarning("Pedido numero {orderNumber} não encontrado.", orderNumber);
				return new CustomHttpResponseException("ORDER_NOTFOUND", "Pedido não existe").ToActionResult();
			}

			return Ok(order);
		}

		/// <summary>
		/// Cria um novo pedido.
		/// </summary>
		/// <param name="order">Dados do pedido.</param>
		/// <returns>O pedido criado.</returns>
		/// <response code="201">Colaborador criado com sucesso.</response>
		/// <response code="400">Dados inválidos enviados para criação.</response>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateOrderDto order)
		{
			logger.LogInformation("Criando novo pedido", []);

			var validation = validator.Validate(order);

			if (!validation.IsValid)
			{
				logger.LogInformation("Dados invalidos {OrderNumber}", order.OrderNumberGenerated().ToString());

				return new CustomHttpResponseException("INVALID_ORDER_DATA", validation.Errors.First()
				.ErrorMessage).ToActionResult();

			}

			try
			{

				await service.CreateOrder(order);

				return Ok(new
				{
					message = "Order created successfully",
					orderNumber = order.OrderNumber
				});

			}
			catch (CustomHttpResponseException ex)
			{
				logger.LogWarning("Erro ao criar pedido: {Message}", ex.Message);
				return ex.ToActionResult();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", ex.Message).ToActionResult();
			}
		}




		/// <summary>
		/// Atualiza um pedido existente.
		/// </summary>
		/// <param name="orderNumber">Numero do pedido a ser atualizado.</param>
		/// <param name="order">Novos dados do pedido.</param>
		/// <response code="204">Atualizado com sucesso.</response>
		/// <response code="400">ID inconsistente entre rota e corpo.</response>
		/// <response code="404">Colaborador não encontrado.</response>
		[HttpPut("{orderNumber}")]
		public async Task<IActionResult> Update(string orderNumber, [FromBody] OrderDto order)
		{
			try
			{
				if (String.IsNullOrEmpty(orderNumber))
				{
					logger.LogInformation("Dados invalidos {orderNumber}", orderNumber);

					return new CustomHttpResponseException("Numero do pedido inconsistente.", "").ToActionResult();
				}

				logger.LogInformation("Atualizando pedido {orderNumber}", orderNumber);

				var existing = await service.GetOrderByNumber(orderNumber);

				if (existing == null)
				{
					logger.LogWarning("Pedido {orderNumber} não encontrado para atualização.", orderNumber);

					return new CustomHttpResponseException("ORDER_NOTFOUND", "Pedido não existe").ToActionResult();
				}

				var orderUpdated = service.UpdateOrder(orderNumber, order);

				return Ok(orderUpdated);
			}
			catch (CustomHttpResponseException ex)
			{
				logger.LogWarning("Não foi possivel atualizar: {Message}", ex.Message);
				return ex.ToActionResult();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", ex.Message).ToActionResult();
			}
		}



		/// <summary>
		/// Remove um pedido pelo ID.
		/// </summary>
		/// <param name="orderNumber">Numero do pedido.</param>
		/// <response code="204">Removido com sucesso.</response>
		/// <response code="404">Pedido não encontrado.</response>
		[HttpDelete("{orderNumber}")]
		public async Task<IActionResult> Delete(string orderNumber)
		{
			try
			{
				logger.LogInformation("Removendo pedido {orderNumber}", orderNumber);

				if (String.IsNullOrEmpty(orderNumber))
				{
					logger.LogInformation("Pedido inválido", orderNumber);
					return new CustomHttpResponseException("BAD_REQUEST", "Pedido inválido").ToActionResult();
				}

				var order = await service.GetOrderByNumber(orderNumber);

				if (order == null) {
					logger.LogInformation("Pedido não encontrado.", orderNumber);
					return new CustomHttpResponseException("ORDER_NOTFOUND", "Pedido não encontrado").ToActionResult();
				}

				var orderDeleted = await service.RemoveOrder(order);

				return Ok(orderDeleted);

			}
			catch (CustomHttpResponseException ex)
			{
				// outros erros
				logger.LogWarning("Não foi possivel excluir: {Message}", ex.Message);
				return ex.ToActionResult();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Erro inesperado ao excluir dados {orderNumber}", orderNumber);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", "Erro interno ao excluir pedido").ToActionResult();
			}
		}
	}
}