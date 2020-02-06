export const mockGetEventListModel = {
  importationRisk: null,
  exportationRisk: null,
  eventsList: [
    {
      isLocal: false,
      eventInformation: {
        id: 482,
        title: 'Measles in Toronto',
        startDate: '2018-09-20T00:00:00',
        endDate: null,
        lastUpdatedDate: '2019-01-04T17:40:50',
        summary: 'Testing fgfdfrervdfrte',
        diseaseId: 10
      },
      importationRisk: null,
      exportationRisk: {
        isModelNotRun: false,
        minProbability: 1,
        maxProbability: 1,
        minMagnitude: 18979.787,
        maxMagnitude: 19107.598
      },
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 555555,
        suspectedCases: 1,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasSuspectedCasesNesting: false,
        hasDeathsNesting: false
      },
      eventLocations: [
        {
          geonameId: 6167865,
          locationName: 'Toronto, Ontario, Canada',
          provinceName: 'Ontario',
          countryName: 'Canada',
          locationType: 2,
          caseCounts: {
            confirmedCases: 0,
            reportedCases: 555555,
            suspectedCases: 1,
            deaths: 0,
            hasConfirmedCasesNesting: false,
            hasReportedCasesNesting: false,
            hasSuspectedCasesNesting: false,
            hasDeathsNesting: false
          }
        }
      ],
      articles: [],
      diseaseInformation: {
        id: 10,
        name: null,
        agents: null,
        agentTypes: null,
        transmissionModes: null,
        incubationPeriod: null,
        preventionMeasure: null,
        biosecurityRisk: null
      }
    },
    {
      isLocal: false,
      eventInformation: {
        id: 514,
        title: 'Oct 09 Test event',
        startDate: '2018-10-01T00:00:00',
        endDate: null,
        lastUpdatedDate: '2019-01-02T10:09:09',
        summary: 'Testing for chagas',
        diseaseId: 52
      },
      importationRisk: null,
      exportationRisk: {
        isModelNotRun: false,
        minProbability: 1,
        maxProbability: 1,
        minMagnitude: 9775.144,
        maxMagnitude: 9842.862
      },
      caseCounts: {
        confirmedCases: 4,
        reportedCases: 555555,
        suspectedCases: 5,
        deaths: 22,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasSuspectedCasesNesting: false,
        hasDeathsNesting: false
      },
      eventLocations: [
        {
          geonameId: 1816670,
          locationName: 'Beijing, Beijing Shi, China',
          provinceName: 'Beijing Shi',
          countryName: 'China',
          locationType: 2,
          caseCounts: {
            confirmedCases: 4,
            reportedCases: 555555,
            suspectedCases: 5,
            deaths: 22,
            hasConfirmedCasesNesting: false,
            hasReportedCasesNesting: false,
            hasSuspectedCasesNesting: false,
            hasDeathsNesting: false
          }
        }
      ],
      articles: [],
      diseaseInformation: {
        id: 52,
        name: null,
        agents: null,
        agentTypes: null,
        transmissionModes: null,
        incubationPeriod: null,
        preventionMeasure: null,
        biosecurityRisk: null
      }
    },
    {
      isLocal: false,
      eventInformation: {
        id: 2129,
        title: 'Swine Influenza in Mexico',
        startDate: '2019-11-26T00:00:00',
        endDate: null,
        lastUpdatedDate: '2019-11-28T12:00:43',
        summary: 'uh oh!',
        diseaseId: 112
      },
      importationRisk: null,
      exportationRisk: {
        isModelNotRun: false,
        minProbability: 1,
        maxProbability: 1,
        minMagnitude: 5464.428,
        maxMagnitude: 5511.338
      },
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 345347,
        suspectedCases: 0,
        deaths: 0,
        hasConfirmedCasesNesting: false,
        hasReportedCasesNesting: false,
        hasSuspectedCasesNesting: false,
        hasDeathsNesting: false
      },
      eventLocations: [
        {
          geonameId: 3816697,
          locationName: 'Baja California, Estado de Chiapas, Mexico',
          provinceName: 'Estado de Chiapas',
          countryName: 'Mexico',
          locationType: 2,
          caseCounts: {
            confirmedCases: 0,
            reportedCases: 1,
            suspectedCases: 0,
            deaths: 0,
            hasConfirmedCasesNesting: false,
            hasReportedCasesNesting: false,
            hasSuspectedCasesNesting: false,
            hasDeathsNesting: false
          }
        },
        {
          geonameId: 8581941,
          locationName: 'Acapulco de Juarez, Estado de Guerrero, Mexico',
          provinceName: 'Estado de Guerrero',
          countryName: 'Mexico',
          locationType: 2,
          caseCounts: {
            confirmedCases: 0,
            reportedCases: 345345,
            suspectedCases: 0,
            deaths: 0,
            hasConfirmedCasesNesting: false,
            hasReportedCasesNesting: false,
            hasSuspectedCasesNesting: false,
            hasDeathsNesting: false
          }
        },
        {
          geonameId: 8583659,
          locationName: 'Aculco, Estado de Mexico, Mexico',
          provinceName: 'Estado de Mexico',
          countryName: 'Mexico',
          locationType: 2,
          caseCounts: {
            confirmedCases: 0,
            reportedCases: 1,
            suspectedCases: 0,
            deaths: 0,
            hasConfirmedCasesNesting: false,
            hasReportedCasesNesting: false,
            hasSuspectedCasesNesting: false,
            hasDeathsNesting: false
          }
        }
      ],
      articles: [
        {
          title: 'The first death due to influenza in BC is recorded',
          url:
            'https://www.debate.com.mx/salud/Se-registra-la-primera-muerte-por-influenza-en-BC-20181228-0130.html',
          publishedDate: '2018-12-29T05:00:00',
          originalLanguage: 'es',
          sourceName: 'News Media'
        },
        {
          title: 'Influenza AH1N1 collects its first victim in BC',
          url:
            'https://www.lacronica.com/Noticias/2018/12/29/1397918-Cobra-influenza-AH1N1-su-primera-victima-en-BC.html',
          publishedDate: '2018-12-29T05:00:00',
          originalLanguage: 'es',
          sourceName: 'News Media'
        }
      ],
      diseaseInformation: {
        id: 112,
        name: null,
        agents: null,
        agentTypes: null,
        transmissionModes: null,
        incubationPeriod: null,
        preventionMeasure: null,
        biosecurityRisk: null
      }
    }
  ]
};
export const mockGetEventModel = {
  isLocal: !1,
  eventInformation: {
    id: 146,
    title: 'Hepatitis A in USA',
    startDate: '2018-01-01T00:00:00',
    endDate: null,
    lastUpdatedDate: '2018-08-16T16:37:18',
    summary:
      'An outbreak of hepatitis A has been occurring in the USA since June 7th, 2018. Hepatitis A has an incubation period of 15-50 days.  Common clinical features can be divided into preicteric and icteric phases. The preicteric phase (5-7 days) is characterized by non-specific febrile symptoms, anorexia, and abdominal pain. The icteric phase (up to 6 months) is characterized by jaundice, acholic stools, and abnormal LFTs. Case fatality is under 2%. Transmission of the virus is food/waterborne.  No additional infection control measures are recommended. Notification of public health is mandatory. '
  },
  importationRisk: {
    isModelNotRun: !1,
    minProbability: 0.9998,
    maxProbability: 1,
    minMagnitude: 8.76,
    maxMagnitude: 11.75
  },
  exportationRisk: {
    isModelNotRun: !1,
    minProbability: 0.9998,
    maxProbability: 1,
    minMagnitude: 8.76,
    maxMagnitude: 11.75
  },
  outbreakPotentialCategory: { id: 5, name: 'Hepatitis' },
  caseCounts: {
    confirmedCases: 282,
    reportedCases: 3575,
    deaths: 32,
    hasConfirmedCasesNesting: !0,
    hasReportedCasesNesting: !1,
    hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !0,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
      }
    },
    {
      geonameId: 4106979,
      locationName: 'Craighead County, Arkansas, United States',
      locationType: 2,
      caseCounts: {
        confirmedCases: 0,
        reportedCases: 4,
        deaths: 10,
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: 1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !1,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
        hasConfirmedCasesNesting: !0,
        hasReportedCasesNesting: !1,
        hasDeathsNesting: !1
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
export const mockDiseaseListProcessed = [
  {
    diseaseInformation: {
      id: 195,
      name: 'Abrin Poisoning',
      agents: 'Abrin',
      agentTypes: 'Toxin',
      transmissionModes: 'Airborne, Foodborne, Waterborne',
      incubationPeriod: '8 hours to 3 days (1 day avg.)',
      preventionMeasure: '-',
      biosecurityRisk:
        'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
    },
    importationRisk: {
      isModelNotRun: !0,
      minProbability: 0,
      maxProbability: 0,
      minMagnitude: 0,
      maxMagnitude: 0
    },
    exportationRisk: {
      isModelNotRun: !0,
      minProbability: 0,
      maxProbability: 0,
      minMagnitude: 0,
      maxMagnitude: 0
    },
    lastUpdatedEventDate: '2019-11-21T13:39:09',
    outbreakPotentialCategory: {
      id: 5,
      attributeId: 4,
      name: 'Negligible or none',
      diseaseId: 195
    },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 10,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 195
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 117,
      name: 'Acute flaccid myelitis',
      agents: '-',
      agentTypes: '-',
      transmissionModes: '-',
      incubationPeriod: '-',
      preventionMeasure: '-',
      biosecurityRisk: '-'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0,
      maxProbability: 0,
      minMagnitude: 0,
      maxMagnitude: 0
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 0,
      maxProbability: 0.04760003,
      minMagnitude: 0,
      maxMagnitude: 0.049
    },
    lastUpdatedEventDate: '2019-09-24T14:20:23',
    outbreakPotentialCategory: { id: 6, attributeId: 5, name: 'Unknown', diseaseId: 117 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 272190,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 117
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 75,
      name: 'African Tick Bite Fever',
      agents: 'Rickettsia africae',
      agentTypes: 'Bacteria',
      transmissionModes: 'Ticks',
      incubationPeriod: '6 days to 10 days (8 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.20239997,
      maxProbability: 0.21600002,
      minMagnitude: 0.226,
      maxMagnitude: 0.243
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 109.519,
      maxMagnitude: 118.447
    },
    lastUpdatedEventDate: '2019-09-04T11:32:13',
    outbreakPotentialCategory: { id: 5, attributeId: 4, name: 'Negligible or none', diseaseId: 75 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 5005,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 75
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 104,
      name: 'Alveolar Echinococcosis',
      agents: 'Echinococcus multilocularis',
      agentTypes: 'Worm',
      transmissionModes: 'Environ. Contact, Zoonotic Fluid Transmission',
      incubationPeriod: '1 year to 30 years (5 years avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.72523546,
      maxProbability: 0.7496841,
      minMagnitude: 1.292,
      maxMagnitude: 1.385
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 337.953,
      maxMagnitude: 362.618
    },
    lastUpdatedEventDate: '2018-10-24T15:17:55',
    outbreakPotentialCategory: {
      id: 5,
      attributeId: 4,
      name: 'Negligible or none',
      diseaseId: 104
    },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 0,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 104
    },
    hasLocalEvents: !1
  },
  {
    diseaseInformation: {
      id: 25,
      name: 'Amebiasis',
      agents: 'Entamoeba histolytica',
      agentTypes: 'Bacteria',
      transmissionModes: 'Fecal/Oral',
      incubationPeriod: '10 days to 2 years (14 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.45977938,
      maxProbability: 0.48433727,
      minMagnitude: 0.616,
      maxMagnitude: 0.662
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 161.42,
      maxMagnitude: 173.613
    },
    lastUpdatedEventDate: '2019-03-19T09:47:05',
    outbreakPotentialCategory: { id: 3, attributeId: 3, name: 'Sustained', diseaseId: 25 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 0,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 25
    },
    hasLocalEvents: !1
  },
  {
    diseaseInformation: {
      id: 26,
      name: 'Anthrax',
      agents: 'Bacillus anthracis',
      agentTypes: 'Bacteria',
      transmissionModes: 'Airborne, Zoonotic Fluid Transmission',
      incubationPeriod: '1 day to 40 days (5 days avg.)',
      preventionMeasure: 'Vaccine',
      biosecurityRisk:
        'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.030799985,
      maxProbability: 0.033599973,
      minMagnitude: 0.031,
      maxMagnitude: 0.034
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 110.8,
      maxMagnitude: 120.563
    },
    lastUpdatedEventDate: '2019-10-29T16:28:03',
    outbreakPotentialCategory: { id: 5, attributeId: 4, name: 'Negligible or none', diseaseId: 26 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 5e3,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 26
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 113,
      name: 'Avian Influenza',
      agents: 'Influenza A H5N1 virus, Influenza A H5N6, Influenza A H7N9',
      agentTypes: 'Virus',
      transmissionModes: 'Zoonotic Fluid Transmission',
      incubationPeriod: '-',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.31878406,
      maxProbability: 0.33256227,
      minMagnitude: 0.384,
      maxMagnitude: 0.404
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 169.02,
      maxMagnitude: 178.36
    },
    lastUpdatedEventDate: '2019-11-01T12:15:43',
    outbreakPotentialCategory: {
      id: 5,
      attributeId: 4,
      name: 'Negligible or none',
      diseaseId: 113
    },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 2,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 113
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 77,
      name: 'B virus',
      agents: 'Herpesvirus B',
      agentTypes: 'Virus',
      transmissionModes: 'Zoonotic Fluid Transmission',
      incubationPeriod: '3 days to 28 days (4 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 31.917,
      maxMagnitude: 32.171
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 3173.544,
      maxMagnitude: 3199.141
    },
    lastUpdatedEventDate: '2019-07-11T12:16:38',
    outbreakPotentialCategory: { id: 5, attributeId: 4, name: 'Negligible or none', diseaseId: 77 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 10,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 77
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 108,
      name: 'Bourbon virus',
      agents: 'Bourbon virus',
      agentTypes: 'Virus',
      transmissionModes: 'Ticks',
      incubationPeriod: '-',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.022099972,
      maxProbability: 0.023699999,
      minMagnitude: 0.022,
      maxMagnitude: 0.024
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 0.9993,
      maxProbability: 0.9996,
      minMagnitude: 7.277,
      maxMagnitude: 7.826
    },
    lastUpdatedEventDate: '2018-10-25T13:04:37',
    outbreakPotentialCategory: {
      id: 5,
      attributeId: 4,
      name: 'Negligible or none',
      diseaseId: 108
    },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 0,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 108
    },
    hasLocalEvents: !1
  },
  {
    diseaseInformation: {
      id: 52,
      name: 'Chagas',
      agents: 'Trypanosoma cruzi',
      agentTypes: 'Bacteria',
      transmissionModes: 'Bloodborne, Foodborne, Insects (Not Ticks/Mosq.)',
      incubationPeriod: '4 days to 22 days (13 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 30.022,
      maxMagnitude: 30.23
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 9776.463,
      maxMagnitude: 9845.62
    },
    lastUpdatedEventDate: '2019-01-17T09:53:58',
    outbreakPotentialCategory: { id: 3, attributeId: 3, name: 'Sustained', diseaseId: 52 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 50,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 52
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 2,
      name: 'Cholera',
      agents: 'Vibrio cholerae',
      agentTypes: 'Bacteria',
      transmissionModes: 'Fecal/Oral',
      incubationPeriod: '2 hours to 5 days (1 day avg.)',
      preventionMeasure: 'Vaccine',
      biosecurityRisk:
        'Category B: Moderate morbidity and low mortality, moderately easy to disseminate.'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.4243753,
      maxProbability: 0.44787484,
      minMagnitude: 0.553,
      maxMagnitude: 0.59499997
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 220.045,
      maxMagnitude: 240.24901
    },
    lastUpdatedEventDate: '2019-12-06T09:40:04',
    outbreakPotentialCategory: { id: 3, attributeId: 3, name: 'Sustained', diseaseId: 2 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 3040,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 2
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 54,
      name: 'Coccidioidomycosis',
      agents: 'Coccidioides immitis, Coccidioides posadasii',
      agentTypes: 'Fungus',
      transmissionModes: 'Environ. Contact',
      incubationPeriod: '7 days to 21 days (14 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.2572,
      maxProbability: 0.28390002,
      minMagnitude: 0.297,
      maxMagnitude: 0.334
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 67.067,
      maxMagnitude: 75.336
    },
    lastUpdatedEventDate: '2018-10-10T14:37:17',
    outbreakPotentialCategory: { id: 5, attributeId: 4, name: 'Negligible or none', diseaseId: 54 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 0,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 54
    },
    hasLocalEvents: !1
  },
  {
    diseaseInformation: {
      id: 51,
      name: 'Crimean-Congo Hemorrhagic Fever (CCHF)',
      agents: 'Crimean-Congo Hemorrhagic Fever Virus',
      agentTypes: 'Virus',
      transmissionModes: 'Intra-species Fluid Transmission, Ticks, Zoonotic Fluid Transmission',
      incubationPeriod: '5 days to 13 days (6 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.1049,
      maxProbability: 0.11689997,
      minMagnitude: 0.111,
      maxMagnitude: 0.124
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 36.362,
      maxMagnitude: 41.833
    },
    lastUpdatedEventDate: '2019-01-24T13:43:26',
    outbreakPotentialCategory: { id: 1, attributeId: 1, name: 'Sustained', diseaseId: 51 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 0,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 51
    },
    hasLocalEvents: !1
  },
  {
    diseaseInformation: {
      id: 80,
      name: 'Cyclosporiasis',
      agents: 'Cyclospora cayetanensis',
      agentTypes: 'Bacteria',
      transmissionModes: 'Fecal/Oral',
      incubationPeriod: '2 days to 14 days (7 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0,
      maxProbability: 0,
      minMagnitude: 0,
      maxMagnitude: 0
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 0.9544,
      maxProbability: 0.9927,
      minMagnitude: 3.088,
      maxMagnitude: 4.915
    },
    lastUpdatedEventDate: '2019-08-19T15:46:51',
    outbreakPotentialCategory: { id: 3, attributeId: 3, name: 'Sustained', diseaseId: 80 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 126,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 80
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 55,
      name: 'Dengue',
      agents:
        'Dengue virus (DEN-1), Dengue virus (DEN-2), Dengue virus (DEN-3), Dengue virus (DEN-4)',
      agentTypes: 'Virus',
      transmissionModes: 'Mosquitoes',
      incubationPeriod: '3 days to 14 days (8 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.99947774,
      maxProbability: 0.9996847,
      minMagnitude: 7.557,
      maxMagnitude: 8.059
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 1544.247,
      maxMagnitude: 1657.296
    },
    lastUpdatedEventDate: '2019-11-28T10:48:51',
    outbreakPotentialCategory: { id: 3, attributeId: 3, name: 'Sustained', diseaseId: 55 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 90372,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 55
    },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 4,
      name: 'Diphtheria',
      agents: 'Corynebacterium diphtheriae',
      agentTypes: 'Bacteria',
      transmissionModes: 'Droplet, Intra-species Fluid Transmission',
      incubationPeriod: '1 day to 10 days (3 days avg.)',
      preventionMeasure: 'Vaccine',
      biosecurityRisk: 'No/unknown risk'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.21130002,
      maxProbability: 0.22530001,
      minMagnitude: 0.237,
      maxMagnitude: 0.255
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 97.117,
      maxMagnitude: 104.529
    },
    lastUpdatedEventDate: '2019-01-02T13:58:53',
    outbreakPotentialCategory: { id: 1, attributeId: 1, name: 'Sustained', diseaseId: 4 },
    caseCounts: { confirmedCases: 0, reportedCases: 5, suspectedCases: 0, deaths: 0, diseaseId: 4 },
    hasLocalEvents: !0
  },
  {
    diseaseInformation: {
      id: 57,
      name: 'Eastern Equine Encephalitis',
      agents: 'Eastern Equine Encephalitis virus',
      agentTypes: 'Virus',
      transmissionModes: 'Mosquitoes',
      incubationPeriod: '4 days to 10 days (7 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk:
        'Category B: Moderate morbidity and low mortality, moderately easy to disseminate.'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0,
      maxProbability: 0.010900021,
      minMagnitude: 0,
      maxMagnitude: 0.011
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 0,
      maxProbability: 0.6119,
      minMagnitude: 0,
      maxMagnitude: 0.947
    },
    lastUpdatedEventDate: '2018-08-13T09:59:53',
    outbreakPotentialCategory: { id: 5, attributeId: 4, name: 'Negligible or none', diseaseId: 57 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 0,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 57
    },
    hasLocalEvents: !1
  },
  {
    diseaseInformation: {
      id: 29,
      name: 'Ebola',
      agents:
        'Bombali virus (species Bombali ebolavirus), Bundibugyo virus (species Bundibugyo ebolavirus), Ebola virus (species Zaire ebolavirus), Reston virus (species Reston ebolavirus), Sudan virus (species Sudan ebolavirus), Taï Forest ebolavirus',
      agentTypes: 'Virus',
      transmissionModes:
        'Droplet, Intra-species Fluid Transmission, Sexual Intercourse, Zoonotic Fluid Transmission',
      incubationPeriod: '2 days to 21 days (5 days avg.)',
      preventionMeasure: '-',
      biosecurityRisk:
        'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
    },
    importationRisk: {
      isModelNotRun: !1,
      minProbability: 0.79529905,
      maxProbability: 0.81783146,
      minMagnitude: 1.587,
      maxMagnitude: 1.702
    },
    exportationRisk: {
      isModelNotRun: !1,
      minProbability: 1,
      maxProbability: 1,
      minMagnitude: 374.124,
      maxMagnitude: 401.86902
    },
    lastUpdatedEventDate: '2019-09-09T14:19:18',
    outbreakPotentialCategory: { id: 1, attributeId: 1, name: 'Sustained', diseaseId: 29 },
    caseCounts: {
      confirmedCases: 0,
      reportedCases: 499,
      suspectedCases: 0,
      deaths: 0,
      diseaseId: 29
    },
    hasLocalEvents: !0
  }
];
export const diseaseInformation = {
  agents: 'Bacillus anthracis',
  agentType: 'Bacteria',
  transmissionModes: 'Airborne, Zoonotic Fluid Transmission',
  incubationPeriod: '?',
  preventionMeasure: 'Vaccine',
  biosecurityRisk:
    'Category A: High mortality rate, easily disseminated or transmitted from person to person.'
};
export const mockAirports = [
  {
    id: 6400,
    name: 'Benito Juarez International Airport',
    code: 'MEX',
    latitude: 19.4363,
    longitude: -99.0721,
    volume: 1487964,
    city: 'Mexico City, Mexico City, Mexico'
  },
  {
    id: 1711,
    name: 'Cancun International Airport',
    code: 'CUN',
    latitude: 21.03653,
    longitude: -86.87708,
    volume: 935912,
    city: 'Cancun, Quintana Roo, Mexico',
    importationRisk: { minMagnitude: 1, maxMagnitude: 2, minProbability: 5, maxProbability: 50 }
  },
  {
    id: 6370,
    name: 'Manuel Crescencio Rejon International Airport',
    code: 'MID',
    latitude: 20.93698,
    longitude: -89.65767,
    volume: 111936,
    city: 'Merida, Yucatan, Mexico'
  },
  {
    id: 55,
    name: 'General Juan N. Alvarez International Airport',
    code: 'ACA',
    latitude: 16.75706,
    longitude: -99.75395,
    volume: 26363,
    city: 'Acapulco de Juarez, Guerrero, Mexico'
  },
  {
    id: 55,
    name:
      'General Juan N. Alvarez International Airport General Juan N. Alvarez International Airport General Juan N. Alvarez International Airport',
    code: 'ACA',
    latitude: 16.75706,
    longitude: -99.75395,
    volume: 26363,
    importationRisk: { minMagnitude: 1, maxMagnitude: 2, minProbability: 5, maxProbability: 50 },
    city:
      'Acapulco de Juarez, Guerrero, Mexico, Acapulco de Juarez, Guerrero, Mexico, Acapulco de Juarez, Guerrero, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico, Mexico'
  }
];
