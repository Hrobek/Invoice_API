using Invoices.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class InvoiceDto
    {
        [JsonPropertyName("_id")]
        public uint InvoiceId { get; set; }
        public ulong InvoiceNumber { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; } = "";
        public long Price { get; set; }
        public int Vat { get; set; }
        public string Note { get; set; } = "";

        //[JsonPropertyName("buyerId")]
        //public uint BuyerId { get; set; }

        //[JsonPropertyName("sellerId")]
        //public uint SellerId { get; set; }
        public virtual PersonDto? Buyer { get; set; }
        public virtual PersonDto? Seller { get; set; }
    }
}
