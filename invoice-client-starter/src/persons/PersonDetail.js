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
import Country from "./Country";
import IdentificationNumberTable from "../invoices/IdentificationNumberTable";
import IdentificationNumberPurchasesTable from "../invoices/IdentifacitonNumberPurchasesTable";

const PersonDetail = () => {
    const {id} = useParams();
    const [person, setPerson] = useState({});

    useEffect(() => {
        if (id) {
            apiGet("/api/persons/" + id).then((data) => setPerson(data));
        }
        // TODO: Add HTTP req.
    }, [id]);
    const country = Country.CZECHIA === person.country ? "Česká republika" : "Slovensko";

    return (
        <>
            <div className="mt-3 container p-3 bg-light border text-left">
                <h1 className="text-center">Detail osoby</h1>
                <hr/>
                <h3>{person.name}</h3>

                
                <p>
                    <strong>IČO:</strong> {person.identificationNumber} <strong>DIČ:</strong> {person.taxNumber}
                </p>
                <p>
                    <strong>Bankovní účet:</strong>
                    <br/>
                    {person.accountNumber}/{person.bankCode} ({person.iban})
                </p>
                <p>
                    <strong>Tel. číslo:</strong>
                    <br/>
                    {person.telephone}
                </p>
                <p>
                    <strong>Email:</strong>
                    <br/>
                    {person.mail}
                </p>
                <p>
                    <strong>Sídlo:</strong>
                    <br/>
                    <div>{person.street}</div> 
                    <div>{person.city} {person.zip}</div>
                    <div>{country}</div>
                </p>
                <p>
                    <strong>Poznámka:</strong>
                    <br/>
                    {person.note}
                </p>
            </div>
            <div className="row mt-3">
                <div className="col-md-6">              
                <IdentificationNumberTable 
                    identificationNumber={person.identificationNumber}/>
                </div>
                <div className="col-md-6">
                    <IdentificationNumberPurchasesTable
                    identificationNumber={person.identificationNumber}/>
                </div>
            </div>
        </>
    );
};

export default PersonDetail;