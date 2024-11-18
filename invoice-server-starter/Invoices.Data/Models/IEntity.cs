using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Models
{
    public interface IEntity
    {
       ulong Id { get; set; }
    }
}
