/** @jsx jsx */
import classNames from 'classnames';
import { jsx, SxProps } from 'theme-ui';
import BdIcon, { InsightsIconLiteral } from 'components/_common/BdIcon/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { Divider } from 'semantic-ui-react';

//-------------------------------------------------------------------------------------------------------------------------------------

export type ModelParametersProps = {
  icon: InsightsIconLiteral;
  label: string;
  value: string;
  valueCaption?: string;
  subParameter?: {
    label: string;
    value: string;
  };
};

//-------------------------------------------------------------------------------------------------------------------------------------

export const ModelParameters: React.FC<SxProps> = ({ children, ...props }) => (
  <div className="model-parameters" {...props}>
    {children}
  </div>
);

export const ModelParameter: React.FC<ModelParametersProps> = ({
  icon,
  label,
  value,
  valueCaption,
  subParameter
}) => (
  <div
    sx={{
      pt: '16px',
      pb: '16px',
      borderBottom: t => `1px solid ${t.colors.stone20}`,
      '&:first-child': {
        borderTop: t => `1px solid ${t.colors.stone20}`
      }
    }}
  >
    <FlexGroup
      alignItems="center"
      prefix={
        <div sx={{ width: '59px', textAlign: 'center' }}>
          <BdIcon name={icon} color="deepSea90" />
        </div>
      }
    >
      <Typography variant="body2" color="deepSea50">
        {label}
      </Typography>
      <Typography variant="subtitle1" color="stone90">
        {value}
        {valueCaption && (
          <Typography variant="body2" color="stone90" inline>
            {` (${valueCaption})`}
          </Typography>
        )}
      </Typography>
    </FlexGroup>
    {subParameter && (
      <FlexGroup prefix={<div sx={{ width: '59px' }} />}>
        <div>
          <Divider className="sublist" />
          <Typography variant="body2" color="stone50">
            {subParameter.label}
          </Typography>
          <Typography variant="subtitle1" color="stone90">
            {subParameter.value}
          </Typography>
        </div>
      </FlexGroup>
    )}
  </div>
);
