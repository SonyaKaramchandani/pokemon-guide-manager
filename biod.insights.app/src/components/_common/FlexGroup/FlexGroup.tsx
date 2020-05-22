/** @jsx jsx */
import { jsx } from 'theme-ui';
import { FunctionComponent } from 'react';
import { Image } from 'semantic-ui-react';

interface FlexGroupProps {
  prefix?: any;
  suffix?: any;
  prefixImg?: string;
  suffixImg?: string;
  alignItems?: 'center' | 'baseline' | 'end' | 'flex-start' | 'flex-end';
  gutter?: string;
  stackable?: boolean | 'inverted';
}

export const FlexGroup: FunctionComponent<FlexGroupProps> = ({
  prefix,
  suffix,
  prefixImg,
  suffixImg,
  children,
  alignItems,
  gutter = '6px',
  stackable,
  ...props
}) => {
  return (
    <div
      {...props}
      sx={{
        display: 'flex',
        // justifyContent: 'space-between',
        alignItems: alignItems || 'baseline',
        ...(stackable && {
          flexDirection: [stackable === 'inverted' ? 'column-reverse' : 'column', 'row'],
          '& .prefix': {
            mr: [0, gutter]
          },
          '& .suffix': {
            ml: [0, gutter]
          }
        })
      }}
    >
      {(prefixImg && (
        <div className="prefix" sx={{ mr: gutter, minWidth: '10px', flexShrink: 0 }}>
          <Image src={prefixImg} />
        </div>
      )) ||
        (prefix && (
          <div className="prefix" sx={{ mr: gutter, minWidth: '10px', flexShrink: 0 }}>
            {prefix}
          </div>
        ))}
      <div sx={{ flexGrow: 1 }}>{children}</div>
      {(suffixImg && (
        <div className="suffix" sx={{ ml: gutter, minWidth: '10px', flexShrink: 0 }}>
          <Image src={suffixImg} />
        </div>
      )) ||
        (suffix && (
          <div className="suffix" sx={{ ml: gutter, minWidth: '10px', flexShrink: 0 }}>
            {suffix}
          </div>
        ))}
    </div>
  );
};

export default FlexGroup;
