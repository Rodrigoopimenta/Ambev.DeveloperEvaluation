namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleItemResponse
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; }
        public Guid ItemCode { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
