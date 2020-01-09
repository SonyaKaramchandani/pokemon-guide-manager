/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { List, Grid, Divider } from 'semantic-ui-react';
import { getInterval, getTravellerInterval } from 'utils/stringFormatingHelpers';
import ImportationSvg from 'assets/importation.svg';

const RiskOfImportation = ({ importationRisk, airports }) => {
  const { minMagnitude, maxMagnitude, minProbability, maxProbability } = importationRisk;
  const probabilityText = getInterval(minProbability, maxProbability, '%');
  const magnitudeText = getTravellerInterval(minMagnitude, maxMagnitude, true);

  return (
    <div sx={{ p: 3 }}>
      <List relaxed>
        <List.Item>
          <List.Content>
            <Grid>
              <Grid.Row columns={2}>
                <Grid.Column>
                  <h4>Overall</h4>
                </Grid.Column>
                <Grid.Column textAlign="right">
                  <img src={ImportationSvg} alt="" />
                </Grid.Column>
              </Grid.Row>
            </Grid>
          </List.Content>
        </List.Item>
        <List.Item>
          <div>Likelyhood of importation</div>
          <h3>{probabilityText}</h3>
          <span>Overall likelihood of at least one imported infected traveller in one month</span>
        </List.Item>
        <List.Item>
          <div>Projected case importations</div>
          <h3>{magnitudeText}</h3>
          <span>Overall expected number of imported infected travellers in one month</span>
        </List.Item>
      </List>
      {!!airports.length && (
        <>
          <h4>Airports Near My Location</h4>
          <Divider />

          <Grid divided="vertically" columns={2}>
            <Grid.Row>
              <Grid.Column>Destination Airport</Grid.Column>
              <Grid.Column textAlign="right">
                Likelihood of importation
                <br />
                Projected case importations
              </Grid.Column>
            </Grid.Row>
            {airports.map(({ id, name, city, code, importationRisk }) => {
              let riskDescription = null;

              if (importationRisk) {
                const {
                  minMagnitude,
                  maxMagnitude,
                  minProbability,
                  maxProbability
                } = importationRisk;
                const probabilityText = getInterval(minProbability, maxProbability, '%');
                const magnitudeText = getTravellerInterval(minMagnitude, maxMagnitude, false);

                riskDescription = (
                  <Grid.Column textAlign="right">
                    <b>{probabilityText}</b>
                    <br />
                    <b>{magnitudeText}</b>
                  </Grid.Column>
                );
              }

              return (
                <Grid.Row key={id}>
                  <Grid.Column>
                    <h5>
                      {name} ({code})
                    </h5>
                    <div>{city}</div>
                  </Grid.Column>
                  {riskDescription}
                </Grid.Row>
              );
            })}
          </Grid>
        </>
      )}
    </div>
  );
};

export default RiskOfImportation;
