namespace Invoices.Api.Models
{
    public class PersonStatisticsDto
    {
        public ulong PersonId { get; set; }
        public string PersonName { get; set; } = "";
        public decimal Revenue { get; set; }
    }
}
