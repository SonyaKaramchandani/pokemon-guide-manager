/** @jsx jsx */
import React, { useEffect, useState } from 'react';
import { Card, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { ProximalCaseVM } from 'models/EventModels';
import { sxMixinActiveHover, sxtheme } from 'utils/cssHelpers';
import { formatNumber } from 'utils/stringFormatingHelpers';

import { BdIcon } from 'components/_common/BdIcon';
import { InsightsIconLiteral } from 'components/_common/BdIcon/BdIcon';
import { FlexGroup } from 'components/_common/FlexGroup';
import { Typography } from 'components/_common/Typography';
import { TypographyColor, VariantLiteral } from 'components/_common/Typography/Typography';
import { Accordian } from 'components/Accordian';
import { Loading } from 'components/Loading';
import { LoadingSVG } from 'components/Loading/Loading';

//=====================================================================================================================================

type ProximalSummaryRowProps = {
  title: string;
  stat: number;
  isHot?: boolean;
  isTotal?: boolean;
  strongBottomBorder?: boolean;
};

// NOTE: d332b7db: while these 2 look similar, there are minor stylistic differences
const ProximalSummaryRow: React.FC<ProximalSummaryRowProps> = ({
  title,
  stat,
  isHot = false,
  isTotal = false,
  strongBottomBorder = false
}) => {
  const color: TypographyColor = isHot ? 'lavender' : 'stone90';
  const icon: InsightsIconLiteral = isTotal ? null : isHot ? 'icon-pin' : 'icon-proximal-location';
  const textVariant: VariantLiteral = isTotal ? 'overline' : 'body2';

  return (
    <List.Item
      sx={{
        '.ui.list > &.item': {
          borderBottomColor: strongBottomBorder && (t => t.colors.stone50)
        }
      }}
    >
      <FlexGroup
        prefix={icon && <BdIcon name={icon} color={color} bold />}
        suffix={
          <Typography variant="subtitle1" color={color}>
            {stat ? formatNumber(stat) : '—'}
          </Typography>
        }
      >
        <Typography variant={textVariant} color={color}>
          {title}
        </Typography>
      </FlexGroup>
    </List.Item>
  );
};

const ProximalSummaryHeading: React.FC = ({ children }) => (
  <div
    sx={{
      borderBottom: sxtheme(t => `1px solid ${t.colors.stone20}`),
      pt: '16px',
      pb: '10px'
    }}
  >
    <Typography variant="subtitle2" color="stone90" marginBottom="0">
      {children}
    </Typography>
  </div>
);

const ProximalNoReportedActivity: React.FC = ({ children }) => (
  <List.Item>
    {/* <Typography variant="body2" color="stone50">
      No reported activity
    </Typography> */}
    <Typography variant="body2" color="stone90" sx={{ textAlign: 'center', fontStyle: 'italic' }}>
      No reported activity
    </Typography>
  </List.Item>
);

//=====================================================================================================================================

type ProximalCaseEntryProps = {
  title: string;
  subtitle?: string;
  stat: number;
  isHot?: boolean;
};

// NOTE: d332b7db: while these 2 look similar, there are minor stylistic differences
const ProximalCaseEntry: React.FC<ProximalCaseEntryProps> = ({
  title,
  subtitle,
  stat,
  isHot = false
}) => {
  const color: TypographyColor = isHot ? 'lavender' : 'deepSea70';
  const icon: InsightsIconLiteral = isHot ? 'icon-pin' : 'icon-proximal-location';
  return (
    <List.Item>
      <FlexGroup
        prefix={<BdIcon name={icon} color={color} bold />}
        suffix={
          <Typography variant="subtitle2" color={color}>
            {stat ? formatNumber(stat) : '—'}
          </Typography>
        }
      >
        <Typography variant="subtitle2" color={color}>
          {title}
        </Typography>
        {subtitle && (
          <Typography variant="caption" color="stone50">
            {subtitle}
          </Typography>
        )}
      </FlexGroup>
    </List.Item>
  );
};

//=====================================================================================================================================

export const ProximalCaseLoading: React.FC = ({}) => {
  return (
    <Card
      fluid
      sx={{
        '&.ui.card': {
          border: sxtheme(t => `1px solid ${t.colors.deepSea30}`)
        }
      }}
    >
      <Card.Content>
        <FlexGroup prefix={<LoadingSVG width={20} height={21} />} alignItems="center">
          <Typography variant="body2" color="lavender" inline>
            {/* <BdIcon name="icon-pin" /> */}
            <span>Calculating proximal cases...</span>
          </Typography>
        </FlexGroup>
      </Card.Content>
    </Card>
  );
};

//=====================================================================================================================================

export const ProximalCaseNoResult: React.FC = ({}) => {
  return (
    <Card
      fluid
      sx={{
        '&.ui.card': {
          border: sxtheme(t => `1px solid ${t.colors.deepSea30}`)
        }
      }}
    >
      <Card.Content>
        <Typography variant="body2" color="lavender" inline>
          <BdIcon name="icon-pin" />
          <span>No cases reported in or near your location</span>
        </Typography>
      </Card.Content>
    </Card>
  );
};

//=====================================================================================================================================

type ProximalCaseCardProps = {
  vm: ProximalCaseVM;
  isOpen?: boolean;
  onCardOpenedChanged?: (isOpen: boolean) => void;
};

export const ProximalCaseCard: React.FC<ProximalCaseCardProps> = ({
  vm,
  isOpen = false,
  onCardOpenedChanged
}) => {
  if (vm.totalCases === 0) return null;
  return (
    <Card
      fluid
      sx={{
        '&.ui.card': {
          border: sxtheme(t => `1px solid ${t.colors.deepSea30}`)
        }
      }}
    >
      <Card.Content
        sx={{
          '.ui.card > &.content': {
            py: 0
          }
        }}
      >
        <List className="xunpadded compact">
          <ProximalSummaryRow
            title="Cases reported in your location"
            stat={vm.totalCasesIn}
            isHot
          />
          <ProximalSummaryRow title="Cases reported near your location" stat={vm.totalCasesNear} />
          <ProximalSummaryRow title="Total Proximal cases" stat={vm.totalCases} isTotal />
        </List>
      </Card.Content>
      {/* <Card.Content>
        <Grid columns={2} divided="vertically">
          <Grid.Row divided>
            <Grid.Column>
              <Typography variant="caption" color="stone90">
                Reported in your location
              </Typography>
              <Typography variant="subtitle1" color="lavender">
                <BdIcon name="icon-proximal-location" />
                <span>22 cases</span>
              </Typography>
            </Grid.Column>
            <Grid.Column>
              <Typography variant="caption" color="stone90">
                Reported near your location
              </Typography>
              <Typography variant="subtitle1" color="deepSea70">
                <BdIcon name="icon-pin" />
                <span>78 cases</span>
              </Typography>
            </Grid.Column>
          </Grid.Row>
        </Grid>
      </Card.Content>
      <Card.Content>
        <FlexGroup
          suffix={
            <Typography variant="subtitle1" color="stone90">
              100 cases
            </Typography>
          }
        >
          <Typography variant="overline" color="stone90">
            Total Proximal cases
          </Typography>
        </FlexGroup>
      </Card.Content> */}
      <Accordian
        title="Proximal case details"
        xunpadContent
        yunpadContent
        expanded={isOpen}
        rhsChevron
        onExpanded={onCardOpenedChanged}
        sx={{
          '&:last-child': {
            borderBottom: 'none',
            borderTop: 'none'
          },
          '& .accordian-header': {
            bg: sxtheme(t => t.colors.deepSea10),
            borderTop: sxtheme(t => `1px solid ${t.colors.deepSea30}`),
            ...sxMixinActiveHover(),
            '&.accordian-open': {
              borderBottom: sxtheme(t => `1px solid ${t.colors.stone20}`)
            }
          }
        }}
      >
        <div sx={{ bg: sxtheme(t => t.colors.deepSea10) }}>
          <div sx={{ px: 3 }}>
            <List className="xunpadded">
              <ProximalSummaryHeading>
                Cases reported at the city-level within 100km of your location
              </ProximalSummaryHeading>
              {!(vm.casesCityLevel && vm.casesCityLevel.length) && <ProximalNoReportedActivity />}
              {vm.casesCityLevel.map(x => (
                <ProximalCaseEntry
                  key={x.locationId}
                  isHot={x.isWithinLocation}
                  title={x.locationName}
                  stat={x.proximalCases}
                />
              ))}
            </List>
          </div>
          <div sx={{ px: 3 }}>
            <List className="xunpadded">
              <ProximalSummaryHeading>
                Cases reported at the provincial/state and/or country-level within 100km of your
                location
              </ProximalSummaryHeading>
              {!(vm.casesProvinceCountryLevel && vm.casesProvinceCountryLevel.length) && (
                <ProximalNoReportedActivity />
              )}
              {vm.casesProvinceCountryLevel.map(x => (
                <ProximalCaseEntry
                  key={x.locationId}
                  isHot={x.isWithinLocation}
                  title={x.locationName}
                  stat={x.proximalCases}
                  subtitle={x.locationTypeSubtitle}
                />
              ))}
            </List>
          </div>
        </div>
      </Accordian>
    </Card>
  );
};