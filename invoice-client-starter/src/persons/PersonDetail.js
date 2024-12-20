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

// Import necessary libraries and components
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import { apiGet } from "../utils/api"; // Utility function for making GET requests
import Country from "./Country"; // Enum-like object defining country constants
import SalesTable from "../invoices/SalesTable"; // Component to display sales invoices
import PurchasesTable from "../invoices/PurchasesTable"; // Component to display purchase invoices
import PersonDetailTable from "./PersonDetailTable"; // Component to display detailed person information

const PersonDetail = () => {
    const { id } = useParams(); // Extracting the `id` parameter from the route
    const [person, setPerson] = useState({}); // State to store person details

    useEffect(() => {
        if (id) {
            // Fetch person details only if an ID is provided
            apiGet("/api/persons/" + id).then((data) => setPerson(data));
        }
        // TODO: Add error handling or loading state for the HTTP request
    }, [id]); // Dependency array ensures the effect runs when `id` changes

    // Determine the country label based on the person's country
    const country = Country.CZECHIA === person.country ? "Česká republika" : "Slovensko";

    return (
        <>
            <div>
                {/* Container for the person detail table */}
                <div className="mt-3 container p-3 bg-light border text-left">
                    <PersonDetailTable id={person._id} /> {/* Display detailed person information */}
                </div>
            </div>

            {/* Buttons for toggling visibility of invoices sections */}
            <button
                className="btn btn-secondary mb-3 mt-3 me-2"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#collapseOne"
                aria-expanded="false"
                aria-controls="collapseExample"
            >
                Přijaté faktury {/* Label for received invoices */}
            </button>
            <button
                className="btn btn-secondary mb-3 mt-3"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#collapseTwo"
                aria-expanded="false"
                aria-controls="collapseExample"
            >
                Vystavené faktury {/* Label for issued invoices */}
            </button>

            {/* Collapsible sections for invoices */}
            <div className="row mt-3">
                {/* Sales invoices section */}
                <div className="col-md-6 collapse" id="collapseOne">
                    <SalesTable identificationNumber={person.identificationNumber} />
                </div>
                {/* Purchases invoices section */}
                <div className="col-md-6 collapse" id="collapseTwo">
                    <PurchasesTable identificationNumber={person.identificationNumber} />
                </div>
            </div>
        </>
    );
};

export default PersonDetail; // Exporting the component
