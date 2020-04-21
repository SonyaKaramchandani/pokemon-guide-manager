import { formatRatio1inX } from './stringFormatingHelpers';

var chai = require('chai');
var expect = chai.expect;

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
