using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using Invoices.Data.Repositories;

namespace Invoices.Api.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;

        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        public IList<InvoiceDto> GetAllInvoices ()
        {
            IList<Invoice> invoices = invoiceRepository.GetAll();
            return mapper.Map<IList<InvoiceDto>>(invoices);
        }

        public InvoiceDto? GetInvoice(ulong invoiceId)
        {
            Invoice? invoice = invoiceRepository.FindById(invoiceId);
            if (invoice is null)
            {
                return null;
            }

            return mapper.Map<InvoiceDto>(invoice);
        }

        public InvoiceDto AddInvoice(InvoiceDto invoiceDto)
        {
            Invoice invoice = mapper.Map<Invoice>(invoiceDto);
            invoice.InvoiceId = default;
            Invoice addedInvoice = invoiceRepository.Insert(invoice);

            return mapper.Map<InvoiceDto>(addedInvoice);
        }

        public void DeleteInvoice(uint invoiceId)
        {
            
        }
    }
}