/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useRef, forwardRef, useImperativeHandle } from 'react';
import { Menu, Input } from 'semantic-ui-react';
import { BdIcon } from 'components/_common/BdIcon';

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
            {name} {disabled ? `(added)` : ''}
          </Menu.Item>
        ))}
      </Menu.Menu>
    </Menu.Item>
  );
};

const Search = (
  { placeholder, isLoading, categories, onSearch, onSelect, closeOnSelect = true, actions },
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

      {noMatchingResults && <div>No matching results</div>}

      {hasMatchingResults && (
        <div
          sx={{
            boxShadow: '0px 0px 16px rgba(0, 0, 0, 0.1)',
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
