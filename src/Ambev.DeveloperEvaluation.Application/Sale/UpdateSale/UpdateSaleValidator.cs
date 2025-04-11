using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

/// <summary>
/// Represents a validator for the UpdateSaleCommand. 
/// Ensures that the sale data is complete, valid, and conforms to business rules 
/// such as customer and branch presence, valid date, total amount constraints, 
/// and quantity limits on sale items.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleCommandValidator"/> class,
    /// defining validation rules for the creation of a sale.
    /// </summary>
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.SaleNumber)
             .NotEmpty().WithMessage("O número da venda é obrigatório.")
             .MaximumLength(50).WithMessage("O número da venda deve ter no máximo 50 caracteres.");

        RuleFor(x => x.SaleDate)
            .NotEmpty().WithMessage("A data da venda é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data da venda não pode ser no futuro.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O cliente é obrigatório.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do cliente deve ter no máximo 100 caracteres.");

        RuleFor(x => x.BranchId)
            .NotEmpty().WithMessage("A filial é obrigatória.");

        RuleFor(x => x.BranchName)
            .NotEmpty().WithMessage("O nome da filial é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da filial deve ter no máximo 100 caracteres.");

        RuleFor(x => x.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("O valor total não pode ser negativo.");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.Quantity)
                .GreaterThanOrEqualTo(4).WithMessage("A quantidade mínima para aplicar descontos é 4.")
                .LessThanOrEqualTo(20).WithMessage("Não é permitido mais que 20 unidades por produto.");
        });
    }
}