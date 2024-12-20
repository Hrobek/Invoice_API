using Invoices.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

/// <summary>
/// Controller for managing invoice operations based on identification numbers.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class IdentificationController : Controller
{
    private readonly IInvoiceManager invoiceManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentificationController"/> class.
    /// </summary>
    /// <param name="invoiceManager">The manager responsible for invoice operations.</param>
    public IdentificationController(IInvoiceManager invoiceManager)
    {
        this.invoiceManager = invoiceManager;
    }

    /// <summary>
    /// Retrieves all sales (invoices where the given identification number is the seller).
    /// </summary>
    /// <param name="identificationNumber">The seller's identification number.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the sales invoices if found,
    /// or a NotFound response if no invoices match the criteria.
    /// </returns>
    [HttpGet("{identificationNumber}/sales")]
    public IActionResult GetByInSeller(string identificationNumber)
    {
        var invoices = invoiceManager.GetByIdentificationNumber(identificationNumber, true);

        if (invoices == null || !invoices.Any())
        {
            return NotFound();
        }

        // Map to response DTO
        var response = invoices.Select(invoiceManager.MapToResponseDto);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves all purchases (invoices where the given identification number is the buyer).
    /// </summary>
    /// <param name="identificationNumber">The buyer's identification number.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the purchase invoices if found,
    /// or a NotFound response if no invoices match the criteria.
    /// </returns>
    [HttpGet("{identificationNumber}/purchases")]
    public IActionResult GetByInBuyer(string identificationNumber)
    {
        var invoices = invoiceManager.GetByIdentificationNumber(identificationNumber, false);

        if (invoices == null || !invoices.Any())
        {
            return NotFound();
        }

        // Map to response DTO
        var response = invoices.Select(invoiceManager.MapToResponseDto);
        return Ok(response);
    }
}
