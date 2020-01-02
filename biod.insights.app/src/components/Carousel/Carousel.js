/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState } from 'react';
import { Card } from 'semantic-ui-react';
import BluedotSvg from 'assets/bluedot.svg';
import GraydotSvg from 'assets/graydot.svg';
import { SvgButton } from 'components/SvgButton';

const Carousel = ({ slides }) => {
  const [selectedIndex, setSetSelectedIndex] = useState(0);

  const buttonGroup = slides.map((s, index) => (
    <SvgButton
      key={index}
      src={index === selectedIndex ? BluedotSvg : GraydotSvg}
      onClick={() => setSetSelectedIndex(index)}
    />
  ));

  return (
    <div sx={{ p: 3 }}>
      <Card fluid sx={{ borderRadius: '2px !important' }}>
        {slides[selectedIndex]}
        <Card.Content
          textAlign="center"
          sx={{
            py: t => `${t.space[1]}px !important`
          }}
        >
          {buttonGroup}
        </Card.Content>
      </Card>
    </div>
  );
};

export default Carousel;
