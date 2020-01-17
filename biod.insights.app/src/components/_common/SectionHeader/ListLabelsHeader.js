/** @jsx jsx */
import { jsx } from 'theme-ui';
import PropTypes from 'prop-types';
import { BdIcon, InsightsIconIds } from 'components/_common/BdIcon'
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';

export const ListLabelsHeader = ({ children, icon }) => {
  return (
    <div
      sx={{
        borderBottom: t => `1px solid ${t.colors.stone20}`,
        pb: '6px',
        mb: '8px'
      }}
    >
      <div sx={{ display: 'flex' }}>
        <div sx={{ flexGrow: 1 }}>
          <Typography variant="body2" color="deepSea50">Destination Airport</Typography>
        </div>
        <div sx={{ flexGrow: 1, textAlign: 'right' }}>
          <Typography variant="body2" color="deepSea50">Likelihood of importation</Typography>
          <Typography variant="body2" color="deepSea50">Projected case importations</Typography>
        </div>
      </div>
    </div>
  );
};

export default ListLabelsHeader;
