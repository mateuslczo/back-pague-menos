using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using OrderDataManagement.Infrastructure.Data;

namespace OrderDataManagement.Infraestructure.Repositories
{

	public class CollaboratorRepository :GenericRepository<Collaborator>, ICollaboratorRepository
	{
		private readonly EFDbContext context;

		public CollaboratorRepository(EFDbContext _context) : base(_context)
		{
			context = _context;
		}


		public IQueryable<Collaborator> GetCollaboratorByName(string name)
		{
			var result = context.Collaborators.Where(p => p.Name.Contains(name));
			return result;
		}

		public IQueryable<Collaborator> GetProductByUserName(string userName)
		{
			var result = context.Collaborators.Where(p => p.User.Contains(userName));
			return result;
		}

	}
}
