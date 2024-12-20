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

using Invoices.Data.Models;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Repository interface for managing Person entities, extending the base repository functionality.
    /// </summary>
    public interface IPersonRepository : IBaseRepository<Person>
    {
        /// <summary>
        /// Retrieves a list of persons filtered by the "hidden" status.
        /// </summary>
        /// <param name="hidden">Filter by hidden status. Only persons with the specified hidden status will be returned.</param>
        /// <returns>A list of persons matching the specified hidden status.</returns>
        IList<Person> GetAllByHidden(bool hidden);

        /// <summary>
        /// Retrieves statistics for persons, including their ID, name, and total revenue from sales.
        /// </summary>
        /// <returns>A list of tuples where each tuple contains the person's ID, name, and total revenue.</returns>
        Task<List<(ulong Id, string PersonName, decimal Revenue)>> GetPersonStatisticsAsync();
    }
}
