using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    // DTO (Data Transfer Object) for representing detailed invoice information in the response without BuyerId and SellerId
    public class InvoiceResponseDto
    {
        // The unique identifier of the invoice
        [JsonPropertyName("_id")]
        public ulong Id { get; set; }

        // The invoice number
        public ulong InvoiceNumber { get; set; }

        // The buyer's details, represented by the PersonDto
        public PersonDto? Buyer { get; set; }

        // The seller's details, represented by the PersonDto
        public PersonDto? Seller { get; set; }

        // The date when the invoice was issued
        public DateTime Issued { get; set; }

        // The date of the transaction invoice 
        public DateTime DueDate { get; set; }

        // The product or service being invoiced
        public string Product { get; set; } = "";

        // The total price of the invoice (excluding VAT)
        public long Price { get; set; }

        // The VAT percentage applied to the invoice
        public int Vat { get; set; }

        // Additional notes related to the invoice
        public string Note { get; set; } = "";
    }
}
