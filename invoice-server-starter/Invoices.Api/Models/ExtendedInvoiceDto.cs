using Invoices.Data.Models;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class ExtendedInvoiceDto : InvoiceDto
    {
        [JsonPropertyName("buyer")]
        public PersonDto Buyer { get; set; } = new PersonDto();

        [JsonPropertyName("seller")]
        public PersonDto Seller { get; set; } = new PersonDto();
    }
}
