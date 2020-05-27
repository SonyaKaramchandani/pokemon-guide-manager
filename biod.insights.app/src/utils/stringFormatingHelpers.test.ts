import { formatRatio1inX, formatNumber } from './stringFormatingHelpers';

var chai = require('chai');
var expect = chai.expect;

describe('formatNumber', () => {
  function testMe(expected: string, num: number, label?: string, labelPlural?: string) {
    test(`test formatNumber(${num}, ${label}, ${labelPlural}) is ${expected}`, () => {
      expect(formatNumber(num, label, labelPlural)).equals(expected);
    });
  }

  testMe('10', 10);
  testMe('1,000', 1000);
  testMe('100,000', 100000);
  testMe('1,000,000', 1000000);
  testMe('1 item', 1, 'item');
  testMe('1 item', 1, 'item', 'whatever');
  testMe('100,000 items', 100000, 'item');
  testMe('100,000 itemos', 100000, 'item', 'itemos');
});

describe('formatRatio1inX', () => {
  function testMe(percent: number, expected: string) {
    test(`test formatRatio1inX(${percent * 100}) is ${expected}`, () => {
      expect(formatRatio1inX(percent)).equals(expected);
    });
  }

  testMe(0.5, '1 in 2');
  testMe(0.1, '1 in 10');
  testMe(0.01, '1 in 100');
  testMe(0.0123, '1 in 81');
});
