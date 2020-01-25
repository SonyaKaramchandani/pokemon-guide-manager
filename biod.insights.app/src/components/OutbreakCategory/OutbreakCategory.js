/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Popup, Icon, Message } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

// todo: add location
// Sustained transmission of measles possible in <<Toronto>>

const getText = id => {
  if (id === 1 || id === 3) {
    return 'Potential for sustained local transmission';
  } else if (id === 2) {
    return 'Potential for sporadic local transmission';
  } else if (id === 4 || id === 5) {
    return 'Potential for local transmission unlikely';
  } else if (id === 6) {
    return 'Unknown potential for local transmission';
  }

  return '';
};

const getDescription = (id, diseaseName) => {
  if (id === 1 || id === 3) {
    return `Sustained transmission of ${diseaseName} is possible in one or more of your areas of interest.`;
  } else if (id === 2) {
    return `Sporadic transmission of ${diseaseName} is possible in one or more of your areas of interest.`;
  } else if (id === 4 || id === 5) {
    return `${diseaseName} is unlikely to cause a local outbreak in one or more of your areas of interest, but may be seen in returning travellers.`;
  } else if (id === 6) {
    return `Insufficient data about ${diseaseName}'s potential for local transmission.`;
  }

  return '';
};

const isPossible = cat => !!(cat && [1, 2, 3].includes(cat.id));

export const OutbreakCategory = ({ outbreakPotentialCategory, diseaseInformation }) => {
  const id = outbreakPotentialCategory && outbreakPotentialCategory.id;
  const name = diseaseInformation && diseaseInformation.name;

  const text = getText(id);
  const description = getDescription(id, name);

  return (
    // CODE: 144b395b: OutbreakCategoryMessage colors
    <Typography variant="subtitle2" color={isPossible(outbreakPotentialCategory) ? 'deepSea50' : 'stone50'}>
      {text}
    </Typography>

    // <h4>
    //   {text}{' '}
    //   <Popup trigger={<Icon name="question circle outline" />} content={description} size="mini" />
    // </h4>
  );
};

export const OutbreakCategoryMessage = ({ outbreakPotentialCategory, diseaseInformation }) => (
  <Message
    attached="bottom"
    className={isPossible(outbreakPotentialCategory) ? 'bd-transmission-possible' : 'bd-transmission-unlikely'}
    sx={{
      '&.bd-transmission-possible': { bg: t => t.colors.sea20 },
      '&.bd-transmission-unlikely': { bg: t => t.colors.stone10 },
    }}
  >
    <OutbreakCategory
      outbreakPotentialCategory={outbreakPotentialCategory}
      diseaseInformation={diseaseInformation}
    />
  </Message>
);
