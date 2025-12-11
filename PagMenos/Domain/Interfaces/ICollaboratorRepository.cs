using PagMenos.Domain.Entities;

namespace PagMenos.Domain.Interfaces
{
	public interface ICollaboratorRepository : IGenericRepository<Collaborator>
	{

		/// <summary>
		/// Buscar colaborador por nome
		/// </summary>
		/// <param name="Name"></param>
		/// <returns>IQueryable<Collaborator>></returns>
		IQueryable<Collaborator> GetCollaboratorByName(string name);

		/// <summary>
		/// Buscar produto por nome
		/// </summary>
		/// <param name="User"></param>
		/// <returns>IQueryable<Collaborator></returns>
		IQueryable<Collaborator> GetProductByUserName(string userName);

	}
}
