using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    public interface IInvoiceManager : IBaseManager<InvoiceDto>
    {
        InvoiceDto? DeleteInvoice(uint invoiceId);
    }
}
