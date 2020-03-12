import { getInterval, getTravellerInterval } from './stringFormatingHelpers';
import { tr } from 'date-fns/locale';
var chai = require('chai');
var expect = chai.expect;

describe('getInterval', () => {
  test('get interval for 0 to 0', () => {
    expect(getInterval(0, 0, '%', false)).equals('Unlikely');
  });
  test('get interval for <1 to <1', () => {
    expect(getInterval(0.0009237, 0.00232, '%', false)).equals('Unlikely');
  });
  test('get interval for 0 to 1%', () => {
    expect(getInterval(0, 0.01, '%', false)).equals('<1% to 1%');
  });
  test('get interval for <1% to >1%', () => {
    expect(getInterval(0.0009237, 0.246, '%', false)).equals('<1% to 25%');
  });
  test('get interval for 1% to >1%', () => {
    expect(getInterval(0.01352, 0.1232, '%', false)).equals('1% to 12%');
  });
  test('get interval for 1 to ~1% rounded', () => {
    expect(getInterval(0.01352, 0.01232, '%', false)).equals('~1%');
  });
  test('get interval for approx value 1%', () => {
    expect(getInterval(0.01, 0.01, '%', false)).equals('~1%');
  });
  test('get interval for greater than 90%', () => {
    expect(getInterval(0.95945672, 1, '%', false)).equals('>90%');
  });
  test('get interval for aprox value >1%', () => {
    expect(getInterval(0.3434, 0.3434, '%', false)).equals('~34%');
  });
  test('get interval for percent range', () => {
    expect(getInterval(0.35645653, 0.434354536, '%', false)).equals('36% to 43%');
  });
  test('get interval for not calculated', () => {
    expect(getInterval(0.1336567, 0.434534546, '%', true)).equals('Not calculated');
  });
});

describe('getTravellerInterval', () => {
  test('get traveller interval for 0 to 0', () => {
    expect(getTravellerInterval(0, 0)).equals('Negligible');
  });
  test('get traveller interval for 0 to 0 with unit', () => {
    expect(getTravellerInterval(0, 0, true)).equals('Negligible');
  });
  test('get traveller interval for 0 to <1', () => {
    expect(getTravellerInterval(0, 0.232)).equals('<1');
  });
  test('get traveller interval for 0 to <1 with unit', () => {
    expect(getTravellerInterval(0, 0.232, true)).equals('<1 case');
  });
  test('get traveller interval for 0 to 1', () => {
    expect(getTravellerInterval(0, 1)).equals('<1 to 1');
  });
  test('get traveller interval for 0 to 1 with unit', () => {
    expect(getTravellerInterval(0, 1, true)).equals('<1 to 1 case');
  });
  test('get traveller interval for 0 to >1', () => {
    expect(getTravellerInterval(0, 24.6)).equals('<1 to 25');
  });
  test('get traveller interval for 0 to >1 with unit', () => {
    expect(getTravellerInterval(0, 24.6, true)).equals('<1 to 25 cases');
  });
  test('get traveller interval for aprox value ~<1', () => {
    expect(getTravellerInterval(0.34, 0.34)).equals('<1');
  });
  test('get traveller interval for aprox value ~<1 with unit', () => {
    expect(getTravellerInterval(0.34, 0.34, true)).equals('<1 case');
  });
  test('get traveller interval for <1 to <1', () => {
    expect(getTravellerInterval(0.09237, 0.232)).equals('<1');
  });
  test('get traveller interval for <1 to <1 with unit', () => {
    expect(getTravellerInterval(0.09237, 0.232, true)).equals('<1 case');
  });
  test('get traveller interval for <1 to 1', () => {
    expect(getTravellerInterval(0.23, 1)).equals('<1 to 1');
  });
  test('get traveller interval for <1 to 1 with unit', () => {
    expect(getTravellerInterval(0.23, 1, true)).equals('<1 to 1 case');
  });
  test('get traveller interval for <1 to >1', () => {
    expect(getTravellerInterval(0.09237, 24.6)).equals('<1 to 25');
  });
  test('get traveller interval for <1 to >1 with unit', () => {
    expect(getTravellerInterval(0.09237, 24.6, true)).equals('<1 to 25 cases');
  });
  test('get traveller interval for 1 to 1', () => {
    expect(getTravellerInterval(1, 1)).equals('~1');
  });
  test('get traveller interval for 1 to 1 with unit', () => {
    expect(getTravellerInterval(1, 1, true)).equals('~1 case');
  });
  test('get traveller interval for <1 to >1 with unit', () => {
    expect(getTravellerInterval(0.09237, 24.6, true)).equals('<1 to 25 cases');
  });
  test('get traveller interval for 1 to >1', () => {
    expect(getTravellerInterval(1.352, 12.32)).equals('1 to 12');
  });
  test('get traveller interval for 1 to >1 with unit', () => {
    expect(getTravellerInterval(1.352, 12.32, true)).equals('1 to 12 cases');
  });
  test('get traveller interval for aprox value >1 rounded', () => {
    expect(getTravellerInterval(34.34, 34.34)).equals('~34');
  });
  test('get traveller interval for aprox value >1 rounded with unit', () => {
    expect(getTravellerInterval(34.34, 34.34, true)).equals('~34 cases');
  });
  test('get traveller interval for >1 to >1 rounded', () => {
    expect(getTravellerInterval(35.645653, 43.4354536)).equals('36 to 43');
  });
  test('get traveller interval for >1 to >1 rounded with unit', () => {
    expect(getTravellerInterval(35.645653, 43.4354536, true)).equals('36 to 43 cases');
  });

  test('get traveller interval for not calculated', () => {
    expect(getTravellerInterval(0.1336567, 0.434534546, false, true)).equals('Not calculated');
  });
});
