import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import plusSvg from './plus.svg';
import styles from './AddNewLocation.module.scss';
import LocationApi from 'api/LocationApi';
import Autosuggest from 'react-autosuggest';
import { useDispatch } from 'react-redux';
import { showSuccessNotification, showErrorNotification } from 'actions';

function AddNewLocation({ onAdd }) {
  const [isExpanded, setIsExpanded] = useState(false);
  const [isAddInProgress, setIsAddInProgress] = useState(false);

  const [value, setValue] = useState('');
  const [suggestions, setSuggestions] = useState([]);
  const [selectedSuggestion, setSelectedSuggestion] = useState({ geonameId: '' });

  const dispatch = useDispatch();

  const handleOnAdd = () => {
    const { geonameId } = selectedSuggestion;
    setIsAddInProgress(true);
    LocationApi.putUserLocation({ geonameId })
      .then(() => {
        dispatch(showSuccessNotification('Location addded'));
        onAdd({ ...selectedSuggestion });
      })
      .catch(() => {
        dispatch(showErrorNotification('Failed to add location'));
      })
      .finally(() => setIsAddInProgress(false));
  };

  const handleToggleAdd = () => {
    setIsExpanded(!isExpanded);
    setValue('');
    setSuggestions([]);
  };

  const isAddDisabled = !(selectedSuggestion && selectedSuggestion.geonameId);
  const shouldRenderSuggestions = value => value.length > 2;
  const onSuggestionsFetchRequested = ({ value }) => {
    LocationApi.search(value).then(({ data }) => {
      setSuggestions(data.geonames);
    });
  };
  const onSuggestionsClearRequested = () => setSuggestions([]);
  const getSuggestionValue = suggestion => {
    const { name } = suggestion;
    setSelectedSuggestion({ ...suggestion });
    return name;
  };
  const renderSuggestion = ({ name }) => name;
  const onChange = (_, { newValue }) => setValue(newValue);
  const renderInputComponent = inputProps => {
    return <input {...inputProps} className="form-control" />;
  };
  const inputProps = {
    placeholder: 'Enter a location',
    value,
    onChange
  };

  return (
    <>
      <Button
        variant="light"
        block
        className={`text-left ${styles.container}`}
        onClick={handleToggleAdd}
        disabled={isAddInProgress}
      >
        <img src={plusSvg} alt="plus" />
        <span className="pl-2">Add a new location</span>
      </Button>

      {isExpanded && (
        <div className="p-4">
          <Autosuggest
            suggestions={suggestions}
            shouldRenderSuggestions={shouldRenderSuggestions}
            onSuggestionsFetchRequested={onSuggestionsFetchRequested}
            onSuggestionsClearRequested={onSuggestionsClearRequested}
            getSuggestionValue={getSuggestionValue}
            renderSuggestion={renderSuggestion}
            renderInputComponent={renderInputComponent}
            inputProps={inputProps}
          />
          <div className="d-flex justify-content-end pt-2">
            <Button variant="light" onClick={handleToggleAdd}>
              Cancel
            </Button>
            <Button
              variant="primary"
              type="submit"
              disabled={isAddDisabled || isAddInProgress}
              onClick={handleOnAdd}
              className="ml-2"
            >
              Add
            </Button>
          </div>
        </div>
      )}
    </>
  );
}

export default AddNewLocation;
