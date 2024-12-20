using Invoices.Data.Models;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Repository interface for managing Invoice entities, extending the base repository functionality.
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        /// <summary>
        /// Retrieves a list of invoices with optional filtering by seller, buyer, product, price range, and limit.
        /// </summary>
        /// <param name="sellerId">Optional filter for the seller's ID.</param>
        /// <param name="buyerId">Optional filter for the buyer's ID.</param>
        /// <param name="product">Optional filter for the product name.</param>
        /// <param name="minPrice">Optional filter for the minimum price.</param>
        /// <param name="maxPrice">Optional filter for the maximum price.</param>
        /// <param name="limit">Optional limit for the number of results returned.</param>
        /// <returns>A list of invoices matching the specified criteria.</returns>
        IList<Invoice> GetAll(
            ulong? sellerId = null,
            ulong? buyerId = null,
            string? product = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? limit = null);

        /// <summary>
        /// Calculates the total revenue for invoices issued in the specified year.
        /// </summary>
        /// <param name="year">The current year for which to calculate the total revenue.</param>
        /// <returns>The total revenue as a long value.</returns>
        Task<long> GetCurrentYearSumAsync(int year);

        /// <summary>
        /// Calculates the total revenue for all invoices issued across all years.
        /// </summary>
        /// <returns>The total revenue as a long value.</returns>
        Task<long> GetAllTimeSumAsync();

        /// <summary>
        /// Retrieves the total number of invoices stored in the repository.
        /// </summary>
        /// <returns>The total count of invoices as an integer.</returns>
        Task<int> GetInvoicesCountAsync();
    }
}
