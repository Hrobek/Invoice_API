using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using Invoices.Data.Repositories;

namespace Invoices.Api.Managers
{
    public class InvoiceManager : BaseManager<InvoiceDto, Invoice>,IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;

        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }
        public IList<InvoiceDto> GetAll()
        {
            var entities = invoiceRepository.GetAll();
            return mapper.Map<IList<InvoiceDto>>(entities);
        }
        public override InvoiceDto Add(InvoiceDto invoiceDto)
        {
            Invoice invoice = mapper.Map<Invoice>(invoiceDto);
            invoice.Id = default;

            Invoice addedInvoice = invoiceRepository.Insert(invoice);

            Invoice? found = invoiceRepository.FindById(invoice.Id);

            return mapper.Map<InvoiceDto>(found);
        }
        public override InvoiceDto? Update(ulong Id, InvoiceDto invoiceDto)
        {
            if (!invoiceRepository.ExistsWithId(Id))
                return null;
            Invoice invoice = mapper.Map<Invoice>(invoiceDto);
            invoice.Id = Id;
            Invoice updatedInvoice = invoiceRepository.Update(invoice);

            Invoice? found = invoiceRepository.FindById(invoice.Id);

            return mapper.Map<InvoiceDto>(found);
        }
        public override InvoiceDto? Delete(ulong Id)
        {
            if (!invoiceRepository.ExistsWithId(Id)) 
                return null;
            Invoice? invoice = invoiceRepository.FindById(Id);
            InvoiceDto invoiceDto = mapper.Map<InvoiceDto>(invoice);

            invoiceRepository.Delete(Id);

            return invoiceDto;
        }
    }
}