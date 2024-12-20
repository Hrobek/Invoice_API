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

using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories;

/// <summary>
/// Repository for managing Person entities with additional query methods.
/// </summary>
public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    /// <summary>
    /// Initializes a new instance of the PersonRepository class.
    /// </summary>
    /// <param name="invoicesDbContext">The database context.</param>
    public PersonRepository(InvoicesDbContext invoicesDbContext) : base(invoicesDbContext)
    {
    }

    /// <summary>
    /// Retrieves a list of persons filtered by their hidden status.
    /// </summary>
    /// <param name="hidden">The hidden status to filter by.</param>
    /// <returns>A list of persons matching the hidden status.</returns>
    public IList<Person> GetAllByHidden(bool hidden)
    {
        return dbSet
            .Where(p => p.Hidden == hidden) // Filter by the hidden property.
            .ToList(); // Execute the query and return the results.
    }

    /// <summary>
    /// Retrieves statistics for each seller, including their ID, name, and total revenue.
    /// </summary>
    /// <returns>
    /// A list of tuples, where each tuple contains the seller's ID, name, and total revenue.
    /// </returns>
    public async Task<List<(ulong Id, string PersonName, decimal Revenue)>> GetPersonStatisticsAsync()
    {
        return await invoicesDbContext.Invoices
            .Where(i => i.Seller != null) // Only include invoices with a seller.
            .GroupBy(i => new { i.Seller.Id, i.Seller.Name }) // Group by seller ID and name.
            .Select(g => new ValueTuple<ulong, string, decimal>(
                g.Key.Id, // Seller ID.
                g.Key.Name, // Seller name.
                g.Sum(i => i.Price) // Calculate total revenue for the seller.
            ))
            .ToListAsync(); // Execute the query asynchronously and return the results.
    }
}
