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

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonManager personManager;


    public PersonsController(IPersonManager personManager)
    {
        this.personManager = personManager;
    }


    [HttpGet]
    public IEnumerable<PersonDto> GetPersons()
    {
        return personManager.GetAll();
    }

    // api/persons/1
    [HttpGet("{Id}")]
    public IActionResult GetPerson(uint Id)
    {
        PersonDto? person = personManager.Get(Id);

        if (person is null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpPost]
    public IActionResult AddPerson([FromBody] PersonDto person)
    {
        PersonDto? createdPerson = personManager.Add(person);
        return StatusCode(StatusCodes.Status201Created, createdPerson);
    }
    [HttpPut("{Id}")]
    public IActionResult UpdatePerson(uint Id, [FromBody] PersonDto updatedPerson)
    {
        PersonDto? updatedPersons = personManager.Update(Id, updatedPerson);
        return StatusCode(StatusCodes.Status201Created, updatedPerson);
    }

    [HttpDelete("{Id}")]
    public IActionResult DeletePerson(uint Id)
    {
        personManager.Delete(Id);
        return NoContent();
    }
}