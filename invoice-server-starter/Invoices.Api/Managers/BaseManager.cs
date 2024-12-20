using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// A generic base manager class to handle common CRUD operations.
    /// </summary>
    /// <typeparam name="TDto">The Data Transfer Object type.</typeparam>
    /// <typeparam name="TEntity">The entity type representing the database model.</typeparam>
    public class BaseManager<TDto, TEntity> : IBaseManager<TDto, TEntity>
        where TDto : class
        where TEntity : class, IEntity
    {
        // Repository for data access operations
        private readonly IBaseRepository<TEntity> repository;

        // AutoMapper instance for mapping between DTOs and entities
        private readonly IMapper mapper;

        /// <summary>
        /// Retrieves all entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of mapped DTOs.</returns>
        public virtual IList<TDto> GetAll()
        {
            var entities = repository.GetAll();
            return mapper.Map<IList<TDto>>(entities);
        }

        /// <summary>
        /// Retrieves a single entity by its ID and maps it to a DTO.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The mapped DTO or null if the entity is not found.</returns>
        public virtual TDto? Get(ulong id)
        {
            var entity = repository.FindById(id);
            if (entity == null) return null;

            return mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// Adds a new entity using the provided DTO and returns the newly created DTO.
        /// </summary>
        /// <param name="dto">The DTO containing data for the new entity.</param>
        /// <returns>The DTO representing the newly added entity.</returns>
        public virtual TDto? Add(TDto dto)
        {
            // Map the DTO to the entity type
            TEntity entity = mapper.Map<TEntity>(dto);

            // Reset the ID to default to avoid conflicts during insertion
            entity.Id = default;

            // Insert the new entity into the repository
            TEntity added = repository.Insert(entity);

            // Retrieve the added entity with any generated fields populated
            TEntity? found = repository.FindById(added.Id);

            // Map the entity back to a DTO and return it
            return mapper.Map<TDto>(found);
        }

        /// <summary>
        /// Updates an existing entity using the provided DTO and returns the updated DTO.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="dto">The DTO containing updated data.</param>
        /// <returns>The DTO representing the updated entity or null if not found.</returns>
        public virtual TDto? Update(ulong id, TDto dto)
        {
            // Check if the entity exists
            if (!repository.ExistsWithId(id))
                return null;

            // Map the DTO to the entity type
            var entity = mapper.Map<TEntity>(dto);

            // Update the entity in the repository
            var updatedEntity = repository.Update(entity);

            // Map the updated entity back to a DTO and return it
            return mapper.Map<TDto>(updatedEntity);
        }

        /// <summary>
        /// Deletes an entity by its ID and returns the DTO of the deleted entity.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>The DTO representing the deleted entity or null if not found.</returns>
        public virtual TDto? Delete(ulong id)
        {
            // Check if the entity exists
            if (!repository.ExistsWithId(id))
                return null;

            // Retrieve the entity before deletion
            var entity = repository.FindById(id);
            var dto = mapper.Map<TDto>(entity);

            // Delete the entity from the repository
            repository.Delete(id);

            // Return the DTO of the deleted entity
            return dto;
        }
    }
}
