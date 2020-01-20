/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Header, Popup } from 'semantic-ui-react';
import { getInterval, getProbabilityName } from 'utils/stringFormatingHelpers';
import { FlexGroup } from 'components/_common/FlexGroup';
import HighSvg from 'assets/high.svg';
import MediumSvg from 'assets/medium.svg';
import LowSvg from 'assets/low.svg';
import NoneSvg from 'assets/none.svg';
import { BdIcon } from 'components/_common/BdIcon';
import { valignHackBottom } from 'utils/cssHelpers';

const IconMappings = {
  High: {
    img: HighSvg,
    text: 'High probability'
  },
  Medium: {
    img: MediumSvg,
    text: 'Medium probability'
  },
  Low: {
    img: LowSvg,
    text: 'Low probability'
  },
  None: {
    img: NoneSvg,
    text: 'No probability'
  }
};

const ProbabilityIcons = ({ importationRisk, exportationRisk }) => {
  if (!(importationRisk || exportationRisk)) {
    return null;
  }

  const { minProbability, maxProbability } = importationRisk || exportationRisk;
  const isImportation = !!importationRisk;

  const probabilityText = getInterval(minProbability, maxProbability, '%');
  const probabilityName = getProbabilityName(maxProbability);

  const iconMapping = IconMappings[probabilityName];
  const textContent = isImportation
    ? `Overall probability of at least one (1) imported infected traveller in one month`
    : `Overall probability of at least one (1) exported infected traveller in one month`;

  return (
    <span>
      <Popup
        basic
        trigger={
          <span sx={{ whiteSpace: 'nowrap' }}>
            <img src={iconMapping.img} height="16" alt="" sx={{
              verticalAlign: "baseline !important",
            }} />
            <BdIcon color="deepSea50" name={isImportation ? "icon-plane-arrival" : "icon-plane-departure"} sx={{
              // LESSON: need a more specific CSS selector because BdIcon already injects its own CSS
              '&.icon.bd-icon': {
                mx: "2px",
                fontSize: "18px",
                ...valignHackBottom('-1px'),
              },
            }} />
          </span>
        }
      >
        <Popup.Header>
          <FlexGroup prefixImg={iconMapping.img}>
            {iconMapping.text}
          </FlexGroup>
        </Popup.Header>
        <Popup.Content>
          <Header size="small">{probabilityText}</Header>
          {textContent}
        </Popup.Content>
      </Popup>
    </span>
  );
};

export default ProbabilityIcons;
