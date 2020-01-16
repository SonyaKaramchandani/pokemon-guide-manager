import React from 'react';
import { List } from 'semantic-ui-react';
import { AirportImportationItem, AirportExportationItem } from './AirportItem';

export default {
  title: 'DiseaseEvent/AirportItem'
};

const airports = [
  {
    id: 6400,
    name: 'Benito Juarez International Airport',
    code: 'MEX',
    latitude: 19.4363,
    longitude: -99.0721,
    volume: 1487964,
    city: 'Mexico City, Mexico City, Mexico'
  },
  {
    id: 1711,
    name: 'Cancun International Airport',
    code: 'CUN',
    latitude: 21.03653,
    longitude: -86.87708,
    volume: 935912,
    city: 'Cancun, Quintana Roo, Mexico',
    importationRisk: {
      minMagnitude: 1,
      maxMagnitude: 2,
      minProbability: 5,
      maxProbability: 50
    }
  },
  {
    id: 6370,
    name: 'Manuel Crescencio Rejon International Airport',
    code: 'MID',
    latitude: 20.93698,
    longitude: -89.65767,
    volume: 111936,
    city: 'Merida, Yucatan, Mexico'
  },
  {
    id: 55,
    name: 'General Juan N. Alvarez International Airport',
    code: 'ACA',
    latitude: 16.75706,
    longitude: -99.75395,
    volume: 26363,
    city: 'Acapulco de Juarez, Guerrero, Mexico'
  }
];

export const importationItem = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List className="xunpadded">
      {airports.map(x => (
        <List.Item>
          <AirportImportationItem airport={x} />
        </List.Item>
      ))}
    </List>
  </div>
);

export const exportationItem = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List className="xunpadded">
      {airports.map(x => (
        <List.Item>
          <AirportExportationItem airport={x} />
        </List.Item>
      ))}
    </List>
  </div>
);
