using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Api.Models;
using Invoices.Data;
using Invoices.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<InvoiceDto> GetInvoices([FromQuery] InvoiceFilterDto invoiceFilter)
        {
            return invoiceManager.GetAll(invoiceFilter);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(ulong Id)
        {
            InvoiceDto? invoice = invoiceManager.Get(Id);

            if (invoice is null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] InvoiceDto invoiceDto)
        {
            InvoiceDto? createdInvoice = invoiceManager.Add(invoiceDto);

            return StatusCode(StatusCodes.Status201Created, createdInvoice);

        }

        [HttpPut("{Id}")]
        public IActionResult EditInvoice(ulong Id, [FromBody] InvoiceDto invoice)
        {
            InvoiceDto? updatedInvoice = invoiceManager.Update(Id, invoice);

            if (updatedInvoice is null)
                return NotFound();
            return Ok(updatedInvoice);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteInvoice(ulong Id)
        {
            InvoiceDto? deletedInvoice = invoiceManager.Delete(Id);

            if (deletedInvoice is null)
                return NotFound();
            return NoContent();
        }
    }
}
