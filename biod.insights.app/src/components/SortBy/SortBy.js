/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Dropdown, Image, Header, Accordion, Divider, Icon } from 'semantic-ui-react';
import sortSvg from 'assets/sort.svg';
import ArrowDownSvg from 'assets/arrow-down.svg';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

function SortBy({ defaultValue, options, onSelect }) {
  const handleChange = (_, { value }) => {
    onSelect(value);
  };

  const trigger = (
        <div sx={{ display: 'inline-flex', flexDirection: 'row', alignItems: 'baseline', verticalAlign: 'sub' }}>
          <Icon name='icon-sort' />
          <div sx={{ margin: '0px 8px' }}>
            <Typography color='deepSea50' variant="body2" inline> Sort By</Typography>
          </div>
        <Typography color='stone90' variant="subtitle2" inline>{defaultValue}</Typography>
        </div>
  )

  return (
    <div><Dropdown
      className='selection'
      icon={<Icon name='icon-chevron-down' />}
      trigger={trigger}
      fluid
      options={options}
      defaultValue={defaultValue}
      onChange={handleChange}
    />
      <Divider />
    </div>
  );
}

export default SortBy;