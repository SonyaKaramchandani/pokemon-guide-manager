/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { SectionHeader } from './SectionHeader';
import { ListLabelsHeader } from './ListLabelsHeader';

export default {
  title: 'Common/SectionHeader'
};

export const test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <SectionHeader>Overall</SectionHeader>
    <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis reprehenderit laudantium ex sint asperiores voluptate fuga aut! Vitae accusantium voluptatum rem quaerat inventore, impedit iste porro enim dignissimos, dolorem ducimus!</p>
    <SectionHeader>Another Section</SectionHeader>
    <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis reprehenderit laudantium ex sint asperiores voluptate fuga aut! Vitae accusantium voluptatum rem quaerat inventore, impedit iste porro enim dignissimos, dolorem ducimus!</p>
    <SectionHeader icon="icon-plane-departure">With icon</SectionHeader>
    <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis reprehenderit laudantium ex sint asperiores voluptate fuga aut! Vitae accusantium voluptatum rem quaerat inventore, impedit iste porro enim dignissimos, dolorem ducimus!</p>
  </div>
);
export const testListLabels = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <ListLabelsHeader lhs={['Destination Airport']} rhs={['Passenger volume to world']} />
  </div>
);
