/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Accordian } from 'components/Accordian';
import { List } from 'semantic-ui-react';
import { formatDate } from 'utils/dateTimeHelpers';

const ReferenceList = ({ articles }) => {
  return (
    <div sx={{ px: 3 }}>
      <List divided>
        {articles.map(({ url, title, publishedDate, sourceName }) => (
          <List.Item key={url}>
            <List.Content>
              <List.Header as="a" href={url}>
                {title}
              </List.Header>
              <List.Description>
                {formatDate(publishedDate)}. <b>{sourceName}</b>.
              </List.Description>
            </List.Content>
          </List.Item>
        ))}
      </List>
    </div>
  );
};

export default ReferenceList;
