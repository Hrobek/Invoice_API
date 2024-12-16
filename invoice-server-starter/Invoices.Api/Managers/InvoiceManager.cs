using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

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
        public IList<InvoiceDto> GetAll(InvoiceFilterDto? invoiceFilterDto = null)
        {
            IList<Invoice> invoices = invoiceFilterDto is null ?
               invoiceRepository.GetAll() :
               invoiceRepository.GetAll(
                   invoiceFilterDto.sellerId,
                   invoiceFilterDto.buyerId,
                   invoiceFilterDto.product,
                   invoiceFilterDto.minPrice ?? int.MinValue,
                    invoiceFilterDto.maxPrice ?? int.MaxValue,
                    invoiceFilterDto.limit ?? int.MaxValue
                   );

            return mapper.Map<IList<InvoiceDto>>(invoices);
        }
        public override InvoiceDto? Get(ulong Id)
        {
            Invoice? invoice = invoiceRepository.FindById(Id);

            if (invoice is null)
            {
                return null;
            }

            return mapper.Map<InvoiceDto>(invoice);
        }
        public List<InvoiceDto> GetByIdentificationNumber(string identificationNumber, bool isSeller)
        {
            var invoices = invoiceRepository
                .GetAll() 
                .Where(i => isSeller ? i.Seller?.IdentificationNumber == identificationNumber : i.Buyer?.IdentificationNumber == identificationNumber)
                .ToList();

            if (!invoices.Any())
            {
                return new List<InvoiceDto>();
            }

            return invoices.Select(invoice => mapper.Map<InvoiceDto>(invoice)).ToList();
        }
        public override InvoiceDto Add(InvoiceDto invoiceDto)
        {
            Invoice invoice = mapper.Map<Invoice>(invoiceDto);
            invoice.Id = default;
            Invoice addedInvoice = invoiceRepository.Insert(invoice);

            Invoice? found = invoiceRepository.FindById(addedInvoice.Id);

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

        public async Task<InvoiceStatisticDto> GetInvoiceStatistics()
        {
            int currentYear = DateTime.Now.Year;

            var currentYearSum = await invoiceRepository.GetCurrentYearSumAsync(currentYear);
            var allTimeSum = await invoiceRepository.GetAllTimeSumAsync();
            var invoicesCount = await invoiceRepository.GetInvoicesCountAsync();

            return new InvoiceStatisticDto
            {
                CurrentYearSum = currentYearSum,
                AllTimeSum = allTimeSum,
                InvoicesCount = invoicesCount
            };
        }

        public InvoiceResponseDto MapToResponseDto(InvoiceDto invoice)
        {
            return new InvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Buyer = invoice.Buyer,
                Seller = invoice.Seller,
                Issued = invoice.Issued,
                Date = invoice.Date,
                Product = invoice.Product,
                Price = invoice.Price,
                Vat = invoice.Vat,
                Note = invoice.Note
            };
        }
    }
}