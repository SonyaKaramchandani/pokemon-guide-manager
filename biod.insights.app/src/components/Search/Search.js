/** @jsx jsx */
import React, { forwardRef, useImperativeHandle, useRef, useState } from 'react';
import { Button, ButtonGroup, Input, Menu } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';

const SearchCategoryItems = ({ selected, name, options, onSelect }) => {

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
            {name} {disabled ? `(added)` : ''}
          </Menu.Item>
        ))}
      </Menu.Menu>
    </Menu.Item>
  );
};

// TODO: 96b2c235: there seems to be no use for this Search component given that it is only used in AdditiveSearch and handles categories (3a34785c). Move all this logic up to AdditiveSearch
const Search = (
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

  const handleOnSelect = value => {
    onSelect(value);
    setSelected(value);
    closeOnSelect && reset();
  };

  const hasValue = !!value.length;
  const hasMatchingResults = hasValue && !!categories.length;
  const noMatchingResults = hasValue && !isLoading && !hasMatchingResults;

  return (
    <div>
      <Input
        icon={<BdIcon name="icon-plus" color='sea100' bold />}
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
        <div
          sx={{
            boxShadow: '0px 4px 4px rgba(0, 0, 0, 0.15)',
            borderRadius: '4px',
            width: '350px',
            position: 'absolute',
            borderRightColor: '@stone20',
            bg: 'seafoam10'
          }}
        >
          <Menu
            vertical
            fluid
            sx={{
              m: '0 !important',
              border: 0
            }}
          >
            {hasMatchingResults && categories.map(({ name, values }) => values.length && (
              <SearchCategoryItems
                key={name}
                selected={selected}
                onSelect={handleOnSelect}
                name={name}
                options={values}
              />
            ))}

            {noMatchingResults && (
              <Menu.Item sx={{
                '.ui.menu &.item': {
                  textAlign: "center",
                  py: '64px'
                }
              }}>
                <div sx={{
                  fontSize: "20px",
                }}>
                  <BdIcon name="icon-search" color="deepSea50" />
                  {/* <BdIcon name="icon-close" color="deepSea50" sx={{
                    '&.icon.bd-icon': {
                      fontSize: '7px',
                      ...valignHackTop('-7px'),
                      right: '17px',
                    }
                  }}/> */}
                </div>
                <Typography variant="subtitle2" color="deepSea50">{noResultsText}</Typography>
              </Menu.Item>
            )}

            <Menu.Item fitted>
              <ButtonGroup attached="top" className="additive-search">
                <Button
                  disabled={isAddInProgress}
                  onClick={onResultCancel}
                >
                  <Typography variant="button" inline>Cancel</Typography>
                </Button>
                <Button
                  className="add-button"
                  loading={isAddInProgress}
                  disabled={isAddDisabled || isAddInProgress || noMatchingResults}
                  onClick={onResultAdd}
                >
                  <Typography variant="button" inline>{addButtonLabel}</Typography>
                </Button>
              </ButtonGroup>
            </Menu.Item>
          </Menu>
        </div>
      )}
    </div>
  );
};

export default forwardRef(Search);
