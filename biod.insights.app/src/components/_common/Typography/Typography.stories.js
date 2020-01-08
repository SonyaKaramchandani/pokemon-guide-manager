/** @jsx jsx */
import React from 'react';
import { action } from '@storybook/addon-actions';
import Typography from './Typography';
import { jsx } from 'theme-ui';
import { Header } from 'semantic-ui-react';

export default {
  title: 'Typography'
};

const sampleText = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis reprehenderit laudantium ex sint asperiores voluptate fuga aut! Vitae accusantium voluptatum rem quaerat inventore, impedit iste porro enim dignissimos, dolorem ducimus!";

export const all = () => (
  <table sx={{ 'td': { border: "1px solid black" } }}>
    <tbody>
      <tr>
        <td sx={{color: "lightgray"}}>---null---</td>
        <td><Typography>{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>subtitle1</td>
        <td><Typography variant="subtitle1">{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>subtitle2</td>
        <td><Typography variant="subtitle2">{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>body1</td>
        <td><Typography variant="body1">{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>body2</td>
        <td><Typography variant="body2">{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>caption</td>
        <td><Typography variant="caption">{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>overline</td>
        <td><Typography variant="overline">{sampleText}</Typography></td>
      </tr>
      <tr>
        <td>button</td>
        <td><Typography variant="button">{sampleText}</Typography></td>
      </tr>
    </tbody>
  </table>
);
