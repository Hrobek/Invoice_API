import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";

import {apiGet, apiPost, apiPut} from "../utils/api";
import { dateStringFormatter } from '../utils/dateStringFormatter';

import InputField from "../components/InputField";
import FlashMessage from "../components/FlashMessage";
import InputSelect from '../components/InputSelect';
import "react-datepicker/dist/react-datepicker.css";

const InvoiceForm = () => {
    const {id} = useParams();
    const navigate = useNavigate();

    const [personListState, setPersonList] = useState([]);
    const [invoiceNumberState, setInvoiceNumber] = useState("");
    const [issuedState, setIssued] = useState("");
    const [dateState, setDate] = useState("");
    const [buyerState, setBuyer] = useState("");
    const [sellerState, setSeller] = useState("");
    const [productState, setProduct] = useState("");
    const [priceState, setPrice] = useState(0);
    const [vatState, setVat] = useState(0);
    const [noteState, setNote] = useState("");
    const [sentState, setSent] = useState(false);
    const [successState, setSuccess] = useState(false);
    const [errorState, setError] = useState();

    

    const handleChange = (e) => {
        const target = e.target;
      
        const name = target.name;
        const value = target.value;


        if (name === 'invoiceNumber') {console.log(value); setInvoiceNumber(value);}
        else if (name === 'buyer'){console.log(value); setBuyer(value);}
        else if (name === 'seller') {console.log(value); setSeller(value);}
        else if (name === 'issued') {console.log(value); setIssued(value);}
        else if (name === 'date') {console.log(value); setDate(value);}
        else if (name === 'product'){console.log(value); setProduct(value);}
        else if (name === 'price'){console.log(value); setPrice(value);}
        else if (name === 'vat'){console.log(value); setVat(value);}
        else if (name === 'note'){console.log(value); setNote(value)};
    };
      
      const handleSubmit = (e) => {
        e.preventDefault();

        const body = {
            invoiceNumber: invoiceNumberState,
            buyerId: buyerState,
            sellerId: sellerState,
            issued: issuedState,
            date: dateState,
            product: productState,
            price: priceState,
            vat: vatState,
            note: noteState,
        };


        (id
            ? apiPut('/api/invoices/' + id, body)
            :  apiPost('/api/invoices/', body)
        )
            .then((data) => {
                console.log('succcess', data)
                setSent(true);
                setSuccess(true);
                navigate('/invoices')

            })
            .catch((error) => {
                console.log(error.message);
                setError(error.message);
                setSent(true);
                setSuccess(false);
            });
    };
    
      useEffect(() => {
        apiGet('/api/persons/').then((data) => {
            setPersonList(data);
        if (id) {
            apiGet("/api/invoices/" + id).then((invoiceData) => {
                setInvoiceNumber(invoiceData.invoiceNumber);
                setIssued(dateStringFormatter(invoiceData.issued));
                setDate(dateStringFormatter(invoiceData.date));
                setBuyer(invoiceData.buyer._id);
                setSeller(invoiceData.seller._id);
                setProduct(invoiceData.product);
                setPrice(invoiceData.price);
                setVat(invoiceData.vat);
                setNote(invoiceData.note);
            });
        }
    });
    }, [id]);


    const sent = sentState;
    const success = successState;
    return (
        <div>
            <h1>{id ? "Upravit" : "Vytvořit"} fakturu</h1>
            <hr />
            {errorState ? <div className="alert alert-danger">{errorState}</div> : ""}
            {sent && success ? (
                <FlashMessage
                    theme={"success"}
                    text={"Uložení filmu proběhlo úspěšně."}
                />
            ): null}

            <form onSubmit={handleSubmit}>
                <InputField
                    required={true}
                    type="text"
                    name="invoiceNumber"
                    label="Číslo faktury"
                    prompt="Zadejte číslo faktury"
                    value={invoiceNumberState}
                    handleChange={handleChange}
                />

                <InputSelect
                    name = "seller"
                    items = {personListState}
                    label = "Dodavatel"
                    prompt = "Vyber dodavatele"
                    value = {sellerState}
                    handleChange={handleChange}
                />

                <InputSelect
                    name = "buyer"
                    items = {personListState}
                    label = "Odběratel"
                    prompt = "Vyber odběratele"
                    value = {buyerState}
                    handleChange={handleChange}
                />
                
                
                <InputField
                    required={true}
                    type="date"
                    name="issued"
                    label="Datum vytvoření"
                    prompt="Zadejte datum vytvoření"
                    min="0000-01-01"
                    value={issuedState}
                    handleChange={(e) => {
                        setIssued(e.target.value);
                        console.log(issuedState);
                    }}
                />

                <InputField
                    required={true}
                    type="date"
                    name="date"
                    label="Datum splatnosti"
                    prompt="Zadejte datum splatnosti"
                    min="0000-01-01"
                    value={dateState}
                    handleChange={(e) => {
                        setDate(e.target.value);
                        console.log(dateState);
                    }}
                />

                <InputField
                    required={true}
                    type="text"
                    name="product"
                    min="3"
                    label="Název produktu"
                    prompt="Zadejte název produktu"
                    value={productState}
                    handleChange={handleChange}
                />

                <InputField
                    required={true}
                    type="text"
                    name="price"
                    min="3"
                    label="Cena"
                    prompt="Zadejte cenu produktu"
                    value={priceState}
                    handleChange={handleChange}
                />
                <InputField
                    required={true}
                    type="text"
                    name="vat"
                    label="DPH"
                    prompt="Zadejte DPH"
                    value={vatState}
                    handleChange={handleChange}
                />
                <InputField
                    required={true}
                    type="text"
                    name="note"
                    label="Poznámka"
                    prompt="Zadejte poznámku"
                    value={noteState}
                    handleChange={handleChange}
                />
                <div className="mt-3 text-center">
                    <input type="submit" className="btn btn-primary btn-lg" value="Uložit"/>
                </div>
            </form>
        </div>
    );
};

export default InvoiceForm;
