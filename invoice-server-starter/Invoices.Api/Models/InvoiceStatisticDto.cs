namespace Invoices.Api.Models
{
    public class InvoiceStatisticDto
    {
        public long CurrentYearSum { get; set; }
        public long AllTimeSum { get; set; }
        public int InvoicesCount { get; set; }
    }
}
