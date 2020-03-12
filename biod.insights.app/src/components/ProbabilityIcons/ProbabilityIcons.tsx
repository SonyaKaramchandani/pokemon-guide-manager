/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { useBreakpointIndex } from '@theme-ui/match-media';
import { Header, Popup } from 'semantic-ui-react';
import { getInterval, getProbabilityName } from 'utils/stringFormatingHelpers';
import HighSvg from 'assets/high.svg';
import MediumSvg from 'assets/medium.svg';
import LowSvg from 'assets/low.svg';
import NoneSvg from 'assets/none.svg';
import MediumDarkSvg from 'assets/medium-dark.svg';
import LowDarkSvg from 'assets/low-dark.svg';
import NoneDarkSvg from 'assets/none-dark.svg';
import { valignHackBottom } from 'utils/cssHelpers';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import * as dto from 'client/dto';
import { isMobile, isNonMobile } from 'utils/responsive';

const IconMappings = {
  High: {
    img: HighSvg,
    imgDark: HighSvg,
    text: 'High probability'
  },
  Medium: {
    img: MediumSvg,
    imgDark: MediumDarkSvg,
    text: 'Medium probability'
  },
  Low: {
    img: LowSvg,
    imgDark: LowDarkSvg,
    text: 'Low probability'
  },
  None: {
    img: NoneSvg,
    imgDark: NoneDarkSvg,
    text: 'No probability'
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

  const probabilityText = getInterval(minProbability, maxProbability, '%', isModelNotRun);
  const probabilityName = getProbabilityName(maxProbability);

  const iconMapping = isModelNotRun ? IconMappings.None : IconMappings[probabilityName];
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
        name={isImportation ? 'icon-plane-arrival' : 'icon-plane-departure'}
        sx={{
          '&.icon.bd-icon': {
            // LESSON: need a more specific CSS selector because BdIcon already injects its own CSS
            mx: '2px',
            fontSize: '18px',
            ...valignHackBottom('-1px')
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
        <Popup.Header>
          <Typography variant="caption" color="stone10">
            {iconMapping.text}
          </Typography>
          <FlexGroup prefix={iconsComponent(true)} alignItems="flex-start" gutter="2px">
            <Typography variant="subtitle2" color="stone10">
              {probabilityText}
            </Typography>
          </FlexGroup>
        </Popup.Header>
        <Popup.Content>
          <Typography variant="caption" color="stone10">
            {textContent}
          </Typography>
        </Popup.Content>
      </Popup>
    </span>
  );
};

export default ProbabilityIcons;
