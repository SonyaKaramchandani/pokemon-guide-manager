/** @jsx jsx */
import { jsx } from 'theme-ui';

export const BdParagraph = ({ children }) => (
  <div
    sx={{
      mb: '1em',
      '&:last-child': {
        mb: 0
      }
    }}
  >
    {children}
  </div>
);

export default BdParagraph;
