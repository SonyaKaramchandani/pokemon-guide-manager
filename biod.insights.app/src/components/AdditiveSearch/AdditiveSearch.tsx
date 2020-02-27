/** @jsx jsx */
import React, { useRef, useState } from 'react';
import { jsx } from 'theme-ui';

import { Search } from 'components/Search';
import { AdditiveSearchCategory } from 'components/Search/Search';

interface AdditiveSearchProps {
  onSearch: (val: string) => void;
  onAdd: (val: string) => void;
  onCancel: () => void;
  placeholder: string;
  categories: AdditiveSearchCategory[];
  isLoading: boolean;
  isAddInProgress: boolean;
  addButtonLabel: string;
  noResultsText: string;
}

const AdditiveSearch: React.FC<AdditiveSearchProps> = ({
  onSearch,
  onAdd,
  onCancel,
  placeholder,
  categories,
  isLoading,
  isAddInProgress = false,
  addButtonLabel = 'Add',
  noResultsText = 'No matching results'
}) => {
  const searchRef = useRef<any>(); // TODO: 7079dada: link these
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

  // TODO: 96b2c235: there is no need for and extra Search component, its not that generic since it handles categories (3a34785c)
  return (
    <Search
      ref={searchRef}
      placeholder={placeholder}
      isLoading={isLoading}
      categories={categories}
      onSearch={onSearch}
      onSelect={handleOnSelect}
      closeOnSelect={false}
      addButtonLabel={addButtonLabel}
      noResultsText={noResultsText}
      isAddDisabled={isAddDisabled}
      isAddInProgress={isAddInProgress}
      onResultCancel={handleOnCancel}
      onResultAdd={handleOnAdd}
    />
  );
};

export default AdditiveSearch;
