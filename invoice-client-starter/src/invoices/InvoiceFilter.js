import React from 'react'; 
import InputSelect from '../components/InputSelect'; // Import InputSelect component for dropdowns
import InputField from '../components/InputField'; // Import InputField component for text inputs

const InvoiceFilter = (props) => {

    // Handles changes in form fields and passes data back to parent
    const handleChange = (e) => {
        props.handleChange(e);
    };

    // Handles form submission
    const handleSubmit = (e) => {
        props.handleSubmit(e);
    };

    // Destructure filter prop to access filter values
    const filter = props.filter;

    return (
        <form onSubmit={handleSubmit}>
            {/* First row - Buyer, Seller, and Product */}
            <div className="row">
                <div className="col">
                    <InputSelect
                        name="buyerID"
                        items={props.personList} // List of persons (buyers/sellers)
                        handleChange={handleChange}
                        label="Odběratel" // Label for Buyer
                        prompt="nevybrán" // Default prompt text
                        value={filter._Id} // Value from filter for the buyer
                    />
                </div>
                <div className="col">
                    <InputSelect
                        name="sellerID"
                        items={props.personList} // List of persons (buyers/sellers)
                        handleChange={handleChange}
                        label="Dodavatel" // Label for Seller
                        prompt="nevybrán" // Default prompt text
                        value={filter._Id} // Value from filter for the seller
                    />
                </div>
                <div className="col">
                    <InputField
                        type="text"
                        name="product"
                        min="0"
                        handleChange={handleChange}
                        label="Produkt" // Label for product field
                        prompt="Neuveden" // Default prompt text
                        value={filter.product ? filter.product : ''} // Value from filter for the product
                    />
                </div>
            </div>

            {/* Second row - Price range and Limit */}
            <div className="row">
                <div className="col">
                    <InputField
                        type="number"
                        min="0"
                        name="minPrice"
                        handleChange={handleChange}
                        label="Min. cena" // Label for minimum price
                        prompt="neuveden" // Default prompt text
                        value={filter.minPrice ? filter.minPrice : ''} // Value from filter for minPrice
                    />
                </div>

                <div className="col">
                    <InputField
                        type="number"
                        min="0"
                        name="maxPrice"
                        handleChange={handleChange}
                        label="Max. cena" // Label for maximum price
                        prompt="neuveden" // Default prompt text
                        value={filter.maxPrice ? filter.maxPrice : ''} // Value from filter for maxPrice
                    />
                </div>

                <div className="col">
                    <InputField
                        type="number"
                        min="1"
                        name="limit"
                        handleChange={handleChange}
                        label="Limit počtu faktur" // Label for the invoice limit
                        prompt="neuveden" // Default prompt text
                        value={filter.limit ? filter.limit : ''} // Value from filter for limit
                    />
                </div>
            </div>

            {/* Submit Button */}
            <div className="row">
                <div className="col">
                    <input
                        type="submit"
                        className="btn btn-primary float-right mt-2"
                        value={props.confirm} // The text of the button (passed as prop)
                    />
                </div>
            </div>
        </form>
    );
};

export default InvoiceFilter;
