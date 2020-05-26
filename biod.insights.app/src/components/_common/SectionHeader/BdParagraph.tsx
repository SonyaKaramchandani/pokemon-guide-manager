/** @jsx jsx */
import { jsx } from 'theme-ui';

export const BdParagraph: React.FC = ({ children }) => (
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
