/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Grid, Divider, List } from 'semantic-ui-react';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import ExportationSvg from 'assets/exportation.svg';

const totalVolume = airports => {
  if (!airports || !airports.length) return 0;
  return airports.map(a => a.volume).reduce((a, c) => a + c);
};

const RiskOfExportation = ({ exportationRisk, airports }) => {
  const { minMagnitude, maxMagnitude, minProbability, maxProbability } = exportationRisk;
  const probabilityText = getInterval(minProbability, maxProbability, '%');
  const magnitudeText = getTravellerInterval(minMagnitude, maxMagnitude, true);

  return (
    <div sx={{ p: 3 }}>
      <List divided relaxed>
        <List.Item>
          <List.Content>
            <Grid>
              <Grid.Row columns={2}>
                <Grid.Column>
                  <h4>Overall</h4>
                </Grid.Column>
                <Grid.Column textAlign="right">
                  <img src={ExportationSvg} alt="" />
                </Grid.Column>
              </Grid.Row>
            </Grid>
          </List.Content>
        </List.Item>
        <List.Item>
          <div>Likelyhood of exportation</div>
          <h3>{probabilityText}</h3>
          <span>Overall likelihood of at least one exported infected traveller in one month</span>
        </List.Item>

        <List.Item>
          <div>Projected case exportations</div>
          <h3>{magnitudeText}</h3>
          <span>Overall expected number of exported infected travellers in one month</span>
        </List.Item>
      </List>

      {!!airports.length && (
        <>
          <h4>Airports Near Outbreak Source</h4>
          <Divider />
          <Grid divided="vertically" columns={2}>
            <Grid.Row>
              <Grid.Column>Destination Airport</Grid.Column>
              <Grid.Column>Passenger volume to world</Grid.Column>
            </Grid.Row>
            {airports.map(({ id, name, code, city, volume }) => (
              <Grid.Row key={id}>
                <Grid.Column>
                  <h5>
                    {name} ({code})
                  </h5>
                  {city}
                </Grid.Column>
                <Grid.Column textAlign="right">{volume}</Grid.Column>
              </Grid.Row>
            ))}
            <Grid.Row>
              <Grid.Column>
                <h5>Total outbound volume from outbreak areas to world</h5>
              </Grid.Column>
              <Grid.Column textAlign="right">{totalVolume(airports)}</Grid.Column>
            </Grid.Row>
          </Grid>
        </>
      )}
    </div>
  );
};

export default RiskOfExportation;
