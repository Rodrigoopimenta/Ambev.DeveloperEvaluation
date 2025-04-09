using AutoMapper;
namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Ambev.DeveloperEvaluation.Domain.Entities.Sale>();
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.Sale, CreateSaleResult>();
    }
}
