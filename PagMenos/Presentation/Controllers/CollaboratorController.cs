using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using OrderDataManagement.Domain.Validators;
using PagMenos.Application.ConfigModels;
using PagMenos.Application.Shared.ExceptionsDTOs;
using PagMenos.Application.Shared.Exceptions;
using PagMenos.Application.Shared.ExceptionsExtensions;

namespace PagMenos.Presentation.Controllers
{

	/// <summary>
	/// Gerencia operações relacionadas aos colaboradores.
	/// Autorização integrada com Azure AD B2C. [somente Authorize é necessário]
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[AllowAnonymous]
	public class CollaboratorController :ControllerBase
	{
		private readonly ICollaboratorService service;
		private readonly ILogger<CollaboratorController> logger;
		private readonly CollaboratorValidator validator;
		private readonly IMapper mapper;

		public CollaboratorController(
			ICollaboratorService _service,
			ILogger<CollaboratorController> _logger,
			CollaboratorValidator _validator,
			IMapper _mapper)
		{
			service = _service;
			logger = _logger;
			validator = _validator;
			mapper = _mapper;
		}

		/// <summary>
		/// Obtém o perfil do usuário autenticado no Azure AD B2C.
		/// </summary>
		/// <remarks>
		/// Essa rota utiliza as claims enviadas pelo Azure AD B2C, como:
		/// - oid (ID único do usuário)
		/// - user name
		/// - name
		/// </remarks>
		/// <returns>Informações básicas do usuário autenticado.</returns>
		/// <response code="200">Retorna o perfil do usuário logado.</response>
		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginDto login)
		{
			if (login.Username?.Trim().ToLower() != "paguemenos" || login.Password?.Trim() != "123456")
				return Unauthorized();


			// Simula Azure AD
			var userId = "oid"; // User.FindFirst("oid")?.Value;
			var userName = "preferred_username"; // User.FindFirst("userName")?.Value ?? User.FindFirst("userName")?.Value;
			var name = "name"; // User.FindFirst("name")?.Value ?? User.FindFirst("name")?.Value;

			logger.LogInformation("Consultando perfil do colaborador {UserId}", userId);

			return Ok(new
			{
				UserId = userId,
				UserName = userName,
				Name = name //User.Identity?.Name
			});
		}



		/// <summary>
		/// Obtém um colaborador pelo ID.
		/// </summary>
		/// <param name="id">Identificador do colaborador.</param>
		/// <returns>O colaborador encontrado.</returns>
		/// <response code="200">Colaborador encontrado.</response>
		/// <response code="404">Nenhum colaborador encontrado com o ID informado.</response>
		[HttpGet("{id:long}")]
		public async Task<IActionResult> GetById(long id)
		{
			logger.LogInformation("Buscando colaborador ID={Id}", id);

			var collaborator = await service.GetByIdAsync(id);
			if (collaborator == null)
			{
				logger.LogWarning("Colaborador ID={Id} não encontrado.", id);
				return new CustomHttpResponseException("COLLABORATOR_NOTFOUND", "Colaborador não existe").ToActionResult();
			}

			var collaboratorFound = mapper.Map<CollaboratorDto>(collaborator);

			return Ok(collaboratorFound);
		}

		/// <summary>
		/// Cria um novo colaborador.
		/// </summary>
		/// <param name="collaborator">Dados do colaborador.</param>
		/// <returns>O colaborador criado.</returns>
		/// <response code="201">Colaborador criado com sucesso.</response>
		/// <response code="400">Dados inválidos enviados para criação.</response>
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CollaboratorDto collaborator)
		{
			try
			{
				logger.LogInformation("Criando novo colaborador {Name}", collaborator.Name);

				var validation = validator.Validate(collaborator);

				if (!validation.IsValid)
				{
					logger.LogInformation("Dados invalidos {Name}", collaborator.Name);

					return new CustomHttpResponseException("INVALID_COLLABORATOR_DATA", validation.Errors.First()
					.ErrorMessage).ToActionResult();

				}

				var collaboratorMapped = mapper.Map<Collaborator>(collaborator);

				await service.AddAsync(collaboratorMapped);

				return Ok(new
				{
					message = "Collaborator created successfully",
					Collaborator = collaborator.Name
				});
			}
			catch (CustomHttpResponseException ex)
			{
				// outros erros
				logger.LogWarning("Erro ao criar colaborador: {Message}", ex.Message);
				return ex.ToActionResult();
			}
			catch (Exception ex)
			{
				// erros genericos application
				logger.LogError(ex, "Erro inesperado ao criar colaborador {Name}", collaborator.Name);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", "Erro interno ao criar colaborador").ToActionResult();
			}
		}




		/// <summary>
		/// Atualiza um colaborador existente.
		/// </summary>
		/// <param name="id">ID do colaborador a ser atualizado.</param>
		/// <param name="collaborator">Novos dados do colaborador.</param>
		/// <response code="204">Atualizado com sucesso.</response>
		/// <response code="400">ID inconsistente entre rota e corpo.</response>
		/// <response code="404">Colaborador não encontrado.</response>
		[HttpPut("{id:long}")]
		public async Task<IActionResult> Update(long id, [FromBody] CollaboratorDto collaborator)
		{
			try
			{
				if (id <= 0)
				{
					logger.LogInformation("Dados inválidos {collaborator}", collaborator.Name);

					return new CustomHttpResponseException("ID inconsistente.", "").ToActionResult();
				}

				logger.LogInformation("Atualizando colaborador {Id}", id);

				var collaboratorFound = await service.GetByIdAsync(id);
				if (collaboratorFound == null)
				{
					logger.LogWarning("Colaborador {Id} não encontrado para atualização.", id);

					return new CustomHttpResponseException("COLLABORATOR_NOTFOUND", "Colaborador não existe").ToActionResult();
				}

				var collaboratorMapped = mapper.Map<Collaborator>(collaborator);

				service.Update(collaboratorFound);

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
				// erros genericos application
				logger.LogError(ex, "Erro inesperado ao atualizar {Name}", collaborator.Name);
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", "Erro interno ao atualizar colaborador").ToActionResult();
			}
		}

		/// <summary>
		/// Remove um colaborador pelo ID.
		/// </summary>
		/// <param name="id">ID do colaborador.</param>
		/// <response code="204">Removido com sucesso.</response>
		/// <response code="404">Colaborador não encontrado.</response>
		[HttpDelete("{id:long}")]
		public async Task<IActionResult> Delete(long id)
		{
			try
			{
				logger.LogInformation("Removendo colaborador {Id}", id);

				var collaborator = await service.GetByIdAsync(id);
				if (collaborator == null)
				{
					logger.LogWarning("Colaborador {id} não encontrado para exclusão.", id);
					return new CustomHttpResponseException("COLLABORATOR_NOTFOUND", "Colaborador não encontrado").ToActionResult();
				}

				service.Remove(collaborator);

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
				return new CustomHttpResponseException("INTERNAL_SERVER_ERROR", "Erro interno ao excluir colaborador").ToActionResult();
			}
		}
	}
}