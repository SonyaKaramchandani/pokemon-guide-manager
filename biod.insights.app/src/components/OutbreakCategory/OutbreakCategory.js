/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Popup, Icon } from 'semantic-ui-react';

// todo: add location
// Sustained transmission of measles possible in <<Toronto>>

const getText = id => {
  if (id === 1 || id === 3) {
    return 'Sustained transmission possible';
  } else if (id === 2) {
    return 'Sporadic transmission possible';
  } else if (id === 4 || id === 5) {
    return 'Local transmission unlikely';
  } else if (id === 6) {
    return 'Local transmissibility unknown';
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

const OutbreakCategory = ({ outbreakPotentialCategory, diseaseInformation }) => {
  const id = outbreakPotentialCategory && outbreakPotentialCategory.id;
  const name = diseaseInformation && diseaseInformation.name;

  const text = getText(id),
    description = getDescription(id, name);
  return (
    <h4>
      {text}{' '}
      <Popup trigger={<Icon name="question circle outline" />} content={description} size="mini" />
    </h4>
  );
};

export default OutbreakCategory;
