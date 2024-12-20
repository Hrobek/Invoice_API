// Importing necessary libraries and components
import React, { useEffect, useState } from "react";
import { apiGet } from "../utils/api"; // Utility function for making GET requests
import Country from "./Country"; // Enum-like object defining country constants

const PersonDetailTable = ({ id }) => {
    const [person, setPerson] = useState({}); // State to store the person details

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
                <h1 className="text-center">Detail osoby</h1> {/* Page heading */}
                <hr />
                <h3>{person.name}</h3> {/* Display person's name */}

                {/* Display person's IČO and DIČ */}
                <p>
                    <strong>IČO:</strong> {person.identificationNumber} <strong>DIČ:</strong> {person.taxNumber}
                </p>

                {/* Display person's bank account details */}
                <p>
                    <strong>Bankovní účet:</strong>
                    <br />
                    {person.accountNumber}/{person.bankCode} ({person.iban})
                </p>

                {/* Display person's phone number */}
                <p>
                    <strong>Tel. číslo:</strong>
                    <br />
                    {person.telephone}
                </p>

                {/* Display person's email */}
                <p>
                    <strong>Email:</strong>
                    <br />
                    {person.mail}
                </p>

                {/* Display person's address */}
                <strong>Sídlo:</strong>
                <br />
                <div>{person.street}</div> {/* Street */}
                <div>{person.city} {person.zip}</div> {/* City and ZIP code */}
                <div>{country}</div> {/* Country */}

                {/* Display person's note */}
                <p>
                    <strong>Poznámka:</strong>
                    <br />
                    {person.note}
                </p>
            </div>
        </>
    );
};

export default PersonDetailTable; // Exporting the component
