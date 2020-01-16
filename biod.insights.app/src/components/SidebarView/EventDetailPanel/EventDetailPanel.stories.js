import React from 'react';
import { action } from '@storybook/addon-actions';
import EventDetailPanel from './EventDetailPanel';
import OutbreakSurveillanceOverall from './OutbreakSurveillanceOverall';


export default {
  title: 'DiseaseEvent/EventDetailPanel'
};

// TODO: 9eae0d15: need to decouple for storybook and pass mock data (no webcalls in storybook!)

// dto: GetEventModel
const event = {
  isLocal: false,
  eventInformation: {
    id: 146,
    title: 'Hepatitis A in USA',
    startDate: '2018-01-01T00:00:00',
    endDate: null,
    lastUpdatedDate: '2018-08-16T16:37:18',
    summary:
      'An outbreak of hepatitis A has been occurring in the USA since June 7th, 2018. Hepatitis A has an incubation period of 15-50 days.  Common clinical features can be divided into preicteric and icteric phases. The preicteric phase (5-7 days) is characterized by non-specific febrile symptoms, anorexia, and abdominal pain. The icteric phase (up to 6 months) is characterized by jaundice, acholic stools, and abnormal LFTs. Case fatality is under 2%. Transmission of the virus is food/waterborne.  No additional infection control measures are recommended. Notification of public health is mandatory. '
  },
  importationRisk: null,
  exportationRisk: {
    isModelNotRun: false,
    minProbability: 0.9998,
    maxProbability: 1,
    minMagnitude: 8.76,
    maxMagnitude: 11.75
  },
  caseCounts: {
    confirmedCases: 282,
    reportedCases: 3575,
    deaths: 32,
    hasConfirmedCasesNesting: true,
    hasReportedCasesNesting: false,
    hasDeathsNesting: false
  },
  eventLocations: [
    {
      geonameId: 4099753,
      locationName: 'Arkansas, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 77,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4106979,
      locationName: 'Craighead County, Arkansas, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 4,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4285302,
      locationName: 'Boyd County, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 98,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4286176,
      locationName: 'Bullitt County, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 36,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4287080,
      locationName: 'Carter County, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 56,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4293301,
      locationName: 'Grayson County, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 6,
        reportedCases: 0,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4293471,
      locationName: 'Greenup County, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 36,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4296212,
      locationName: 'Jefferson County, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 453,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4299276,
      locationName: 'Louisville, Kentucky, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 482,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4478884,
      locationName: 'Mecklenburg County, North Carolina, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 5,
        reportedCases: 0,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4516351,
      locationName: 'Lawrence County, Ohio, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 28,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4522889,
      locationName: 'Ross County, Ohio, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 3,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4524221,
      locationName: 'Scioto County, Ohio, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 2,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4612862,
      locationName: 'Chattanooga, Tennessee, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 7,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4644585,
      locationName: 'Nashville, Tennessee, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 74,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4800996,
      locationName: 'Cabell County, West Virginia, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 155,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4810630,
      locationName: 'Kanawha County, West Virginia, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 363,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4826850,
      locationName: 'West Virginia, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 791,
        deaths: 2,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 4921868,
      locationName: 'Indiana, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 334,
        deaths: 1,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 5001836,
      locationName: 'Michigan, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 869,
        deaths: 27,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 5160090,
      locationName: 'Lake County, Ohio, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 1,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 5165418,
      locationName: 'Ohio, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 82,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 5549030,
      locationName: 'Utah, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 271,
        reportedCases: 0,
        deaths: 2,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    },
    {
      geonameId: 6254925,
      locationName: 'Kentucky, United States',
      locationType: 4,
      caseCounts: {
        confirmedCases: 6,
        reportedCases: 1341,
        deaths: 0,
        hasConfirmedCasesNesting: true,
        hasReportedCasesNesting: false,
        hasDeathsNesting: false
      }
    }
  ],
  articles: [
    {
      title: '"Combining a vaccine with current methods would allow HIV eradication"',
      url:
        'http://www.rfi.fr/es/salud/20190724-combinar-una-vacuna-con-los-metodos-actuales-permitiria-erradicar-el-vih',
      publishedDate: '2019-08-06T18:15:00',
      originalLanguage: 'es',
      sourceName: 'GPHIN'
    },
    {
      title:
        '1 chikungunya fever: Learn chikungunya fever symptoms, causes and prevention methods - chikungunya fever causes, signs, symptoms and prevention',
      url:
        'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
      publishedDate: '2019-08-06T17:45:00',
      originalLanguage: 'hi',
      sourceName: 'IDC1'
    },
    {
      title:
        '2 chikungunya fever: Learn chikungunya fever symptoms, causes and prevention methods - chikungunya fever causes, signs, symptoms and prevention',
      url:
        'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
      publishedDate: '2019-08-06T17:45:00',
      originalLanguage: 'hi',
      sourceName: 'IDC2'
    },
    {
      title:
        '3 chikungunya fever: Learn chikungunya fever symptoms, causes and prevention methods - chikungunya fever causes, signs, symptoms and prevention',
      url:
        'https://navbharattimes.indiatimes.com/lifestyle/health/chikungunya-fever-causes-signs-symptoms-and-prevention/articleshow/70558798.cms',
      publishedDate: '2019-08-06T17:45:00',
      originalLanguage: 'hi',
      sourceName: 'IDC3'
    }
  ],
  sourceAirports: [
    {
      id: 1990,
      name: "Chicago O'Hare International Airport",
      code: 'ORD',
      latitude: 41.97959,
      longitude: -87.90446,
      volume: 2143075,
      city: 'Chicago, Illinois, United States'
    },
    {
      id: 616,
      name: 'Hartsfield-Jackson Atlanta International Airport',
      code: 'ATL',
      latitude: 33.64099,
      longitude: -84.42265,
      volume: 1788288,
      city: 'Atlanta, Georgia, United States'
    },
    {
      id: 10357,
      name: 'Toronto Pearson International Airport',
      code: 'YYZ',
      latitude: 43.68066,
      longitude: -79.61286,
      volume: 1691538,
      city: 'Toronto, Ontario, Canada'
    },
    {
      id: 2446,
      name: 'Dallas/Fort Worth International Airport',
      code: 'DFW',
      latitude: 32.89595,
      longitude: -97.0372,
      volume: 1323033,
      city: null
    },
    {
      id: 7901,
      name: 'Philadelphia International Airport',
      code: 'PHL',
      latitude: 39.87835,
      longitude: -75.24018,
      volume: 959158,
      city: 'Philadelphia, Pennsylvania, United States'
    },
    {
      id: 2589,
      name: 'Detroit Metropolitan Wayne County Airport',
      code: 'DTW',
      latitude: 42.22205,
      longitude: -83.35147,
      volume: 936350,
      city: 'Detroit, Michigan, United States'
    },
    {
      id: 10960,
      name: 'Ronald Reagan Washington National Airport',
      code: 'DCA',
      latitude: 38.85233,
      longitude: -77.0372,
      volume: 908290,
      city: 'Washington, D.C., District of Columbia, United States'
    },
    {
      id: 785,
      name: 'Baltimore-Washington International Airport',
      code: 'BWI',
      latitude: 39.17539,
      longitude: -76.66802,
      volume: 871402,
      city: 'Baltimore, Maryland, United States'
    },
    {
      id: 10956,
      name: 'Washington Dulles International Airport',
      code: 'IAD',
      latitude: 38.94877,
      longitude: -77.4491,
      volume: 737916,
      city: 'Washington, D.C., District of Columbia, United States'
    },
    {
      id: 8839,
      name: 'Salt Lake City International Airport',
      code: 'SLC',
      latitude: 40.78688,
      longitude: -111.98203,
      volume: 679483,
      city: 'Salt Lake City, Utah, United States'
    },
    {
      id: 1915,
      name: 'Charlotte Douglas International Airport',
      code: 'CLT',
      latitude: 35.2207,
      longitude: -80.94413,
      volume: 616453,
      city: 'Charlotte, North Carolina, United States'
    },
    {
      id: 1988,
      name: 'Chicago Midway International Airport',
      code: 'MDW',
      latitude: 41.7868,
      longitude: -87.74555,
      volume: 614484,
      city: 'Chicago, Illinois, United States'
    },
    {
      id: 6992,
      name: 'Nashville International Airport',
      code: 'BNA',
      latitude: 36.1195,
      longitude: -86.68416,
      volume: 594935,
      city: 'Nashville, Tennessee, United States'
    },
    {
      id: 2785,
      name: 'Raleigh-Durham International Airport',
      code: 'RDU',
      latitude: 35.87946,
      longitude: -78.7871,
      volume: 543294,
      city: null
    },
    {
      id: 9678,
      name: 'Lambert-St. Louis International Airport',
      code: 'STL',
      latitude: 38.74321,
      longitude: -90.36591,
      volume: 484694,
      city: 'St. Louis, Missouri, United States'
    },
    {
      id: 4732,
      name: 'Kansas City International Airport',
      code: 'MCI',
      latitude: 39.29623,
      longitude: -94.71775,
      volume: 464840,
      city: 'Kansas City, Missouri, United States'
    },
    {
      id: 2448,
      name: 'Dallas Love Field',
      code: 'DAL',
      latitude: 32.84707,
      longitude: -96.85195,
      volume: 433256,
      city: 'Dallas, Texas, United States'
    },
    {
      id: 7976,
      name: 'Pittsburgh International Airport',
      code: 'PIT',
      latitude: 40.49608,
      longitude: -80.25547,
      volume: 406247,
      city: 'Pittsburgh, Pennsylvania, United States'
    },
    {
      id: 2107,
      name: 'Cleveland-Hopkins International Airport',
      code: 'CLE',
      latitude: 41.41083,
      longitude: -81.84944,
      volume: 401679,
      city: 'Cleveland, Ohio, United States'
    },
    {
      id: 2072,
      name: 'Cincinnati/Northern Kentucky International Airport',
      code: 'CVG',
      latitude: 39.04614,
      longitude: -84.66217,
      volume: 362441,
      city: 'Cincinnati, Ohio, United States'
    },
    {
      id: 4322,
      name: 'Indianapolis International Airport',
      code: 'IND',
      latitude: 39.71507,
      longitude: -86.29762,
      volume: 355450,
      city: 'Indianapolis, Indiana, United States'
    },
    {
      id: 2217,
      name: 'John Glenn Columbus International Airport',
      code: 'CMH',
      latitude: 39.99795,
      longitude: -82.88352,
      volume: 309293,
      city: 'Columbus, Ohio, United States'
    },
    {
      id: 1541,
      name: 'Greater Buffalo International Airport',
      code: 'BUF',
      latitude: 42.94034,
      longitude: -78.7317,
      volume: 245042,
      city: 'Buffalo, New York, United States'
    },
    {
      id: 8546,
      name: 'Richmond International Airport',
      code: 'RIC',
      latitude: 37.50517,
      longitude: -77.31967,
      volume: 187594,
      city: 'Richmond, Virginia, United States'
    },
    {
      id: 6346,
      name: 'Memphis International Airport',
      code: 'MEM',
      latitude: 35.04218,
      longitude: -89.98157,
      volume: 183832,
      city: 'Memphis, Tennessee, United States'
    },
    {
      id: 7219,
      name: 'Norfolk International Airport',
      code: 'ORF',
      latitude: 36.89543,
      longitude: -76.20049,
      volume: 182776,
      city: 'Norfolk, Virginia, United States'
    },
    {
      id: 7412,
      name: 'Will Rogers World Airport',
      code: 'OKC',
      latitude: 35.39312,
      longitude: -97.60087,
      volume: 167945,
      city: 'Oklahoma City, Oklahoma, United States'
    },
    {
      id: 5788,
      name: 'Louisville International Airport',
      code: 'SDF',
      latitude: 38.17439,
      longitude: -85.736,
      volume: 151806,
      city: 'Louisville, Kentucky, United States'
    },
    {
      id: 3683,
      name: 'Gerald R. Ford International Airport',
      code: 'GRR',
      latitude: 42.88461,
      longitude: -85.52958,
      volume: 141905,
      city: 'Grand Rapids, Michigan, United States'
    },
    {
      id: 10487,
      name: 'Tulsa International Airport',
      code: 'TUL',
      latitude: 36.19839,
      longitude: -95.88811,
      volume: 121564,
      city: 'Tulsa, Oklahoma, United States'
    },
    {
      id: 1203,
      name: 'Birmingham-Shuttlesworth International Airport',
      code: 'BHM',
      latitude: 33.56166,
      longitude: -86.75254,
      volume: 116089,
      city: 'Birmingham, Alabama, United States'
    },
    {
      id: 5655,
      name: 'Adams Field',
      code: 'LIT',
      latitude: 34.73009,
      longitude: -92.22431,
      volume: 89667,
      city: 'Little Rock, Arkansas, United States'
    },
    {
      id: 5077,
      name: 'McGhee Tyson Airport',
      code: 'TYS',
      latitude: 35.80673,
      longitude: -83.99115,
      volume: 87148,
      city: 'Knoxville, Tennessee, United States'
    },
    {
      id: 3720,
      name: 'Piedmont Triad International Airport',
      code: 'GSO',
      latitude: 36.10613,
      longitude: -79.93701,
      volume: 77599,
      city: null
    },
    {
      id: 2512,
      name: 'James M Cox Dayton International Airport',
      code: 'DAY',
      latitude: 39.90228,
      longitude: -84.21939,
      volume: 76348,
      city: 'Dayton, Ohio, United States'
    },
    {
      id: 3141,
      name: 'Northwest Arkansas Regional Airport',
      code: 'XNA',
      latitude: 36.28194,
      longitude: -94.30694,
      volume: 61609,
      city: 'Fayetteville, Arkansas, United States'
    },
    {
      id: 4217,
      name: 'Huntsville International Airport-Carl T Jones Field',
      code: 'HSV',
      latitude: 34.6464,
      longitude: -86.77494,
      volume: 53297,
      city: 'Huntsville, Alabama, United States'
    },
    {
      id: 5569,
      name: 'Blue Grass Airport',
      code: 'LEX',
      latitude: 38.03702,
      longitude: -84.60522,
      volume: 51164,
      city: 'Lexington, Kentucky, United States'
    },
    {
      id: 1932,
      name: 'Lovell Field',
      code: 'CHA',
      latitude: 35.03507,
      longitude: -85.20357,
      volume: 45121,
      city: 'Chattanooga, Tennessee, United States'
    },
    {
      id: 9633,
      name: 'Springfield-Branson National Airport',
      code: 'SGF',
      latitude: 37.24421,
      longitude: -93.38686,
      volume: 41971,
      city: 'Springfield, Missouri, United States'
    },
    {
      id: 146,
      name: 'Akron-Canton Regional Airport',
      code: 'CAK',
      latitude: 40.91617,
      longitude: -81.44234,
      volume: 39755,
      city: null
    },
    {
      id: 3207,
      name: 'Bishop International Airport',
      code: 'FNT',
      latitude: 42.96531,
      longitude: -83.74329,
      volume: 28028,
      city: 'Flint, Michigan, United States'
    },
    {
      id: 8603,
      name: 'Roanoke Regional Airport-Woodrum Field',
      code: 'ROA',
      latitude: 37.32541,
      longitude: -79.97532,
      volume: 26837,
      city: 'Roanoke, Virginia, United States'
    },
    {
      id: 1911,
      name: 'Yeager Airport',
      code: 'CRW',
      latitude: 38.37315,
      longitude: -81.59318,
      volume: 21015,
      city: 'Charleston, West Virginia, United States'
    },
    {
      id: 12343,
      name: 'Concord Regional Airport',
      code: 'USA',
      latitude: 35.38555,
      longitude: -80.71097,
      volume: 15135,
      city: 'Concord, North Carolina, United States'
    },
    {
      id: 5423,
      name: 'Lansing, Capital City Airport',
      code: 'LAN',
      latitude: 42.7787,
      longitude: -84.58736,
      volume: 13757,
      city: 'Lansing, Michigan, United States'
    },
    {
      id: 2218,
      name: 'Rickenbacker International Airport',
      code: 'LCK',
      latitude: 39.8172,
      longitude: -82.93616,
      volume: 13121,
      city: 'Columbus, Ohio, United States'
    },
    {
      id: 953,
      name: 'MBS International Airport',
      code: 'MBS',
      latitude: 43.53211,
      longitude: -84.0877,
      volume: 11056,
      city: 'Bay City, Michigan, United States'
    },
    {
      id: 572,
      name: 'Tri-State Airport',
      code: 'HTS',
      latitude: 38.36647,
      longitude: -82.55794,
      volume: 8334,
      city: 'Ashland, Kentucky, United States'
    },
    {
      id: 8235,
      name: 'Provo Municipal Airport',
      code: 'PVU',
      latitude: 40.21551,
      longitude: -111.72132,
      volume: 7729,
      city: 'Provo, Utah, United States'
    },
    {
      id: 2094,
      name: 'North Central West Virginia Airport',
      code: 'CKB',
      latitude: 39.29891,
      longitude: -80.23171,
      volume: 3725,
      city: 'Clarksburg, West Virginia, United States'
    },
    {
      id: 980,
      name: 'Raleigh County Memorial Airport',
      code: 'BKW',
      latitude: 37.78706,
      longitude: -81.12399,
      volume: 1536,
      city: 'Beckley, West Virginia, United States'
    },
    {
      id: 4580,
      name: 'Jonesboro Municipal Airport',
      code: 'JBR',
      latitude: 35.83119,
      longitude: -90.64622,
      volume: 386,
      city: 'Jonesboro, Arkansas, United States'
    }
  ],
  diseaseInformation: {
    id: 32,
    name: 'Hepatitis A',
    agents: 'Hepatitis A virus',
    agentTypes: 'Virus',
    transmissionModes: 'Fecal/Oral, Sexual Intercourse',
    incubationPeriod: '15 days to 50 days (28 days avg.)',
    preventionMeasure: 'Vaccine',
    biosecurityRisk: 'No/unknown risk'
  }
};

export const test = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <EventDetailPanel
      event={event}
      isLoading={false}
      onClose={action('closed')} />
  </div>
);

export const outbreakSurveillance = () => (
  <div style={{ width: 370, padding: '10px' }}>
    <OutbreakSurveillanceOverall
      caseCounts={event.caseCounts}
      eventLocations={event.eventLocations}
    />
  </div>
);

