/** @jsx jsx */
import { jsx } from 'theme-ui';
import { List } from 'semantic-ui-react';

const _List = props => {
  return <List {...props} divided selection sx={{ mt: t => `${t.space[0]}px !important` }} />;
};

export default _List;
