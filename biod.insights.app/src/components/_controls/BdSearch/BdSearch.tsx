import { useDebouncedState } from 'hooks/useDebouncedState';
import React, { useCallback, useEffect, useImperativeHandle, forwardRef } from 'react';
import { Input } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';

export type BdSearchRefAttributes = {
  clear: () => void;
};
type BdSearchProps = {
  searchText?: string;
  onSearchTextChange: (text: string) => void;
  onNonDebouncedSearchTextChange?: (text: string) => void;
  placeholder: string;
  debounceDelay?: number;
  isLoading?: boolean;
};
export const BdSearch = forwardRef<BdSearchRefAttributes, BdSearchProps>(
  (
    {
      searchText: searchTextSeed,
      onSearchTextChange,
      onNonDebouncedSearchTextChange,
      placeholder,
      debounceDelay = 500,
      isLoading = false
    },
    ref
  ) => {
    useImperativeHandle(ref, () => ({
      clear: handleSearchTextReset
    }));

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

    useEffect(() => {
      onNonDebouncedSearchTextChange && onNonDebouncedSearchTextChange(searchText);
    }, [onNonDebouncedSearchTextChange, searchText]);

    return (
      <Input
        icon
        className="bd-2-icons"
        value={searchText}
        onChange={handleSearchTextOnChange}
        placeholder={placeholder}
        fluid
        attached="top"
        loading={isLoading}
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
  }
);
