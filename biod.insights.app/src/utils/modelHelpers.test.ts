import { getTravellerInterval } from './stringFormatingHelpers';
import { getInterval, RiskLikelihood } from './modelHelpers';

var chai = require('chai');
var expect = chai.expect;

// LINK: https://bluedotglobal.atlassian.net/projects/PT/issues/PT-1301
// <1% = Unlikely
// 1%-10% = Low
// >10%-50% = Moderate
// >50%-90% = High
// >90%-100% = Very High

function testGetInterval(min: number, max: number, notCalc: boolean, expected: RiskLikelihood) {
  const avg = (min + max) / 2;
  const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
  test(`${notCalcStr}get interval for ${min} to ${max} (avg=${avg})`, () => {
    expect(getInterval(min, max, notCalc)).equals(expected);
  });
}

describe('getInterval', () => {
  testGetInterval(0, 0, false, 'Unlikely');
  testGetInterval(0.005, 0.008, false, 'Unlikely');
  testGetInterval(0.01, 0.01, false, 'Low');
  testGetInterval(0.05, 0.1, false, 'Low');
  testGetInterval(0.005, 0.05, false, 'Low');
  testGetInterval(0.1, 0.1, false, 'Low');
  testGetInterval(0.1, 0.1005, false, 'Moderate');
  testGetInterval(0.1, 0.2, false, 'Moderate');
  testGetInterval(0.2, 0.3, false, 'Moderate');
  testGetInterval(0.3, 0.4, false, 'Moderate');
  testGetInterval(0.4, 0.5, false, 'Moderate');
  testGetInterval(0.5, 0.5, false, 'Moderate');
  testGetInterval(0.5, 0.6, false, 'High');
  testGetInterval(0.6, 0.7, false, 'High');
  testGetInterval(0.7, 0.8, false, 'High');
  testGetInterval(0.8, 0.9, false, 'High');
  testGetInterval(0.9, 0.9, false, 'High');
  testGetInterval(0.9, 0.95, false, 'Very High');
  testGetInterval(0.95, 0.99, false, 'Very High');
  testGetInterval(0.99, 1, false, 'Very High');
  testGetInterval(1, 1, false, 'Very High');
  testGetInterval(1, 1, true, 'Not calculated');
  testGetInterval(0.5, 0.5, true, 'Not calculated');
  testGetInterval(0.01, 0.8, true, 'Not calculated');
  testGetInterval(0.01, 0.08, true, 'Not calculated');
  testGetInterval(0, 0, true, 'Not calculated');
  testGetInterval(1, 2, false, 'Not calculated');
  testGetInterval(100, 200, false, 'Not calculated');
});
