/** @jsx jsx */
import React, { forwardRef, useImperativeHandle, useRef, useState } from 'react';
import { Button, ButtonGroup, Input, Menu } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';
import NotFoundMessage from 'components/_controls/Misc/NotFoundMessage';

export interface ISearchTextProps {
  searchText?;
  onSearchTextChanged?;
}

export interface AdditiveSearchCategory {
  name: string;
  values: AdditiveSearchCategoryOption[];
}

interface AdditiveSearchCategoryOption {
  id: number;
  name: string;
  disabled: boolean;
}

interface SearchProps {
  placeholder: string;
  isLoading: boolean;
  categories: AdditiveSearchCategory[];
  onSearch: (val: string) => void;
  onSelect: (val: string) => void;
  addButtonLabel: string;
  noResultsText: string;
  isAddDisabled: boolean;
  isAddInProgress: boolean;
  onResultCancel: () => void;
  onResultAdd: () => void;
  closeOnSelect: boolean;
}

//-------------------------------------------------------------------------------------------------------------------------------------

interface SearchCategoryItemsProps {
  selected: string;
  name: string;
  options: AdditiveSearchCategoryOption[];
  onSelect: (name: string) => void;
}

const SearchCategoryItems: React.FC<SearchCategoryItemsProps> = ({
  selected,
  name,
  options,
  onSelect
}) => {
  const handleClick = (_, { name }) => {
    onSelect(name);
  };

  return (
    <Menu.Item key={name}>
      <Menu.Header>{name}</Menu.Header>
      <Menu.Menu>
        {options.map(({ id, name, disabled }) => (
          <Menu.Item
            key={id}
            name={`${id}`}
            active={selected === `${id}`}
            onClick={handleClick}
            disabled={disabled}
          >
            {name} {disabled ? `(Added)` : ''}
          </Menu.Item>
        ))}
      </Menu.Menu>
    </Menu.Item>
  );
};

//-------------------------------------------------------------------------------------------------------------------------------------

// TODO: 96b2c235: there seems to be no use for this Search component given that it is only used in AdditiveSearch and handles categories (3a34785c). Move all this logic up to AdditiveSearch
const Search: React.FC<SearchProps> = (
  {
    placeholder,
    isLoading,
    categories,
    onSearch,
    onSelect,
    addButtonLabel = 'Add',
    noResultsText = 'No matching results',
    isAddDisabled,
    isAddInProgress,
    onResultCancel,
    onResultAdd,
    closeOnSelect = true
  },
  ref
) => {
  const [value, setValue] = useState('');
  const [selected, setSelected] = useState(null);
  const inputRef = useRef(null);

  // TODO: 7079dada: link these
  useImperativeHandle(ref, () => ({
    reset: () => {
      setValue('');
    }
  }));

  const handleChange = (_, { value }) => {
    setValue(value);
    onSearch(value);
  };

  const reset = () => {
    setValue('');
  };

  const handleOnSelect = (value: string) => {
    onSelect(value);
    setSelected(value);
    closeOnSelect && reset();
  };
  const handleCancelClick = () => {
    setSelected(null);
    onResultCancel();
  };

  const hasValue = !!value.length;
  const hasMatchingResults = hasValue && !!categories.length;
  const noMatchingResults = hasValue && !isLoading && !hasMatchingResults;

  return (
    <div sx={{ position: 'relative' }}>
      <Input
        data-testid="searchInput"
        icon={<BdIcon name="icon-plus" color="sea100" bold />}
        iconPosition="left"
        placeholder={placeholder}
        fluid
        value={value}
        onChange={handleChange}
        attached="top"
        ref={inputRef}
        loading={isLoading}
      />

      {(hasMatchingResults || noMatchingResults) && (
        <React.Fragment>
          <div
            sx={{
              boxShadow: [null, '0px 4px 4px rgba(0, 0, 0, 0.15)'],
              borderRadius: '4px',
              width: ['100%', '350px'],
              borderRightColor: '@stone20',
              bg: 'seafoam10',
              position: [null, 'absolute']
            }}
          >
            <Menu
              vertical
              attached
              fluid
              sx={{
                m: '0 !important',
                border: '0 !important'
              }}
            >
              {hasMatchingResults &&
                categories.map(
                  ({ name, values }) =>
                    values.length && (
                      <SearchCategoryItems
                        key={name}
                        selected={selected}
                        onSelect={handleOnSelect}
                        name={name}
                        options={values}
                      />
                    )
                )}

              {noMatchingResults && (
                <Menu.Item>
                  <NotFoundMessage text={noResultsText} />
                </Menu.Item>
              )}

              {hasMatchingResults && (
                <ButtonGroup
                  className="additive-search"
                  sx={{
                    display: 'flex !important',
                    position: 'sticky',
                    bottom: '0',
                    '.button': {
                      borderBottom: [t => `1px solid ${t.colors.deepSea40}`, null]
                    }
                  }}
                >
                  <Button disabled={isAddInProgress} onClick={handleCancelClick}>
                    <Typography variant="button" inline>
                      Cancel
                    </Typography>
                  </Button>
                  <Button
                    data-testid="searchAddButton"
                    className="add-button"
                    loading={isAddInProgress}
                    disabled={isAddDisabled || isAddInProgress || noMatchingResults}
                    onClick={onResultAdd}
                  >
                    <Typography variant="button" inline>
                      {addButtonLabel}
                    </Typography>
                  </Button>
                </ButtonGroup>
              )}
            </Menu>
          </div>
        </React.Fragment>
      )}
    </div>
  );
};

export default forwardRef(Search);
