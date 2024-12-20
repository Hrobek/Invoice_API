namespace Invoices.Api.Models
{
    // DTO (Data Transfer Object) for filtering invoices based on specific criteria
    public class InvoiceFilterDto
    {
        // Optional: The ID of the seller to filter invoices by
        public ulong? sellerId { get; set; }

        // Optional: The ID of the buyer to filter invoices by
        public ulong? buyerId { get; set; }

        // Optional: The product description to filter invoices by
        public string? product { get; set; }

        // Optional: Minimum price of the invoice to filter by
        public decimal? minPrice { get; set; }

        // Optional: Maximum price of the invoice to filter by
        public decimal? maxPrice { get; set; }

        // Optional: Limit the number of invoices returned by the filter
        public int? limit { get; set; }
    }
}
