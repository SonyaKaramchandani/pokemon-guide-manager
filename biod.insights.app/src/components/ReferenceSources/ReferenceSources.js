/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';

const FullReferenceSources = ({ sources, label }) => (
  <div sx={{ p: 3 }}>
    <div>{label}</div>
    <div>{sources.join(' / ')}</div>
  </div>
);

const MiniReferenceSources = ({ sources }) => {
  let content = '';
  if (sources.length <= 3) content = sources.join(' / ');
  else content = [...sources.slice(0, 3), ` + ${sources.length - 3} source(s)`].join(' / ');

  return <div>{content}</div>;
};

const ReferenceSources = ({ articles, label = 'Sources from', mini = false }) => {
  const sources = (articles || []).map(a => a.sourceName).filter(i => i && i.length);
  if (!sources.length) return '';

  return mini ? (
    <MiniReferenceSources sources={sources} />
  ) : (
    <FullReferenceSources label={label} sources={sources} />
  );
};

export default ReferenceSources;
