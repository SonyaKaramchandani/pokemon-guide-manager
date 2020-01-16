/** @jsx jsx */
import { jsx } from 'theme-ui';
import React from 'react';
import { Accordian } from 'components/Accordian';

const OutbreakSurveillanceUnderstandingReporting = () => {
  return (
    <Accordian expanded={false} title="Understanding case and death reporting">
      <div sx={{ p: 3 }}>
        <p>
          <b>Reported cases</b> are reported by the media and/or official sources, but not
          necessarily verified.
        </p>
        <p>
          <b>Confirmed cases</b> are either laboratory confirmed or meet the clinical definition and
          are epidemiologically linked.
        </p>
        <p>
          <b>Suspected cases</b> meet the clinical definition but are not yet confirmed by
          laboratory testing.
        </p>
        <p>
          <b>Deaths</b> attributable to the disease.
        </p>
      </div>
    </Accordian>
  );
};

export default OutbreakSurveillanceUnderstandingReporting;
