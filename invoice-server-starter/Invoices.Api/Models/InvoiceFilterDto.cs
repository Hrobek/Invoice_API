namespace Invoices.Api.Models
{
    public class InvoiceFilterDto
    {
        public uint? sellerId {  get; set; }
        public uint? buyerId { get; set; }
        public string? product { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public  int? limit { get; set; }
    }
}
