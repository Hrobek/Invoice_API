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

// Importing required libraries and components
import React from "react";
import "bootstrap/dist/css/bootstrap.min.css"; // Importing Bootstrap for styling
import {
  BrowserRouter as Router, // Router component to enable routing
  Link, // Link component to navigate between pages
  Route, // Route component to define route paths
  Routes, // Routes component to define all routes in the application
  Navigate, // Navigate component to redirect to another route
} from "react-router-dom"; // Importing the routing library

// Importing components for different views
import PersonIndex from "./persons/PersonIndex"; // List of persons
import PersonDetail from "./persons/PersonDetail"; // Person details view
import PersonForm from "./persons/PersonForm"; // Form for creating or editing a person

import InvoiceIndex from "./invoices/InvoiceIndex"; // List of invoices
import InvoiceDetail from "./invoices/InvoiceDetail"; // Invoice details view
import InvoiceForm from "./invoices/InvoiceForm"; // Form for creating or editing an invoice
import PersonStatistics from "./statistics/PersonStatistics"; // Statistics for persons

// Main App component
export function App() {
  return (
    // Wrapping the entire application in a Router for routing functionality
    <Router>
      <div className="container">
        {/* Navbar with links to navigate between different sections of the app */}
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <ul className="navbar-nav mr-auto">
            {/* Link to the list of persons */}
            <li className="nav-item">
              <Link to={"/persons"} className="nav-link">
                Osoby
              </Link>
            </li>
            {/* Link to the list of invoices */}
            <li className="nav-item">
              <Link to={"/invoices"} className="nav-link">
                Faktury
              </Link>
            </li>
            {/* Link to the statistics page */}
            <li className="nav-item">
              <Link to={"/persons/statistics"} className="nav-link">
                Statistiky
              </Link>
            </li>
          </ul>
        </nav>

        {/* Defining all the routes for the application */}
        <Routes>
          {/* Redirecting the default route to the list of persons */}
          <Route index element={<Navigate to={"/persons"} />} />

          {/* Routes related to persons */}
          <Route path="/persons">
            {/* Default route to show the list of persons */}
            <Route index element={<PersonIndex />} />
            {/* Route to show the details of a specific person */}
            <Route path="show/:id" element={<PersonDetail />} />
            {/* Route to create a new person */}
            <Route path="create" element={<PersonForm />} />
            {/* Route to edit an existing person */}
            <Route path="edit/:id" element={<PersonForm />} />
            {/* Route to show person statistics */}
            <Route path="statistics" element={<PersonStatistics />} />
          </Route>

          {/* Routes related to invoices */}
          <Route path="/invoices">
            {/* Default route to show the list of invoices */}
            <Route index element={<InvoiceIndex />} />
            {/* Route to show the details of a specific invoice */}
            <Route path="show/:id" element={<InvoiceDetail />} />
            {/* Route to create a new invoice */}
            <Route path="create" element={<InvoiceForm />} />
            {/* Route to edit an existing invoice */}
            <Route path="edit/:id" element={<InvoiceForm />} />
          </Route>
        </Routes>
      </div>
    </Router>
  );
}

// Exporting the App component as the default export
export default App;
