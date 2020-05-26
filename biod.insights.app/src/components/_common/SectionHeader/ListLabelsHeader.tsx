/** @jsx jsx */
import { jsx } from 'theme-ui';
import { sxtheme } from 'utils/cssHelpers';
import { Typography } from 'components/_common/Typography';

interface ListLabelsHeaderProps {
  lhs: string | string[];
  rhs: string | string[];
}

export const ListLabelsHeader: React.FC<ListLabelsHeaderProps> = ({ lhs, rhs }) => {
  const lhsArr = Array.isArray(lhs) ? lhs : lhs ? [lhs] : [];
  const rhsArr = Array.isArray(rhs) ? rhs : rhs ? [rhs] : [];
  return (
    <div
      sx={{
        borderBottom: sxtheme(t => `1px solid ${t.colors.stone20}`),
        pb: '6px',
        mb: '8px'
      }}
    >
      <div sx={{ display: 'flex' }}>
        <div sx={{ flexGrow: 1 }}>
          {lhsArr.map((x, i) => (
            <Typography variant="body2" color="deepSea50" key={i}>
              {x}
            </Typography>
          ))}
        </div>
        <div sx={{ flexGrow: 1, textAlign: 'right' }}>
          {rhsArr.map((x, i) => (
            <Typography variant="body2" color="deepSea50" key={i}>
              {x}
            </Typography>
          ))}
        </div>
      </div>
    </div>
  );
};

export default ListLabelsHeader;
