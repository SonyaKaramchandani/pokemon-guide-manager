/** @jsx jsx */
import { jsx } from 'theme-ui';
import { List } from 'semantic-ui-react';

const ListItem = props => {
  return <List.Item {...props} sx={{ p: t => `${t.space[3]}px !important` }} />;
};

export default ListItem;
