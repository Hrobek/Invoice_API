using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

/// <summary>
/// API controller for managing invoices.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class InvoicesController : Controller
{
    private readonly IInvoiceManager invoiceManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoicesController"/> class.
    /// </summary>
    /// <param name="invoiceManager">The manager responsible for invoice operations.</param>
    public InvoicesController(IInvoiceManager invoiceManager)
    {
        this.invoiceManager = invoiceManager;
    }

    /// <summary>
    /// Retrieves all invoices, optionally filtered by criteria.
    /// </summary>
    /// <param name="invoiceFilter">The filter criteria for invoices.</param>
    /// <returns>A collection of filtered <see cref="InvoiceResponseDto"/> objects.</returns>
    [HttpGet]
    public IEnumerable<InvoiceResponseDto> GetInvoices([FromQuery] InvoiceFilterDto invoiceFilter)
    {
        IEnumerable<InvoiceDto> invoices = invoiceManager.GetAll(invoiceFilter);

        return invoices.Select(invoiceManager.MapToResponseDto);
    }

    /// <summary>
    /// Retrieves a specific invoice by its ID.
    /// </summary>
    /// <param name="Id">The ID of the invoice to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the invoice data if found, or a NotFound result otherwise.</returns>
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

    /// <summary>
    /// Adds a new invoice.
    /// </summary>
    /// <param name="invoiceDto">The invoice data to add.</param>
    /// <returns>A 201 Created response containing the created invoice data.</returns>
    [HttpPost]
    public IActionResult AddInvoice([FromBody] InvoiceDto invoiceDto)
    {
        InvoiceDto? createdInvoice = invoiceManager.Add(invoiceDto);

        return StatusCode(StatusCodes.Status201Created, invoiceManager.MapToResponseDto(createdInvoice));
    }

    /// <summary>
    /// Updates an existing invoice.
    /// </summary>
    /// <param name="Id">The ID of the invoice to update.</param>
    /// <param name="invoice">The updated invoice data.</param>
    /// <returns>
    /// An Ok response containing the updated invoice data if successful,
    /// or a NotFound response if the invoice does not exist.
    /// </returns>
    [HttpPut("{Id}")]
    public IActionResult UpdateInvoice(ulong Id, [FromBody] InvoiceDto invoice)
    {
        InvoiceDto? updatedInvoice = invoiceManager.Update(Id, invoice);

        if (updatedInvoice is null)
            return NotFound();
        return Ok(invoiceManager.MapToResponseDto(updatedInvoice));
    }

    /// <summary>
    /// Deletes an invoice by its ID.
    /// </summary>
    /// <param name="Id">The ID of the invoice to delete.</param>
    /// <returns>A NoContent response if the invoice is successfully deleted, or NotFound if it does not exist.</returns>
    [HttpDelete("{Id}")]
    public IActionResult DeleteInvoice(ulong Id)
    {
        InvoiceDto? deletedInvoice = invoiceManager.Delete(Id);

        if (deletedInvoice is null)
            return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Retrieves statistics about invoices.
    /// </summary>
    /// <returns>An <see cref="ActionResult"/> containing an <see cref="InvoiceStatisticDto"/> with statistics data.</returns>
    [HttpGet("statistics")]
    public async Task<ActionResult<InvoiceStatisticDto>> GetInvoiceStatistics()
    {
        var statistics = await invoiceManager.GetInvoiceStatistics();

        return Ok(statistics);
    }
}
