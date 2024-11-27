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
import {useNavigate, useParams} from "react-router-dom";

import {apiGet, apiPost, apiPut} from "../utils/api";

import InputField from "../components/InputField";
import FlashMessage from "../components/FlashMessage";
import InputSelect from '../components/InputSelect';


const InvoiceForm = () => {
    const navigate = useNavigate();
    const {id} = useParams();
    const [invoice, setInvoice] = useState({
        invoiceNumber: 0,
        buyerId: 0, // Buyer as ID
        sellerId: 0, // Seller as ID
        issued: "",
        date: "",
        product: "",
        price: 0,
        vat: 0,
        note: "",
    });
    const [persons, setPersons] = useState([]);
    const [sentState, setSent] = useState(false);
    const [successState, setSuccess] = useState(false);
    const [errorState, setError] = useState(null);

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
    }, [id]);

    const handleSubmit = (e) => {
        e.preventDefault();
        (id ? apiPut("/api/invoices/" + id, invoice) : apiPost("/api/invoices", invoice))
            .then((data) => {
                setSent(true);
                setSuccess(true);
                navigate("/invoices");
            })
            .catch((error) => {
                console.log(error.message);
                setError(error.message);
                setSent(true);
                setSuccess(false);
            });
    };

    const sent = sentState;
    const success = successState;

    return (
        <div>
            <h1>{id ? "Upravit" : "Vytvořit"} fakturu</h1>
            <hr/>
            {errorState ? (
                <div className="alert alert-danger">{errorState}</div>
            ) : null}
            {sent && (
                <FlashMessage
                    theme={success ? "success" : ""}
                    text={success ? "Uložení faktury proběhlo úspěšně." : ""}
                />
            )}
            <form onSubmit={handleSubmit}>
                <InputField
                    required={true}
                    type="text"
                    name="invoiceNumber"
                    min="3"
                    label="Číslo faktury"
                    prompt="Zadejte číslo faktury"
                    value={invoice.invoiceNumber}
                    handleChange={(e) => {
                        setInvoice({...invoice, invoiceNumber: e.target.value});
                    }}
                />

                <InputSelect
                    name = "seller"
                    items = {persons}
                    label = "Dodavatel"
                    prompt = "Vyber dodavatele"
                    value = {invoice.sellerId}
                    handleChange={(e) => {
                        setInvoice({...invoice, sellerId: e.target.value});
                    }}
                />

                <InputSelect
                    name = "buyer"
                    items = {persons}
                    label = "Odběratel"
                    prompt = "Vyber odběratele"
                    value = {invoice.buyerId}
                    handleChange={(e) => {
                        setInvoice({...invoice, buyerId: e.target.value});
                    }}
                />
                
                
                <InputField
                    required={true}
                    type="date"
                    name="issued"
                    label="Datum vytvoření"
                    prompt="Zadejte datum vytvoření"
                    value={invoice.issued}
                    handleChange={(e) => {
                        setInvoice({...invoice, issued: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="date"
                    name="date"
                    label="Datum splatnosti"
                    prompt="Zadejte datum splatnosti"
                    value={invoice.date}
                    handleChange={(e) => {
                        setInvoice({...invoice, date: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="text"
                    name="product"
                    min="3"
                    label="Název produktu"
                    prompt="Zadejte název produktu"
                    value={invoice.product}
                    handleChange={(e) => {
                        setInvoice({...invoice, product: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="text"
                    name="price"
                    min="3"
                    label="Cena"
                    prompt="Zadejte cenu produktu"
                    value={invoice.price}
                    handleChange={(e) => {
                        setInvoice({...invoice, price: e.target.value});
                    }}
                />
                <InputField
                    required={true}
                    type="text"
                    name="vat"
                    label="DPH"
                    prompt="Zadejte DPH"
                    value={invoice.vat}
                    handleChange={(e) => {
                        setInvoice({...invoice, vat: e.target.value});
                    }}
                />
                <InputField
                    required={true}
                    type="text"
                    name="note"
                    label="Poznámka"
                    prompt="Zadejte poznámku"
                    value={invoice.note}
                    handleChange={(e) => {
                        setInvoice({...invoice, note: e.target.value});
                    }}
                />

                <input type="submit" className="btn btn-primary" value="Uložit"/>
            </form>
        </div>
    );
};

export default InvoiceForm;
