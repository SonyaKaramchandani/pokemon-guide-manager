import React from 'react';
import { Dropdown, Image } from 'semantic-ui-react';
import sortSvg from 'assets/sort.svg';

function SortBy({ defaultValue, options, onSelect }) {
  const handleChange = (_, { value }) => {
    onSelect(value);
  };

  return (
    <div
      style={{
        display: 'flex'
      }}
    >
      <span style={{ flexBasis: 40 }}>
        <Image src={sortSvg} alt="Sort" />
      </span>
      <span style={{ flexBasis: 70, color: '#ABB3CA' }}>Sort by</span>
      <Dropdown
        inline
        fluid
        options={options}
        defaultValue={defaultValue}
        onChange={handleChange}
      />
    </div>
  );
}

export default SortBy;
