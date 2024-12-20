namespace Invoices.Api.Models
{
    // DTO (Data Transfer Object) for representing statistics about a person
    public class PersonStatisticsDto
    {
        // Unique identifier for the person
        public ulong PersonId { get; set; }

        // The name of the person
        public string PersonName { get; set; } = "";

        // The total revenue for the person, calculated from their associated invoices
        public decimal Revenue { get; set; }
    }
}
