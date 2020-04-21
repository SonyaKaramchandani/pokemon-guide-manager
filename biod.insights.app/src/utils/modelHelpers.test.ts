import { RiskLikelihood, RiskMagnitude } from 'models/RiskCategories';
import { getInterval, getTravellerInterval } from './modelHelpers';

var chai = require('chai');
var expect = chai.expect;

// LINK: https://bluedotglobal.atlassian.net/projects/PT/issues/PT-1301
// <1% = Unlikely
// 1%-10% = Low
// >10%-50% = Moderate
// >50%-90% = High
// >90%-100% = Very High

describe('getInterval', () => {
  function testCombo(min: number, max: number, notCalc: boolean, expected: RiskLikelihood) {
    const avg = (min + max) / 2;
    const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
    test(`${notCalcStr}getInterval for ${min} to ${max} (avg=${avg})`, () => {
      expect(getInterval(min, max, notCalc)).equals(expected);
    });
  }

  testCombo(0, 0, false, 'Unlikely');
  testCombo(0.005, 0.008, false, 'Unlikely');
  testCombo(0.01, 0.01, false, 'Low');
  testCombo(0.05, 0.1, false, 'Low');
  testCombo(0.005, 0.05, false, 'Low');
  testCombo(0.1, 0.1, false, 'Low');
  testCombo(0.1, 0.1005, false, 'Moderate');
  testCombo(0.1, 0.2, false, 'Moderate');
  testCombo(0.2, 0.3, false, 'Moderate');
  testCombo(0.3, 0.4, false, 'Moderate');
  testCombo(0.4, 0.5, false, 'Moderate');
  testCombo(0.5, 0.5, false, 'Moderate');
  testCombo(0.5, 0.6, false, 'High');
  testCombo(0.6, 0.7, false, 'High');
  testCombo(0.7, 0.8, false, 'High');
  testCombo(0.8, 0.9, false, 'High');
  testCombo(0.9, 0.9, false, 'High');
  testCombo(0.9, 0.95, false, 'Very High');
  testCombo(0.95, 0.99, false, 'Very High');
  testCombo(0.99, 1, false, 'Very High');
  testCombo(1, 1, false, 'Very High');
  testCombo(1, 1, true, 'Not calculated');
  testCombo(0.5, 0.5, true, 'Not calculated');
  testCombo(0.01, 0.8, true, 'Not calculated');
  testCombo(0.01, 0.08, true, 'Not calculated');
  testCombo(0, 0, true, 'Not calculated');
  testCombo(1, 2, false, 'Not calculated');
  testCombo(100, 200, false, 'Not calculated');
  testCombo(undefined, 1, false, 'Not calculated');
  testCombo(null, 1, false, 'Not calculated');
  testCombo(null, null, false, 'Not calculated');
  testCombo(0, null, false, 'Not calculated');
  testCombo(0, undefined, false, 'Not calculated');
  testCombo(undefined, undefined, false, 'Not calculated');
  testCombo(undefined, undefined, true, 'Not calculated');
});

//=====================================================================================================================================

// LINK: https://bluedotglobal.atlassian.net/browse/PT-1306
// 0 = Negligible
// >0-10 = Up to 10
// >10-100 = 11-100
// >100-1000 = 101-1000
// >1000 = >1000

describe('getTravellerInterval', () => {
  function testCombo(min: number, max: number, notCalc: boolean, expected: RiskMagnitude) {
    const avg = (min + max) / 2;
    const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
    test(`${notCalcStr}getTravellerInterval for ${min} to ${max} (avg=${avg})`, () => {
      expect(getTravellerInterval(min, max, notCalc)).equals(expected);
    });
  }

  testCombo(0, 0, false, 'Negligible');
  testCombo(0, 1, false, 'Up to 10');
  testCombo(1, 1, false, 'Up to 10');
  testCombo(1, 9, false, 'Up to 10');
  testCombo(1, 10, false, 'Up to 10');
  testCombo(9, 10, false, 'Up to 10');
  testCombo(10, 10, false, 'Up to 10');
  testCombo(10, 11, false, '11-100');
  testCombo(9, 12, false, '11-100'); // unusual case
  testCombo(10, 12, false, '11-100');
  testCombo(11, 11, false, '11-100');
  testCombo(11, 12, false, '11-100');
  testCombo(99, 100, false, '11-100');
  testCombo(100, 100, false, '11-100');
  testCombo(100, 101, false, '101-1000');
  testCombo(100, 102, false, '101-1000'); // TODO: corner case?
  testCombo(101, 101, false, '101-1000');
  testCombo(101, 102, false, '101-1000');
  testCombo(101, 1000, false, '101-1000');
  testCombo(101, 1001, false, '101-1000');
  testCombo(101, 1100, false, '101-1000');
  testCombo(101, 1200, false, '101-1000');
  testCombo(101, 1300, false, '101-1000');
  testCombo(1000, 1000, false, '101-1000');
  testCombo(1000, 1001, false, '>1000');
  testCombo(1000, 1002, false, '>1000');
  testCombo(1001, 1001, false, '>1000');
  testCombo(1001, 9999, false, '>1000');
  testCombo(9999, 99999, false, '>1000');
  testCombo(1, 1, true, 'Not calculated');
  testCombo(1, 9, true, 'Not calculated');
  testCombo(1, 10, true, 'Not calculated');
  testCombo(10, 10, true, 'Not calculated');
  testCombo(99, 100, true, 'Not calculated');
  testCombo(101, 102, true, 'Not calculated');
  testCombo(1000, 1002, true, 'Not calculated');
  testCombo(undefined, 1, false, 'Not calculated');
  testCombo(null, 1, false, 'Not calculated');
  testCombo(null, null, false, 'Not calculated');
  testCombo(0, null, false, 'Not calculated');
  testCombo(0, undefined, false, 'Not calculated');
  testCombo(undefined, undefined, false, 'Not calculated');
  testCombo(undefined, undefined, true, 'Not calculated');
});
