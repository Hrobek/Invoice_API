/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Controllers;

/// <summary>
/// API controller for managing persons.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonManager personManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonsController"/> class.
    /// </summary>
    /// <param name="personManager">The manager responsible for person operations.</param>
    public PersonsController(IPersonManager personManager)
    {
        this.personManager = personManager;
    }

    /// <summary>
    /// Retrieves all persons.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="PersonDto"/> objects.</returns>
    [HttpGet]
    public IEnumerable<PersonDto> GetPersons()
    {
        return personManager.GetAll();
    }

    /// <summary>
    /// Retrieves a specific person by their ID.
    /// </summary>
    /// <param name="Id">The ID of the person to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the person data if found, or a NotFound result otherwise.</returns>
    [HttpGet("{Id}")]
    public IActionResult GetPerson(ulong Id)
    {
        PersonDto? person = personManager.Get(Id);

        if (person is null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    /// <summary>
    /// Adds a new person.
    /// </summary>
    /// <param name="person">The person data to add.</param>
    /// <returns>A 201 Created response containing the created person data.</returns>
    [HttpPost]
    public IActionResult AddPerson([FromBody] PersonDto person)
    {
        PersonDto? createdPerson = personManager.Add(person);
        return StatusCode(StatusCodes.Status201Created, createdPerson);
    }

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="Id">The ID of the person to update.</param>
    /// <param name="updatedPerson">The updated person data.</param>
    /// <returns>A 201 Created response containing the updated person data.</returns>
    [HttpPut("{Id}")]
    public IActionResult UpdatePerson(ulong Id, [FromBody] PersonDto updatedPerson)
    {
        PersonDto? updatedPersons = personManager.Update(Id, updatedPerson);
        return StatusCode(StatusCodes.Status201Created, updatedPerson);
    }

    /// <summary>
    /// Deletes a person by their ID.
    /// </summary>
    /// <param name="Id">The ID of the person to delete.</param>
    /// <returns>A 204 No Content response.</returns>
    [HttpDelete("{Id}")]
    public IActionResult DeletePerson(ulong Id)
    {
        personManager.Delete(Id);
        return NoContent();
    }

    /// <summary>
    /// Retrieves person statistics asynchronously.
    /// </summary>
    /// <returns>An <see cref="ActionResult"/> containing a list of <see cref="PersonStatisticsDto"/>.</returns>
    [HttpGet("statistics")]
    public async Task<ActionResult<List<PersonStatisticsDto>>> GetPersonStatistics()
    {
        var statistics = await personManager.GetPersonStatisticsAsync();
        return Ok(statistics);
    }
}
