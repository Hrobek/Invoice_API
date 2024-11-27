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
import {useParams} from "react-router-dom";

import {apiGet} from "../utils/api";

const InvoiceDetail = () => {
    const {id} = useParams();
    const [invoice, setInvoice] = useState({});

    useEffect(() => {
        apiGet("/api/persons") // Replace with your actual endpoint to fetch persons
            .then((data) => setPersons(data))
            .catch((error) => {
                console.log(error);
                setError("Failed to load persons");
            });
        if (id) {
            apiGet("/api/invoices/" + id).then((data) => setInvoice(data));
        }
        // TODO: Add HTTP req.
    }, [id]);

    return (
        <>
            <div>
                <h1>Detail faktury</h1>
                <hr/>
                <h3>{invoice.invoiceNumber}</h3>
                <p>
                    <strong>Dodavatel:</strong>
                    <br/>
                    {invoice.seller?.name} ({invoice.seller?.identificationNumber})
                </p>
                <p>
                    <strong>Odběratel:</strong>
                    <br/>
                    {invoice.buyer?.name} ({invoice.buyer?.identificationNumber})
                </p>
                <p>
                    <strong>Vytvořeno:</strong>
                    <br/>
                    {invoice.issued? new Date(invoice.date).toLocaleDateString("cs-CZ") : "Neuvedeno"}
                </p>
                <p>
                    <strong>Splatnost:</strong>
                    <br/>
                    {invoice.date? new Date(invoice.date).toLocaleDateString("cs-CZ") : "Neuvedeno"}
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
