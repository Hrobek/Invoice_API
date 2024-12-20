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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Invoices.Data;

/// <summary>
/// Represents the database context for managing invoices and persons.
/// </summary>
public class InvoicesDbContext : DbContext
{
    /// <summary>
    /// The table representing persons in the database.
    /// </summary>
    public DbSet<Person>? Persons { get; set; }

    /// <summary>
    /// The table representing invoices in the database.
    /// </summary>
    public DbSet<Invoice>? Invoices { get; set; }

    /// <summary>
    /// Configures the database context with the specified options.
    /// </summary>
    /// <param name="options">Options for configuring the DbContext.</param>
    public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the entity relationships and model behaviors.
    /// </summary>
    /// <param name="modelBuilder">The builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the relationship between Invoice and Buyer.
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Buyer) // An invoice can have one buyer.
            .WithMany() // No navigation from Buyer to a collection of invoices.
            .HasForeignKey(i => i.BuyerId) // The foreign key in Invoice referencing Buyer.
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for Buyer.

        // Configure the relationship between Invoice and Seller.
        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Seller) // An invoice can have one seller.
            .WithMany() // No navigation from Seller to a collection of invoices.
            .HasForeignKey(i => i.SellerId) // The foreign key in Invoice referencing Seller.
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for Seller.

        // Ensure no cascading deletes for any foreign key relationships.
        IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(type => type.GetForeignKeys()) // Get all foreign keys in the model.
            .Where(foreignKey => !foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (IMutableForeignKey foreignKey in cascadeFKs)
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict; // Change delete behavior to Restrict.
    }
}