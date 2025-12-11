using Microsoft.EntityFrameworkCore;
using PagMenos.Domain.Entities;
using PagMenos.Domain.Interfaces;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Shared.Exceptions;

namespace PagMenos.Application.Services
{

    public class OrderItemService : GenericService<OrderItem>, IOrderItemService
    {

        private readonly IOrderItemRepository repository;

        public OrderItemService(IOrderItemRepository _repository) : base(_repository)
        {
            repository = _repository;
        }

        public async Task<List<OrderItem>> GetOrderItemByProduct(string productName)
        {
            var result = await repository.GetOrderItemByProduct(productName).ToListAsync();

            return result == null ? throw new CustomHttpResponseException("ITEM_NOTFOUND", "Itens não encontrado, produto: {productName}") : result;
        }

        public async Task<OrderItem> GetOrderItemByOrderId(long orderId)
        {
            var result = await repository.GetOrderItemByOrderId(orderId).FirstOrDefaultAsync();

            return result == null ? throw new CustomHttpResponseException("ITEM_NOTFOUND", "Itens não encontrado, pedido: {orderId}") : result;
        }

    }
}
