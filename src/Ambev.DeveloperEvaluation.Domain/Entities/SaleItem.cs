using Ambev.DeveloperEvaluation.Domain.Common;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item in a sale, including material details, pricing, and cancellation status.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier of the sale that this item belongs to.
        /// </summary>
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }
        /// <summary>
        /// Represents the item code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Represents the item name
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the unit price of the product in this sale item.
        /// The price must be a positive number.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the quantity of the product in this sale item.
        /// The quantity must be a positive integer.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the discount applied to the sale item.
        /// The discount must not be negative.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets a value indicating whether the sale item is cancelled.
        /// If true, the item is no longer part of the active sale.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets the total price for the sale item, calculated as (UnitPrice * Quantity) - Discount.
        /// Excludes cancelled items from sale calculations.
        /// </summary>
        public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

        /// <summary>
        /// Cancels the sale item. Sets the IsCancelled property to true.
        /// </summary>
        public void Cancel() => IsCancelled = true;
    }
}