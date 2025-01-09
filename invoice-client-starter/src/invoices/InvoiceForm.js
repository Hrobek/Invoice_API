import React, { useEffect, useState } from "react"; // Importing React, useEffect, useState hooks
import { useNavigate, useParams } from "react-router-dom"; // Importing hooks for navigation and URL parameters

import { apiGet, apiPost, apiPut } from "../utils/api"; // Custom API functions for GET, POST, PUT requests
import { dateStringFormatter } from '../utils/dateStringFormatter'; // Utility function to format date strings

import InputField from "../components/InputField"; // Import InputField component for input fields
import FlashMessage from "../components/FlashMessage"; // Import FlashMessage for displaying success/error messages
import InputSelect from '../components/InputSelect'; // Import InputSelect for dropdown inputs
import "react-datepicker/dist/react-datepicker.css"; // Import the date picker CSS

const InvoiceForm = () => {
    const { id } = useParams(); // Get the invoice ID from the URL (if editing an existing invoice)
    const navigate = useNavigate(); // Hook for navigation after saving or submitting the form

    // State variables for managing form data
    const [personListState, setPersonList] = useState([]); // List of people (buyers/sellers) for selection
    const [invoiceNumberState, setInvoiceNumber] = useState(""); // Invoice number
    const [issuedState, setIssued] = useState(""); // Issued date
    const [dateState, setDate] = useState(""); // Due date
    const [buyerState, setBuyer] = useState(""); // Buyer ID
    const [sellerState, setSeller] = useState(""); // Seller ID
    const [productState, setProduct] = useState(""); // Product name
    const [priceState, setPrice] = useState(""); // Price of the product
    const [vatState, setVat] = useState(""); // VAT amount
    const [noteState, setNote] = useState(""); // Invoice note
    const [sentState, setSent] = useState(false); // Flag to track if the form has been submitted
    const [successState, setSuccess] = useState(false); // Flag to track if the submission was successful
    const [errorState, setError] = useState(); // Error message if the submission fails

    // Handle input field changes
    const handleChange = (e) => {
        const target = e.target;
        const name = target.name;
        const value = target.value;

        // Update the appropriate state based on the input name
        if (name === 'invoiceNumber') { setInvoiceNumber(value); }
        else if (name === 'buyer') { setBuyer(value); }
        else if (name === 'seller') { setSeller(value); }
        else if (name === 'issued') { setIssued(value); }
        else if (name === 'dueDate') { setDate(value); }
        else if (name === 'product') { setProduct(value); }
        else if (name === 'price') { setPrice(value); }
        else if (name === 'vat') { setVat(value); }
        else if (name === 'note') { setNote(value); }
    };

    // Handle form submission (create or update invoice)
    const handleSubmit = (e) => {
        e.preventDefault(); // Prevent the default form submission behavior

        // Create the body object to send in the API request
        const body = {
            invoiceNumber: invoiceNumberState,
            buyerId: buyerState,
            sellerId: sellerState,
            issued: issuedState,
            dueDate: dateState,
            product: productState,
            price: priceState,
            vat: vatState,
            note: noteState,
        };

        // Send the API request based on whether we're editing or creating a new invoice
        (id
            ? apiPut('/api/invoices/' + id, body) // PUT request to update the invoice if ID exists
            : apiPost('/api/invoices/', body) // POST request to create a new invoice
        )
            .then((data) => {
                setSent(true); // Set sent flag to true
                setSuccess(true); // Set success flag to true
                setTimeout(() => {
                    navigate("/invoices"); // Redirect to the invoices list after 1 second
                }, 1000);
            })
            .catch((error) => {
                setError(error.message); // Set error message if the request fails
                setSent(true); // Set sent flag to true
                setSuccess(false); // Set success flag to false
            });
    };

    // useEffect hook to fetch person data and invoice data (if editing an invoice)
    useEffect(() => {
        apiGet('/api/persons/').then((data) => {
            setPersonList(data); // Set the list of people (buyers/sellers) for selection
            if (id) {
                // If we're editing an existing invoice, fetch the invoice data by ID
                apiGet("/api/invoices/" + id).then((invoiceData) => {
                    setInvoiceNumber(invoiceData.invoiceNumber);
                    setIssued(dateStringFormatter(invoiceData.issued)); // Format issued date
                    setDate(dateStringFormatter(invoiceData.dueDate)); // Format due date
                    setBuyer(invoiceData.buyer._id); // Set the buyer
                    setSeller(invoiceData.seller._id); // Set the seller
                    setProduct(invoiceData.product); // Set the product
                    setPrice(invoiceData.price); // Set the price
                    setVat(invoiceData.vat); // Set the VAT amount
                    setNote(invoiceData.note); // Set the note
                });
            }
        });
    }, [id]); // Fetch data when the component mounts or when ID changes

    // Conditional rendering for success and error messages
    const sent = sentState;
    const success = successState;
    return (
        <div>
            <h1>{id ? "Upravit" : "Vytvořit"} fakturu</h1> {/* Title changes depending on whether it's creating or editing */}
            <hr />
            {errorState ? <div className="alert alert-danger">{errorState}</div> : ""} {/* Display error message */}
            {sent && success ? (
                <FlashMessage
                    theme={"success"}
                    text={"Uložení faktury proběhlo úspěšně."} // Display success message
                />
            ) : null}

            {/* The form to input invoice data */}
            <form onSubmit={handleSubmit}>
                <InputField
                    required={true}
                    type="text"
                    name="invoiceNumber"
                    label="Číslo faktury"
                    prompt="Zadejte číslo faktury"
                    value={invoiceNumberState}
                    handleChange={handleChange}
                />

                <InputSelect
                    name="seller"
                    items={personListState}
                    label="Dodavatel"
                    prompt="Vyber dodavatele"
                    value={sellerState}
                    handleChange={handleChange}
                />

                <InputSelect
                    name="buyer"
                    items={personListState}
                    label="Odběratel"
                    prompt="Vyber odběratele"
                    value={buyerState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="Date"
                    name="issued"
                    label="Datum vytvoření"
                    prompt="Zadejte datum vytvoření"
                    min="0000-01-01"
                    value={issuedState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="date"
                    name="dueDate"
                    label="Datum splatnosti"
                    prompt="Zadejte datum splatnosti"
                    min="0000-01-01"
                    value={dateState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="text"
                    name="product"
                    label="Název produktu"
                    prompt="Zadejte název produktu"
                    value={productState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="text"
                    name="price"
                    label="Cena"
                    prompt="Zadejte cenu produktu"
                    value={priceState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="text"
                    name="vat"
                    label="DPH"
                    prompt="Zadejte DPH"
                    value={vatState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="text"
                    name="note"
                    label="Poznámka"
                    prompt="Zadejte poznámku"
                    value={noteState}
                    handleChange={handleChange}
                />

                <div className="mt-3 text-center">
                    <input type="submit" className="btn btn-primary btn-lg" value="Uložit" />
                </div>
            </form>
        </div>
    );
};

export default InvoiceForm; // Export the InvoiceForm component
