import React, { useEffect, useState } from "react"; // Importing necessary React hooks

import { apiGet } from "../utils/api"; // Importing the API call utility function

import StatisticTable from "./StatisticTable"; // Importing the StatisticTable component to display the data

// PersonIndex component that fetches and displays a list of persons
const PersonIndex = () => {
    // State to hold the list of persons
    const [persons, setPersons] = useState([]);

    // useEffect hook to fetch the list of persons when the component mounts
    useEffect(() => {
        // Making an API call to fetch the persons data
        apiGet("/api/persons").then((data) => setPersons(data)); // On success, update the state with the fetched data
    }, []); // Empty dependency array ensures this effect runs only once when the component mounts

    return (
        <div>
            {/* Passing the fetched persons data to the StatisticTable component to display */}
            <StatisticTable
                items={persons} // The 'items' prop will be populated with the 'persons' state data
            />
        </div>
    );
};

export default PersonIndex; // Exporting the PersonIndex component as the default export
