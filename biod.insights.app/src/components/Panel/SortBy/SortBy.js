import React from 'react';
import { Dropdown, Icon } from 'semantic-ui-react';

function SortBy({ defaultValue, options, onSelect }) {
  const handleChange = (_, { value }) => {
    onSelect(value);
  };

  return (
    <div
      style={{
        display: 'flex',
        justifyContent: 'space-between'
      }}
    >
      <span>
        <Icon name="sort"></Icon>
        <span>Sort by</span>{' '}
      </span>
      <Dropdown inline options={options} defaultValue={defaultValue} onChange={handleChange} />
    </div>
  );
}

export default SortBy;
