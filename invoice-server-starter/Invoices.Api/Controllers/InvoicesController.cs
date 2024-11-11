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
        public IActionResult GetInvoice(uint invoiceId)
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

        [HttpPut("{invoiceId}")]
        public IActionResult ÉditInvoice(uint invoiceId, [FromBody] InvoiceDto invoice)
        {
            InvoiceDto? updatedInvoice = invoiceManager.UpdateInvoice(invoiceId, invoice);

            if (updatedInvoice is null)
                return NotFound();
            return Ok(updatedInvoice);
        }

        [HttpDelete("{invoiceId}")]
        public IActionResult DeleteInvoice(uint invoiceId)
        {
            InvoiceDto? deletedInvoice = invoiceManager.DeleteInvoice(invoiceId);

            if (deletedInvoice is null)
                return NotFound();
            return Ok(deletedInvoice);
        }
    }
}
