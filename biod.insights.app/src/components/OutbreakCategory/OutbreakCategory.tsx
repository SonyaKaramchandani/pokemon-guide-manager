/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Message } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';
import * as dto from 'client/dto';

// todo: add location
// Sustained transmission of measles possible in <<Toronto>>

interface OutbreakCategoryProps {
  outbreakPotentialCategory: dto.OutbreakPotentialCategoryModel;
  diseaseInformation: dto.DiseaseInformationModel;
}

const getText = id =>
  id === dto.OutbreakPotentialCategory.Sustained ||
  id === dto.OutbreakPotentialCategory.NeedsMapSustained
    ? 'Potential for sustained local transmission'
    : id === dto.OutbreakPotentialCategory.Sporadic
    ? 'Potential for sporadic local transmission'
    : id === dto.OutbreakPotentialCategory.Unlikely ||
      id === dto.OutbreakPotentialCategory.NeedsMapUnlikely
    ? 'Potential for local transmission unlikely'
    : id === dto.OutbreakPotentialCategory.Unknown
    ? 'Unknown potential for local transmission'
    : '';

const getDescription = (id, diseaseName) =>
  id === dto.OutbreakPotentialCategory.Sustained ||
  id === dto.OutbreakPotentialCategory.NeedsMapSustained
    ? `Sustained transmission of ${diseaseName} is possible in one or more of your areas of interest.`
    : id === dto.OutbreakPotentialCategory.Sporadic
    ? `Sporadic transmission of ${diseaseName} is possible in one or more of your areas of interest.`
    : id === dto.OutbreakPotentialCategory.Unlikely ||
      id === dto.OutbreakPotentialCategory.NeedsMapUnlikely
    ? `${diseaseName} is unlikely to cause a local outbreak in one or more of your areas of interest, but may be seen in returning travellers.`
    : id === dto.OutbreakPotentialCategory.Unknown
    ? `Insufficient data about ${diseaseName}'s potential for local transmission.`
    : '';

const CategoriesWhereTrasmissionPossible = [
  dto.OutbreakPotentialCategory.Sustained,
  dto.OutbreakPotentialCategory.Sporadic,
  dto.OutbreakPotentialCategory.NeedsMapSustained
];

const isPossible = cat => !!(cat && CategoriesWhereTrasmissionPossible.includes(cat.id));

export const OutbreakCategoryStandAlone: React.FC<OutbreakCategoryProps> = ({
  outbreakPotentialCategory,
  diseaseInformation
}) => {
  const id = outbreakPotentialCategory && outbreakPotentialCategory.id;
  const name = diseaseInformation && diseaseInformation.name;

  const text = getText(id);
  const description = getDescription(id, name);

  return (
    <Typography
      variant="subtitle2"
      color={isPossible(outbreakPotentialCategory) ? 'deepSea50' : 'stone50'}
    >
      {text}
    </Typography>

    // <h4>
    //   {text}{' '}
    //   <Popup trigger={<Icon name="question circle outline" />} content={description} size="mini" />
    // </h4>
  );
};

export const OutbreakCategoryMessage: React.FC<OutbreakCategoryProps> = ({
  outbreakPotentialCategory,
  diseaseInformation
}) => (
  <Message
    attached="bottom"
    className={
      isPossible(outbreakPotentialCategory)
        ? 'bd-transmission-possible'
        : 'bd-transmission-unlikely'
    }
    sx={{
      '&.bd-transmission-possible': { bg: t => t.colors.sea20 },
      '&.bd-transmission-unlikely': { bg: t => t.colors.stone10 }
    }}
  >
    <OutbreakCategoryStandAlone
      outbreakPotentialCategory={outbreakPotentialCategory}
      diseaseInformation={diseaseInformation}
    />
  </Message>
);
