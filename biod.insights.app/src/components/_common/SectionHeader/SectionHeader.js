/** @jsx jsx */
import { jsx } from 'theme-ui';
import PropTypes from 'prop-types';
import { BdIcon, InsightsIconIds } from 'components/_common/BdIcon'
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

export const SectionHeader = ({ children, icon }) => {
  return (
    <div
      sx={{
        borderBottom: t => `1px solid ${t.colors.stone20}`,
        pb: '6px',
        mb: '8px'
      }}
    >
      <FlexGroup suffix={ icon && <BdIcon name={icon}/>}>
        <Typography variant="subtitle2" color="stone90">{children}</Typography>
      </FlexGroup>
    </div>
  );
};

SectionHeader.propTypes = {
  icon: PropTypes.oneOf(InsightsIconIds)
};

export default SectionHeader;
