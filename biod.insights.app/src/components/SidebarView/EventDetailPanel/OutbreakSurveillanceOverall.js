/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Grid, Statistic, Label } from 'semantic-ui-react';
import LocationType from 'domainTypes/LocationType';
import { SectionHeader } from 'components/_common/SectionHeader';

const OutbreakSurveillanceOverall = ({ caseCounts, eventLocations }) => {
  const { reportedCases, deaths } = caseCounts;

  return (
    <SectionHeader>Overall</SectionHeader>

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
