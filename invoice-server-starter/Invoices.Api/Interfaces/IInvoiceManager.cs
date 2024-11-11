using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    public interface IInvoiceManager
    {
        IList<InvoiceDto> GetAllInvoices();
        InvoiceDto? GetInvoice(uint invoiceId);
        InvoiceDto AddInvoice(InvoiceDto invoiceDto);
        InvoiceDto? UpdateInvoice(uint invoiceId, InvoiceDto invoiceDto);
        InvoiceDto? DeleteInvoice(uint invoiceId);
    }
}
