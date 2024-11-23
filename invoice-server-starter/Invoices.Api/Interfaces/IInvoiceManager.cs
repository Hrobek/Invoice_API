using Invoices.Api.Models;
using Invoices.Data.Models;

namespace Invoices.Api.Interfaces
{
    public interface IInvoiceManager : IBaseManager<InvoiceDto, Invoice>
    {
        public IList<InvoiceDto> GetAll(InvoiceFilterDto? invoiceFilterDto = null);
        List<InvoiceDto> GetByIdentificationNumber(string identificationNumber, bool isSeller);
        Task<InvoiceStatisticDto> GetInvoiceStatistics();
    }
}
