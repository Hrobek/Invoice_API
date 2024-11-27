using Invoices.Data.Models;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class InvoiceResponseDto
    {
        [JsonPropertyName("_id")]
        public ulong Id { get; set; }
        public ulong InvoiceNumber { get; set; }
        public PersonDto? Buyer { get; set; }
        public PersonDto? Seller { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; } = "";
        public long Price { get; set; }
        public int Vat { get; set; }
        public string Note { get; set; } = "";
    }
}
