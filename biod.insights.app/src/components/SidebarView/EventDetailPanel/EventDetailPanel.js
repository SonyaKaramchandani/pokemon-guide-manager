/** @jsx jsx */
import { jsx } from 'theme-ui';
import React, { useState, useEffect } from 'react';
import { Panel } from 'components/Panel';
import EventApi from 'api/EventApi';

function EventDetailPanel({ eventId, onClose }) {
  const [event, setEvent] = useState({ eventInformation: {} });
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (eventId) {
      setIsLoading(true);
      EventApi.getEvent({ eventId })
        .then(({ data }) => {
          setEvent(data);
        })
        .finally(() => setIsLoading(false));
    }
  }, [eventId]);

  if (!eventId) {
    return null;
  }

  const { title, summary } = event.eventInformation;

  return (
    <Panel title={title} isLoading={isLoading} onClose={onClose}>
      <div sx={{ p: 3 }}>{summary}</div>
    </Panel>
  );
}

export default EventDetailPanel;
