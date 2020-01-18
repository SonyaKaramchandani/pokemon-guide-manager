/** @jsx jsx */
import { jsx } from 'theme-ui';
import { Typography } from 'components/_common/Typography';

export const ListLabelsHeader = ({ lhs, rhs }) => {
  const lhsArr = Array.isArray(lhs) ? lhs : lhs ? [lhs] : [];
  const rhsArr = Array.isArray(rhs) ? rhs : rhs ? [rhs] : [];
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
          {lhsArr.map(x => <Typography variant="body2" color="deepSea50">{x}</Typography>)}
        </div>
        <div sx={{ flexGrow: 1, textAlign: 'right' }}>
          {rhsArr.map(x => <Typography variant="body2" color="deepSea50">{x}</Typography>)}
        </div>
      </div>
    </div>
  );
};

export default ListLabelsHeader;
