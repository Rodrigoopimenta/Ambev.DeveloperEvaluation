using AutoMapper;
namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

/// <summary>
/// Profile for mapping between User entity and UpdateUserResponse
/// </summary>
public class UpdateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser operation
    /// </summary>
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, Ambev.DeveloperEvaluation.Domain.Entities.Sale>();
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.Sale, UpdateSaleResult>();
    }
}
