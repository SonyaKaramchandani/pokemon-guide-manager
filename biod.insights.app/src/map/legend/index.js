import './style.scss';
import { locationTypes } from 'utils/constants';
import assetUtils from 'utils/assetUtils';

const LOCATION_ICON_COLOR = '#B2B2B2';
let $legend = null;
let $globalElement = null;
let $eventElement = null;

function init(isGlobalView = true) {
  $legend = window.jQuery('#map-legend');
  const wrapper = `<div class="map-legend__wrapper">` + createHeader() + createDetails() + `</div>`;

  $legend.append(window.jQuery(wrapper));
  $globalElement = window.jQuery('.map-legend__details__pins.global');
  $eventElement = window.jQuery('.map-legend__details__pins.event');

  window.jQuery('.map-legend__header-toggle').on('click', () => {
    window.jQuery('.map-legend__details').toggle();
  });

  updateDetails(isGlobalView);
}

function createHeader() {
  return `
      <div class="map-legend__header row">
        <div class="map-legend__headertext col-8">Legend</div>
        <span class="map-legend__header-toggle col-1" data-toggle="collapse" data-target=".map-legend__details">
          <svg class="expand" width="13" height="13" viewBox="0 0 13 13" fill="none" xmlns="http://www.w3.org/2000/svg">
            <g clip-path="url(#clip0)">
              <path d="M5.28125 7.71875L0.40625 12.5937" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
              <path d="M12.5938 4.0625V0.40625H8.9375" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
              <path d="M0.40625 8.9375V12.5938H4.0625" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
              <path d="M12.5938 0.40625L7.71875 5.28125" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
            </g>
            <defs>
              <clipPath id="clip0">
                <rect width="13" height="13" fill="white" />
              </clipPath>
            </defs>
          </svg>
          <svg class="collapse" width="15" height="15" viewBox="0 0 15 15" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M13.5938 1.40625L8.71875 6.28125" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
            <path d="M6.28125 12.375V8.71875H2.625" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
            <path d="M8.71875 2.625V6.28125H12.375" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
            <path d="M6.28125 8.71875L1.40625 13.5938" stroke="#CBD2D3" stroke-width="1.25" stroke-linecap="round" stroke-linejoin="round" />
          </svg>
        </span>
      </div>
    `;
}

function createDetails() {
  return (
    `<div class="map-legend__details collapse show">` +
    createPinDetailsForGlobalView() +
    createPinDetailsForEventDetailView() +
    createClassBreakDescription() +
    createImportationDetails() +
    `</div>`
  );
}

function createPinDetailsForGlobalView() {
  return (
    `
      <div class="map-legend__details__pins global" style="display:none;">
        <div class="row">
          <div class="col-3 map-legend__icon"><div class="map-legend__icon-aoi map-legend__icon-outline"></div></div>
          <div class="col-9 map-legend__description">My Location(s)</div>
        </div>
        <div class="row">
          <div class="col-3 map-legend__icon">
            <svg width="27" height="31" viewBox="0 0 27 31" fill="none" xmlns="http://www.w3.org/2000/svg">
              <g filter="url(#filter0_d)">
                <path fill-rule="evenodd" clip-rule="evenodd" d="M21 7.59976C21 6.86294 20.4027 6.26562 19.6659 6.26562H7.33413C6.59731 6.26562 6 6.86294 6 7.59976V19.9315C6 20.6683 6.59731 21.2656 7.33414 21.2656H9.863C10.3683 21.2656 10.8303 21.5511 11.0563 22.0031L12.3067 24.504C12.7984 25.4873 14.2016 25.4873 14.6933 24.504L15.9437 22.0031C16.1697 21.5511 16.6317 21.2656 17.137 21.2656H19.6659C20.4027 21.2656 21 20.6683 21 19.9315V7.59976Z" fill="#AE5451"/>
              </g>
                <path d="M11.9699 15.1366H13.1343V12.04H12.1303V11.2722C12.4228 11.2159 12.6703 11.1484 12.8728 11.0697C13.0753 10.9909 13.2721 10.8953 13.4634 10.7828H14.3746V15.1366H15.3787V16.1406H11.9699V15.1366Z" fill="white"/>
              <defs>
                <filter id="filter0_d" x="0.663462" y="0.929087" width="25.6731" height="29.6489" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                  <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                  <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0"/>
                  <feOffset/>
                  <feGaussianBlur stdDeviation="2.66827"/>
                  <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                  <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow"/>
                  <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow" result="shape"/>
                </filter>
              </defs>
            </svg>
          </div>
          <div class="col-9 map-legend__description">Outbreak event</div>
        </div>` +
    createLocationRows('col-3', 'col-9') +
    ` </div>`
  );
}

