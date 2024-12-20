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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Models;
/// <summary>
/// Represents a person entity in the invoices database.
/// </summary>
public class Person : IEntity
{
    /// <summary>
    /// The unique identifier for the person.
    /// This is an auto-generated primary key.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }

    /// <summary>
    /// The name of the person.
    /// </summary>
    [Required]
    public string Name { get; set; } = "";

    /// <summary>
    /// The unique identification number of the person.
    /// </summary>
    [Required]
    public string IdentificationNumber { get; set; } = "";

    /// <summary>
    /// The tax identification number of the person.
    /// </summary>
    [Required]
    public string TaxNumber { get; set; } = "";

    /// <summary>
    /// The person's bank account number.
    /// </summary>
    [Required]
    public string AccountNumber { get; set; } = "";

    /// <summary>
    /// The bank code associated with the account number.
    /// </summary>
    [Required]
    public string BankCode { get; set; } = "";

    /// <summary>
    /// The International Bank Account Number (IBAN) of the person.
    /// </summary>
    [Required]
    public string Iban { get; set; } = "";

    /// <summary>
    /// The telephone number of the person.
    /// </summary>
    [Required]
    public string Telephone { get; set; } = "";

    /// <summary>
    /// The email address of the person.
    /// </summary>
    [Required]
    public string Mail { get; set; } = "";

    /// <summary>
    /// The street address of the person.
    /// </summary>
    [Required]
    public string Street { get; set; } = "";

    /// <summary>
    /// The ZIP or postal code of the person's address.
    /// </summary>
    [Required]
    public string Zip { get; set; } = "";

    /// <summary>
    /// The city where the person resides.
    /// </summary>
    [Required]
    public string City { get; set; } = "";

    /// <summary>
    /// Additional notes related to the person.
    /// </summary>
    [Required]
    public string Note { get; set; } = "";

    /// <summary>
    /// The country associated with the person's address.
    /// </summary>
    [Required]
    public Country Country { get; set; }

    /// <summary>
    /// A flag indicating whether the person's record is hidden.
    /// Default is false.
    /// </summary>
    [Required]
    public bool Hidden { get; set; } = false;
}