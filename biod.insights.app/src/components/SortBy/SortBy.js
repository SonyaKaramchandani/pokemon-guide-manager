/** @jsx jsx */
import { jsx } from 'theme-ui';
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
      <div sx={{ flexBasis: 40 }}>
        <Image src={sortSvg} alt="Sort" />
      </div>
      <div sx={{ flexBasis: 80, color: 'sea60' }}>Sort by</div>
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
