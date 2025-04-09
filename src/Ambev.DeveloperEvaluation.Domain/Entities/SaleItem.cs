using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }

        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Discount { get; private set; }

        public bool IsCancelled { get; private set; }

        public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

        // Construtor para EF
        private SaleItem() { }

        /// <summary>
        /// Constructs a SaleItem applying business rules: quantity validation and discount.
        /// </summary>
        /// <param name="itemCode">The unique code of the item.</param>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="unitPrice">The unit price of the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        public SaleItem(string itemCode, string itemName, decimal unitPrice, int quantity)
        {
            if (quantity > 20)
                throw new BusinessRuleException("Cannot sell more than 20 units of the same product.");

            if (quantity < 1)
                throw new BusinessRuleException("Quantity must be at least 1.");

            if (unitPrice <= 0)
                throw new BusinessRuleException("Unit price must be greater than zero.");

            ItemCode = itemCode;
            ItemName = itemName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = CalculateDiscount(quantity, unitPrice);
        }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10)
                return quantity * unitPrice * 0.20m;

            if (quantity >= 4)
                return quantity * unitPrice * 0.10m;

            return 0;
        }

        /// <summary>
        /// Cancels the sale item.
        /// </summary>
        public void Cancel() => IsCancelled = true;
    }
}