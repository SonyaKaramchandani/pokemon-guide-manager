/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useRef } from 'react';
import { Search } from 'components/Search';
import { ButtonGroup, Button } from 'semantic-ui-react';

const AdditiveSearch = ({
  onSearch,
  onCancel,
  onAdd,
  placeholder,
  categories,
  isLoading,
  addButtonLabel = 'Add',
  isAddInProgress = false
}) => {
  const searchRef = useRef();
  const [selected, setSelected] = useState(null);

  const handleOnCancel = () => {
    searchRef.current.reset();
    setSelected(null);
    onCancel();
  };

  const handleOnSelect = value => {
    setSelected(value);
  };

  const handleOnAdd = () => {
    searchRef.current.reset();
    onAdd(selected);
  };

  const isAddDisabled = !selected;

  return (
    <Search
      ref={searchRef}
      placeholder={placeholder}
      isLoading={isLoading}
      categories={categories}
      onSearch={onSearch}
      onSelect={handleOnSelect}
      closeOnSelect={false}
      actions={
        <ButtonGroup attached="top">
          <Button basic color="grey" disabled={isAddInProgress} onClick={handleOnCancel}>
            Cancel
          </Button>
          <Button
            basic
            color="blue"
            loading={isAddInProgress}
            disabled={isAddDisabled || isAddInProgress}
            onClick={handleOnAdd}
          >
            {addButtonLabel}
          </Button>
        </ButtonGroup>
      }
    />
  );
};

export default AdditiveSearch;
