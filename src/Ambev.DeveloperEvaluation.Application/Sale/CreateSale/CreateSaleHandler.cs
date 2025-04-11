using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Application.Common.Messaging;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMessagePublisher messagePublisher)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _publisher = messagePublisher;

    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Mapeia os dados principais, mas ignora os itens
        var sale = _mapper.Map<Domain.Entities.Sale>(command);

        // Adiciona os itens um a um
        foreach (var item in command.Items)
        {
            var saleItem = new SaleItem(
                item.ItemCode,
                item.ItemName,
                item.UnitPrice,
                item.Quantity
            );

            // Idealmente, você deveria ter um método AddItem() no agregado
            sale.AddItem(saleItem);
        }

        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
        var message = new
        {
            SaleId = createdSale.Id,
            Event = "SaleCreated",
            CreatedAt = DateTime.UtcNow
        };

        await _publisher.PublishAsync("sales_events", message);
        var result = _mapper.Map<CreateSaleResult>(createdSale);

        return result;
    }
}
