using Invoices.Api.Models;
using Invoices.Data.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Defines the operations for managing invoices and their related DTOs.
    /// </summary>
    public interface IInvoiceManager : IBaseManager<InvoiceDto, Invoice>
    {
        /// <summary>
        /// Retrieves all invoices, optionally filtered by the provided criteria.
        /// </summary>
        /// <param name="invoiceFilterDto">An optional filter object containing criteria for filtering invoices.</param>
        /// <returns>A list of invoices matching the specified filter criteria.</returns>
        IList<InvoiceDto> GetAll(InvoiceFilterDto? invoiceFilterDto = null);

        /// <summary>
        /// Retrieves invoices based on an identification number, either for a seller or a buyer.
        /// </summary>
        /// <param name="identificationNumber">The identification number of the seller or buyer.</param>
        /// <param name="isSeller">Indicates whether to filter invoices by seller (true) or buyer (false).</param>
        /// <returns>A list of invoices matching the specified identification number.</returns>
        List<InvoiceDto> GetByIdentificationNumber(string identificationNumber, bool isSeller);

        /// <summary>
        /// Retrieves statistical data for invoices, including sums and counts.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an InvoiceStatisticDto with the statistics.</returns>
        Task<InvoiceStatisticDto> GetInvoiceStatistics();

        /// <summary>
        /// Maps an <see cref="InvoiceDto"/> object to an <see cref="InvoiceResponseDto"/>.
        /// </summary>
        /// <param name="invoice">The <see cref="InvoiceDto"/> to map.</param>
        /// <returns>An <see cref="InvoiceResponseDto"/> containing the mapped data.</returns>
        InvoiceResponseDto MapToResponseDto(InvoiceDto invoice);
    }
}
