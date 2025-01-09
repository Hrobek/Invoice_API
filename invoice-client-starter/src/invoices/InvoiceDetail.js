// Importing necessary modules and components for the InvoiceDetail page
import React, { useEffect, useState } from "react"; // React and hooks for state and effect management
import { Link, useParams } from "react-router-dom"; // Router for dynamic URL routing
import { apiGet } from "../utils/api"; // Custom function for GET request to fetch data
import { dateStringFormatter } from "../utils/dateStringFormatter"; // Utility function to format date strings
import PersonDetailTable from "../persons/PersonDetailTable"; // Component to display details for persons (buyer and seller)

const InvoiceDetail = () => {
    // Extracting the invoice ID from the URL params
    const { id } = useParams();
    
    // State to store the invoice details
    const [invoice, setInvoice] = useState({});

    useEffect(() => {
        // Fetching the invoice details using the `id` from the URL
        apiGet("/api/invoices/" + id)
            .then((data) => {
                // Formatting and setting the fetched data in the state
                setInvoice({
                    invoiceNumber: data.invoiceNumber,   // Invoice number
                    seller: data.seller,                  // Seller information
                    buyer: data.buyer,                    // Buyer information
                    issued: dateStringFormatter(data.issued, true), // Formatted issued date
                    dueDate: dateStringFormatter(data.dueDate, true),     // Formatted due date
                    product: data.product,                // Product information
                    price: data.price,                    // Price of the product
                    vat: data.vat,                        // VAT percentage
                    note: data.note,                      // Additional notes for the invoice
                });
            })
            .catch((error) => {
                // Handling any errors that occur during the fetch request
                console.error(error);
            });
        // Effect runs again when `id` changes
    }, [id]);

    return (
        <>
            {/* Invoice Detail Title */}
            <div>
                <h1>Detail faktury</h1>
                <hr/>
                <h3 className="text-center display-4">Číslo faktury: {invoice.invoiceNumber}</h3>
                <div className="container mt-5">
                    <div className="row">
                        {/* Seller Details Section */}
                        <div className="col-md-6 nameContainer p-3 bg-light border text-left">
                            <PersonDetailTable
                            id={invoice.seller?._id} label="Dodavatel"/>
                            <Link
                                    to={`/persons/show/${invoice.seller?._id}`}
                                    className="btn btn-sm btn-primary"
                                >
                                    Zobrazit
                                </Link>
                        </div>
                             {/* Buyer Details Section */}
                            <div className="col-md-6 nameContainer p-3 bg-light border text-left">
                                <PersonDetailTable
                                id={invoice.buyer?._id}  label="Odběratel"/>
                                <Link
                                    to={`/persons/show/${invoice.buyer?._id}`}
                                    className="btn btn-sm btn-primary"
                                >
                                    Zobrazit
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>
                   {/* Invoice Details Display Section */}
                <div className="mt-4 p-3 bg-secondary text-white text-center">
                    <p>
                        <strong>Vytvořeno:</strong>
                        <br/>
                        {invoice.issued}
                    </p>
                    <p>
                        <strong>Splatnost:</strong>
                        <br/>
                        {invoice.dueDate}
                    </p>
                    <p>
                        <strong>Produkt:</strong>
                        <br/>
                        {invoice.product}
                    </p>
                    <p>
                        <strong>Cena:</strong>
                        <br/>
                        {invoice.price} Kč
                    </p>
                    <p>
                        <strong>DPH:</strong>
                        <br/>
                        {invoice.vat}%
                    </p>
                    <p>
                        <strong>Poznámka:</strong>
                        <br/>
                        {invoice.note}
                    </p>
                </div>
        </>
    );
};

export default InvoiceDetail;
