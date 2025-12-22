using Microsoft.EntityFrameworkCore;
using PagMenos.Domain.Entities;
using PagMenos.Domain.Interfaces;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.Exceptions;
using AutoMapper;
using PagMenos.Application.Shared.ExceptionsDTOs;

namespace PagMenos.Application.Services
{

	public class ProductService :GenericService<Product>, IProductService
	{
		private readonly IProductRepository repository;
		private readonly IMapper mapper;

		public ProductService(IProductRepository _repository, IMapper _mapper) : base(_repository)
		{
			repository = _repository;
			mapper = _mapper;
		}

		public async Task<long> CreateProduct(ProductDto product)
		{
			var productMapped = mapper.Map<Product>(product);

			await AddAsync(productMapped);

			await CommitAsync();

			return productMapped.Id;
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

		public async Task<int> UpdateProduct(long id, ProductDto product)
		{

			var productMapped = mapper.Map<Product>(product);
			productMapped.Id = id;

			Update(productMapped);

			var rowsAffected = await CommitAsync();

			return rowsAffected;

		}

	}
}
