using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceManager invoiceManager;

        public InvoicesController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }

        [HttpGet]
        public IEnumerable<InvoiceDto> GetInvoices()
        {
            return invoiceManager.GetAllInvoices();
        }

        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoice(ulong invoiceId)
        {
            InvoiceDto? invoice = invoiceManager.GetInvoice(invoiceId);

            if (invoice is null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public IActionResult AddInvoice([FromBody] InvoiceDto invoice)
        {
            InvoiceDto? createdInvoice = invoiceManager.AddInvoice(invoice);
            return StatusCode(StatusCodes.Status201Created, createdInvoice);
        }
    }
}
