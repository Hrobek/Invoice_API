namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Defines the base operations for managing entities and their corresponding DTOs.
    /// </summary>
    /// <typeparam name="TDto">The type of the Data Transfer Object (DTO).</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IBaseManager<TDto, TEntity> where TDto : class where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities as a list of DTOs.
        /// </summary>
        /// <returns>A list of DTOs representing all entities.</returns>
        IList<TDto> GetAll();

        /// <summary>
        /// Retrieves a single entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The DTO of the entity if found; otherwise, null.</returns>
        TDto? Get(ulong id);

        /// <summary>
        /// Adds a new entity based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing the details of the entity to be added.</param>
        /// <returns>The DTO of the newly added entity.</returns>
        TDto Add(TDto dto);

        /// <summary>
        /// Updates an existing entity with the given DTO.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="dto">The DTO containing the updated details of the entity.</param>
        /// <returns>The DTO of the updated entity if the update is successful; otherwise, null.</returns>
        TDto? Update(ulong id, TDto dto);

        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>The DTO of the deleted entity if found; otherwise, null.</returns>
        TDto? Delete(ulong id);
    }
}
