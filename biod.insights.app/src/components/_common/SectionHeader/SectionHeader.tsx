/** @jsx jsx */
import { jsx } from 'theme-ui';
import { BdIcon } from 'components/_common/BdIcon';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { InsightsIconLiteral } from '../BdIcon/BdIcon';

export const SectionHeader: React.FC<{
  icon?: InsightsIconLiteral;
}> = ({ children, icon }) => (
  <div
    sx={{
      borderBottom: t => `1px solid ${t.colors.stone20}`,
      pb: '6px',
      mb: '8px'
    }}
  >
    <FlexGroup suffix={icon && <BdIcon name={icon} />} alignItems="end">
      <Typography variant="subtitle2" color="stone90">
        {children}
      </Typography>
    </FlexGroup>
  </div>
);

export default SectionHeader;
