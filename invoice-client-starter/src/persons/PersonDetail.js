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
import SalesTable from "../invoices/SalesTable";
import PurchasesTable from "../invoices/PurchasesTable";
import PersonDetailTable from "./PersonDetailTable";

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
            <div>
            <div className="mt-3 container p-3 bg-light border text-left">
                <PersonDetailTable
                id = {person._id}/>
            </div>
            </div>
            <button className="btn btn-secondary mb-3 mt-3 me-2" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseExample">
                Přijaté faktury
            </button>
            <button className="btn btn-secondary mb-3 mt-3" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo" aria-expanded="false" aria-controls="collapseExample">
                Vystavené faktury
            </button>
            <div className="row mt-3 " >
                <div className="col-md-6 collapse" id="collapseOne">              
                    <SalesTable 
                    identificationNumber={person.identificationNumber}/>
                </div>
                <div className="col-md-6 collapse" id="collapseTwo">
                    <PurchasesTable
                    identificationNumber={person.identificationNumber}/>
                </div>
            </div>

        </>
    );
};

export default PersonDetail;