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
import React, {useState} from "react";
import {Link} from "react-router-dom";

const PersonTable = ({label, items, deletePerson}) => {
    const [currentPage, setCurrentPage] = useState(0);
    const itemsPerPage = 10;

    // Výpočet aktuálních položek
    const offset = currentPage * itemsPerPage;
    const currentItems = items.slice(offset, offset + itemsPerPage);
    const pageCount = Math.ceil(items.length / itemsPerPage);

    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected);
    }
    return (
        <div>
            <p>
                {label} {items.length}
            </p>

            <table className="table table-bordered">
                <thead>
                <tr>
                    <th>#</th>
                    <th>Jméno</th>
                    <th colSpan={3}>Akce</th>
                </tr>
                </thead>
                <tbody>
                {currentItems.map((item, index) => (
                    <tr key={index + offset + 1}>
                        <td>{index + 1 + offset}</td>
                        <td>{item.name}</td>
                        <td>
                            <div className="btn-group">
                                <Link
                                    to={"/persons/show/" + item._id}
                                    className="btn btn-sm btn-info"
                                >
                                    Zobrazit
                                </Link>
                                <Link
                                    to={"/persons/edit/" + item._id}
                                    className="btn btn-sm btn-warning"
                                >
                                    Upravit
                                </Link>
                                <button
                                    onClick={() => deletePerson(item._id)}
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
            {items.length > itemsPerPage && (
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
            <Link to={"/persons/create"} className="btn btn-success">
                Nová osoba
            </Link>
        </div>
    );
};

export default PersonTable;
