using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Http;
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
            var invoice = invoiceManager.GetByIdentificationNumber(identificationNumber,true);

            if (invoice is null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpGet("{identificationNumber}/purchases")]
        public IActionResult GetByInBuyer(string identificationNumber)
        {
            var invoice = invoiceManager.GetByIdentificationNumber(identificationNumber, false);

            if (invoice is null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }
    }
}
