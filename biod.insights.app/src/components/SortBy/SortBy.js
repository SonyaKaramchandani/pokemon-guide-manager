/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Dropdown, Image, Header, Accordion, Divider } from 'semantic-ui-react';
import sortSvg from 'assets/sort.svg';
import ArrowDownSvg from 'assets/arrow-down.svg';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

function SortBy({ defaultValue, options, onSelect }) {
  const handleChange = (_, { value }) => {
    onSelect(value);
  };

  return (
    <div><Dropdown
      class='sortByMenu'
      icon='dropdown' //TODO: replace icon with bd font
      text={
        <div sx={{ display: 'flex', flexDirection: 'row', alignItems: 'baseline' }}>
          <Image src={sortSvg} />
          <div sx={{ margin: '0px 8px' }}>
            <Typography color='deepSea50' variant="body2" inline>Sort By</Typography>
          </div>
          <Typography color='stone90' variant="subtitle2" inline>{defaultValue}</Typography>
        </div>
      }
      fluid
      selection
      options={options}
      defaultValue={defaultValue}
      onChange={handleChange}
    />
      <Divider />
    </div>
  );
}

export default SortBy;