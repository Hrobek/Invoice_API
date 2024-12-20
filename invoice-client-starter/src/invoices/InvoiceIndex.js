import React, { useEffect, useState } from "react"; // Importing React, useEffect, and useState hooks
import { apiDelete, apiGet } from "../utils/api"; // Importing custom API functions for GET and DELETE requests
import InvoiceTable from "./InvoiceTable"; // Import InvoiceTable component for displaying invoices
import InvoiceFilter from "./InvoiceFilter"; // Import InvoiceFilter component for filtering invoices

const InvoiceIndex = () => {
    // State to hold all invoices
    const [invoices, setInvoices] = useState([]);
    // State to hold the list of persons (for filtering purposes)
    const [personListState, setPersonList] = useState([]);
    // State to hold filter criteria for searching invoices
    const [filterState, setFilter] = useState({
        buyerID: undefined,
        sellerID: undefined,
        product: undefined,
        minPrice: undefined,
        maxPrice: undefined,
        limit: undefined,
    });

    // Function to delete an invoice by ID
    const deleteInvoice = async (id) => {
        try {
            await apiDelete("/api/invoices/" + id); // Perform DELETE request to remove the invoice
        } catch (error) {
            console.log(error.message); // Log error if the DELETE request fails
            alert(error.message); // Show error message to the user
        }
        // Update state to remove the deleted invoice from the displayed list
        setInvoices(invoices.filter((item) => item._id !== id));
    };

    // useEffect hook to fetch person data and invoices when the component is mounted
    useEffect(() => {
        apiGet('/api/persons').then((data) => setPersonList(data)); // Fetch persons for filter options
        apiGet("/api/invoices").then((data) => setInvoices(data)); // Fetch all invoices initially
    }, []); // Empty dependency array ensures this runs only once on component mount

    // Handle change in filter input fields (for example, selecting filter values)
    const handleChange = (e) => {
        // If the selected value is "false", "true", or an empty string, set the corresponding filter value to undefined
        if (e.target.value === "false" || e.target.value === "true" || e.target.value === '') {
            setFilter(prevState => {
                return { ...prevState, [e.target.name]: undefined }; // Reset filter value to undefined
            });
        } else {
            // Otherwise, update the filter state with the new selected value
            setFilter(prevState => {
                return { ...prevState, [e.target.name]: e.target.value };
            });
        }
    };

    // Handle form submission for the filter form
    const handleSubmit = async (e) => {
        e.preventDefault(); // Prevent the default form submission behavior
        const params = filterState; // Get the filter parameters from state

        // Fetch filtered invoices based on the selected filter criteria
        const data = await apiGet("/api/invoices", params);
        setInvoices(data); // Update the state with the filtered invoices
    };

    return (
        <div>
            <h1>Seznam faktur</h1> {/* Title of the page */}
            
            {/* Render the InvoiceFilter component with necessary props for filtering invoices */}
            <InvoiceFilter
                handleChange={handleChange} // Function to handle changes in filter inputs
                handleSubmit={handleSubmit} // Function to handle form submission (filtering)
                personList={personListState} // List of persons to be used in the filter dropdown
                filter={filterState} // Current state of the filter
                confirm="Filtrovat faktury" // Button label to confirm the filter action
            />
            
            <hr/>

            {/* Render the InvoiceTable component to display the list of invoices */}
            <InvoiceTable
                deleteInvoice={deleteInvoice} // Pass the deleteInvoice function for handling invoice deletion
                items={invoices} // Pass the list of invoices to be displayed
                label="Počet faktur:" // Label for the invoice count
            />
        </div>
    );
};

export default InvoiceIndex; // Export the InvoiceIndex component for use in other parts of the application
