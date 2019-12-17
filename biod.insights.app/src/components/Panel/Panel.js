import React from 'react';
import { Header } from 'semantic-ui-react';
import { Loading } from 'components/Loading';

function Panel({ loading, header, toolbar, children }) {
  return (
    <div style={{ minWidth: 350, maxWidth: 350, overflow: 'auto', backgroundColor: '#fff' }}>
      {loading && <Loading />}

      {!loading && (
        <>
          <div className="pad border-b">
            <Header size="medium">{header}</Header>
          </div>

          <div className="pad pad-half-tb">{toolbar}</div>
          <div>{children}</div>
        </>
      )}
    </div>
  );
}

export default Panel;
