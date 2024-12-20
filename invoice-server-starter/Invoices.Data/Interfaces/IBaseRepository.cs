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

namespace Invoices.Data.Interfaces;

/// <summary>
/// Base repository interface defining standard data access operations for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity, which must implement the IEntity interface.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    IList<TEntity> GetAll();

    /// <summary>
    /// Inserts a new entity into the repository.
    /// </summary>
    /// <param name="entity">The entity to be inserted.</param>
    /// <returns>The inserted entity.</returns>
    TEntity Insert(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>The updated entity.</returns>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Deletes an entity with the specified ID from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    void Delete(ulong id);

    /// <summary>
    /// Checks if an entity with the specified ID exists in the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns><c>true</c> if the entity exists; otherwise, <c>false</c>.</returns>
    bool ExistsWithId(ulong id);

    /// <summary>
    /// Finds an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to find.</param>
    /// <returns>
    /// The entity if found; otherwise, <c>null</c>.
    /// </returns>
    TEntity? FindById(ulong id);
}
