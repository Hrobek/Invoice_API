
import React, {useEffect, useState} from "react";

import {apiGet} from "../utils/api";

import StatisticTable from "./StatisticTable";

const PersonIndex = () => {
    const [persons, setPersons] = useState([]);


    useEffect(() => {
        apiGet("/api/persons").then((data) => setPersons(data));
    }, []);

    return (
        <div>
            <StatisticTable
                items={persons}
            />
        </div>
    );
};
export default PersonIndex;
