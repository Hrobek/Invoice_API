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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Invoices.Data.Repositories;

/// <summary>
/// A generic repository providing common CRUD operations for any entity implementing IEntity.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// The database context used to interact with the database.
    /// </summary>
    protected readonly InvoicesDbContext invoicesDbContext;

    /// <summary>
    /// The DbSet representing the entity type in the database.
    /// </summary>
    protected readonly DbSet<TEntity> dbSet;

    /// <summary>
    /// Initializes a new instance of the BaseRepository class.
    /// </summary>
    /// <param name="invoicesDbContext">The database context.</param>
    public BaseRepository(InvoicesDbContext invoicesDbContext)
    {
        this.invoicesDbContext = invoicesDbContext;
        dbSet = invoicesDbContext.Set<TEntity>();
    }

    /// <summary>
    /// Finds an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    public virtual TEntity? FindById(ulong id)
    {
        return dbSet.Find(id);
    }

    /// <summary>
    /// Checks if an entity with the given ID exists.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>True if the entity exists; otherwise, false.</returns>
    public bool ExistsWithId(ulong id)
    {
        TEntity? entity = dbSet.Find(id);
        if (entity is not null)
            invoicesDbContext.Entry(entity).State = EntityState.Detached; // Detach the entity to prevent tracking issues.
        return entity is not null;
    }

    /// <summary>
    /// Retrieves all entities from the database.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public virtual IList<TEntity> GetAll()
    {
        return dbSet.ToList();
    }

    /// <summary>
    /// Inserts a new entity into the database.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>The added entity.</returns>
    public TEntity Insert(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = dbSet.Add(entity);
        invoicesDbContext.SaveChanges(); // Save changes to persist the entity.
        return entityEntry.Entity;
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>The updated entity.</returns>
    public TEntity Update(TEntity entity)
    {
        EntityEntry<TEntity> entityEntry = dbSet.Update(entity);
        invoicesDbContext.SaveChanges(); // Save changes to persist the updates.
        return entityEntry.Entity;
    }

    /// <summary>
    /// Deletes an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    public virtual void Delete(ulong id)
    {
        TEntity? entity = dbSet.Find(id);

        if (entity is null)
            return; // Exit if the entity does not exist.

        try
        {
            dbSet.Remove(entity); // Mark the entity for deletion.
            invoicesDbContext.SaveChanges(); // Save changes to delete the entity.
        }
        catch
        {
            invoicesDbContext.Entry(entity).State = EntityState.Unchanged; // Revert changes if an error occurs.
            throw; // Rethrow the exception to handle it further up the stack.
        }
    }
}
