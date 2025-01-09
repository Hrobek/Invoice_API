using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    // Data Transfer Object (DTO) for transferring invoice data between layers
    public class InvoiceDto
    {
        // The unique identifier for the invoice, mapped to "_id" in JSON
        [JsonPropertyName("_id")]
        public ulong Id { get; set; }

        // The invoice number associated with the invoice
        public ulong InvoiceNumber { get; set; }

        // The buyer information of the invoice (null if not provided)
        public PersonDto? Buyer { get; set; }

        // The unique identifier of the buyer associated with the invoice (nullable)
        public ulong? BuyerId { get; set; }

        // The seller information of the invoice (null if not provided)
        public PersonDto? Seller { get; set; }

        // The unique identifier of the seller associated with the invoice (nullable)
        public ulong? SellerId { get; set; }

        // The date when the invoice was issued
        public DateTime Issued { get; set; }

        // The date when the transaction (purchase) occurred
        public DateTime DueDate { get; set; }

        // The name or description of the product associated with the invoice
        public string Product { get; set; } = "";

        // The price of the product or service on the invoice
        public long Price { get; set; }

        // The VAT (Value-Added Tax) rate applied to the invoice
        public int Vat { get; set; }

        // Any additional notes related to the invoice
        public string Note { get; set; } = "";
    }
}
