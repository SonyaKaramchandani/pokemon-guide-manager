/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { formatDate } from 'utils/dateTimeHelpers';

const FullReferenceSources = ({ sources, label }) => (
  <>
    <Typography variant="caption" color="deepSea50">
      {label}
    </Typography>
    <Typography variant="overline" color="stone70">
      {sources.join(' / ')}
    </Typography>
  </>
);

const MiniReferenceSources = ({ sources }) => {
  let content = '';
  if (sources.length <= 3) content = sources.join(' / ');
  else content = [...sources.slice(0, 3), ` + ${sources.length - 3} source(s)`].join(' / ');

  return (
    <Typography variant="overline" color="stone70">
      {content}
    </Typography>
  );
};

const ReferenceSources = ({ articles, label = 'With sources from:', mini = false }) => {
  const sources = [...new Set((articles || []).map(a => a.sourceName).filter(i => i && i.length))];
  if (!sources.length) return null;

  return mini ? (
    <MiniReferenceSources sources={sources} />
  ) : (
    <FullReferenceSources label={label} sources={sources} />
  );
};

export default ReferenceSources;
