/** @jsx jsx */
import React from 'react';
import { action } from '@storybook/addon-actions';
import copyCodeBlock from '@pickra/copy-code-block';
import Typography, { TypographyVariants, TypographyColors } from './Typography';
import { jsx } from 'theme-ui';
import { Label, Header } from 'semantic-ui-react';
import { InsightsIconIds } from './insights-icons'

export default {
  title: 'Common/Typography'
};

const sampleText = "The quick brown fox jumps over the lazy dog";
const sampleText2 = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Debitis reprehenderit laudantium ex sint asperiores voluptate fuga aut! Vitae accusantium voluptatum rem quaerat inventore, impedit iste porro enim dignissimos, dolorem ducimus!";

export const variants = () => (
  <table sx={{ 'td': { border: "1px solid black" } }}>
    <tbody>
      <tr>
        <td sx={{color: "lightgray"}}>---null---</td>
        <td><Typography>{sampleText}</Typography></td>
      </tr>
      {Object.keys(TypographyVariants).map((variant, i) => (
        <tr key={i}>
          <td>{variant}</td>
          <td><Typography variant={variant}>{sampleText}</Typography></td>
        </tr>
      ))}
    </tbody>
  </table>
);

export const colors = () => (
  <table sx={{
    'td': { border: "1px solid black" },
    ':hover td.sample': { background: "black" },
  }}>
    <tbody>
      {Object.keys(TypographyColors).map((color, i) => (
        <tr key={i}>
          <td>{color}</td>
          <td><Label sx={{ background: t => `${t.colors[color]} !important` }}>&nbsp;</Label></td>
          <td className="sample"><Typography color={color} variant="button">{sampleText}</Typography></td>
        </tr>
      ))}
    </tbody>
  </table>
);

export const xReference = () => (
  <table sx={{
    'td': { border: "1px solid black" },
    ':hover td.sample': { background: "black" },
  }}>
    <thead>
      <tr>
        <td></td>
        {Object.keys(TypographyVariants).map((variant, j) => (
          <td key={`-${j}`}>{variant}</td>
        ))}
      </tr>
    </thead>
    <tbody>
      {Object.keys(TypographyColors).map((color, i) => (
        <tr key={i}>
          <td>{color}</td>
          {Object.keys(TypographyVariants).map((variant, j) => (
            <td className="sample" key={`${i}-${j}`}><Typography color={color} variant={variant}>ABC</Typography></td>
          ))}
        </tr>
      ))}
    </tbody>
  </table>
);

export const iconFontTest = () => (
  <table sx={{
    'td': { border: "1px solid black" },
  }}>
    <thead>
      <tr>
        <th>Icon</th>
        <th>Code</th>
      </tr>
    </thead>
    <tbody>
      {InsightsIconIds.map((icon, i) => (
        <tr key={i}>
          <td><i className={icon}></i></td>
          <td><code sx={{fontSize: "10px"}}>{`<i class="${icon}"></i>`}</code></td>
        </tr>
      ))}
    </tbody>
  </table>
);
