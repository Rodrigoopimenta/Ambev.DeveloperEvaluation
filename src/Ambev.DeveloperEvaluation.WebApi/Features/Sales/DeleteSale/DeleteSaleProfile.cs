using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
/// Profile for mapping DeleteSale feature requests to commands
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteSale feature
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<Guid, Application.Sale.DeleteSale.DeleteSaleCommand>()
            .ConstructUsing(id => new Application.Sale.DeleteSale.DeleteSaleCommand(id));
    }
}
