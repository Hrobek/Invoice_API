using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manages invoice-related operations, extending the BaseManager with custom logic for the `Invoice` entity.
    /// </summary>
    public class InvoiceManager : BaseManager<InvoiceDto, Invoice>, IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository; // Repository for accessing invoice data.
        private readonly IMapper mapper; // Mapper for converting between entities and DTOs.

        /// <summary>
        /// Initializes a new instance of the InvoiceManager class.
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository instance.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves all invoices, optionally applying filters.
        /// </summary>
        /// <param name="invoiceFilterDto">Optional filters for fetching invoices.</param>
        /// <returns>A list of invoice DTOs.</returns>
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

            return mapper.Map<IList<InvoiceDto>>(invoices); // Map the filtered results to DTOs.
        }

        /// <summary>
        /// Retrieves a single invoice by its ID.
        /// </summary>
        /// <param name="Id">The ID of the invoice to retrieve.</param>
        /// <returns>The invoice DTO or null if not found.</returns>
        public override InvoiceDto? Get(ulong Id)
        {
            Invoice? invoice = invoiceRepository.FindById(Id); // Find the invoice by ID.

            if (invoice is null)
            {
                return null; // Return null if the invoice is not found.
            }

            return mapper.Map<InvoiceDto>(invoice); // Map the invoice to a DTO and return it.
        }

        /// <summary>
        /// Retrieves invoices filtered by a person's identification number.
        /// </summary>
        /// <param name="identificationNumber">The identification number of the buyer or seller.</param>
        /// <param name="isSeller">True to filter by sellers, false to filter by buyers.</param>
        /// <returns>A list of invoice DTOs.</returns>
        public List<InvoiceDto> GetByIdentificationNumber(string identificationNumber, bool isSeller)
        {
            var invoices = invoiceRepository
                .GetAll() // Fetch all invoices.
                .Where(i => isSeller ?
                    i.Seller?.IdentificationNumber == identificationNumber :
                    i.Buyer?.IdentificationNumber == identificationNumber)
                .ToList();

            if (!invoices.Any())
            {
                return new List<InvoiceDto>(); // Return an empty list if no invoices match the filter.
            }

            return invoices.Select(invoice => mapper.Map<InvoiceDto>(invoice)).ToList(); // Map and return the filtered invoices.
        }

        /// <summary>
        /// Adds a new invoice.
        /// </summary>
        /// <param name="invoiceDto">The DTO containing invoice details.</param>
        /// <returns>The added invoice DTO.</returns>
        public override InvoiceDto Add(InvoiceDto invoiceDto)
        {
            Invoice invoice = mapper.Map<Invoice>(invoiceDto); // Map DTO to entity.
            invoice.Id = default; // Reset the ID to ensure a new record is created.
            Invoice addedInvoice = invoiceRepository.Insert(invoice); // Insert the invoice into the repository.

            Invoice? found = invoiceRepository.FindById(addedInvoice.Id); // Retrieve the newly added invoice.

            return mapper.Map<InvoiceDto>(found); // Map the added entity back to a DTO and return it.
        }

        /// <summary>
        /// Updates an existing invoice by its ID.
        /// </summary>
        /// <param name="Id">The ID of the invoice to update.</param>
        /// <param name="invoiceDto">The updated invoice details.</param>
        /// <returns>The updated invoice DTO or null if the invoice does not exist.</returns>
        public override InvoiceDto? Update(ulong Id, InvoiceDto invoiceDto)
        {
            if (!invoiceRepository.ExistsWithId(Id)) // Check if the invoice exists.
                return null;

            Invoice invoice = mapper.Map<Invoice>(invoiceDto); // Map DTO to entity.
            invoice.Id = Id; // Set the ID for the update.
            Invoice updatedInvoice = invoiceRepository.Update(invoice); // Update the invoice in the repository.

            Invoice? found = invoiceRepository.FindById(invoice.Id); // Retrieve the updated invoice.

            return mapper.Map<InvoiceDto>(found); // Map the updated entity back to a DTO and return it.
        }

        /// <summary>
        /// Deletes an invoice by its ID.
        /// </summary>
        /// <param name="Id">The ID of the invoice to delete.</param>
        /// <returns>The deleted invoice DTO or null if not found.</returns>
        public override InvoiceDto? Delete(ulong Id)
        {
            if (!invoiceRepository.ExistsWithId(Id)) // Check if the invoice exists.
                return null;

            Invoice? invoice = invoiceRepository.FindById(Id); // Find the invoice by ID.
            InvoiceDto invoiceDto = mapper.Map<InvoiceDto>(invoice); // Map the entity to a DTO.

            invoiceRepository.Delete(Id); // Delete the invoice from the repository.

            return invoiceDto; // Return the deleted invoice DTO.
        }

        /// <summary>
        /// Retrieves statistical data for invoices.
        /// </summary>
        /// <returns>An InvoiceStatisticDto containing statistical data.</returns>
        public async Task<InvoiceStatisticDto> GetInvoiceStatistics()
        {
            int currentYear = DateTime.Now.Year;

            // Fetch statistical data from the repository.
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

        /// <summary>
        /// Maps an InvoiceDto to an InvoiceResponseDto.
        /// </summary>
        /// <param name="invoice">The invoice DTO to map.</param>
        /// <returns>The mapped InvoiceResponseDto.</returns>
        public InvoiceResponseDto MapToResponseDto(InvoiceDto invoice)
        {
            return new InvoiceResponseDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Buyer = invoice.Buyer,
                Seller = invoice.Seller,
                Issued = invoice.Issued,
                DueDate = invoice.DueDate,
                Product = invoice.Product,
                Price = invoice.Price,
                Vat = invoice.Vat,
                Note = invoice.Note
            };
        }
    }
}
