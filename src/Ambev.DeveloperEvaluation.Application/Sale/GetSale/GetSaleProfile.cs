using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleResponse
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale operation
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.Sale, GetSaleResult>();
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem, GetSaleItemResult>();
    }
}
