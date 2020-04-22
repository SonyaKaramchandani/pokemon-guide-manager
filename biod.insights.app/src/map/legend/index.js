import './style.scss';
import { locationTypes } from 'utils/constants';
import assetUtils from 'utils/assetUtils';

const LOCATION_ICON_COLOR = '#B2B2B2';
let $legend = null;
let $globalSection = null;
let $eventSections = null;
let $airportSection = null;

function init(isGlobalView = true) {
  $legend = window.jQuery('#map-legend');
  const wrapper = `<div class="map-legend__wrapper">` + createHeader() + createDetails() + `</div>`;

  $legend.append(window.jQuery(wrapper));
  $globalSection = window.jQuery('.map-legend__details__pins.global');
  $eventSections = window.jQuery('.map-legend__details__pins.event');
  $airportSection = window.jQuery('.map-legend__details__pins.airport-pins');

  window.jQuery('.map-legend__details').toggle(); //set default state collapsed
  toggleLegend();
  toggleGlobalLegend(isGlobalView);
}

function toggleLegend() {
  window.jQuery('.map-legend__header.row').on('click', () => {
    window.jQuery('.map-legend__details').toggle();
    const target = window.jQuery('.map-legend__header-toggle');
    if (target.hasClass('collapsed')) {
      target.removeClass('collapsed');
    } else {
      target.addClass('collapsed');
    }
  });
}

