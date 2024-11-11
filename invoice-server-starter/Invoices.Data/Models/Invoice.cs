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
        public uint InvoiceId { get; set; }

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
        [ForeignKey(nameof(Buyer))]
        public uint? BuyerId { get; set; }
        public virtual Person? Buyer { get; set; }
        [ForeignKey(nameof(Seller))]
        public uint? SellerId { get; set; }
        public virtual Person? Seller { get; set; }

    }
}
