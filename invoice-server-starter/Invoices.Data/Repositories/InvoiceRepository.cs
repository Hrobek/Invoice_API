using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories;

/// <summary>
/// Repository for managing Invoice entities with additional query functionality.
/// </summary>
public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
{
    /// <summary>
    /// Initializes a new instance of the InvoiceRepository class.
    /// </summary>
    /// <param name="invoicesDbContext">The database context.</param>
    public InvoiceRepository(InvoicesDbContext invoicesDbContext) : base(invoicesDbContext)
    {
    }

    /// <summary>
    /// Retrieves all invoices with optional filtering and limiting.
    /// </summary>
    /// <param name="sellerId">Filter by seller ID.</param>
    /// <param name="buyerId">Filter by buyer ID.</param>
    /// <param name="product">Filter by product name.</param>
    /// <param name="minPrice">Filter by minimum price.</param>
    /// <param name="maxPrice">Filter by maximum price.</param>
    /// <param name="limit">Limit the number of returned results.</param>
    /// <returns>A list of filtered invoices.</returns>
    public IList<Invoice> GetAll(
        ulong? sellerId = null,
        ulong? buyerId = null,
        string? product = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? limit = null)
    {
        IQueryable<Invoice> query = dbSet
            .Include(i => i.Seller) // Include related Seller details.
            .Include(i => i.Buyer); // Include related Buyer details.

        if (sellerId is not null)
            query = query.Where(i => i.SellerId == sellerId); // Filter by seller ID.

        if (buyerId is not null)
            query = query.Where(i => i.BuyerId == buyerId); // Filter by buyer ID.

        if (product is not null)
            query = query.Where(i => i.Product == product); // Filter by product name.

        if (minPrice is not null)
            query = query.Where(i => i.Price >= minPrice.Value); // Filter by minimum price.

        if (maxPrice is not null)
            query = query.Where(i => i.Price <= maxPrice.Value); // Filter by maximum price.

        if (limit is not null && limit >= 0)
            query = query.Take(limit.Value); // Limit the number of results.

        return query.ToList(); // Execute the query and return the results.
    }

    /// <summary>
    /// Finds an invoice by its unique identifier, including related Buyer and Seller details.
    /// </summary>
    /// <param name="id">The unique identifier of the invoice.</param>
    /// <returns>The invoice if found; otherwise, null.</returns>
    public override Invoice? FindById(ulong id)
    {
        return dbSet
            .Include(i => i.Seller) // Include related Seller details.
            .Include(i => i.Buyer) // Include related Buyer details.
            .FirstOrDefault(i => i.Id == id); // Find the invoice by ID.
    }

    /// <summary>
    /// Calculates the total price of invoices for the specified year.
    /// </summary>
    /// <param name="year">The year for which to calculate the sum.</param>
    /// <returns>The total price of invoices for the year.</returns>
    public async Task<long> GetCurrentYearSumAsync(int year)
    {
        return await invoicesDbContext.Invoices
            .Where(i => i.DueDate.Year == year) // Filter invoices by year.
            .SumAsync(i => i.Price); // Calculate the sum of prices.
    }

    /// <summary>
    /// Calculates the total price of all invoices in the database.
    /// </summary>
    /// <returns>The total price of all invoices.</returns>
    public async Task<long> GetAllTimeSumAsync()
    {
        return await invoicesDbContext.Invoices
            .SumAsync(i => i.Price); // Calculate the sum of prices.
    }

    /// <summary>
    /// Counts the total number of invoices in the database.
    /// </summary>
    /// <returns>The total count of invoices.</returns>
    public async Task<int> GetInvoicesCountAsync()
    {
        return await invoicesDbContext.Invoices
            .CountAsync(); // Count the total number of invoices.
    }
}
