import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { apiGet } from "../utils/api"; // Import utility function for API calls
import ReactPaginate from "react-paginate"; // Import pagination library

const PurchasesTable = ({ identificationNumber }) => {
    // State to store invoices fetched from the API
    const [invoices, setInvoices] = useState([]);

    // State to track the current page in pagination
    const [currentPage, setCurrentPage] = useState(0);
    const itemsPerPage = 10; // Number of items displayed per page

    // Calculate the starting index for the current page
    const offset = currentPage * itemsPerPage;

    // Slice the invoices to get the items for the current page
    const currentItems = invoices.slice(offset, offset + itemsPerPage);

    // Calculate the total number of pages for pagination
    const pageCount = Math.ceil(invoices.length / itemsPerPage);

    // Event handler for page change in the pagination component
    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected); // Update the current page
    };

    // Fetch invoices when the identification number changes
    useEffect(() => {
        if (identificationNumber) {
            apiGet(`/api/identification/${identificationNumber}/purchases/`)
                .then((data) => setInvoices(data)); // Update state with fetched data
        }
    }, [identificationNumber]); // Dependency array ensures this runs when `identificationNumber` changes

    return (
        <div>
            {/* Table title */}
            <div className="fw-bold">Vystavené faktury</div>

            {/* Display a table of purchase invoices */}
            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Číslo Faktury</th> {/* Invoice number */}
                        <th>Dodavatel</th> {/* Supplier */}
                        <th>Cena</th> {/* Price */}
                        <th colSpan={3}>Akce</th> {/* Actions */}
                    </tr>
                </thead>
                <tbody>
                    {/* Render rows for each invoice in the current page */}
                    {invoices.map((invoice, index) => (
                        <tr key={index + offset + 1}>
                            {/* Display sequential index */}
                            <td>{index + offset + 1}</td>
                            {/* Display invoice details */}
                            <td>{invoice.invoiceNumber}</td>
                            <td>{invoice.seller?.name}</td> {/* Optional chaining to handle null/undefined */}
                            <td>{invoice.price} Kč</td>
                            <td>
                                {/* Action buttons */}
                                <div className="btn-group">
                                    <Link
                                        to={`/invoices/show/${invoice._id}`} // Link to view invoice details
                                        className="btn btn-sm btn-primary"
                                    >
                                        Zobrazit {/* Button label */}
                                    </Link>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* Pagination controls */}
            {currentItems.length > itemsPerPage && (
                <ReactPaginate
                    previousLabel={"Předchozí"} // Label for the previous page button
                    nextLabel={"Další"} // Label for the next page button
                    breakLabel={"..."} // Label for breaks between page numbers
                    breakClassName={"page-item"} // Class for the break element
                    breakLinkClassName={"page-link"} // Class for the break link
                    pageCount={pageCount} // Total number of pages
                    marginPagesDisplayed={2} // Number of margin pages displayed
                    pageRangeDisplayed={5} // Number of pages displayed in the range
                    onPageChange={handlePageClick} // Handler for page change
                    containerClassName={"pagination justify-content-center"} // Class for pagination container
                    pageClassName={"page-item"} // Class for individual page items
                    pageLinkClassName={"page-link"} // Class for individual page links
                    previousClassName={"page-item"} // Class for the previous button
                    previousLinkClassName={"page-link"} // Class for the previous link
                    nextClassName={"page-item"} // Class for the next button
                    nextLinkClassName={"page-link"} // Class for the next link
                    activeClassName={"active"} // Class for the active page
                />
            )}
        </div>
    );
};

export default PurchasesTable; // Export the component for use in other parts of the app
