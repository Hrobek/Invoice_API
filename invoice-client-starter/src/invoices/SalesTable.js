import React, { useState, useEffect } from "react"; // Import React and necessary hooks
import { Link } from "react-router-dom"; // Import Link for navigation
import { apiGet } from "../utils/api"; // Import the custom API GET function
import ReactPaginate from "react-paginate"; // Import pagination component

// Component to display a table of sales invoices
const SalesTable = ({ identificationNumber }) => {
    const [invoices, setInvoices] = useState([]); // State to store the list of invoices
    const [currentPage, setCurrentPage] = useState(0); // State to track the current page in pagination
    const itemsPerPage = 10; // Number of items displayed per page

    // Calculate the starting index for the current page
    const offset = currentPage * itemsPerPage;

    // Extract the items to display on the current page
    const currentItems = invoices.slice(offset, offset + itemsPerPage);

    // Calculate the total number of pages
    const pageCount = Math.ceil(invoices.length / itemsPerPage);

    // Handle page change in pagination
    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected);
    };

    // Fetch invoices when the `identificationNumber` changes
    useEffect(() => {
        if (identificationNumber) {
            apiGet(`/api/identification/${identificationNumber}/sales/`)
                .then((data) => setInvoices(data)); // Update invoices state with the fetched data
        }
    }, [identificationNumber]); // Dependency array ensures this effect runs when `identificationNumber` changes

    return (
        <div>
            <div className="fw-bold">
                Přijaté faktury {/* Display table title */}
            </div>

            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>#</th> {/* Column for item number */}
                        <th>Číslo Faktury</th> {/* Invoice number */}
                        <th>Odběratel</th> {/* Buyer */}
                        <th>Cena</th> {/* Price */}
                        <th colSpan={3}>Akce</th> {/* Action column */}
                    </tr>
                </thead>
                <tbody>
                    {currentItems.map((invoice, index) => (
                        <tr key={index + offset + 1}>
                            <td>{index + offset + 1}</td> {/* Item number */}
                            <td>{invoice.invoiceNumber}</td> {/* Invoice number */}
                            <td>{invoice.buyer?.name}</td> {/* Buyer name */}
                            <td>{invoice.price} Kč</td> {/* Price in CZK */}
                            <td>
                                <div className="btn-group">
                                    {/* Link to view the invoice details */}
                                    <Link
                                        to={`/invoices/show/${invoice._id}`}
                                        className="btn btn-sm btn-primary"
                                    >
                                        Zobrazit {/* Display text */}
                                    </Link>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {/* Display pagination only if the total invoices exceed the items per page */}
            {invoices.length > itemsPerPage && (
                <ReactPaginate
                    previousLabel={"Předchozí"} // Previous button label
                    nextLabel={"Další"} // Next button label
                    breakLabel={"..."} // Break label for skipped pages
                    breakClassName={"page-item"} // Styling for the break
                    breakLinkClassName={"page-link"} // Styling for the break link
                    pageCount={pageCount} // Total number of pages
                    marginPagesDisplayed={2} // Number of margin pages to display
                    pageRangeDisplayed={5} // Number of page links to display
                    onPageChange={handlePageClick} // Handle page click
                    containerClassName={"pagination justify-content-center"} // Styling for pagination container
                    pageClassName={"page-item"} // Styling for individual page links
                    pageLinkClassName={"page-link"} // Styling for page link
                    previousClassName={"page-item"} // Styling for previous button
                    previousLinkClassName={"page-link"} // Styling for previous button link
                    nextClassName={"page-item"} // Styling for next button
                    nextLinkClassName={"page-link"} // Styling for next button link
                    activeClassName={"active"} // Styling for active page
                />
            )}
        </div>
    );
};

export default SalesTable; // Export the component as default
