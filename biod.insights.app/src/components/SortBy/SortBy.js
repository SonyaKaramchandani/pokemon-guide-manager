/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Dropdown, Image, Header } from 'semantic-ui-react';
import sortSvg from 'assets/sort.svg';
import ArrowDownSvg from 'assets/arrow-down.svg';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

function SortBy({ defaultValue, options, onSelect }) {
  const handleChange = (_, { value }) => {
    onSelect(value);
  };

  return (
    <div sx={{ alignItems: 'start', flexDirection: 'row' }}>
      <FlexGroup prefixImg={sortSvg}>
        <Typography color='deepSea50' variant="body2">Sort By</Typography>

      </FlexGroup>
      <Typography variant="subtitle2">{
        <Dropdown
          inline
          fluid
          options={options}
          defaultValue={defaultValue}
          onChange={handleChange}
        />
      }
      </Typography>
    </div>
  );
}

export default SortBy;
