/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Dropdown, Image, Header, Accordion, Divider, Icon } from 'semantic-ui-react';
import sortSvg from 'assets/sort.svg';
import ArrowDownSvg from 'assets/arrow-down.svg';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';

function SortBy({ selectedValue, options, onSelect }) {

  const handleChange = (_, { value }) => {
    onSelect(value);
  };
  const activeOption = options && options.find(x => x.value == selectedValue);
  const activeOptionName = activeOption && activeOption.text;

  // const trigger = (
  //       // <div sx={{ display: 'inline-flex', flexDirection: 'row', alignItems: 'baseline', verticalAlign: 'sub' }}>
  //       //   <div sx={{ margin: '0px 8px' }}>
  //       //   </div>
  //       // </div>
  // )

  return (
    <div><Dropdown
      className='selection'
      icon={<BdIcon name='icon-chevron-down' />}
      trigger={
        <FlexGroup prefix={
          <>
            <BdIcon name='icon-sort' />
            <Typography color='deepSea50' variant="body2" inline> Sort By</Typography>
          </>
        }>
          <Typography color='stone90' variant="subtitle2" inline>{activeOptionName}</Typography>
        </FlexGroup>
      }
      fluid
      options={options}
      defaultValue={selectedValue}
      onChange={handleChange}
    />
      <Divider />
    </div>
  );
}

export default SortBy;