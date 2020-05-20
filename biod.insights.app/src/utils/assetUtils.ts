import base64 from 'base-64';
import utf8 from 'utf8';
import * as dto from 'client/dto';

function getLocationIcon(locationType: dto.LocationType, color, encoded = false) {
  let icon = '';

  switch (locationType) {
    case dto.LocationType.City:
      icon = `
        <svg width="9" height="8" viewBox="0 0 9 8" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M4.95563 0.51718L4.9557 0.517302L8.67482 6.92245C8.67482 6.92245 8.67483 6.92246 8.67483 6.92247C8.7232 7.00579 8.74924 7.10103 8.74998 7.19846C8.75073 7.2959 8.72613 7.39156 8.67902 7.47568C8.63192 7.55976 8.56415 7.62907 8.48306 7.67703C8.402 7.72498 8.31031 7.75002 8.21723 7.75H8.21717H0.782241C0.689212 7.74993 0.597583 7.72481 0.516598 7.67683C0.435577 7.62883 0.367885 7.55951 0.320852 7.47544C0.273802 7.39134 0.249252 7.29572 0.250017 7.19832C0.250783 7.10093 0.276833 7.00574 0.325182 6.92245L0.325226 6.92237L4.04044 0.517204L4.04045 0.51718C4.08806 0.435084 4.15553 0.367608 4.23576 0.320963C4.31595 0.274338 4.40634 0.25 4.49804 0.25C4.58975 0.25 4.68013 0.274338 4.76033 0.320963C4.84056 0.367608 4.90803 0.435084 4.95563 0.51718Z" fill="${color}" stroke="white" stroke-width="0.5"/>
          <circle cx="4.5" cy="5" r="1" fill="white"/>
        </svg>
      `;
      break;
    case dto.LocationType.Province:
      icon = `
        <svg width="11" height="11" viewBox="0 0 11 11" fill="none" xmlns="http://www.w3.org/2000/svg">
          <rect x="5.02344" y="0.353553" width="6.60145" height="6.60145" rx="0.25" transform="rotate(45 5.02344 0.353553)" fill="${color}" stroke="white" stroke-width="0.5"/>
          <circle cx="5" cy="5" r="1" fill="white"/>
        </svg>
      `;
      break;
    case dto.LocationType.Country:
      icon = `
        <svg width="10" height="10" viewBox="0 0 10 10" fill="none" xmlns="http://www.w3.org/2000/svg">
          <rect x="9.75" y="0.25" width="9.5" height="9.5" rx="4.75" transform="rotate(90 9.75 0.25)" fill="${color}" stroke="white" stroke-width="0.5"/>
          <circle cx="5" cy="5" r="1" fill="white"/>
        </svg>
      `;
      break;
    default:
      break;
  }

  if (encoded) {
    icon = encode(icon);
  }

  return icon;
}

function encode(str: string) {
  return base64.encode(utf8.encode(str));
}

export default {
  getLocationIcon,
  encode
};
