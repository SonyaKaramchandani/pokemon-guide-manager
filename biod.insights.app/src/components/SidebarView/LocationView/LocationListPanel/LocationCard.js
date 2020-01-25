/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { List, Header } from 'semantic-ui-react';
import { IconButton } from 'components/_controls/IconButton';
import LocationApi from 'api/LocationApi';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

function LocationCard({ selected, geonameId, name, country, canDelete, onSelect, onDelete }) {
  const [isDeleting, setIsDeleting] = useState(false);

  const handleDeleteUserLocation = (e, geonameId) => {
    e.stopPropagation();

    setIsDeleting(true);
    LocationApi.deleteUserLocation({ geonameId })
      .then(data => {
        onDelete(data);
      })
      .catch(() => setIsDeleting(false));
  };

  return (
    <List.Item
      active={selected === geonameId}
      onClick={() => onSelect(geonameId)}
      sx={{
        // TODO: d5f7224a
        cursor: 'pointer',
        '.ui.list &:hover': {
          borderRight: theme => `1px solid ${theme.colors.stone20}`,
          bg: t => t.colors.deepSea20,
          transition: '0.5s all',
          '& .suffix': {
            display: 'block'
          }
        },
        '.ui.list &.active,&:active': {
          borderRight: theme => `1px solid ${theme.colors.stone20}`,
          bg: t => t.colors.seafoam20
        },
        '& .suffix': {
          display: 'none'
        }
      }}
    >
      <FlexGroup
        alignItems="center"
        suffix={
          canDelete && (
            <IconButton
              icon="icon-close"
              color="sea100"
              onClick={e => handleDeleteUserLocation(e, geonameId)}
              disabled={isDeleting}
            />
          )
        }
      >
        <Typography variant="subtitle2" color="stone90">
          {name}
        </Typography>
        <Typography variant="body2" color="deepSea50">
          {country}
        </Typography>
      </FlexGroup>
    </List.Item>
  );
}

export default LocationCard;
