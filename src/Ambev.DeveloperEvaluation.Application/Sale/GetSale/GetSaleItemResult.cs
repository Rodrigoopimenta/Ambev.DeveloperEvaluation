using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    public class GetSaleItemResult
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; }
        public Guid ItemCode { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
