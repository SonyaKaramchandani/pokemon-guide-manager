/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useRef, forwardRef, useImperativeHandle } from 'react';
import { Menu, Input } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

const SearchCategoryItems = ({ selected, name, options, onSelect }) => {
  if (!options.length) {
    return null;
  }

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
            {name} {disabled ? `(existing)` : ''}
          </Menu.Item>
        ))}
      </Menu.Menu>
    </Menu.Item>
  );
};

const Search = (
  {
    placeholder,
    isLoading,
    categories,
    minLength = 1,
    onSearch,
    onSelect,
    closeOnSelect = true,
    actions
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

    if (value.length >= minLength) {
      onSearch(value);
    }

    if (value.length === 0) {
      reset();
    }
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
        icon="plus"
        iconPosition="left"
        placeholder={placeholder}
        fluid
        value={value}
        onChange={handleChange}
        loading={isLoading}
        attached="top"
        sx={{ border: '0 !important' }}
        ref={inputRef}
      />

      {noMatchingResults && <div>No matching results</div>}

      {hasMatchingResults && (
        <div sx={{ position: 'absolute', bg: 'seafoam10', zIndex: 10000 }}>
          <Menu
            vertical
            fluid
            sx={{
              m: '0 !important',
              border: 0
            }}
          >
            {categories.map(({ name, values }) => (
              <SearchCategoryItems
                key={name}
                selected={selected}
                onSelect={handleOnSelect}
                name={name}
                options={values}
              />
            ))}

            {actions && <Menu.Item fitted>{actions}</Menu.Item>}
          </Menu>
        </div>
      )}
    </div>
  );
};

export default forwardRef(Search);
