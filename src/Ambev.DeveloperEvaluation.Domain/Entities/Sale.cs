using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


/// <summary>
/// Represents a sale in the system, including sale details, customer information, branch, and items sold.
/// This entity follows domain-driven design principles and includes domain event tracking.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets the unique sale number that identifies the sale.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets the date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets the unique identifier of the customer who made the purchase.
    /// </summary>
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }

    /// <summary>
    /// Gets the name of the customer who made the purchase.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the unique identifier of the branch that made the purchase.
    /// </summary>
    public Guid BranchId { get; set; }
    public Branch? Branch { get; set; }

    /// <summary>
    /// Gets the name of the branch where the sale occurred.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the total amount for the sale, summing up the amounts of all non-cancelled items.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets a value indicating whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the list of items associated with the sale.
    /// </summary>
    public IList<SaleItem> Items { get; set; }

    /// <summary>
    /// Gets the list of domain events associated with the sale.
    /// Used to track significant state changes.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Updates the customer information for the sale.
    /// </summary>
    /// <param name="customerId">The updated customer ID.</param>
    /// <param name="customerName">The updated customer name.</param>
    public void UpdateCustomerInfo(Guid customerId, string customerName)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        _domainEvents.Add(new SaleModifiedEvent(this));
    }

    /// <summary>
    /// Adds a new item to the sale.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(SaleItem item)
    {
        Items.Add(item);
        CalculateTotal();
        _domainEvents.Add(new SaleModifiedEvent(this));
    }

    /// <summary>
    /// Cancels the sale.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
        _domainEvents.Add(new SaleCancelledEvent(this));
    }

    /// <summary>
    /// Cancels a specific item in the sale by its identifier.
    /// </summary>
    /// <param name="itemId">The identifier of the item to cancel.</param>
    public void CancelItem(Guid itemId)
    {
        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            item.Cancel();
            CalculateTotal();
            _domainEvents.Add(new ItemCancelledEvent(itemId, Id));
        }
    }

    /// <summary>
    /// Recalculates the total amount of the sale, excluding cancelled items.
    /// </summary>
    private void CalculateTotal()
    {
        TotalAmount = Items.Where(i => !i.IsCancelled)
                            .Sum(i => i.TotalPrice);
    }

    /// <summary>
    /// Clears all domain events associated with the sale.
    /// Useful for persisting the sale without carrying pending events.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}