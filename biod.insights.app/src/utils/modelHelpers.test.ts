import { RiskLikelihood, RiskMagnitude } from 'models/RiskCategories';
import { map2RiskLikelihood, map2RiskMagnitude } from './modelHelpers';

var chai = require('chai');
var expect = chai.expect;

// LINK: https://bluedotglobal.atlassian.net/projects/PT/issues/PT-1301
// <1% = Unlikely
// 1%-10% = Low
// >10%-50% = Moderate
// >50%-90% = High
// >90%-100% = Very high

describe('getInterval', () => {
  function testCombo(
    min: number,
    max: number,
    notCalc: boolean,
    isCalcInProgress: boolean,
    expected: RiskLikelihood
  ) {
    const avg = (min + max) / 2;
    const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
    test(`${notCalcStr}getInterval for ${min} to ${max} (avg=${avg})`, () => {
      expect(map2RiskLikelihood(min, max, notCalc, isCalcInProgress)).equals(expected);
    });
  }

  testCombo(0, 0, false, false, 'Unlikely');
  testCombo(0.005, 0.008, false, false, 'Unlikely');
  testCombo(0.01, 0.01, false, false, 'Low');
  testCombo(0.05, 0.1, false, false, 'Low');
  testCombo(0.005, 0.05, false, false, 'Low');
  testCombo(0.1, 0.1, false, false, 'Low');
  testCombo(0.1, 0.1005, false, false, 'Moderate');
  testCombo(0.1, 0.2, false, false, 'Moderate');
  testCombo(0.2, 0.3, false, false, 'Moderate');
  testCombo(0.3, 0.4, false, false, 'Moderate');
  testCombo(0.4, 0.5, false, false, 'Moderate');
  testCombo(0.5, 0.5, false, false, 'Moderate');
  testCombo(0.5, 0.6, false, false, 'High');
  testCombo(0.6, 0.7, false, false, 'High');
  testCombo(0.7, 0.8, false, false, 'High');
  testCombo(0.8, 0.9, false, false, 'High');
  testCombo(0.9, 0.9, false, false, 'High');
  testCombo(0.9, 0.95, false, false, 'Very high');
  testCombo(0.95, 0.99, false, false, 'Very high');
  testCombo(0.99, 1, false, false, 'Very high');
  testCombo(1, 1, false, false, 'Very high');
  testCombo(1, 1, true, false, 'Not calculated');
  testCombo(0.5, 0.5, true, false, 'Not calculated');
  testCombo(0.01, 0.8, true, false, 'Not calculated');
  testCombo(0.01, 0.08, true, false, 'Not calculated');
  testCombo(0, 0, true, false, 'Not calculated');
  testCombo(0, 0, true, true, 'Not calculated');
  testCombo(0, 0, false, true, 'Calculating, revisit later!');
  testCombo(undefined, undefined, true, false, 'Not calculated');
});

//=====================================================================================================================================

// LINK: https://bluedotglobal.atlassian.net/browse/PT-1306
// LINK: https://bluedotglobal.atlassian.net/browse/PT-1335
// 0 = Negligible
// >0-10 = Up to 10 cases
// >10-100 = 11 to 100 cases
// >100-1000 = 101 to 1,000 cases
// >1,000 cases = >1,000 cases

describe('getTravellerInterval', () => {
  function testCombo(
    min: number,
    max: number,
    notCalc: boolean,
    isCalcInProgress: boolean,
    expected: RiskMagnitude
  ) {
    const avg = (min + max) / 2;
    const notCalcStr = notCalc ? '(NOT CALCULATED) ' : '';
    test(`${notCalcStr}getTravellerInterval for ${min} to ${max} (avg=${avg})`, () => {
      expect(map2RiskMagnitude(min, max, notCalc, isCalcInProgress)).equals(expected);
    });
  }

  testCombo(0, 0, false, false, 'Negligible');
  testCombo(0, 1, false, false, 'Up to 10 cases');
  testCombo(1, 1, false, false, 'Up to 10 cases');
  testCombo(1, 9, false, false, 'Up to 10 cases');
  testCombo(1, 10, false, false, 'Up to 10 cases');
  testCombo(9, 10, false, false, 'Up to 10 cases');
  testCombo(10, 10, false, false, 'Up to 10 cases');
  testCombo(10, 11, false, false, '11 to 100 cases');
  testCombo(9, 12, false, false, '11 to 100 cases'); // unusual case
  testCombo(10, 12, false, false, '11 to 100 cases');
  testCombo(11, 11, false, false, '11 to 100 cases');
  testCombo(11, 12, false, false, '11 to 100 cases');
  testCombo(99, 100, false, false, '11 to 100 cases');
  testCombo(100, 100, false, false, '11 to 100 cases');
  testCombo(100, 101, false, false, '101 to 1,000 cases');
  testCombo(100, 102, false, false, '101 to 1,000 cases'); // TODO: corner case?
  testCombo(101, 101, false, false, '101 to 1,000 cases');
  testCombo(101, 102, false, false, '101 to 1,000 cases');
  testCombo(101, 1000, false, false, '101 to 1,000 cases');
  testCombo(101, 1001, false, false, '101 to 1,000 cases');
  testCombo(101, 1100, false, false, '101 to 1,000 cases');
  testCombo(101, 1200, false, false, '101 to 1,000 cases');
  testCombo(101, 1300, false, false, '101 to 1,000 cases');
  testCombo(1000, 1000, false, false, '101 to 1,000 cases');
  testCombo(1000, 1001, false, false, '>1,000 cases');
  testCombo(1000, 1002, false, false, '>1,000 cases');
  testCombo(1001, 1001, false, false, '>1,000 cases');
  testCombo(1001, 9999, false, false, '>1,000 cases');
  testCombo(9999, 99999, false, false, '>1,000 cases');
  testCombo(1, 1, true, false, 'Not calculated');
  testCombo(1, 9, true, false, 'Not calculated');
  testCombo(1, 10, true, false, 'Not calculated');
  testCombo(10, 10, true, false, 'Not calculated');
  testCombo(99, 100, true, false, 'Not calculated');
  testCombo(101, 102, true, false, 'Not calculated');
  testCombo(1000, 1002, true, false, 'Not calculated');
  testCombo(undefined, 1, true, false, 'Not calculated');
  testCombo(0, 0, true, true, 'Not calculated');
  testCombo(0, 0, false, true, 'Calculating, revisit later!');
  testCombo(undefined, undefined, true, false, 'Not calculated');
});
