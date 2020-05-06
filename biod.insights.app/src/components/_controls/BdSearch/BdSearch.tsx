import { useDebouncedState } from 'hooks/useDebouncedState';
import React, { useCallback, useEffect } from 'react';
import { Input } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';

type BdSearchProps = {
  searchText: string;
  placeholder: string;
  debounceDelay?: number;
  onSearchTextChange: (text: string) => void;
};
export const BdSearch: React.FC<BdSearchProps> = ({
  searchText: searchTextSeed,
  placeholder,
  debounceDelay = 500,
  onSearchTextChange
}) => {
  const [
    searchText,
    searchTextDebounced,
    setSearchText,
    setSearchTextForceNoProxy
  ] = useDebouncedState(searchTextSeed || '', debounceDelay);

  const handleSearchTextOnChange = useCallback(
    event => {
      setSearchText(event.target.value);
    },
    [setSearchText]
  );
  const handleSearchTextReset = useCallback(() => {
    setSearchTextForceNoProxy('');
  }, [setSearchTextForceNoProxy]);

  useEffect(() => {
    onSearchTextChange && onSearchTextChange(searchTextDebounced);
  }, [onSearchTextChange, searchTextDebounced]);

  return (
    <Input
      icon
      className="bd-2-icons"
      value={searchText}
      onChange={handleSearchTextOnChange}
      placeholder={placeholder}
      fluid
      attached="top"
    >
      <BdIcon name="icon-search" className="prefix" color="sea100" bold />
      <input />
      {!!searchTextDebounced.length && (
        <BdIcon
          name="icon-close"
          className="suffix link b5780684"
          color="sea100"
          bold
          onClick={handleSearchTextReset}
        />
      )}
    </Input>
  );
};
