import { parseIntOrNull } from './stringHelpers';
var chai = require('chai');
var expect = chai.expect;

describe('parseIntOrNull', () => {
  test('parseIntOrNull of null', () => {
    expect(parseIntOrNull(null)).equals(null);
  });
  test('parseIntOrNull of "trash"', () => {
    expect(parseIntOrNull('trash')).equals(null);
  });
  test('parseIntOrNull of "10trash"', () => {
    expect(parseIntOrNull('10trash')).equals(null);
  });
  test('parseIntOrNull of "0"', () => {
    expect(parseIntOrNull('0')).equals(0);
  });
  test('parseIntOrNull of "1"', () => {
    expect(parseIntOrNull('1')).equals(1);
  });
  test('parseIntOrNull of "2"', () => {
    expect(parseIntOrNull('2')).equals(2);
  });
});
