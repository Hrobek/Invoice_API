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
import { dateStringFormatter } from "../utils/dateStringFormatter";

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
                            <p>
                                <strong>Dodavatel:</strong>
                                <br/>
                                {invoice.seller?.name}
                            </p>
                            <p>
                                <strong>IČ:</strong> {invoice.seller?.identificationNumber} <strong>DIČ:</strong> {invoice.seller?.taxNumber}
                            </p>
                            <p>
                                <strong>Číslo účtu:</strong> {invoice.seller?.accountNumber}/{invoice.seller?.bankCode}
                            </p>
                            <p>
                                <strong>IBAN:</strong> {invoice.seller?.iban}
                            </p>
                            <p>
                                <strong>Tel. číslo:</strong> {invoice.seller?.telephone}
                            </p>
                            <p>
                                <strong>Email:</strong> {invoice.seller?.mail}
                            </p>
                            <p>
                                <strong>Ulice:</strong> {invoice.seller?.street}
                            </p>
                            <p>
                                <strong>Město:</strong> {invoice.seller?.city} {invoice.seller?.zip}
                            </p>
                        </div>
        
                            <div class="col-md-6 nameContainer p-3 bg-light border text-left">
                                <p>
                                    <strong>Odběratel:</strong>
                                    <br/>
                                    {invoice.buyer?.name}
                                </p>
                                <p>
                                    <strong>IČ:</strong> {invoice.buyer?.identificationNumber} <strong>DIČ:</strong> {invoice.buyer?.taxNumber}
                                </p>
                                <p>
                                    <strong>Číslo účtu:</strong> {invoice.buyer?.accountNumber}/{invoice.buyer?.bankCode}
                                </p>
                                <p>
                                    <strong>IBAN:</strong> {invoice.buyer?.iban}
                                </p>
                                <p>
                                    <strong>Tel. číslo:</strong> {invoice.buyer?.telephone}
                                </p>
                                <p>
                                    <strong>Email:</strong> {invoice.buyer?.mail}
                                </p>
                                <p>
                                    <strong>Ulice:</strong> {invoice.buyer?.street}
                                </p>
                                <p>
                                    <strong>Město:</strong> {invoice.buyer?.city} {invoice.buyer?.zip}
                                </p>
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
