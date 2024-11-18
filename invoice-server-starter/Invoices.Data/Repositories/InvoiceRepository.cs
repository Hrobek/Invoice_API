using Invoices.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Invoices.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(InvoicesDbContext invoicesDbContext) : base(invoicesDbContext)
        {

        }
        public override IList<Invoice> GetAll()
        {
            return dbSet
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .ToList();
        }

        public override Invoice? FindById(ulong id)
        {
            return dbSet
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .FirstOrDefault(i => i.Id == id);
        }
    }
}
