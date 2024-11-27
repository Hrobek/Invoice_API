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
        public IEnumerable<InvoiceResponseDto> GetInvoices([FromQuery] InvoiceFilterDto invoiceFilter)
        {
            IEnumerable<InvoiceDto> invoices = invoiceManager.GetAll(invoiceFilter);

            return invoices.Select(invoiceManager.MapToResponseDto);
        }

        [HttpGet("{Id}")]
        public IActionResult Get(ulong Id)
        {
            InvoiceDto? invoice = invoiceManager.Get(Id);

            if (invoice is null)
            {
                return NotFound();
            }
            return Ok(invoiceManager.MapToResponseDto(invoice));
        }

        [HttpPost]
        public IActionResult AddInvoice([FromBody] InvoiceDto invoiceDto)
        {
            InvoiceDto? createdInvoice = invoiceManager.Add(invoiceDto);

            return StatusCode(StatusCodes.Status201Created, invoiceManager.MapToResponseDto(createdInvoice));

        }

        [HttpPut("{Id}")]
        public IActionResult UpdateInvoice(ulong Id, [FromBody] InvoiceDto invoice)
        {
            InvoiceDto? updatedInvoice = invoiceManager.Update(Id, invoice);

            if (updatedInvoice is null)
                return NotFound();
            return Ok(invoiceManager.MapToResponseDto(updatedInvoice));
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteInvoice(ulong Id)
        {
            InvoiceDto? deletedInvoice = invoiceManager.Delete(Id);

            if (deletedInvoice is null)
                return NotFound();
            return NoContent();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<InvoiceStatisticDto>> GetInvoiceStatistics()
        {
            var statistics = await invoiceManager.GetInvoiceStatistics();

            return Ok(statistics);
        }

       
    }
}
