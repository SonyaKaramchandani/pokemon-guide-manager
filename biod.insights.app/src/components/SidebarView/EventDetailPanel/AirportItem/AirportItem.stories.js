import React from 'react';
import { List } from 'semantic-ui-react';
import { AirportImportationItem, AirportExportationItem } from './AirportItem';
import { mockAirports } from '__mocks__/dtoSamples';

export default {
  title: 'EventDetails/AirportItem'
};

export const importationItem = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List className="xunpadded">
      {mockAirports.map(x => (
        <List.Item key={x.id}>
          <AirportImportationItem airport={x} />
        </List.Item>
      ))}
    </List>
  </div>
);

export const exportationItem = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <List className="xunpadded">
      {mockAirports.map(x => (
        <List.Item key={x.id}>
          <AirportExportationItem airport={x} />
        </List.Item>
      ))}
    </List>
  </div>
);