function createHeader() {
  return `
      <div class="map-legend__header row">
        <div class="map-legend__headertext col-8">Legend</div>
        <span class="map-legend__header-toggle collapsed col-1" data-toggle="collapse" data-target=".map-legend__details">
          <svg class="collapse" width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M8.6 11.4001L3 17.0001" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M16.9988 7.2V3H12.7988" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M3 12.8003V17.0003H7.2" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M17.0014 3L11.4014 8.6" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
          <svg class="expand"  width="21" height="20" viewBox="0 0 21 20" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M3.02891 17.0002L8.62891 11.4002" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M11.4367 4.39985L11.4367 8.59985L15.6367 8.59985" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M8.62891 15.5999L8.62891 11.3999L4.42891 11.3999" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M11.4313 8.59985L17.0313 2.99985" stroke="#CBD2D3" stroke-linecap="round" stroke-linejoin="round"/>
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
    createEstimatedImportedCasesSection() +
    createReportedCasesSection() +
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
                <path fill-rule="evenodd" clip-rule="evenodd" d="M21 7.33413C21 6.59731 20.4027 6 19.6659 6H7.33413C6.59731 6 6 6.59731 6 7.33414V19.6659C6 20.4027 6.59731 21 7.33414 21H9.863C10.3683 21 10.8303 21.2855 11.0563 21.7375L12.3067 24.2384C12.7984 25.2217 14.2016 25.2217 14.6933 24.2384L15.9437 21.7375C16.1697 21.2855 16.6317 21 17.137 21H19.6659C20.4027 21 21 20.4027 21 19.6659V7.33413Z" fill="#AE5451"/>
              </g>
              <path d="M11.9699 15.875V14.8709H13.1343V11.7744H12.1303V11.0066C12.4228 10.9503 12.6703 10.8828 12.8728 10.8041C13.0753 10.7253 13.2721 10.6297 13.4634 10.5172H14.3746V14.8709H15.3787V15.875H11.9699Z" fill="white"/>
              <defs>
                <filter id="filter0_d" x="0.663462" y="0.663462" width="25.6731" height="29.6489" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
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
            <svg width="11" height="10" viewBox="0 0 11 10" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M9.80155 5C9.80155 7.62214 7.66457 9.75 5.02577 9.75C2.38697 9.75 0.25 7.62214 0.25 5C0.25 2.37786 2.38697 0.25 5.02577 0.25C7.66457 0.25 9.80155 2.37786 9.80155 5Z" fill="#536169" stroke="white" stroke-width="0.5"/>
              <path d="M7.78502 6.1242C7.91316 6.16205 8.04167 6.066 8.04167 5.93239V5.71415C8.04167 5.6434 8.0043 5.57792 7.94339 5.54195L5.60061 4.15805C5.5397 4.12208 5.50233 4.0566 5.50233 3.98585V2.45C5.50233 2.201 5.28966 2 5.02621 2C4.76275 2 4.55008 2.201 4.55008 2.45V3.98585C4.55008 4.0566 4.51271 4.12208 4.4518 4.15805L2.10902 5.54195C2.04811 5.57792 2.01074 5.6434 2.01074 5.71415V5.93239C2.01074 6.06601 2.13925 6.16205 2.26739 6.1242L4.29343 5.5258C4.42157 5.48796 4.55008 5.584 4.55008 5.71761V6.99662C4.55008 7.06146 4.51864 7.12229 4.46574 7.15978L3.99959 7.49022C3.94668 7.52772 3.91525 7.58854 3.91525 7.65338V7.73883C3.91525 7.8705 4.04027 7.96624 4.16739 7.93191L4.97407 7.71408C5.00821 7.70486 5.0442 7.70486 5.07835 7.71408L5.88503 7.93191C6.01214 7.96624 6.13717 7.8705 6.13717 7.73883V7.65338C6.13717 7.58854 6.10573 7.52772 6.05283 7.49022L5.58667 7.15978C5.53377 7.12229 5.50233 7.06146 5.50233 6.99662V5.71761C5.50233 5.584 5.63084 5.48796 5.75898 5.5258L7.78502 6.1242Z" fill="white"/>
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

function createEstimatedImportedCasesSection() {
  return `
    <div class="map-legend__details__pins airport-pins" style="display:none;">
      <div class="map-legend__subheader">
        <div class="map-legend__subheader-title">Estimated imported cases</div>
        <div class="map-legend__subheader-description">Estimated number of imported infected travellers in one month</div>
      </div>
      ${createPlaneCircleLegendRow(16, 'Up to 10')}
      ${createPlaneCircleLegendRow(24, '11 to 100')}
      ${createPlaneCircleLegendRow(32, '101 to 1,000')}
      ${createPlaneCircleLegendRow(40, '>1,000')}
    </div>`;
}

function createPlaneCircleLegendRow(diameter, label) {
  const diameterX = 56;
  const diameterY = diameter;
  return `
  <div class="row">
    <div class="col-4 map-legend__icon">
      <svg width="${diameterX}" height="${diameterY}" viewBox="0 0 ${diameterX} ${diameterY}" fill="none" xmlns="http://www.w3.org/2000/svg">
        <style type="text/css">
          .st0{fill:#E0E4E5;}
          .st1{fill:none;stroke:#FFFFFF;}
          .st2{fill:#536169;}
          .st3{fill:#FFFFFF;}
        </style>
        <circle class="st0" cx="${diameterX / 2}" cy="${diameterY / 2}" r="${diameterY / 2}"/>
        <g transform="translate(${diameterX / 2 - 16}, ${diameterY / 2 - 16})">
          <g>
            <circle class="st2" cx="16" cy="16" r="4.8"/>
            <path class="st3" d="M16,21c-2.8,0-5-2.2-5-5s2.2-5,5-5s5,2.2,5,5S18.8,21,16,21z M16,11.5c-2.5,0-4.5,2-4.5,4.5s2,4.5,4.5,4.5
              s4.5-2,4.5-4.5S18.5,11.5,16,11.5z"/>
          </g>
          <path class="st3" d="M18.7,17.1c0.1,0,0.3-0.1,0.3-0.2v-0.2c0-0.1,0-0.1-0.1-0.2l-2.3-1.4c-0.1,0-0.1-0.1-0.1-0.2v-1.5
            c0-0.2-0.2-0.4-0.5-0.4s-0.5,0.2-0.5,0.4V15c0,0.1,0,0.1-0.1,0.2l-2.3,1.4c-0.1,0-0.1,0.1-0.1,0.2v0.2c0,0.1,0.1,0.2,0.3,0.2l2-0.6
            c0.1,0,0.3,0.1,0.3,0.2V18c0,0.1,0,0.1-0.1,0.2L15,18.5c-0.1,0-0.1,0.1-0.1,0.2v0.1c0,0.1,0.1,0.2,0.3,0.2l0.8-0.2c0,0,0.1,0,0.1,0
            l0.8,0.2c0.1,0,0.3-0.1,0.3-0.2v-0.1c0-0.1,0-0.1-0.1-0.2l-0.5-0.3c-0.1,0-0.1-0.1-0.1-0.2v-1.3c0-0.1,0.1-0.2,0.3-0.2L18.7,17.1z"
            />
        </g>
        <circle class="st1" cx="${diameterX / 2}" cy="${diameterY / 2}" r="4.5"/>
      </svg>
    </div>
    <div class="col-8 map-legend__description">${label}</div>
  </div>`;
}

function createReportedCasesSection() {
  return `
    <div class="map-legend__details__pins event" style="display:none;">
      <div class="map-legend__subheader">
        <div class="map-legend__subheader-title">Reported cases</div>
        <div class="map-legend__subheader-description">Number of reported cases since the event start</div>
      </div>
      ${createMagnitudeCircleLegendRow(16, 'Up to 10')}
      ${createMagnitudeCircleLegendRow(24, '11 to 100')}
      ${createMagnitudeCircleLegendRow(32, '101 to 1,000')}
      ${createMagnitudeCircleLegendRow(40, '1,001 to 10,000')}
      ${createMagnitudeCircleLegendRow(48, '10,001 to 100,000')}
      ${createMagnitudeCircleLegendRow(56, '>100,000')}
    </div>`;
}

function createMagnitudeCircleLegendRow(diameter, label) {
  const diameterX = 56;
  const diameterY = diameter;
  return `
  <div class="row">
    <div class="col-4 map-legend__icon">
      <svg width="${diameterX}" height="${diameterY}" viewBox="0 0 ${diameterX} ${diameterY}" fill="none" xmlns="http://www.w3.org/2000/svg">
        <circle cx="${diameterX / 2}" cy="${diameterY / 2}" r="${diameterY /
    2}" fill="#9A4A48" fill-opacity="0.25"/>
        <circle cx="${diameterX / 2}" cy="${diameterY / 2}" r="4.5" stroke="white"/>
      </svg>
    </div>
    <div class="col-8 map-legend__description">${label}</div>
  </div>`;
}

function toggleGlobalLegend(isGlobalView) {
  if (isGlobalView) {
    $globalSection.show();
    $eventSections.hide();
    toggleAirportLegend(false);
  } else {
    $globalSection.hide();
    $eventSections.show();
  }
}

function toggleAirportLegend(isDetailsView) {
  if (isDetailsView) {
    $airportSection.show();
  } else {
    $airportSection.hide();
  }
}

export default {
  init,
  toggleGlobalLegend,
  toggleAirportLegend
};
