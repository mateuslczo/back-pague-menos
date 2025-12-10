using FluentValidation;
using OrderDataManagement.Domain.Entities;
using PagMenos.Application.Shared.ExceptionsDTOs;

namespace OrderDataManagement.Domain.Validators
{
	public class CollaboratorValidator :AbstractValidator<CollaboratorDto>
	{
		public CollaboratorValidator()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("O nome é obrigatório.")
				.MaximumLength(50).WithMessage("O nome pode ter no máximo 50 caracteres.");

			RuleFor(c => c.LastName)
				.NotEmpty().WithMessage("O sobrenome é obrigatório.")
				.MaximumLength(100).WithMessage("O sobrenome pode ter no máximo 50 caracteres.");

			RuleFor(c => c.User)
				.NotEmpty().WithMessage("O usuário é obrigatório.")
				.MinimumLength(50).WithMessage("O usuário pode ter no máximo 6 caracteres.");

			RuleFor(c => c.Password)
				.NotEmpty().WithMessage("A senha é obrigatória.")
				.MaximumLength(255).WithMessage("A senha pode ter no máximo 12 caracteres.");

			RuleFor(c => c.Password)
				.MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
		}
	}
}
