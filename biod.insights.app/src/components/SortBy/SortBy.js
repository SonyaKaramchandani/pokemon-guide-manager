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

  // TODO: 516031d7
  const trigger = (
    <FlexGroup prefix={
      <>
        <BdIcon name='icon-sort' sx={{ "&.icon.bd-icon": { verticalAlign: "text-bottom", color: 'deepSea50', fontSize: '20px' } }} />
        <Typography color='deepSea50' variant="body2" inline> Sort By</Typography>
      </>
    }
      suffix={
        <BdIcon name='icon-chevron-down' sx={{ "&.icon.bd-icon": { color: 'deepSea100', fontWeight: 'bold' } }} />
      }
    >
      <Typography color='stone90' variant="subtitle2" inline>{activeOptionName}</Typography>
    </FlexGroup>
  )

  return (

    <div><Dropdown
      className='selection'
      icon={null}
      trigger={trigger}
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