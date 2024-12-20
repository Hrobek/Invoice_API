/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *                                _
 *              ___ ___ ___ _____|_|_ _ _____
 *             | . |  _| -_|     | | | |     |  LICENCE
 *             |  _|_| |___|_|_|_|_|___|_|_|_|
 *             |_|
 *
 *   PROGRAMOVÁNÍ  <>  DESIGN  <>  PRÁCE/PODNIKÁNÍ  <>  HW A SW
 *
 * Tento zdrojový kód je součástí výukových seriálů na
 * IT sociální síti WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci prémiového obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

// Function to format a date string in different formats
export const dateStringFormatter = (str, locale = false) => {
    // Convert the input string to a Date object
    const d = new Date(str);

    // If the locale is set to true, return a localized date string (in Czech format)
    if (locale) {
        return d.toLocaleDateString("cs-CZ", { // Using Czech locale (cs-CZ)
            year: "numeric", // Display full year
            month: "long", // Display full month name (e.g., January, February)
            day: "numeric", // Display day of the month
        });
    }

    // If locale is false or not provided, return the date in "YYYY-MM-DD" format
    const year = d.getFullYear(); // Get the full year (e.g., 2024)
    const month = "" + (d.getMonth() + 1); // Get the month (1-12), adding 1 because months are 0-indexed
    const day = "" + d.getDate(); // Get the day of the month (1-31)

    // Format month and day to ensure they are two digits (e.g., "03" instead of "3")
    return [
        year, // Year as a 4-digit number (e.g., "2024")
        month.length < 2 ? "0" + month : month, // Add leading zero to the month if it's a single digit
        day.length < 2 ? "0" + day : day, // Add leading zero to the day if it's a single digit
    ].join("-"); // Join year, month, and day with a hyphen to form "YYYY-MM-DD"
};

// Export the dateStringFormatter function for use in other parts of the application
export default dateStringFormatter;
