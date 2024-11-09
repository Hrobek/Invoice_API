using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Models
{
    public class Invoice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong InvoiceId { get; set; }

        [Required]
        public ulong InvoiceNumber { get; set; }

        [Required]
        public DateTime Issued { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Product { get; set; } = "";

        [Required]
        public long Price { get; set; }

        [Required]
        public int Vat { get; set; }
 
        public string Note { get; set; } = "";

        [Required]
        [ForeignKey(nameof(Buyer))]
        public ulong BuyerId { get; set; }
        public required Person Buyer { get; set; }

        [Required]
        [ForeignKey(nameof(Seller))]
        public ulong SellerId { get; set; }
        public required Person Seller { get; set; }

    }
}
