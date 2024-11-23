namespace Invoices.Api.Models
{
    public class InvoiceFilterDto
    {
        public ulong? sellerId {  get; set; }
        public ulong? buyerId { get; set; }
        public string? product { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public  int? limit { get; set; }
    }
}
