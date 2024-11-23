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
        public IList<Invoice> GetAll(
            ulong? sellerId = null,
            ulong? buyerId = null,
            string? product = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? limit = null)
        {
            IQueryable<Invoice> query = dbSet
                .Include(i => i.Seller)
                .Include(i => i.Buyer);

            if (sellerId is not null)
                query = query.Where(i => i.SellerId == sellerId);

            if (buyerId is not null)
                query = query.Where(i => i.BuyerId == buyerId);
            if (product is not null)
                query = query.Where(i => i.Product == product);

            if (minPrice is not null)
                query = query.Where(i => i.Price >= minPrice.Value);

            if (maxPrice is not null)
                query = query.Where(i => i.Price <= maxPrice.Value);

            if (limit is not null && limit >= 0)
                query = query.Take(limit.Value);

            return query.ToList();
        }

        public override Invoice? FindById(ulong id)
        {
            return dbSet
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .FirstOrDefault(i => i.Id == id);
        }

        public async Task<long> GetCurrentYearSumAsync(int year)
        {
            return await invoicesDbContext.Invoices
                .Where(i => i.Date.Year == year)
                .SumAsync(i => i.Price);
        }

        public async Task<long> GetAllTimeSumAsync()
        {
            return await invoicesDbContext.Invoices
                .SumAsync(i => i.Price);
        }

        public async Task<int> GetInvoicesCountAsync()
        {
            return await invoicesDbContext.Invoices
                .CountAsync();
        }
    }
}
