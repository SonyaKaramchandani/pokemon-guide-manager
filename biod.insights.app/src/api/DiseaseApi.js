import axios from 'axios';

function getDiseases({ id }) {
  const data = [
    { id: '1', name: id + ' : 1 : Measles', description: 'New York, USA' },
    { id: '2', name: id + ' : 2 : Mumps', description: 'Canada' },
    { id: '3', name: id + ' : 3 : Dengue', description: 'Japan' },
    { id: '4', name: id + ' : 4 : Avian Flu', description: 'Paris, France' },
    { id: '5', name: id + ' : 5 : Rocky Mountain Spotted Fever', description: 'Canada' },
    { id: '6', name: id + ' : 6 : Avian Influenza', description: 'Singapore' }
  ];

  return new Promise(resolve => setTimeout(() => resolve(data), 500));
}

function getDisease({ id }) {
  const data = { id: '1', name: id + ' : Measles', description: 'New York, USA' };
  return new Promise(resolve => setTimeout(() => resolve({ data }), 500));
}

export default {
  getDiseases,
  getDisease
};
