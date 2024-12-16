
import React, { useEffect, useState } from 'react';
import { apiGet } from '../utils/api';
import ReactPaginate from "react-paginate";
import { Link } from 'react-router-dom';

const StatisticTable= ({items}) => {
  const [statistics, setStatistics] = useState([]);
  const [currentPage, setCurrentPage] = useState(0);
    const itemsPerPage = 10;

    // Výpočet aktuálních položek
    const offset = currentPage * itemsPerPage;
    const currentItems = items.slice(offset, offset + itemsPerPage);
    const pageCount = Math.ceil(items.length / itemsPerPage);

    const handlePageClick = ({ selected }) => {
        setCurrentPage(selected);
    }

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
            <th>Akce</th>
          </tr>
        </thead>
        <tbody>
          {currentItems.map((item, index) => (
            <tr key={item._id}>
              <td>{index + 1 + offset}</td>
              <td>{item.name}</td>
              <td>{statistics[index]?.revenue} Kč</td>
              <td>
                <Link
                    to={"/persons/show/" + item._id}
                    className="btn btn-sm btn-secondary"
                >
                    Zobrazit
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {items.length > itemsPerPage && (
      <ReactPaginate
                previousLabel={"Předchozí"}
                nextLabel={"Další"}
                breakLabel={"..."}
                breakClassName={"page-item"}
                breakLinkClassName={"page-link"}
                pageCount={pageCount}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={handlePageClick}
                containerClassName={"pagination justify-content-center"}
                pageClassName={"page-item"}
                pageLinkClassName={"page-link"}
                previousClassName={"page-item"}
                previousLinkClassName={"page-link"}
                nextClassName={"page-item"}
                nextLinkClassName={"page-link"}
                activeClassName={"active"}
            />
      )}
    </div>
  );
};

export default StatisticTable;