function createPinDetailsForEventDetailView() {
  return (
    `
      <div class="map-legend__details__pins event" style="display:none;">
        <div class="row">
          <div class="col-2 map-legend__icon"><div class="map-legend__icon-aoi map-legend__icon-outline"></div></div>
          <div class="col-10 map-legend__description">My Location(s)</div>
        </div>
        <div class="row">
          <div class="col-2 map-legend__icon"><div class="map-legend__icon-outbreak map-legend__icon-outline"></div></div>
          <div class="col-10 map-legend__description">Location of outbreak</div>
        </div>` +
    createLocationRows() +
    `   <div class="row">
          <div class="col-2 map-legend__icon">
            <svg width="10" height="10" viewBox="0 0 10 10" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="5" cy="5" r="4.75" fill="#3F4B56" stroke="white" stroke-width="0.5"/>
              <path d="M7.74308 6.12373C7.87128 6.16179 8 6.06573 8 5.932V5.71385C8 5.64327 7.9628 5.57791 7.90211 5.54188L5.57158 4.15812C5.51089 4.12209 5.47368 4.05673 5.47368 3.98615V2.45C5.47368 2.201 5.26211 2 5 2C4.73789 2 4.52632 2.201 4.52632 2.45V3.98615C4.52632 4.05673 4.48911 4.12209 4.42842 4.15812L2.09789 5.54188C2.0372 5.57791 2 5.64327 2 5.71385V5.932C2 6.06573 2.12872 6.16179 2.25692 6.12373L4.2694 5.52627C4.3976 5.48821 4.52632 5.58427 4.52632 5.718V6.99693C4.52632 7.0616 4.49504 7.12228 4.44237 7.15981L3.97868 7.49019C3.92601 7.52772 3.89474 7.5884 3.89474 7.65307V7.73848C3.89474 7.87025 4.01995 7.96601 4.14713 7.93149L4.94761 7.71422C4.98192 7.70491 5.01808 7.70491 5.05239 7.71422L5.85287 7.93149C5.98005 7.96601 6.10526 7.87025 6.10526 7.73848V7.65307C6.10526 7.5884 6.07399 7.52772 6.02132 7.49019L5.55763 7.15981C5.50496 7.12228 5.47368 7.0616 5.47368 6.99693V5.718C5.47368 5.58427 5.6024 5.48821 5.7306 5.52627L7.74308 6.12373Z" fill="white"/>
            </svg>
          </div>
          <div class="col-10 map-legend__description">Airport</div>
        </div>
      </div>
    `
  );
}

