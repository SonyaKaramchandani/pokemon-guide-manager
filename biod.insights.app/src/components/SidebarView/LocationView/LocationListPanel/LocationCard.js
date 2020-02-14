/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { List, Header } from 'semantic-ui-react';
import { IconButton } from 'components/_controls/IconButton';
import LocationApi from 'api/LocationApi';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

function LocationCard({
  selected,
  geonameId,
  name,
  country,
  onSelect,
  canDelete,
  onDelete = null
}) {
  const [isDeleting, setIsDeleting] = useState(false);
  const isActive = (selected === geonameId);

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
      active={isActive}
      onClick={() => !isActive && onSelect(geonameId, name)}
      sx={{
        // TODO: d5f7224a
        cursor: 'pointer',
        '.ui.list &:hover': {
          bg: t => t.colors.deepSea20,
          transition: '0.5s all',
          '& .suffix': {
            display: 'block'
          }
        },
        '.ui.list &.active,&:active': {
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
