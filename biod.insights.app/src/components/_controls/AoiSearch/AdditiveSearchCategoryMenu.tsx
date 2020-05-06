/** @jsx jsx */
import React from 'react';
import { Menu, ButtonGroup, Button } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { AdditiveSearchCategoryOption, AdditiveSearchCategory } from './models';
import NotFoundMessage from '../Misc/NotFoundMessage';
import { Typography } from 'components/_common/Typography';

interface SearchCategoryItemsProps {
  selectedId: number;
  name: string;
  options: AdditiveSearchCategoryOption[];
  onSelect: (id: number) => void;
}

const SearchCategoryItems: React.FC<SearchCategoryItemsProps> = ({
  selectedId,
  name,
  options,
  onSelect
}) => (
  <Menu.Item key={name}>
    <Menu.Header>{name}</Menu.Header>
    <Menu.Menu>
      {options.map(({ id, name, disabled }) => (
        <Menu.Item
          key={id}
          name={`${id}`}
          active={selectedId === id}
          onClick={() => onSelect(id)}
          disabled={disabled}
        >
          {name} {disabled ? `(Added)` : ''}
        </Menu.Item>
      ))}
    </Menu.Menu>
  </Menu.Item>
);

type AdditiveSearchCategoryMenuProps = {
  categories: AdditiveSearchCategory[];
  selectedId?: number;
  addButtonLabel?: string;
  noResultsText?: string;
  trayButtonsState?: 'enabled' | 'disabled' | 'busy';
  onSelect: (id: number) => void;
  onAdd: () => void;
  onCancel: () => void;
};

export const AdditiveSearchCategoryMenu: React.FC<AdditiveSearchCategoryMenuProps> = ({
  categories,
  selectedId,
  addButtonLabel = 'Add',
  noResultsText = 'No matching results',
  trayButtonsState = 'enabled',
  onSelect,
  onAdd,
  onCancel
}) => {
  const hasMatchingResults = !!categories.length;

  return (
    <Menu
      vertical
      attached
      fluid
      sx={{
        m: '0 !important',
        border: '0 !important'
      }}
    >
      {hasMatchingResults &&
        categories.map(
          ({ name, values }) =>
            values.length && (
              <SearchCategoryItems
                key={name}
                selectedId={selectedId}
                onSelect={onSelect}
                name={name}
                options={values}
              />
            )
        )}

      {!hasMatchingResults && (
        <Menu.Item>
          <NotFoundMessage text={noResultsText} />
        </Menu.Item>
      )}

      {hasMatchingResults && (
        <ButtonGroup
          className="additive-search"
          sx={{
            display: 'flex !important',
            position: 'sticky',
            bottom: '0',
            '.button': {
              borderBottom: [t => `1px solid ${t.colors.deepSea40}`, null]
            }
          }}
        >
          <Button disabled={trayButtonsState === 'busy'} onClick={onCancel}>
            <Typography variant="button" inline>
              Cancel
            </Typography>
          </Button>
          <Button
            data-testid="searchAddButton"
            className="add-button"
            loading={trayButtonsState === 'busy'}
            disabled={trayButtonsState !== 'enabled' || !hasMatchingResults}
            onClick={onAdd}
          >
            <Typography variant="button" inline>
              {addButtonLabel}
            </Typography>
          </Button>
        </ButtonGroup>
      )}
    </Menu>
  );
};