function createLocationRows(iconFlexClassSize = 'col-2', descriptionFlexClassSize = 'col-10') {
  return (
    `
      <div class="row">
        <div class="${iconFlexClassSize} map-legend__icon"><div class="map-legend__icon-city">` +
    assetUtils.getLocationIcon(locationTypes.CITY, LOCATION_ICON_COLOR) +
    `</div></div>
        <div class="${descriptionFlexClassSize} map-legend__description">City/Township</div>
      </div>
      <div class="row">
        <div class="${iconFlexClassSize} map-legend__icon"><div class="map-legend__icon-province">` +
    assetUtils.getLocationIcon(locationTypes.PROVINCE, LOCATION_ICON_COLOR) +
    `</div></div>
        <div class="${descriptionFlexClassSize} map-legend__description">Province/State</div>
      </div>
      <div class="row">
        <div class="${iconFlexClassSize} map-legend__icon"><div class="map-legend__icon-country">` +
    assetUtils.getLocationIcon(locationTypes.COUNTRY, LOCATION_ICON_COLOR) +
    `</div></div>
        <div class="${descriptionFlexClassSize} map-legend__description">Country</div>
      </div>
    `
  );
}

function createClassBreakDescription() {
  return `
      <div class="map-legend__details__pins event" style="display:none;">
        <div class="row">
          <div class="col-3 map-legend__icon">
            <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="16" cy="16" r="16" fill="#9A4A48" fill-opacity="0.25"/>
              <circle cx="16" cy="16" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-9 map-legend__description">Reported cases<br/>since event start</span></div>
        </div>
        <div class="row">
          <div class="col-3 map-legend__icon">
            <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="16" cy="16" r="16" fill="#3F4B56" fill-opacity="0.25"/>
              <circle cx="16" cy="16" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-9 map-legend__description">Estimated number of imported infected travellers/month</div>
        </div>
      </div>
    `;
}

function createImportationDetails() {
  return `
      <div class="map-legend__details__pins event" style="display:none;">
        <div class="map-legend__subheadertext">Number of cases</div>
        <div class="row">
          <div class="col-6 map-legend__icon" style="padding-left: 40px;">
            <svg width="10" height="10" viewBox="0 0 10 10" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="5" cy="5" r="4.5" stroke="#D9DBDD"/>
            </svg>
          </div>
          <div class="col-6 map-legend__description" style="padding-left: 42px;">&lt;1</div>
        </div>
        <div class="row">
          <div class="col-6 map-legend__icon" style="padding-left: 37px;">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="8" cy="8" r="8" fill="#969696" fill-opacity="0.2"/>
              <circle cx="8" cy="8" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-6 map-legend__description" style="padding-left: 39px;">1-9</div>
        </div>
        <div class="row">
          <div class="col-6 map-legend__icon" style="padding-left: 33px;">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="12" cy="12" r="12" fill="#969696" fill-opacity="0.2"/>
              <circle cx="12" cy="12" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-6 map-legend__description" style="padding-left: 35px;">10-99</div>
        </div>
        <div class="row">
          <div class="col-6 map-legend__icon" style="padding-left: 29px;">
            <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="16" cy="16" r="16" fill="#969696" fill-opacity="0.2"/>
              <circle cx="16" cy="16" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-6 map-legend__description" style="padding-left: 31px;">100-9,999</div>
        </div>
        <div class="row">
          <div class="col-6 map-legend__icon" style="padding-left: 25px;">
            <svg width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="20" cy="20" r="20" fill="#969696" fill-opacity="0.25"/>
              <circle cx="20" cy="20" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-6 map-legend__description" style="padding-left: 27px;">10,000-99,999</div>
        </div>
        <div class="row">
          <div class="col-6 map-legend__icon" style="padding-left: 18px;">
            <svg width="54" height="54" viewBox="0 0 54 54" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="27" cy="27" r="27" fill="#969696" fill-opacity="0.25"/>
              <circle cx="27" cy="27" r="4.5" stroke="white"/>
            </svg>
          </div>
          <div class="col-6 map-legend__description" style="padding-left: 20px;">100,000+</div>
        </div>
      </div>  
    `;
}

function updateDetails(isGlobalView) {
  if (isGlobalView) {
    $globalElement.show();
    $eventElement.hide();
  } else {
    $globalElement.hide();
    $eventElement.show();
  }
}

export default {
  init,
  updateDetails
};
