using FluentValidation;
using OrderDataManagement.Domain.Entities;

public class ProductValidator :AbstractValidator<Product>
{
	public ProductValidator()
	{
		RuleFor(p => p.ProductName)
			.NotEmpty().WithMessage("O nome do produto é obrigatório.")
			.MinimumLength(10).WithMessage("O nome do produto deve ter no minimo 10 caracteres.");

		RuleFor(p => p.Description)
			.MinimumLength(10)
			.WithMessage("A descrição deve ter no minimo 10 caracteres.");

		RuleFor(p => p.StockQuantity)
			.NotNull().WithMessage("A quantidade em estoque é obrigatória.")
			.GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");

		RuleFor(p => p.Price)
			.NotNull().WithMessage("O preço é obrigatório.")
			.GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

	}
}
