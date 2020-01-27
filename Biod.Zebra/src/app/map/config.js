import utils from './assetUtils';

export const featureCountryPolygonCollection = {
    "featureSet": {
        "features": [],
        "geometryType": "esriGeometryPolygon"
    },
    layerDefinition: {
        "geometryType": "esriGeometryPolygon",
        "objectIdField": "ObjectID",
        "drawingInfo": {
            "renderer": {
                "type": "simple",
                "symbol": {
                    "type": "esriSFS",
                    "style": "esriSFSSolid",
                    "color": [41, 97, 169, 70],
                    "outline": {
                        "type": "esriSLS",
                        "style": "esriSLSSolid",
                        "color": [41, 97, 169],
                        "width": 1
                    }
                }
            }
        },
        "fields": [
            {
                "name": "ObjectID",
                "alias": "ObjectID",
                "type": "esriFieldTypeOID"
            }
        ]
    }
};

const OUTBREAK_PIN_ICON = 
  `
  <svg width="28" height="32" viewBox="0 0 28 32" fill="none" xmlns="http://www.w3.org/2000/svg">
    <g filter="url(#filter0_d)">
      <path fill-rule="evenodd" clip-rule="evenodd" d="M22 7.42308C22 6.63713 21.3629 6 20.5769 6H7.42308C6.63713 6 6 6.63713 6 7.42308V20.5769C6 21.3629 6.63713 22 7.42308 22H10.1205C10.6595 22 11.1523 22.3045 11.3933 22.7867L12.7272 25.4543C13.2516 26.5032 14.7484 26.5032 15.2728 25.4543L16.6067 22.7867C16.8477 22.3045 17.3405 22 17.8795 22H20.5769C21.3629 22 22 21.3629 22 20.5769V7.42308Z" fill="#AE5451"/>
    </g>
    <defs>
      <filter id="filter0_d" x="0.307693" y="0.307693" width="27.3846" height="31.6256" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
        <feFlood flood-opacity="0" result="BackgroundImageFix"/>
        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
        <feOffset/>
        <feGaussianBlur stdDeviation="2.84615"/>
        <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow"/>
        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
      </filter>
    </defs>
  </svg>
  `;

export const featureCountryPointCollection = {
    "featureSet": {
        "features": [],
        "geometryType": "esriGeometryPoint"
    },
    "layerDefinition": {
        "geometryType": "esriGeometryPoint",
        "objectIdField": "ObjectID",
        "drawingInfo": {
            "renderer": {
                "type": "simple",
                "symbol": {
                    "type": "esriPMS",
                  "imageData": utils.encode(OUTBREAK_PIN_ICON),
                    "contentType": "image/svg+xml",
                    "width": 31.43,
                    "height": 36.43,
                    "angle": 0,
                    "xoffset": 0,
                    "yoffset": 13
                }
            }
        },
        "fields": [
            {
                "name": "ObjectID",
                "alias": "ObjectID",
                "type": "esriFieldTypeOID"
            }
        ]
    }
};

export const featureAirportPointCollection = {
    "featureSet": {
        "features": [],
        "geometryType": "esriGeometryPoint"
    },
    layerDefinition: {
        "geometryType": "esriGeometryPoint",
        "objectIdField": "ObjectID",
        "drawingInfo": {
            "renderer": {
                "type": "simple",
                "symbol": {
                    "type": "esriSMS",
                    "style": "esriSMSCircle",
                    "color": [211, 89, 85],
                    "size": 5,
                    "angle": 0,
                    "xoffset": 0,
                    "yoffset": 0,
                    "outline":
                    {
                        "color": [211, 89, 85, 51],
                        "width": 4
                    }
                }
            }
        },
        "fields": [
            {
                "name": "ObjectID",
                "alias": "ObjectID",
                "type": "esriFieldTypeOID"
            }
        ]
    }
};

export const countryPointLabelClassObject = {
    "labelExpressionInfo": { "expression": "' ' + $feature.eventCount + ' '" },
    "useCodedValues": false,
    "labelPlacement": "center-center",
    "symbol": {
        "type": "esriTS",
        "color": [255, 255, 255],
        "backgroundColor": [45, 48, 64],
        "borderLineSize": 0,
        "borderLineColor": [45, 48, 64],
        "haloSize": 0,
        "haloColor": [45, 48, 64],
        "verticalAlignment": "middle",
        "horizontalAlignment": "center",
        "rightToLeft": false,
        "angle": 0,
        "xoffset": 0,
        "yoffset": 15,
        "kerning": false,
        "font": {
            "family": "Arial",
            "size": 9,
            "style": "normal",
            "weight": "bold",
            "decoration": "none"
        }
    }
};
