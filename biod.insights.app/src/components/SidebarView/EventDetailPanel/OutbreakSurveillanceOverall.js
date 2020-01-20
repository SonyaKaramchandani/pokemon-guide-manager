/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Grid, Statistic, Label, List } from 'semantic-ui-react';
import LocationType from 'domainTypes/LocationType';
import { SectionHeader } from 'components/_common/SectionHeader';
import { Typography } from 'components/_common/Typography';
import { formatNumber, locationTypePrint } from 'utils/stringFormatingHelpers';

const OutbreakSurveillanceOverall = ({ caseCounts, eventLocations }) => {
  const { reportedCases, deaths } = caseCounts;

  return (
    <>
      <SectionHeader>Overall</SectionHeader>
      <div sx={{ mb: '24px' }}>
        <Grid columns={2} divided='vertically'>
          <Grid.Row divided>
            <Grid.Column>
              <Typography variant="body2" color="stone90">Reported cases</Typography>
              <Typography variant="h1" color="stone90">{formatNumber(reportedCases)}</Typography>
            </Grid.Column>
            <Grid.Column>
              <Typography variant="body2" color="stone90">Reported deaths</Typography>
              <Typography variant="h1" color="stone90">{formatNumber(deaths)}</Typography>
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
            caseCounts: {
              hasReportedCasesNesting,
              reportedCases,
              deaths
            }
          }) => (
            <List.Item key={geonameId}>
              <Typography variant="subtitle2" color="deepSea70">{locationName}</Typography>
              <Typography variant="caption" color="deepSea50">{locationTypePrint(locationType)}</Typography>
              <Grid columns={2} divided='vertically'>
                <Grid.Row divided>
                  <Grid.Column>
                    <Typography variant="body2" color="stone90">Reported cases</Typography>
                    <Typography variant="subtitle1" color="stone90">{formatNumber(reportedCases)}</Typography>
                  </Grid.Column>
                  <Grid.Column>
                    <Typography variant="body2" color="stone90">Reported deaths</Typography>
                    <Typography variant="subtitle1" color="stone90">{formatNumber(deaths)}</Typography>
                  </Grid.Column>
                </Grid.Row>
              </Grid>
            </List.Item>
          )
        )}
      </List>
    </>
  )

  // return (
  //   <Grid divided columns={2}>
  //     <Grid.Row>
  //       <Grid.Column width={2}>
  //         <h4>Overall</h4>
  //       </Grid.Column>
  //     </Grid.Row>
  //     <Grid.Row columns="equal">
  //       <Grid.Column>
  //         <Statistic size="mini">
  //           <Statistic.Label>Reported cases</Statistic.Label>
  //           <Statistic.Value>{reportedCases}</Statistic.Value>
  //         </Statistic>
  //       </Grid.Column>
  //       <Grid.Column>
  //         <Statistic size="mini">
  //           <Statistic.Label>Reported deaths</Statistic.Label>
  //           <Statistic.Value>{deaths}</Statistic.Value>
  //         </Statistic>
  //       </Grid.Column>
  //     </Grid.Row>
  //     <Grid.Row columns={2}>
  //       <Grid.Column>
  //         <h4>By Locations</h4>
  //       </Grid.Column>
  //     </Grid.Row>
  //     {eventLocations.map(
  //       ({
  //         geonameId,
  //         locationName,
  //         locationType,
  //         caseCounts: { hasReportedCasesNesting, reportedCases, deaths }
  //       }) => (
  //         <React.Fragment key={geonameId}>
  //           <Grid.Row columns={1}>
  //             <Grid.Column>
  //               <b>{locationName}</b>
  //               <div>{LocationType[locationType]}</div>
  //             </Grid.Column>
  //           </Grid.Row>
  //           <Grid.Row columns={2}>
  //             <Grid.Column>
  //               <Statistic size="mini">
  //                 <Statistic.Label>Reported cases</Statistic.Label>
  //                 <Statistic.Value>
  //                   {hasReportedCasesNesting && <Label color="grey">{reportedCases}</Label>}
  //                   {!hasReportedCasesNesting && <>{reportedCases}</>}
  //                 </Statistic.Value>
  //               </Statistic>
  //             </Grid.Column>
  //             <Grid.Column>
  //               <Statistic size="mini">
  //                 <Statistic.Label>Reported deaths</Statistic.Label>
  //                 <Statistic.Value>{deaths}</Statistic.Value>
  //               </Statistic>
  //             </Grid.Column>
  //           </Grid.Row>
  //         </React.Fragment>
  //       )
  //     )}
  //   </Grid>
  // );
};

export default OutbreakSurveillanceOverall;
