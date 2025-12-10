using Microsoft.AspNetCore.Mvc;
using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.Exceptions;
using PagMenos.Application.Shared.ExceptionsExtensions;
using PagMenos.Presentation.Filters;

namespace PagMenos.Presentation.Controllers
{
	[ApiController]
	[ServiceFilter(typeof(RequireAuthenticatedUserFilter))]
	[Route("api/[controller]")]
	public class InsumoController :ControllerBase
	{

		private readonly IProductService service;
		private readonly ILogger<InsumoController> logger;
		private readonly ProductValidator validator;

		public InsumoController(
			IProductService _service,
			ILogger<InsumoController> _logger,
			ProductValidator _validator)
		{
			service = _service;
			logger = _logger;
			validator = _validator;
		}

		
		/// <summary>
		/// Obtém um produto pelo ID.
		/// </summary>
		/// <param name="id">Identificador do produto.</param>
		/// <returns>O produto encontrado.</returns>
		/// <response code="200">Produto encontrado.</response>
		/// <response code="404">Nenhum produto encontrado com o ID informado.</response>
		[HttpGet("{id:long}")]
		public async Task<IActionResult> GetById(long id)
		{
			logger.LogInformation("Buscando produto ID={Id}", id);

			var product = await service.GetByIdAsync(id);
			if (product == null)
			{
				logger.LogWarning("Produto ID={Id} não encontrado.", id);
				return new CustomHttpResponseException("PRODUCT_NOTFOUND", "Produto não existe").ToActionResult();
			}

			return Ok(product);
		}

		/// <summary>
		/// Cria um novo produto.
		/// </summary>
		/// <param name="product">Dados do produto.</param>
		/// <returns>O produto criado.</returns>
		/// <response code="201">Produto criado com sucesso.</response>
		/// <response code="400">Dados inválidos enviados para criação.</response>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Product product)
		{
			try
			{
				logger.LogInformation("Dados invalidos {ProductName}", product.ProductName);

				var validation = validator.Validate(product);

				if (!validation.IsValid)
				{
					logger.LogInformation("Dados invalidos {product}", product.ProductName);

					return new CustomHttpResponseException("INVALID_PRODUCT_DATA", validation.Errors.First()
					.ErrorMessage).ToActionResult();

				}

				await service.AddAsync(product);

				return CreatedAtAction(nameof(GetById),
					new { id = product.Id },
					product);
			}
			catch (CustomHttpResponseException ex)
			{
				// outros erros
				logger.LogWarning("Erro ao criar produto: {Message}", ex.Message);
				return ex.ToActionResult();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", ex.Message).ToActionResult();
			}
		}


		/// <summary>
		/// Atualiza um produto existente.
		/// </summary>
		/// <param name="id">ID do produto a ser atualizado.</param>
		/// <param name="product">Novos dados do produto.</param>
		/// <response code="204">Atualizado com sucesso.</response>
		/// <response code="400">ID inconsistente entre rota e corpo.</response>
		/// <response code="404">Produto não encontrado.</response>
		[HttpPut("{id:long}")]
		public async Task<IActionResult> Update(long id, [FromBody] Product product)
		{
			try
			{
				if (id != product.Id)
				{
					logger.LogInformation("Dados invalidos {ProductName}", product.ProductName);

					return new CustomHttpResponseException("ID inconsistente.", "").ToActionResult();
				}

				logger.LogInformation("Atualizando produto {Id}", id);

				var existing = await service.GetByIdAsync(id);
				if (existing == null)
				{
					logger.LogWarning("Produto {Id} não encontrado para atualização.", id);

					return new CustomHttpResponseException("PRODUCT_NOTFOUND", "Produto não existe").ToActionResult();
				}

				service.Update(product);

				return NoContent();
			}
			catch (CustomHttpResponseException ex)
			{
				// outros erros
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
		/// Remove um produto pelo ID.
		/// </summary>
		/// <param name="id">ID do produto.</param>
		/// <response code="204">Removido com sucesso.</response>
		/// <response code="404">Produto não encontrado.</response>
		[HttpDelete("{id:long}")]
		public async Task<IActionResult> Delete(long id)
		{
			try
			{
				logger.LogInformation("Removendo produto {Id}", id);

				var product = await service.GetByIdAsync(id);
				if (product == null)
				{
					logger.LogWarning("Produto {id} não encontrado para exclusão.", id);
					return new CustomHttpResponseException("PRODUCT_NOTFOUND", "Produto não encontrado").ToActionResult();
				}

				service.Remove(product);

				return NoContent();

			}
			catch (CustomHttpResponseException ex)
			{
				// outros erros
				logger.LogWarning("Não foi possivel excluir: {Message}", ex.Message);
				return ex.ToActionResult();
			}
			catch (Exception ex)
			{
				// erros genericos application
				logger.LogError(ex, "Erro inesperado ao excluir dados {id}", id);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", ex.Message).ToActionResult();
			}
		}
	}
}