

import { Link } from "react-router-dom"; // Importing Link from react-router-dom for routing between pages
import InvoiceStatistic from "../statistics/InvoiceStatistic"; // Import custom InvoiceStatistic component for displaying invoice statistics
import ReactPaginate from "react-paginate"; // Import pagination library to handle paginated data
import React, { useState } from "react"; // Import React and useState for handling component state

const InvoiceTable = ({ items, deleteInvoice }) => {
    const [currentPage, setCurrentPage] = useState(0); // State for tracking the current page of the pagination
    const itemsPerPage = 10; // Number of items to display per page

    // Calculate the current page's items by slicing the `items` array
    const offset = currentPage * itemsPerPage; 
    const currentItems = items.slice(offset, offset + itemsPerPage);

    // Calculate the total number of pages
    const pageCount = Math.ceil(items.length / itemsPerPage);

    // Function to handle page click event from ReactPaginate
    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected); // Update currentPage with the selected page index
    };

    return (
        <div>
            {/* Render the InvoiceStatistic component */}
            <InvoiceStatistic />

            {/* Table to display invoice data */}
            <table className="table table-bordered mt-3">
                <thead>
                    <tr>
                        <th>#</th> {/* Serial number */}
                        <th>Číslo faktury</th>{/* Invoice number */}
                        <th>Odběratel</th> 
                        <th>Dodavatel</th>
                        <th>Produkt</th> 
                        <th>Cena</th> 
                        <th colSpan={3}>Akce</th> {/* Actions column with buttons for each row */}
                    </tr>
                </thead>
                <tbody>
                    {/* Loop through currentItems to display each invoice */}
                    {currentItems.map((item, index) => (
                        <tr key={index + offset + 1}>
                            <td>{index + 1 + offset}</td> {/* Display the item number with offset */}
                            <td>{item.invoiceNumber}</td> {/* Display the invoice number */}
                            <td>{item.buyer?.name}</td> 
                            <td>{item.seller?.name}</td>
                            <td>{item.product}</td> 
                            <td>{item.price} Kč</td> 
                            <td>
                                {/* Grouped action buttons for each invoice */}
                                <div className="btn-group">
                                    {/* View invoice button */}
                                    <Link
                                        to={"/invoices/show/" + item._id} // Link to view detailed invoice
                                        className="btn btn-sm btn-info"
                                    >
                                        Zobrazit
                                    </Link>
                                    {/* Edit invoice button */}
                                    <Link
                                        to={"/invoices/edit/" + item._id} // Link to edit invoice
                                        className="btn btn-sm btn-warning"
                                    >
                                        Upravit
                                    </Link>
                                    {/* Delete invoice button */}
                                    <button
                                        onClick={() => {
                                            if (window.confirm("Opravdu chcete odstranit tuto fakturu?")) {
                                            deleteInvoice(item._id); // Call deleteInvoice only if the user confirms
                                            }
                                        }}
                                        className="btn btn-sm btn-danger"
>
                                        Odstranit
                                    </button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* Pagination controls */}
            {items.length > itemsPerPage && (
                <ReactPaginate
                    previousLabel={"Předchozí"} // Label for previous page button
                    nextLabel={"Další"} // Label for next page button
                    breakLabel={"..."} // Label for page breaks
                    breakClassName={"page-item"} // Class for the break element
                    breakLinkClassName={"page-link"} // Class for the break link
                    pageCount={pageCount} // Total number of pages
                    marginPagesDisplayed={2} // Number of margin pages displayed at the edges
                    pageRangeDisplayed={5} // Number of page links to display in the range
                    onPageChange={handlePageClick} // Function to handle page change
                    containerClassName={"pagination justify-content-center"} // Class for pagination container
                    pageClassName={"page-item"} // Class for each page item
                    pageLinkClassName={"page-link"} // Class for each page link
                    previousClassName={"page-item"} // Class for previous page button
                    previousLinkClassName={"page-link"} // Class for previous button link
                    nextClassName={"page-item"} // Class for next page button
                    nextLinkClassName={"page-link"} // Class for next button link
                    activeClassName={"active"} // Class for the active page link
                />
            )}

            {/* Link to create a new invoice */}
            <Link to={"/invoices/create"} className="btn btn-success">
                Nová faktura
            </Link>
        </div>
    );
};

export default InvoiceTable; // Export the InvoiceTable component for use in other parts of the application
