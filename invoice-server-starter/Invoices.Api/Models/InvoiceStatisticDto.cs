namespace Invoices.Api.Models
{
    // DTO (Data Transfer Object) for representing invoice statistics
    public class InvoiceStatisticDto
    {
        // The total sum of invoices for the current year
        public long CurrentYearSum { get; set; }

        // The total sum of all invoices for all time
        public long AllTimeSum { get; set; }

        // The total count of invoices
        public int InvoicesCount { get; set; }
    }
}
