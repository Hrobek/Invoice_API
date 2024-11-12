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
            return invoiceManager.GetAll();
        }

        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoice(uint invoiceId)
        {
            InvoiceDto? invoice = invoiceManager.Get(invoiceId);

            if (invoice is null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public IActionResult AddInvoice([FromBody] InvoiceDto invoice)
        {
            InvoiceDto? createdInvoice = invoiceManager.Add(invoice);
            return StatusCode(StatusCodes.Status201Created, createdInvoice);
        }

        [HttpPut("{invoiceId}")]
        public IActionResult EditInvoice(uint invoiceId, [FromBody] InvoiceDto invoice)
        {
            InvoiceDto? updatedInvoice = invoiceManager.Update(invoiceId, invoice);

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
