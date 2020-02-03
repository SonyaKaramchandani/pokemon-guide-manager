/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Typography } from 'components/_common/Typography';

const Error = ({ title, subtitle, linkText, linkCallback }) => {
  return (
    <>
      <Typography
        variant="subtitle2"
        color="deepSea50"
        sx={{
          padding: '60px 0px 0px 0px',
          textAlign: 'center'
        }}
      >
        {title}
      </Typography>
      <Typography
        variant="caption"
        color="deepSea50"
        sx={{
          padding: '2px 40px',
          textAlign: 'center'
        }}
      >
        {subtitle}
      </Typography>
      <div onClick={linkCallback} sx={{ mt: '5px' }}>
        <Typography
          variant="body2"
          color="sea90"
          sx={{
            paddingTop: '12px',
            textAlign: 'center',
            textDecoration: 'underline',
            cursor: 'pointer',
            '&:hover': {
              color: t => t.colors.sea60,
              textDecoration: 'underline',
              transition: 'ease .3s'
            }
          }}
        >
          {linkText}
        </Typography>
      </div>
    </>
  );
};

export default Error;
