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

using Invoices.Api.Models;
using Invoices.Data.Models;

namespace Invoices.Api.Interfaces;

/// <summary>
/// Defines operations for managing persons and their related DTOs.
/// </summary>
public interface IPersonManager : IBaseManager<PersonDto, Person>
{
    /// <summary>
    /// Asynchronously retrieves statistics for persons, such as their revenue, name and ID.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="PersonStatisticsDto"/>.</returns>
    Task<List<PersonStatisticsDto>> GetPersonStatisticsAsync();

    /// <summary>
    /// Updates an existing person with the specified ID and new data.
    /// </summary>
    /// <param name="Id">The ID of the person to update.</param>
    /// <param name="updatedPersonDto">The new data for the person.</param>
    /// <returns>A <see cref="PersonDto"/> representing the updated person, or null if the person does not exist.</returns>
    PersonDto? Update(ulong Id, PersonDto updatedPersonDto);
}
