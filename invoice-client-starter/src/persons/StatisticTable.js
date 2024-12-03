
import React, { useEffect, useState } from 'react';
import { apiGet } from '../utils/api';

const StatisticTable= ({items}) => {
  const [statistics, setStatistics] = useState([]);

  console.log(statistics)
  useEffect(() => {
    apiGet('/api/persons/statistics').then((data) => setStatistics(data));
}, []);

return (
    <div>
      <h1>Statistiky osob</h1>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>#</th>
            <th>Jméno</th>
            <th>Fakturované příjmy</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item, index) => (
            <tr key={item._id}>
              <td>{index + 1}</td>
              <td>{item.name}</td>
              <td>{statistics[index]?.revenue} Kč</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default StatisticTable;