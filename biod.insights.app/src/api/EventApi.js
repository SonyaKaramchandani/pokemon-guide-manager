import axios from 'axios';

function getEvents(params) {
  const data = [
    { id: '1', name: params.id + ' : 1 : Event1', description: 'New York, USA' },
    { id: '2', name: params.id + ' : 2 : Event2', description: 'Canada' },
    { id: '3', name: params.id + ' : 3 : Event3', description: 'Japan' },
    { id: '4', name: params.id + ' : 4 : Event4', description: 'Paris, France' },
    { id: '5', name: params.id + ' : 5 : Event5', description: 'Canada' },
    { id: '6', name: params.id + ' : 6 : Event6', description: 'Singapore' }
  ];
  return new Promise(resolve => setTimeout(() => resolve({ data }), 500));
}

function getEvent(params) {
  const data = {
    id: '1',
    name: params.id + ' : Event',
    description: 'New York, USA',
    locations: [
      { id: '1', name: params.id + ' : Location1', description: 'New York, USA' },
      { id: '2', name: params.id + ' : Location2', description: 'Canada' },
      { id: '3', name: params.id + ' : Location3', description: 'Japan' },
      { id: '4', name: params.id + ' : Location4', description: 'Paris, France' },
      { id: '5', name: params.id + ' : Location5', description: 'Canada' },
      { id: '6', name: params.id + ' : Location6', description: 'Singapore' }
    ]
  };
  return new Promise(resolve => setTimeout(() => resolve({ data }), 500));
}

export default {
  getEvents,
  getEvent
};
