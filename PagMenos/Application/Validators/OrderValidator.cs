using FluentValidation;
using PagMenos.Application.Shared.ExceptionsDTOs;

public class OrderValidator :AbstractValidator<CreateOrderDto>
{
	public OrderValidator()
	{

		RuleFor(o => o.CollaboratorId)
			.GreaterThan(0).WithMessage("O colaborador é obrigatório.");


		// OrderItems (pelo menos 1 item)
		RuleFor(o => o.OrderItems)
			.NotNull().WithMessage("Itens do pedido obrigatórios.")
			.Must(list => list.Count > 0)
			.WithMessage("O pedido deve conter pelo menos 1 item.");

	}
}
