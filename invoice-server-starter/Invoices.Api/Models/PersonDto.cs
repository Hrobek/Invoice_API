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
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    // DTO (Data Transfer Object) for representing a person with their detailed information
    public class PersonDto
    {
        // Unique identifier for the person, serialized with the name "_id"
        [JsonPropertyName("_id")]
        public uint Id { get; set; }

        // Name of the person
        public string Name { get; set; } = "";

        // Identification number (e.g., national ID, company registration number)
        public string IdentificationNumber { get; set; } = "";

        // Tax identification number for the person or entity
        public string TaxNumber { get; set; } = "";

        // Bank account number of the person
        public string AccountNumber { get; set; } = "";

        // Bank code (e.g., bank branch or specific identifier for the bank)
        public string BankCode { get; set; } = "";

        // International Bank Account Number (IBAN) for the person
        public string Iban { get; set; } = "";

        // Telephone number of the person
        public string Telephone { get; set; } = "";

        // Email address of the person
        public string Mail { get; set; } = "";

        // Street address of the person
        public string Street { get; set; } = "";

        // Postal code for the person's address
        public string Zip { get; set; } = "";

        // City where the person resides
        public string City { get; set; } = "";

        // Additional notes related to the person
        public string Note { get; set; } = "";

        // Country where the person is located
        public Country Country { get; set; }
    }
}
