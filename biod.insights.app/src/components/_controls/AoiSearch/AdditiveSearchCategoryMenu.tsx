/** @jsx jsx */
import React from 'react';
import { Button, ButtonGroup, Menu } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { AdditiveSearchCategory, AdditiveSearchCategoryOption } from './models';

import { Typography } from 'components/_common/Typography';

import NotFoundMessage from '../Misc/NotFoundMessage';

interface SearchCategoryItemsProps<T> {
  selectedId: number;
  name: string;
  options: AdditiveSearchCategoryOption<T>[];
  onSelect: (object: T) => void;
}

function SearchCategoryItems<T>({
  selectedId,
  name,
  options,
  onSelect
}: React.PropsWithChildren<SearchCategoryItemsProps<T>>): React.ReactElement {
  return (
    <Menu.Item key={name}>
      <Menu.Header>{name}</Menu.Header>
      <Menu.Menu>
        {options.map(option => (
          <Menu.Item
            key={option.id}
            name={`${option.id}`}
            active={selectedId === option.id}
            onClick={() => onSelect(option.data)}
            disabled={option.disabled}
          >
            {option.name} {option.disabled ? `(Added)` : ''}
          </Menu.Item>
        ))}
      </Menu.Menu>
    </Menu.Item>
  );
}

type AdditiveSearchCategoryMenuProps<T> = {
  categories: AdditiveSearchCategory<T>[];
  selectedId?: number;
  addButtonLabel?: string;
  noResultsText?: string;
  trayButtonsState?: 'enabled' | 'disabled' | 'busy';
  onSelect: (object: T) => void;
  onAdd: () => void;
  onCancel: () => void;
};
export function AdditiveSearchCategoryMenu<T>({
  categories,
  selectedId,
  addButtonLabel = 'Add',
  noResultsText = 'No matching results',
  trayButtonsState = 'enabled',
  onSelect,
  onAdd,
  onCancel
}: React.PropsWithChildren<AdditiveSearchCategoryMenuProps<T>>): React.ReactElement {
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
              <SearchCategoryItems<T>
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
}
