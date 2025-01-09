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

import ReactPaginate from "react-paginate";
import React, { useState } from "react";
import { Link } from "react-router-dom";

// Component to display a paginated table of persons with actions for each person
const PersonTable = ({ label, items, deletePerson }) => {
    const [currentPage, setCurrentPage] = useState(0); // State to track the current page
    const itemsPerPage = 10; // Number of items displayed per page

    // Calculate the starting index of the current page
    const offset = currentPage * itemsPerPage;
    // Extract the items to be displayed on the current page
    const currentItems = items.slice(offset, offset + itemsPerPage);
    // Calculate the total number of pages
    const pageCount = Math.ceil(items.length / itemsPerPage);

    // Handle pagination when the user clicks a page number
    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected); // Update the current page
    };

    return (
        <div>
            {/* Display the label and total count of items */}
            <p>
                {label} {items.length}
            </p>

            {/* Table to display person data */}
            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Jméno</th>
                        <th colSpan={3}>Akce</th> {/* Action buttons */}
                    </tr>
                </thead>
                <tbody>
                    {/* Render each item in the currentItems list */}
                    {currentItems.map((item, index) => (
                        <tr key={index + offset + 1}>
                            <td>{index + 1 + offset}</td> {/* Row number */}
                            <td>{item.name}</td> {/* Person's name */}
                            <td>
                                <div className="btn-group">
                                    {/* Link to view details of the person */}
                                    <Link
                                        to={"/persons/show/" + item._id}
                                        className="btn btn-sm btn-info"
                                    >
                                        Zobrazit
                                    </Link>
                                    {/* Link to edit the person */}
                                    <Link
                                        to={"/persons/edit/" + item._id}
                                        className="btn btn-sm btn-warning"
                                    >
                                        Upravit
                                    </Link>
                                    {/* Button to delete the person */}
                                    <button
                                        onClick={() => {
                                            if (window.confirm("Opravdu chcete odstranit tuto osobu?")) {
                                            deletePerson(item._id); // Call deleteInvoice only if the user confirms
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

            {/* Display pagination controls if there are more items than itemsPerPage */}
            {items.length > itemsPerPage && (
                <ReactPaginate
                    previousLabel={"Předchozí"} // Label for previous page button
                    nextLabel={"Další"} // Label for next page button
                    breakLabel={"..."} // Label for break in pagination
                    breakClassName={"page-item"} // Class for break element
                    breakLinkClassName={"page-link"} // Class for break link
                    pageCount={pageCount} // Total number of pages
                    marginPagesDisplayed={2} // Number of margin pages displayed
                    pageRangeDisplayed={5} // Number of page links displayed in the middle
                    onPageChange={handlePageClick} // Function to handle page changes
                    containerClassName={"pagination justify-content-center"} // Class for pagination container
                    pageClassName={"page-item"} // Class for each page item
                    pageLinkClassName={"page-link"} // Class for each page link
                    previousClassName={"page-item"} // Class for previous button
                    previousLinkClassName={"page-link"} // Class for previous button link
                    nextClassName={"page-item"} // Class for next button
                    nextLinkClassName={"page-link"} // Class for next button link
                    activeClassName={"active"} // Class for the active page
                />
            )}

            {/* Button to navigate to the "Create New Person" form */}
            <Link to={"/persons/create"} className="btn btn-success">
                Nová osoba
            </Link>
        </div>
    );
};

export default PersonTable;
