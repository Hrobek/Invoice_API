using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Invoices.Data.Models
{
    /// <summary>
    /// Represents an invoice entity in the invoices database.
    /// </summary>
    public class Invoice : IEntity
    {
        /// <summary>
        /// The unique identifier for the invoice.
        /// This is an auto-generated primary key.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        /// <summary>
        /// The unique invoice number.
        /// </summary>
        [Required]
        public ulong InvoiceNumber { get; set; }

        /// <summary>
        /// The date the invoice was issued.
        /// </summary>
        [Required]
        public DateTime Issued { get; set; }

        /// <summary>
        /// The date of the invoice transaction.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Name of the product or service associated with the invoice.
        /// </summary>
        [Required]
        public string Product { get; set; } = "";

        /// <summary>
        /// The total price of the product or service (excluding VAT).
        /// </summary>
        [Required]
        public long Price { get; set; }

        /// <summary>
        /// The Value Added Tax (VAT) percentage applied to the price.
        /// </summary>
        [Required]
        public int Vat { get; set; }

        /// <summary>
        /// Additional notes or comments related to the invoice.
        /// This field is optional.
        /// </summary>
        public string Note { get; set; } = "";

        /// <summary>
        /// The foreign key referencing the buyer of the invoice.
        /// </summary>
        [ForeignKey(nameof(Buyer))]
        public ulong? BuyerId { get; set; }

        /// <summary>
        /// Navigation property for the buyer (a person associated with the invoice as the purchaser).
        /// </summary>
        public virtual Person? Buyer { get; set; }

        /// <summary>
        /// The foreign key referencing the seller of the invoice.
        /// </summary>
        [ForeignKey(nameof(Seller))]
        public ulong? SellerId { get; set; }

        /// <summary>
        /// Navigation property for the seller (a person associated with the invoice as the vendor).
        /// </summary>
        public virtual Person? Seller { get; set; }
    }
}
