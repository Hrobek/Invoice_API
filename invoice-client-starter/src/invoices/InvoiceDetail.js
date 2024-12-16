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

import React, {useEffect, useState} from "react";
import {Link, useParams} from "react-router-dom";

import {apiGet} from "../utils/api";
import { dateStringFormatter } from "../utils/dateStringFormatter";
import PersonDetailTable from "../persons/PersonDetailTable";

const InvoiceDetail = () => {
    const {id} = useParams();
    const [invoice, setInvoice] = useState({});

    useEffect(() => {
            apiGet("/api/invoices/" + id)
            .then((data) => {
                setInvoice({
                invoiceNumber: data.invoiceNumber,
                seller: data.seller,
                buyer: data.buyer,
                issued: dateStringFormatter(data.issued, true),
                date: dateStringFormatter(data.date, true),
                product: data.product,
                price: data.price,
                vat: data.vat,
                note: data.note,
            });
        })
            .catch((error) => {
            console.error(error);
          });
        // TODO: Add HTTP req.
    }, [id]);

    return (
        <>
            <div>
                <h1>Detail faktury</h1>
                <hr/>
                <h3 className="text-center display-4">{invoice.invoiceNumber}</h3>
                <div className="container mt-5">
                    <div className="row">
                        <div className="col-md-6 nameContainer p-3 bg-light border text-left">
                            <PersonDetailTable
                            id={invoice.seller?._id}/>
                            <Link
                                    to={`/persons/show/${invoice.seller?._id}`}
                                    className="btn btn-sm btn-secondary"
                                >
                                    Zobrazit
                                </Link>
                        </div>
        
                            <div className="col-md-6 nameContainer p-3 bg-light border text-left">
                                <PersonDetailTable
                                id={invoice.buyer?._id}/>
                                <Link
                                    to={`/persons/show/${invoice.buyer?._id}`}
                                    className="btn btn-sm btn-secondary"
                                >
                                    Zobrazit
                                </Link>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="mt-4 p-3 bg-secondary text-white text-center">
                    <p>
                        <strong>Vytvořeno:</strong>
                        <br/>
                        {invoice.issued}
                    </p>
                    <p>
                        <strong>Splatnost:</strong>
                        <br/>
                        {invoice.date}
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
