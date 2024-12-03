

import React, {useState, useEffect} from "react";
import {Link} from "react-router-dom";
import { apiGet } from "../utils/api";

const IdentificationNumberTable = ({ identificationNumber}) => {
    const [invoices, setInvoices] = useState([]);

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
                    <th colSpan={3}>Akce</th>
                </tr>
                </thead>
                <tbody>
                {invoices.map((invoice, index) => (
                    <tr key={index + 1}>
                        <td>{index + 1}</td>
                        <td>{invoice.invoiceNumber}</td>
                        <td>{invoice.buyer?.name}</td>
                        <td>
                            <div className="btn-group">
                                <Link
                                    to={`/invoices/show/${invoice._id}`}
                                    className="btn btn-sm btn-info"
                                >
                                    Zobrazit
                                </Link>
                            </div>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default IdentificationNumberTable;
