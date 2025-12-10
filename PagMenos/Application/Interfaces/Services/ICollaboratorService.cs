using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Interfaces.Services;

namespace OrderDataManagement.Domain.Interfaces
{
	public interface ICollaboratorService : IGenericService<Collaborator>
	{

		/// <summary>
		/// Buscar colaborador por nome
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>IQueryable<Collaborator>></returns>
		Task<List<Collaborator>> GetCollaboratorByName(string name);

		/// <summary>
		/// Buscar produto por nome
		/// </summary>
		/// <param name="User"></param>
		/// <returns>IQueryable<Collaborator></returns>
		Task<Collaborator> GetProductByUserName(string userName);

	}
}
