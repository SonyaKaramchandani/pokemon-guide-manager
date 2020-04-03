/** @jsx jsx */
import classNames from 'classnames';
import { jsx, SxProps } from 'theme-ui';
import BdIcon, { InsightsIconLiteral } from 'components/_common/BdIcon/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { Divider } from 'semantic-ui-react';
import { IWithClassName } from 'components/_common/common-props';
import { VariantLiteral } from 'components/_common/Typography/Typography';

//-------------------------------------------------------------------------------------------------------------------------------------

type ModelParametersProps = IWithClassName & {
  compact?: boolean;
};

type ModelParameterProps = {
  compact?: boolean;
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

export const ModelParameters: React.FC<ModelParametersProps> = ({
  compact,
  children,
  ...props
}) => (
  <div
    {...props}
    className={classNames({
      'model-parameters': 1,
      compact: compact,
      [props.className]: 1
    })}
  >
    {children}
  </div>
);

export const ModelParameter: React.FC<ModelParameterProps> = ({
  icon,
  label,
  value,
  valueCaption,
  subParameter,
  compact
}) => {
  const labelTypography: VariantLiteral = compact ? 'caption' : 'body2';
  return (
    <div
      sx={{
        py: '16px',
        borderBottom: t => `1px solid ${t.colors.stone20}`,
        '&:first-child': {
          borderTop: t => `1px solid ${t.colors.stone20}`
        },
        '.model-parameters.compact &': {
          py: '6px'
        }
      }}
    >
      <FlexGroup
        alignItems="center"
        prefix={
          <div
            sx={{
              width: '59px',
              textAlign: 'center',
              '.model-parameters.compact &': {
                width: '40px'
              }
            }}
          >
            <BdIcon name={icon} color="deepSea90" />
          </div>
        }
      >
        <Typography variant={labelTypography} color="deepSea50">
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
        <FlexGroup
          prefix={
            <div
              sx={{
                width: '59px',
                '.model-parameters.compact &': {
                  width: '40px'
                }
              }}
            />
          }
        >
          <div>
            <Divider
              className="sublist"
              sx={{
                '&.ui.divider.sublist': {
                  my: '6px'
                }
              }}
            />
            <Typography variant={labelTypography} color="stone50">
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
};
