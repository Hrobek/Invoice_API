

import React, {useState, useEffect} from "react";
import {Link} from "react-router-dom";
import { apiGet } from "../utils/api";
import ReactPaginate from "react-paginate";

const SalesTable = ({ identificationNumber}) => {
    const [invoices, setInvoices] = useState([]);

    const [currentPage, setCurrentPage] = useState(0);
    const itemsPerPage = 10;

    // Výpočet aktuálních položek
    const offset = currentPage * itemsPerPage;
    const currentItems = invoices.slice(offset, offset + itemsPerPage);
    const pageCount = Math.ceil(invoices.length / itemsPerPage);

    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected);
    }
    useEffect(() => {
        if (identificationNumber) {
            apiGet("/api/identification/" + identificationNumber + "/sales/").then((data) => setInvoices(data));
        }
    }, [identificationNumber]);

    return (
        <div>
            <div className="fw-bold">
                Přijaté faktury
            </div>

            <table className="table table-bordered">
                <thead>
                <tr>
                    <th>#</th>
                    <th>Číslo Faktury</th>
                    <th>Odběratel</th>
                    <th>Cena</th>
                    <th colSpan={3}>Akce</th>
                </tr>
                </thead>
                <tbody>
                {currentItems.map((invoice, index) => (
                    <tr key={index + offset + 1}>
                        <td>{index + offset + 1}</td>
                        <td>{invoice.invoiceNumber}</td>
                        <td>{invoice.buyer?.name}</td>
                        <td>{invoice.price} Kč</td>
                        <td>
                            <div className="btn-group">
                                <Link
                                    to={`/invoices/show/${invoice._id}`}
                                    className="btn btn-sm btn-secondary"
                                >
                                    Zobrazit
                                </Link>
                            </div>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
            {invoices.length > itemsPerPage && (
            <ReactPaginate
                previousLabel={"Předchozí"}
                nextLabel={"Další"}
                breakLabel={"..."}
                breakClassName={"page-item"}
                breakLinkClassName={"page-link"}
                pageCount={pageCount}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={handlePageClick}
                containerClassName={"pagination justify-content-center"}
                pageClassName={"page-item"}
                pageLinkClassName={"page-link"}
                previousClassName={"page-item"}
                previousLinkClassName={"page-link"}
                nextClassName={"page-item"}
                nextLinkClassName={"page-link"}
                activeClassName={"active"}
            />
            )}
        </div>
    );
};

export default SalesTable;
