/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Typography } from 'components/_common/Typography';
import { FlexGroup } from 'components/_common/FlexGroup';
import { BdIcon } from 'components/_common/BdIcon';
import { formatDate } from 'utils/dateTimeHelpers';


// dto: ArticleModel[]
const ReferenceSources = ({ articles }) => {
  const sources = (articles || []).map(a => a.sourceName).join(' / ');
  const date1 = articles && articles[0] && articles[0].publishedDate.toString();

  return (
    <>
      <Typography variant="caption" color="deepSea50">With sources from:</Typography>
      <Typography variant="overline" color="stone70">{sources}</Typography>
      <Typography variant="caption" color="stone50">{formatDate(date1)}</Typography>
    </>
  );
};

export default ReferenceSources;




// TODO: c0a533c8: what was ReferenceSources.mini for? -MT

// const FullReferenceSources = ({ sources, label }) => (
//   <div sx={{ p: 3 }}>
//     <div>{label}</div>
//     <div>{sources.join(' / ')}</div>
//   </div>
// );

// const MiniReferenceSources = ({ sources }) => {
//   let content = '';
//   if (sources.length <= 3) content = sources.join(' / ');
//   else content = [...sources.slice(0, 3), ` + ${sources.length - 3} source(s)`].join(' / ');

//   return <div>{content}</div>;
// };

// const ReferenceSources = ({ articles, label = 'Sources from', mini = false }) => {
//   const sources = (articles || []).map(a => a.sourceName).filter(i => i && i.length);
//   if (!sources.length) return '';

//   return mini ? (
//     <MiniReferenceSources sources={sources} />
//   ) : (
//     <FullReferenceSources label={label} sources={sources} />
//   );
// };

// export default ReferenceSources;
