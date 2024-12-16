using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Invoices.Data.Models
{
    public class Invoice : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

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
        public ulong? BuyerId { get; set; }
        public virtual Person? Buyer { get; set; }
        [ForeignKey(nameof(Seller))]
        public ulong? SellerId { get; set; }
        public virtual Person? Seller { get; set; }



    }
}
