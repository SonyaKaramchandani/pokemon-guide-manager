/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Typography } from 'components/_common/Typography';
import * as dto from 'client/dto';

interface ReferenceSourcesProps {
  articles: dto.ArticleModel[];
  label?: string;
  mini?: boolean;
}

interface ReferenceSourceStringsProps {
  sources: string[];
  label?: string;
}

const FullReferenceSources: React.FC<ReferenceSourceStringsProps> = ({ sources, label }) => (
  <React.Fragment>
    <Typography variant="caption" color="deepSea50">
      {label}
    </Typography>
    <Typography variant="overline" color="stone70">
      {sources.join(' / ')}
    </Typography>
  </React.Fragment>
);

const MiniReferenceSources: React.FC<ReferenceSourceStringsProps> = ({ sources }) => {
  let content = '';
  if (sources.length <= 3) content = sources.join(' / ');
  else content = [...sources.slice(0, 3), ` + ${sources.length - 3} source(s)`].join(' / ');

  return (
    <Typography variant="overline" color="stone70" marginBottom="0">
      {content}
    </Typography>
  );
};

const ReferenceSources: React.FC<ReferenceSourcesProps> = ({
  articles,
  label = 'With sources from:',
  mini = false
}) => {
  const sources = [...new Set((articles || []).map(a => a.sourceName).filter(i => i && i.length))];
  if (!sources.length) return null;

  return mini ? (
    <MiniReferenceSources sources={sources} />
  ) : (
    <FullReferenceSources label={label} sources={sources} />
  );
};

export default ReferenceSources;
