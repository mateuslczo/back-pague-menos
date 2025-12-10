using Microsoft.EntityFrameworkCore;
using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using PagMenos.Application.Shared.Exceptions;

namespace PagMenos.Application.Services
{

	public class CollaboratorService : GenericService<Collaborator>, ICollaboratorService
    {
        private readonly ICollaboratorRepository repository;

        public CollaboratorService(ICollaboratorRepository _repository) : base(_repository)
        {
            repository = _repository;
        }


        public async Task<List<Collaborator>> GetCollaboratorByName(string name)
        {
            var result = await repository.GetCollaboratorByName(name).ToListAsync();

            return result.Count == 0 ? throw new CustomHttpResponseException("COLLABORATOR_NOTFOUND", "Colaboradore(s) não encontrados") : result;
        }

        public async Task<Collaborator> GetProductByUserName(string userName)
        {
            var result = await repository.GetProductByUserName(userName).FirstOrDefaultAsync();

            return result == null ? throw new CustomHttpResponseException("INVALID_USER_NAME", "Nome de usuário inválido") : result;
        }

    }
}
