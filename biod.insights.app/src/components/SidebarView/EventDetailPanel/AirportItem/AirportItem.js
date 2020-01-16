/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';


// const totalVolume = airports => {
//   if (!airports || !airports.length) return 0;
//   return airports.map(a => a.volume).reduce((a, c) => a + c);
// };


// TODO: 8b061ee9
// dto: GetAirportModel
export const AirportImportationItem = ({ airport }) => {
  const { id, name, code, city, volume } = airport;
  return (
    <span>{name}</span>
    // <Grid>
    //   <Grid.Row key={id}>
    //     <Grid.Column>
    //       <h5>
    //         {name} ({code})
    //       </h5>
    //       {city}
    //     </Grid.Column>
    //     <Grid.Column textAlign="right">{volume}</Grid.Column>
    //   </Grid.Row>
    //   ))}
    //   <Grid.Row>
    //     <Grid.Column>
    //       <h5>Total outbound volume from outbreak areas to world</h5>
    //     </Grid.Column>
    //     {/* <Grid.Column textAlign="right">{totalVolume(airports)}</Grid.Column> */}
    //   </Grid.Row>
    // </Grid>
  );
};
