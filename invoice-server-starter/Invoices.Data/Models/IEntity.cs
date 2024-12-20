namespace Invoices.Data.Models
{
    /// <summary>
    /// Defines a common interface for entities with an identifier.
    /// This interface ensures that all implementing classes have an Id property.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier for the entity.
        /// </summary>
        ulong Id { get; set; }
    }
}