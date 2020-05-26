/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Accordian } from 'components/Accordian';
import { List } from 'semantic-ui-react';
import { formatDate } from 'utils/dateTimeHelpers';
import { sxtheme } from 'utils/cssHelpers';
import { Typography } from 'components/_common/Typography';
import * as dto from 'client/dto';

const stripLastPeriod = s => (!s ? s : s.replace(/\.+$/, ''));

type ReferenceListProps = {
  articles: dto.ArticleModel[];
};

const ReferenceList: React.FC<ReferenceListProps> = ({ articles }) => {
  return (
    <List className="xunpadded">
      {articles.map(({ url, title, publishedDate, sourceName }, index) => (
        <List.Item key={index}>
          <a target="_blank" href={url}>
            <Typography
              variant="body2"
              color="deepSea90"
              inline
              sx={{
                textDecoration: 'underline',
                '&:hover': {
                  color: sxtheme(t => t.colors.sea90)
                }
              }}
            >
              {stripLastPeriod(title)}
            </Typography>
          </a>
          <Typography variant="body2" color="deepSea90" inline>
            .&nbsp;
            {formatDate(publishedDate)}.
          </Typography>
          <Typography variant="overline" color="deepSea90" inline>
            {' '}
            {sourceName}.
          </Typography>
        </List.Item>
      ))}
    </List>
  );
};

export default ReferenceList;
