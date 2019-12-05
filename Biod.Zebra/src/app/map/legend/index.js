import './style.scss'

let $legend = null;
let $globalElement = null;
let $eventElement = null;

function init(isGlobalView = true) {
  $legend = $('#map-legend');
  const wrapper = `<div class="map-legend__wrapper">` + createHeader() + createDetails() + `</div>`;

  $legend.append($(wrapper));
  $globalElement = $(".map-legend__details__pins.global");
  $eventElement = $(".map-legend__details__pins.event");

  updateDetails(isGlobalView);
}

function createHeader() {
  return (
    `
            <div class="map-legend__header">
                <div class="map-legend__headertext">Legend</div>
                <span class="map-legend__header-toggle" data-toggle="collapse" data-target=".map-legend__details">
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
            `
  );
}

function createDetails() {
  return `<div class="map-legend__details collapse show">`
    + createPinDetailsForGlobalView()
    + createPinDetailsForEventDetailView()
    + createImportationDetails()
    + `</div>`;
}

function createPinDetailsForGlobalView() {
  return (
    `
            <div class="map-legend__details__pins global" style="display:none;">
                <div class="row">
                    <div class="col-4 map-legend__icon"><div class="map-legend__icon-aoi map-legend__icon-outline"></div></div>
                    <div class="col-8 map-legend__description">My Location(s)</div>
                </div>
                <div class="row">
                    <div class="col-4 map-legend__icon">
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
                    <div class="col-8 map-legend__description">Outbreak event</div>
                </div>
            </div>
            `
  );
}

function createPinDetailsForEventDetailView() {
  return (
    `
            <div class="map-legend__details__pins event" style="display:none;">
                <div class="row">
                    <div class="col-4 map-legend__icon"><div class="map-legend__icon-aoi map-legend__icon-outline"></div></div>
                    <div class="col-8 map-legend__description">My Location(s)</div>
                </div>
                <div class="row">
                    <div class="col-4 map-legend__icon"><div class="map-legend__icon-outbreak map-legend__icon-outline"></div></div>
                    <div class="col-8 map-legend__description">Location of outbreak</div>
                </div>
                <div class="row">
                    <div class="col-4 map-legend__icon">
                        <svg width="10" height="10" viewBox="0 0 10 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <circle cx="5" cy="5" r="4.75" fill="#3F4B56" stroke="white" stroke-width="0.5"/>
                            <path d="M7.74308 6.12373C7.87128 6.16179 8 6.06573 8 5.932V5.71385C8 5.64327 7.9628 5.57791 7.90211 5.54188L5.57158 4.15812C5.51089 4.12209 5.47368 4.05673 5.47368 3.98615V2.45C5.47368 2.201 5.26211 2 5 2C4.73789 2 4.52632 2.201 4.52632 2.45V3.98615C4.52632 4.05673 4.48911 4.12209 4.42842 4.15812L2.09789 5.54188C2.0372 5.57791 2 5.64327 2 5.71385V5.932C2 6.06573 2.12872 6.16179 2.25692 6.12373L4.2694 5.52627C4.3976 5.48821 4.52632 5.58427 4.52632 5.718V6.99693C4.52632 7.0616 4.49504 7.12228 4.44237 7.15981L3.97868 7.49019C3.92601 7.52772 3.89474 7.5884 3.89474 7.65307V7.73848C3.89474 7.87025 4.01995 7.96601 4.14713 7.93149L4.94761 7.71422C4.98192 7.70491 5.01808 7.70491 5.05239 7.71422L5.85287 7.93149C5.98005 7.96601 6.10526 7.87025 6.10526 7.73848V7.65307C6.10526 7.5884 6.07399 7.52772 6.02132 7.49019L5.55763 7.15981C5.50496 7.12228 5.47368 7.0616 5.47368 6.99693V5.718C5.47368 5.58427 5.6024 5.48821 5.7306 5.52627L7.74308 6.12373Z" fill="white"/>
                        </svg>
                    </div>
                    <div class="col-8 map-legend__description">Airport</div>
                </div>
            </div>
            `
  );
}

