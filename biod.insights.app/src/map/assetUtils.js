import { locationTypes } from './constants';
import base64 from 'base-64';
import utf8 from 'utf8';

function getLocationIcon(locationType, color, encoded=false) {
  let icon = '';

  switch (locationType) {
    case locationTypes.CITY:
      icon =
        `
        <svg width="21" height="20" viewBox="0 0 21 20" fill="none" xmlns="http://www.w3.org/2000/svg">
          <g filter="url(#filter0_d)">
            <path d="M11.1719 3.39177C11.1027 3.27243 11.0042 3.17354 10.886 3.10484C10.7678 3.03613 10.6341 3 10.498 3C10.362 3 10.2283 3.03613 10.1101 3.10484C9.99193 3.17354 9.89339 3.27243 9.82419 3.39177L6.10897 9.79694C6.03872 9.91796 6.00113 10.0558 6.00003 10.1964C5.99892 10.337 6.03433 10.4753 6.10268 10.5975C6.17102 10.7197 6.26986 10.8212 6.38917 10.8919C6.50848 10.9626 6.64402 10.9999 6.78205 11H14.2172C14.3553 11 14.4909 10.9628 14.6103 10.8922C14.7297 10.8216 14.8287 10.72 14.8971 10.5978C14.9656 10.4757 15.001 10.3372 15 10.1966C14.9989 10.0559 14.9613 9.91802 14.891 9.79694L11.1719 3.39177Z" fill="${color}" />
            <path d="M10.9556 3.51718L10.9557 3.5173L14.6748 9.92245C14.7232 10.0058 14.7492 10.101 14.75 10.1985C14.7507 10.2959 14.7261 10.3916 14.679 10.4757C14.6319 10.5598 14.5642 10.6291 14.4831 10.677C14.402 10.725 14.3103 10.75 14.2172 10.75H14.2172H6.78224C6.68921 10.7499 6.59758 10.7248 6.5166 10.6768C6.43558 10.6288 6.36788 10.5595 6.32085 10.4754C6.2738 10.3913 6.24925 10.2957 6.25002 10.1983C6.25078 10.1009 6.27683 10.0057 6.32518 9.92245L6.32523 9.92237L10.0404 3.5172L10.0405 3.51718C10.0881 3.43508 10.1555 3.36761 10.2358 3.32096C10.316 3.27434 10.4063 3.25 10.498 3.25C10.5897 3.25 10.6801 3.27434 10.7603 3.32096C10.8406 3.36761 10.908 3.43508 10.9556 3.51718Z" stroke="white" stroke-width="0.5" />
          </g>
          <circle cx="10.5" cy="8" r="1" fill="white" />
          <defs>
            <filter id="filter0_d" x="0" y="0" width="21" height="20" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
              <feFlood flood-opacity="0" result="BackgroundImageFix" />
              <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" />
              <feOffset dy="3" />
              <feGaussianBlur stdDeviation="3" />
              <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0" />
              <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow" />
              <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow" result="shape" />
            </filter>
          </defs>
        </svg>
      `;
      break;
    case locationTypes.PROVINCE:
      icon =
        `
        <svg width="23" height="23" viewBox="0 0 23 23" fill="none" xmlns="http://www.w3.org/2000/svg">
          <g filter="url(#filter0_d)">
            <rect x="11.0234" y="3" width="7.10145" height="7.10145" rx="0.5" transform="rotate(45 11.0234 3)" fill="${color}"/>
            <rect x="11.0234" y="3.35355" width="6.60145" height="6.60145" rx="0.25" transform="rotate(45 11.0234 3.35355)" stroke="white" stroke-width="0.5"/>
          </g>
          <circle cx="11" cy="8" r="1" fill="white"/>
          <defs>
            <filter id="filter0_d" x="0.00390625" y="0" width="22.043" height="22.043" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
              <feFlood flood-opacity="0" result="BackgroundImageFix"/>
              <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
              <feOffset dy="3"/>
              <feGaussianBlur stdDeviation="3"/>
              <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
              <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow"/>
              <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
            </filter>
          </defs>
        </svg>
      `;
      break;
    case locationTypes.COUNTRY:
      icon =
        `
        <svg width="22" height="22" viewBox="0 0 22 22" fill="none" xmlns="http://www.w3.org/2000/svg">
          <g filter="url(#filter0_d)">
            <rect x="16" y="3" width="10" height="10" rx="5" transform="rotate(90 16 3)" fill="${color}"/>
            <rect x="15.75" y="3.25" width="9.5" height="9.5" rx="4.75" transform="rotate(90 15.75 3.25)" stroke="white" stroke-width="0.5"/>
          </g>
        <circle cx="11" cy="8" r="1" fill="white"/>
        <defs>
          <filter id="filter0_d" x="0" y="0" width="22" height="22" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
            <feFlood flood-opacity="0" result="BackgroundImageFix"/>
            <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
            <feOffset dy="3"/>
            <feGaussianBlur stdDeviation="3"/>
            <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
            <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow"/>
            <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
          </filter>
        </defs>
        </svg>
      `;
      break;
    default: break;
  }

  if (encoded) {
    icon = encode(icon);
  }

  return icon;
}

function encode(str) {
  return base64.encode(utf8.encode(str));
}

export default {
  getLocationIcon,
  encode
};