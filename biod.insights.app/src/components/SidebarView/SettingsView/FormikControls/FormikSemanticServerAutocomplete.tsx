/** @jsx jsx */
import { useField } from 'formik';
import { useDebounceEffect } from 'hooks/useDebounceEffect';
import React, { useEffect, useState } from 'react';
import { Search } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { SemanticFormikProps } from './FormikSemanticProps';

export type SemanticFormikAutocompleteProps = SemanticFormikProps & {
  onSearch: (text: string) => Promise<any[]>;
  resultSelected?: (result) => void;
  debounceMs?: number;
  forceFirstFetch?: boolean;
};

export const FormikSemanticServerAutocomplete: React.FC<SemanticFormikAutocompleteProps> = ({
  name,
  placeholder,
  onSearch,
  debounceMs = 500,
  resultSelected,
  forceFirstFetch = false,
  error
}) => {
  const [field, meta, helpers] = useField(name);
  const { setValue, setTouched } = helpers;
  const { value } = field;

  const [isLoading, setIsLoading] = useState(false);
  const [searchText, setSearchText] = useState('');
  const [results, setResults] = useState([]);
  const [isFirstFetchPerformed, setIsFirstFetchPerformed] = useState(false);

  useDebounceEffect(
    text => {
      if (forceFirstFetch)
        if (
          text &&
          text.length >= 3 &&
          (text !== (value && value.title) || (forceFirstFetch && !isFirstFetchPerformed))
        ) {
          setIsLoading(true);
          onSearch(text)
            .then(results => {
              setIsFirstFetchPerformed(true);
              setIsLoading(false);
              setResults(results);
            })
            .catch(error => {
              setIsLoading(false);
              setResults([]);
            });
        }
    },
    searchText,
    debounceMs,
    [value]
  );

  useEffect(() => {
    if (value && value.title) setSearchText(value.title);
  }, [value]);

  return (
    <Search
      fluid
      input={{ fluid: true, error: error }}
      loading={isLoading}
      onResultSelect={(_, { result }) => setValue(result)}
      onSearchChange={(_, { value: newSearchText }) => {
        setSearchText(newSearchText);
        if (newSearchText === '' && !!value) setValue(null);
      }}
      onBlur={(_, { value }) => setTouched(true)}
      results={(results && results.length) > 0 ? results : value ? [value] : []}
      value={searchText}
      placeholder={placeholder}
    />
  );
};
