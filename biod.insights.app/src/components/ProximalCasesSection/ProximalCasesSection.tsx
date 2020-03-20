/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Typography } from 'components/_common/Typography';
import { BdIcon } from 'components/_common/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { formatNumber } from 'utils/stringFormatingHelpers';
import * as dto from 'client/dto';

const ProximalCasesSection: React.FC<{
  localCaseCounts: dto.CaseCountModel;
}> = ({ localCaseCounts }) => {
  const { reportedCases } = localCaseCounts;

  if (reportedCases === 0) {
    return null;
  }

  return (
    <div
      sx={{
        color: 'clay100',
        bg: 'white',
        border: theme => `1px solid ${theme.colors.clay100}`,
        borderRadius: '2px',
        p: 2
      }}
    >
      <FlexGroup gutter="2px" prefix={<BdIcon nomargin color="clay100" name="icon-pin" />}>
        <Typography variant="subtitle1" sx={{ display: 'inline', verticalAlign: 'top' }}>
          {formatNumber(reportedCases, 'case')}
        </Typography>
        <Typography variant="body2" sx={{ display: 'inline', verticalAlign: 'top' }}>
          {' '}
          reported in or near your location
        </Typography>
      </FlexGroup>
    </div>
  );
};
export default ProximalCasesSection;
