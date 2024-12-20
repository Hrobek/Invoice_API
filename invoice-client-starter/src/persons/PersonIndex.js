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

import React, { useEffect, useState } from "react";
import { apiDelete, apiGet } from "../utils/api";
import PersonTable from "./PersonTable";

const PersonIndex = () => {
    const [persons, setPersons] = useState([]); // State to hold the list of persons

    // Function to handle deleting a person
    const deletePerson = async (id) => {
        try {
            // Call API to delete the person by ID
            await apiDelete("/api/persons/" + id);
            // Update the local state by filtering out the deleted person
            setPersons(persons.filter((item) => item._id !== id));
        } catch (error) {
            // Handle any errors that occur during the API call
            console.log(error.message);
            alert(error.message); // Notify the user of the error
        }
    };

    // Fetch the list of persons when the component mounts
    useEffect(() => {
        apiGet("/api/persons").then((data) => setPersons(data));
    }, []);

    return (
        <div>
            <h1>Seznam osob</h1> {/* Title of the page */}
            <PersonTable
                deletePerson={deletePerson} // Pass deletePerson function to the table
                items={persons} // Pass the list of persons to the table
                label="Počet osob:" // Label for the table
            />
        </div>
    );
};

export default PersonIndex;

