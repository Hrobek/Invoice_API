// InvoiceStatistic.js
import React, { useEffect, useState } from 'react'; // Importing React and hooks for state and effect handling
import { apiGet } from '../utils/api'; // Importing the utility function for making GET requests to the API

const InvoiceStatistic = () => {
  const [statistics, setStatistics] = useState(""); // State to store the invoice statistics data

  // useEffect hook to fetch invoice statistics when the component mounts
  useEffect(() => {
    apiGet('/api/invoices/statistics').then((data) => setStatistics(data)); // Fetch the statistics and set them in state
  }, []); // Empty dependency array ensures this effect runs only once when the component mounts

  return (
    <div>
      {/* Displaying invoice statistics in a row with three columns */}
      <div className='row'>
        {/* Current year's balance */}
        <div className='col-md-4'>
          <strong>Bilance za tento rok:</strong> {statistics.currentYearSum} Kč
        </div>
        
        {/* All-time balance */}
        <div className='col-md-4'>
          <strong>Bilance za celé období: </strong>{statistics.allTimeSum} Kč
        </div>
        
        {/* Number of invoices */}
        <div className='col-md-4'>
          <strong>Počet faktur: </strong>{statistics.invoicesCount}
        </div>
      </div>
    </div>
  );
};

export default InvoiceStatistic; // Exporting the component for use in other parts of the application
