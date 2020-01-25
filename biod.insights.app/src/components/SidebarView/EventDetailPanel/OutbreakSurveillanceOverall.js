/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Grid, Statistic, Label, List } from 'semantic-ui-react';
import LocationType from 'domainTypes/LocationType';
import { SectionHeader } from 'components/_common/SectionHeader';
import { Typography } from 'components/_common/Typography';
import { formatNumber, locationTypePrint } from 'utils/stringFormatingHelpers';
import { CaseCountDisplayCases, CaseCountDisplayDeaths } from 'components/_controls/CaseCountDisplay';

// DTO: caseCounts: Biod.Insights.Api.Models.CaseCountModel
const OutbreakSurveillanceOverall = ({ caseCounts, eventLocations }) => {
  const { deaths, reportedCases } = caseCounts;
  return (
    <>
      <SectionHeader>Overall</SectionHeader>
      <div sx={{ mb: '24px' }}>
        <Grid columns={2} divided="vertically">
          <Grid.Row divided>
            <Grid.Column>
              <Typography variant="body2" color="stone90">
                Reported cases
              </Typography>
              <Typography variant="h1" color="stone90">
                <CaseCountDisplayCases caseCounts={caseCounts} />
              </Typography>
            </Grid.Column>
            <Grid.Column>
              <Typography variant="body2" color="stone90">
                Reported deaths
              </Typography>
              <Typography variant="h1" color="stone90">
                <CaseCountDisplayDeaths caseCounts={caseCounts} />
              </Typography>
            </Grid.Column>
          </Grid.Row>
        </Grid>
      </div>

      <SectionHeader>By Locations</SectionHeader>
      <List className="xunpadded">
        {eventLocations.map(
          ({
            geonameId,
            locationName,
            locationType,
            caseCounts
          }) => (
            <List.Item key={geonameId}>
              <Typography variant="subtitle2" color="deepSea70">
                {locationName}
              </Typography>
              <Typography variant="caption" color="deepSea50">
                {locationTypePrint(locationType)}
              </Typography>
              <Grid columns={2} divided="vertically">
                <Grid.Row divided>
                  <Grid.Column>
                    <Typography variant="body2" color="stone90">
                      Reported cases
                    </Typography>
                    <Typography variant="subtitle1" color="stone90">
                      <CaseCountDisplayCases caseCounts={caseCounts} />
                    </Typography>
                  </Grid.Column>
                  <Grid.Column>
                    <Typography variant="body2" color="stone90">
                      Reported deaths
                    </Typography>
                    <Typography variant="subtitle1" color="stone90">
                      <CaseCountDisplayDeaths caseCounts={caseCounts} />
                    </Typography>
                  </Grid.Column>
                </Grid.Row>
              </Grid>

            </List.Item>
          )
        )}
      </List>
    </>
  );
};

export default OutbreakSurveillanceOverall;
