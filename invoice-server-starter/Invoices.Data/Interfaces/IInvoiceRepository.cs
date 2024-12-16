
using Invoices.Data.Models;


namespace Invoices.Data.Interfaces
{
    public interface IInvoiceRepository :IBaseRepository<Invoice>
    {
        IList<Invoice> GetAll(
             ulong? sellerId = null,
             ulong? buyerId = null,
             string? product = null,
             decimal? minPrice = null,
             decimal? maxPrice = null,
             int? limit = null);
        Task<long> GetCurrentYearSumAsync(int year);
        Task<long> GetAllTimeSumAsync();
        Task<int> GetInvoicesCountAsync();
    }

}
