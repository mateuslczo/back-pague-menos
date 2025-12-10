using Microsoft.EntityFrameworkCore;
using OrderDataManagement.Domain.Entities;
using OrderDataManagement.Domain.Interfaces;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.Exceptions;

namespace PagMenos.Application.Services
{

    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository _repository) : base(_repository)
        {
            repository = _repository;
        }

        public async Task<List<Product>> GetProductByDescriptionAsync(string description)
        {
            var result = await repository.GetProductByDescriptionAsync(description).ToListAsync();

            return result.Count == 0
                ? throw new CustomHttpResponseException("PRODUCT_NOTFOUND", "Não existe produto(s) com a descrição informada")
                : result;
        }

        public async Task<List<Product>> GetProductByNameAsync(string product)
        {
            var result = await repository.GetProductByNameAsync(product).ToListAsync();

            return result.Count == 0 ? throw new CustomHttpResponseException("PRODUCT_NOTFOUND", "Não existe produto(s) com esse nome") : result;
        }

    }
}
