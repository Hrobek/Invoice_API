// InvoiceStatistic.js
import React, { useEffect, useState } from 'react';
import { apiGet } from '../utils/api';

const InvoiceStatistic = () => {
  const [statistics, setStatistics] = useState("");
  
  useEffect(() => {
    apiGet('/api/invoices/statistics').then((data) => setStatistics(data));
    
}, []);

  return (
    <div>
      <div className='row'>
        <div className='col-md-4'><strong>Bilance za tento rok:</strong> {statistics.currentYearSum} Kč</div>
        <div className='col-md-6'><strong>Bilance za celé období: </strong>{statistics.allTimeSum} Kč</div>
      </div>
    </div>
  );
};

export default InvoiceStatistic;