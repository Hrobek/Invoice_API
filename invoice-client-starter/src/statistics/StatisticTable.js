import React, { useEffect, useState } from 'react'; // Importing React hooks for state and effect handling
import { apiGet } from '../utils/api'; // Importing the utility function for making GET requests to the API
import ReactPaginate from "react-paginate"; // Importing the React Paginate component for pagination
import { Link } from 'react-router-dom'; // Importing the Link component for navigation

// StatisticTable component to display a paginated table of statistics
const StatisticTable = ({ items }) => {
  const [statistics, setStatistics] = useState([]); // State to store the statistics data fetched from the API
  const [currentPage, setCurrentPage] = useState(0); // State to track the current page of the table
  const itemsPerPage = 10; // Number of items to show per page

  // Calculate the offset for the items to display on the current page
  const offset = currentPage * itemsPerPage;
  const currentItems = items.slice(offset, offset + itemsPerPage); // Get the items for the current page
  const pageCount = Math.ceil(items.length / itemsPerPage); // Calculate the total number of pages

  // Function to handle page changes in pagination
  const handlePageClick = ({ selected }) => {
    setCurrentPage(selected); // Update the current page when the user clicks on a pagination link
  }

  // useEffect hook to fetch statistics data when the component mounts
  useEffect(() => {
    apiGet('/api/persons/statistics').then((data) => setStatistics(data)); // Fetch the statistics and set them in state
  }, []); // Empty dependency array ensures this effect runs only once when the component mounts

  return (
    <div>
      <h1>Statistiky osob</h1> {/* Heading for the statistics table */}
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>#</th> {/* Index */}
            <th>Jméno</th> {/* Name */}
            <th>Fakturované příjmy</th> {/* Invoiced revenue */}
            <th>Akce</th> {/* Action button */}
          </tr>
        </thead>
        <tbody>
          {/* Map through the currentItems for the current page and render a table row for each item */}
          {currentItems.map((item, index) => (
            <tr key={item._id}>
              <td>{index + 1 + offset}</td> {/* Display the item index with the offset */}
              <td>{item.name}</td> {/* Display the person's name */}
              <td>{statistics[index]?.revenue} Kč</td> {/* Display the revenue from the statistics data */}
              <td>
                {/* Link to show the details of the person */}
                <Link
                  to={"/persons/show/" + item._id}
                  className="btn btn-sm btn-primary"
                >
                  Zobrazit
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Display pagination if there are more items than can fit on one page */}
      {items.length > itemsPerPage && (
        <ReactPaginate
          previousLabel={"Předchozí"} // Label for the "Previous" button
          nextLabel={"Další"} // Label for the "Next" button
          breakLabel={"..."} // Label for page breaks
          breakClassName={"page-item"} // Class for the page break item
          breakLinkClassName={"page-link"} // Class for the page break link
          pageCount={pageCount} // Total number of pages
          marginPagesDisplayed={2} // Number of pages to display on the edges of the pagination
          pageRangeDisplayed={5} // Number of pages to display in the middle
          onPageChange={handlePageClick} // Function to handle page changes
          containerClassName={"pagination justify-content-center"} // Styling for the pagination container
          pageClassName={"page-item"} // Class for individual page items
          pageLinkClassName={"page-link"} // Class for individual page links
          previousClassName={"page-item"} // Class for the "Previous" button
          previousLinkClassName={"page-link"} // Class for the "Previous" button link
          nextClassName={"page-item"} // Class for the "Next" button
          nextLinkClassName={"page-link"} // Class for the "Next" button link
          activeClassName={"active"} // Class for the active page
        />
      )}
    </div>
  );
};

export default StatisticTable; // Export the component for use in other parts of the application
