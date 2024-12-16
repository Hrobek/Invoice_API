using Invoices.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentificationController : Controller
    {
        private readonly IInvoiceManager invoiceManager;

        public IdentificationController(IInvoiceManager invoiceManager)
        {
            this.invoiceManager = invoiceManager;
        }
        [HttpGet("{identificationNumber}/sales")]
        public IActionResult GetByInSeller(string identificationNumber)
        {
            var invoices = invoiceManager.GetByIdentificationNumber(identificationNumber, true);

            if (invoices == null || !invoices.Any())
            {
                return NotFound();
            }

            // Mapování na response DTO
            var response = invoices.Select(invoiceManager.MapToResponseDto);
            return Ok(response);
        }

        [HttpGet("{identificationNumber}/purchases")]
        public IActionResult GetByInBuyer(string identificationNumber)
        {
            var invoices = invoiceManager.GetByIdentificationNumber(identificationNumber, false);

            if (invoices == null || !invoices.Any())
            {
                return NotFound();
            }

            // Mapování na response DTO
            var response = invoices.Select(invoiceManager.MapToResponseDto);
            return Ok(response);
        }
    }
}
