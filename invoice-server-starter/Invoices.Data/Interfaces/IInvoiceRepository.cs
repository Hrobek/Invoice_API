using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Interfaces
{
    public interface IInvoiceRepository :IBaseRepository<Invoice>
    {
        IList<Invoice> GetAll(
             uint? sellerId = null,
             uint? buyerId = null,
             string? product = null,
             decimal? minPrice = null,
             decimal? maxPrice = null,
             int? limit = null);

    }
}
