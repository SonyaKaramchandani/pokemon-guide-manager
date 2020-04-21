/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React, { useState } from 'react';
import { Popup } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { RiskLikelihood } from 'models/RiskCategories';
import { valignHackBottom } from 'utils/cssHelpers';
import { getInterval } from 'utils/modelHelpers';
import { isMobile, isNonMobile } from 'utils/responsive';
import { valueof } from 'utils/typeHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';

import SvgRiskBars_NegligibleLight from 'assets/RiskBars/risk-bars-negligible-light.svg';
import SvgRiskBars_NegligibleDark from 'assets/RiskBars/risk-bars-negligible-dark.svg';
import SvgRiskBars_LowLight from 'assets/RiskBars/risk-bars-low-light.svg';
import SvgRiskBars_LowDark from 'assets/RiskBars/risk-bars-low-dark.svg';
import SvgRiskBars_ModerateLight from 'assets/RiskBars/risk-bars-moderate-light.svg';
import SvgRiskBars_ModerateDark from 'assets/RiskBars/risk-bars-moderate-dark.svg';
import SvgRiskBars_HighLight from 'assets/RiskBars/risk-bars-high-light.svg';
import SvgRiskBars_HighDark from 'assets/RiskBars/risk-bars-high-dark.svg';
import SvgRiskBars_VeryHigh from 'assets/RiskBars/risk-bars-very-high.svg';

type IconMapping = {
  img: string;
  imgDark: string;
  title: string;
  numbers: string;
};
const IconMappings: Partial<{ [key in RiskLikelihood]: IconMapping }> = {
  'Not calculated': {
    img: SvgRiskBars_NegligibleLight,
    imgDark: SvgRiskBars_NegligibleDark,
    title: 'Not calculated',
    numbers: 'Not calculated'
  },
  Unlikely: {
    img: SvgRiskBars_NegligibleLight,
    imgDark: SvgRiskBars_NegligibleDark,
    title: 'Unlikely',
    numbers: '<1%'
  },
  Low: {
    img: SvgRiskBars_LowLight,
    imgDark: SvgRiskBars_LowDark,
    title: 'Low Likelihood',
    numbers: '1% to 10%'
  },
  Moderate: {
    img: SvgRiskBars_ModerateLight,
    imgDark: SvgRiskBars_ModerateDark,
    title: 'Moderate Likelihood',
    numbers: '11% to 50%'
  },
  High: {
    img: SvgRiskBars_HighLight,
    imgDark: SvgRiskBars_HighDark,
    title: 'High Likelihood',
    numbers: '51% to 90%'
  },
  'Very High': {
    img: SvgRiskBars_VeryHigh,
    imgDark: SvgRiskBars_VeryHigh,
    title: 'Very High Likelihood',
    numbers: '91% to 100%'
  }
};

interface ProbabilityIconsProps {
  importationRisk: dto.RiskModel;
  exportationRisk: dto.RiskModel;
}

const ProbabilityIcons: React.FC<ProbabilityIconsProps> = ({
  importationRisk,
  exportationRisk
}) => {
  const [isOpen, setIsOpen] = useState(false);
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());

  if (!(importationRisk || exportationRisk)) {
    return null;
  }

  const { isModelNotRun, minProbability, maxProbability } = importationRisk || exportationRisk;
  // if (isModelNotRun) {
  // return '-'
  // }

  const isImportation = !!importationRisk;

  const probabilityText = getInterval(minProbability, maxProbability, isModelNotRun);

  const iconMapping = IconMappings[probabilityText];
  const textContent = isImportation
    ? `Overall probability of at least one imported infected traveller in one month`
    : `Overall probability of at least one exported infected traveller in one month`;

  const iconsComponent = isPopup => (
    <span sx={{ whiteSpace: 'nowrap' }}>
      <img
        src={isPopup ? iconMapping.imgDark : iconMapping.img}
        height="16"
        alt=""
        sx={{
          verticalAlign: 'baseline !important',
          minWidth: '12px'
        }}
      />
      <BdIcon
        color={isPopup ? 'stone10' : 'deepSea50'}
        name={
          probabilityText === 'Not calculated'
            ? 'icon-not-calculated-export'
            : isImportation
            ? 'icon-plane-import'
            : 'icon-plane-export'
        }
        sx={{
          '&.icon.bd-icon': {
            // LESSON: need a more specific CSS selector because BdIcon already injects its own CSS
            mx: '2px',
            fontSize: '18px'
            // ...valignHackBottom('-1px')
          }
        }}
      />
    </span>
  );

  return (
    <span>
      <Popup
        // TODO: 68382fe1: many props are duplicate for these popup controls. Refactor setIsOpen hook to a common ancestor control and destructure-assign shared atomic props
        // pinned open // DEBUG only!

        wide
        open={isOpen}
        onOpen={e => {
          setIsOpen(true);
          e && e.stopPropagation && e.stopPropagation();
        }}
        onClose={e => {
          if (!isOpen) return;
          setIsOpen(false);
          e && e.nativeEvent && e.stopPropagation && e.stopPropagation();
        }}
        onClick={e => {
          setIsOpen(false);
          e && e.stopPropagation && e.stopPropagation();
        }}
        position="left center"
        trigger={iconsComponent(false)}
        hideOnScroll
        closeOnDocumentClick
        closeOnEscape
        closeOnPortalMouseLeave
        closeOnTriggerBlur
        closeOnTriggerClick
        closeOnTriggerMouseLeave
        className="prob-icons"
        offset="-4px, 0"
        on={isNonMobileDevice ? ['hover', 'focus', 'click'] : ['click']}
        popperModifiers={{ preventOverflow: { boundariesElement: 'window' } }}
        // popperModifiers={{
        //   preventOverflow: isNonMobileDevice
        //     ? { boundariesElement: 'window' } // NOTE: this will prevent constraining of tooltips by the closest overflow:auto parent. (LINK: https://github.com/Semantic-Org/Semantic-UI-React/issues/3687#issuecomment-508046784)
        //     : { enabled: true },
        // }}
      >
        {probabilityText !== 'Not calculated' && (
          <Typography variant="subtitle2" color="stone10" marginBottom="8px">
            {iconMapping.title}
          </Typography>
        )}
        <FlexGroup prefix={iconsComponent(true)} alignItems="flex-start" gutter="2px">
          <Typography variant="subtitle2" color="stone10">
            {iconMapping.numbers}
          </Typography>
        </FlexGroup>
        <Typography variant="caption" color="stone10" marginBottom="0">
          {textContent}
        </Typography>
      </Popup>
    </span>
  );
};

export default ProbabilityIcons;