function createImportationDetails() {
  return (
    `
            <div class="map-legend__details__pins event" style="display:none;">
                <div class="map-legend__subheadertext">Risk of importing one or<br />more infected cases (%)</div>
                <div class="row">
                    <div class="col-4 map-legend__icon">
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <circle cx="8" cy="8" r="8" fill="#3F4B56" fill-opacity="0.2"/>
                            <circle cx="8" cy="8" r="4.75" fill="#3F4B56" stroke="white" stroke-width="0.5"/>
                            <path d="M10.7431 9.12373C10.8713 9.16179 11 9.06573 11 8.932V8.71385C11 8.64327 10.9628 8.57791 10.9021 8.54188L8.57158 7.15812C8.51089 7.12209 8.47368 7.05673 8.47368 6.98615V5.45C8.47368 5.201 8.26211 5 8 5C7.73789 5 7.52632 5.201 7.52632 5.45V6.98615C7.52632 7.05673 7.48911 7.12209 7.42842 7.15812L5.09789 8.54188C5.0372 8.57791 5 8.64327 5 8.71385V8.932C5 9.06573 5.12872 9.16179 5.25692 9.12373L7.2694 8.52627C7.3976 8.48821 7.52632 8.58427 7.52632 8.718V9.99693C7.52632 10.0616 7.49504 10.1223 7.44237 10.1598L6.97868 10.4902C6.92601 10.5277 6.89474 10.5884 6.89474 10.6531V10.7385C6.89474 10.8703 7.01995 10.966 7.14713 10.9315L7.94761 10.7142C7.98192 10.7049 8.01808 10.7049 8.05239 10.7142L8.85287 10.9315C8.98005 10.966 9.10526 10.8703 9.10526 10.7385V10.6531C9.10526 10.5884 9.07399 10.5277 9.02132 10.4902L8.55763 10.1598C8.50496 10.1223 8.47368 10.0616 8.47368 9.99693V8.718C8.47368 8.58427 8.6024 8.48821 8.7306 8.52627L10.7431 9.12373Z" fill="white"/>
                        </svg>
                    </div>
                    <div class="col-8 map-legend__description">1 - 20</div>
                </div>
                <div class="row">
                    <div class="col-4 map-legend__icon">
                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <circle cx="12" cy="12" r="12" fill="#3F4B56" fill-opacity="0.2"/>
                            <circle cx="12" cy="12" r="4.75" fill="#3F4B56" stroke="white" stroke-width="0.5"/>
                            <path d="M14.7431 13.1237C14.8713 13.1618 15 13.0657 15 12.932V12.7138C15 12.6433 14.9628 12.5779 14.9021 12.5419L12.5716 11.1581C12.5109 11.1221 12.4737 11.0567 12.4737 10.9862V9.45C12.4737 9.201 12.2621 9 12 9C11.7379 9 11.5263 9.201 11.5263 9.45V10.9862C11.5263 11.0567 11.4891 11.1221 11.4284 11.1581L9.09789 12.5419C9.0372 12.5779 9 12.6433 9 12.7138V12.932C9 13.0657 9.12872 13.1618 9.25692 13.1237L11.2694 12.5263C11.3976 12.4882 11.5263 12.5843 11.5263 12.718V13.9969C11.5263 14.0616 11.495 14.1223 11.4424 14.1598L10.9787 14.4902C10.926 14.5277 10.8947 14.5884 10.8947 14.6531V14.7385C10.8947 14.8703 11.02 14.966 11.1471 14.9315L11.9476 14.7142C11.9819 14.7049 12.0181 14.7049 12.0524 14.7142L12.8529 14.9315C12.98 14.966 13.1053 14.8703 13.1053 14.7385V14.6531C13.1053 14.5884 13.074 14.5277 13.0213 14.4902L12.5576 14.1598C12.505 14.1223 12.4737 14.0616 12.4737 13.9969V12.718C12.4737 12.5843 12.6024 12.4882 12.7306 12.5263L14.7431 13.1237Z" fill="white" />
                        </svg>
                    </div>
                    <div class="col-8 map-legend__description">21 - 70</div>
                </div>
                <div class="row">
                    <div class="col-4 map-legend__icon">
                        <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <circle cx="16" cy="16" r="16" fill="#3F4B56" fill-opacity="0.2"/>
                            <circle cx="16" cy="16" r="4.75" fill="#3F4B56" stroke="white" stroke-width="0.5"/>
                            <path d="M18.7431 17.1237C18.8713 17.1618 19 17.0657 19 16.932V16.7138C19 16.6433 18.9628 16.5779 18.9021 16.5419L16.5716 15.1581C16.5109 15.1221 16.4737 15.0567 16.4737 14.9862V13.45C16.4737 13.201 16.2621 13 16 13C15.7379 13 15.5263 13.201 15.5263 13.45V14.9862C15.5263 15.0567 15.4891 15.1221 15.4284 15.1581L13.0979 16.5419C13.0372 16.5779 13 16.6433 13 16.7138V16.932C13 17.0657 13.1287 17.1618 13.2569 17.1237L15.2694 16.5263C15.3976 16.4882 15.5263 16.5843 15.5263 16.718V17.9969C15.5263 18.0616 15.495 18.1223 15.4424 18.1598L14.9787 18.4902C14.926 18.5277 14.8947 18.5884 14.8947 18.6531V18.7385C14.8947 18.8703 15.02 18.966 15.1471 18.9315L15.9476 18.7142C15.9819 18.7049 16.0181 18.7049 16.0524 18.7142L16.8529 18.9315C16.98 18.966 17.1053 18.8703 17.1053 18.7385V18.6531C17.1053 18.5884 17.074 18.5277 17.0213 18.4902L16.5576 18.1598C16.505 18.1223 16.4737 18.0616 16.4737 17.9969V16.718C16.4737 16.5843 16.6024 16.4882 16.7306 16.5263L18.7431 17.1237Z" fill="white"/>
                        </svg>
                    </div>
                    <div class="col-8 map-legend__description">≥ 70</div>
                </div>
            </div>  
            `
  );
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