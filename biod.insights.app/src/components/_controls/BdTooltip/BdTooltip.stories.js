/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import BdTooltip from './BdTooltip';
import { Grid } from 'semantic-ui-react';
import { Typography } from 'components/_common/Typography';

export default {
  title: 'Controls/BdTooltip'
};

export const caveats = () => (
  <>
    <div style={{ width: 370, padding: '10px' }}>
      <Typography variant="h1" color="stone100">
        <span>Tooltip trigger is</span>
        <BdTooltip text="Tool tip text via `text` property passed as a string.">
          <span style={{ color: 'red' }}> here</span>
        </BdTooltip>
      </Typography>
      <Typography variant="caption" color="stone50">
        NOTE: this ballon will show at the bottom because the top part of the page jams it up.
      </Typography>
    </div>

    <div style={{ width: 370, padding: '10px' }}>
      <Typography variant="h1" color="stone100">
        Typography not inline
      </Typography>
      <BdTooltip text="Tool tip text via `text` property passed as a string">
        <Typography variant="h1" color="sea90" sx={{ border: "1px solid black" }}>TRIGGER TEXT</Typography>
      </BdTooltip>
      <Typography variant="caption" color="stone50">
        NOTE: since trigger element spans full width, the balloon is in the middle
      </Typography>
    </div>

    <div style={{ width: 370, padding: '10px' }}>
      <Typography variant="h1" color="stone100">
        Typography inline
      </Typography>
      <BdTooltip text="Tool tip text via `text` property passed as a string">
        <Typography variant="h1" color="sea90" inline sx={{ border: "1px solid black" }}>TRIGGER TEXT</Typography>
      </BdTooltip>
    </div>

    <div style={{ width: 670, padding: '10px' }}>
      <Grid columns={2} divided='vertically'>
        <Grid.Row divided>
          <Grid.Column>
            <Typography variant="subtitle2" color="stone100">
              Typography not inline but place the BdTooltip inside
            </Typography>
            <Typography variant="h1" color="sea90">
              <BdTooltip text="Tool tip text via `text` property passed as a string">
                TRIGGER
              </BdTooltip>
            </Typography>
            <Typography variant="caption" color="stone50">
              NOTE: the balloon appears displaced here because the page jams it up from the left
            </Typography>
          </Grid.Column>
          <Grid.Column>
            <Typography variant="subtitle2" color="stone100">
              Typography not inline but place the BdTooltip inside
            </Typography>
            <Typography variant="h1" color="sea90">
              <BdTooltip text="Tool tip text via `text` property passed as a string">
                TRIGGER
              </BdTooltip>
            </Typography>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </div>
  </>
);
