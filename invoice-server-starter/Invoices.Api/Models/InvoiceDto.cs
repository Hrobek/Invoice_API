﻿using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    public class InvoiceDto
    {
        [JsonPropertyName("_id")]
        public ulong Id { get; set; }
        public ulong InvoiceNumber { get; set; }
        public PersonDto? Buyer { get; set; }
        public ulong ? BuyerId { get; set; }
        public PersonDto? Seller { get; set; }
        public ulong ? SellerId { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; } = "";
        public long Price { get; set; }
        public int Vat { get; set; }
        public string Note { get; set; } = "";
        
    }
}
