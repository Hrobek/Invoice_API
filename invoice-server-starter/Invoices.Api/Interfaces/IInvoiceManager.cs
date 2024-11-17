using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    public interface IInvoiceManager //: IBaseManager<InvoiceDto>
    {
        IList<InvoiceDto> GetAll();
        InvoiceDto? Get(uint id);
        InvoiceDto Add(InvoiceDto inoviceDto);
        InvoiceDto? Update(uint id, InvoiceDto invoiceDto);
        InvoiceDto? DeleteInvoice(uint invoiceId);
    }
}
