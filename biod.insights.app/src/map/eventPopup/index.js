﻿import events from 'map/events';
import './style.scss';
import EventApi from 'api/EventApi';
import { formatDate } from 'utils/dateTimeHelpers';
import { getInterval, getRiskLevel } from 'utils/stringFormatingHelpers';
import { Geoname } from 'utils/constants';
import { formatNumber } from 'utils/stringFormatingHelpers';

const POPUP_DIMENSIONS_LIST = [280, 285];
const POPUP_DIMENSIONS_DETAILS = [280, 355];

const ICON_PROXIMAL = `
  <svg width="10" height="15" viewBox="0 0 10 15" fill="none" xmlns="http://www.w3.org/2000/svg">
    <path d="M9.28571 4.28571C9.28571 1.92143 7.36429 0 5 0C2.63571 0 0.714286 1.92143 0.714286 4.28571C0.714286 7.5 5 12.1429 5 12.1429C5 12.1429 9.28571 7.5 9.28571 4.28571ZM3.57143 4.28571C3.57143 3.5 4.21429 2.85714 5 2.85714C5.78571 2.85714 6.42857 3.5 6.42857 4.28571C6.42857 5.07143 5.79286 5.71429 5 5.71429C4.21429 5.71429 3.57143 5.07143 3.57143 4.28571ZM0 12.8571V14.2857H10V12.8571H0Z" fill="#334457"/>
  </svg>
  `;
const ICON_IMPORTATION_NONE = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="9.00903" width="3.49997" height="9.00928" transform="rotate(90 9.00903 0)" fill="#F0F0F0" />
    <rect x="9.00903" y="5.5" width="3.49997" height="9.00928" transform="rotate(90 9.00903 5.5)" fill="#F0F0F0" />
    <rect x="9.00903" y="11" width="3.49997" height="9.00928" transform="rotate(90 9.00903 11)" fill="#F0F0F0" />
  </svg>
  `;
const ICON_IMPORTATION_LOW = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#76A3DC"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#ECECEC"/>
  </svg>
  `;
const ICON_IMPORTATION_MED = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EDD78F"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#DCBA49"/>
  </svg>
  `;
const ICON_IMPORTATION_HIGH = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#D32721"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EA8D8A"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#E4625E"/>
  </svg>
  `;
const ICON_EXPORTATION_NONE = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="9.00903" width="3.49997" height="9.00928" transform="rotate(90 9.00903 0)" fill="#F0F0F0" />
    <rect x="9.00903" y="5.5" width="3.49997" height="9.00928" transform="rotate(90 9.00903 5.5)" fill="#F0F0F0" />
    <rect x="9.00903" y="11" width="3.49997" height="9.00928" transform="rotate(90 9.00903 11)" fill="#F0F0F0" />
  </svg>
  `;
const ICON_EXPORTATION_LOW = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#76A3DC"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#ECECEC"/>
  </svg>
  `;
const ICON_EXPORTATION_MED = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#ECECEC"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EDD78F"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#DCBA49"/>
  </svg>
  `;
const ICON_EXPORTATION_HIGH = `
  <svg width="10" height="14" viewBox="0 0 10 14" fill="none" xmlns="http://www.w3.org/2000/svg">
    <rect x="8.63281" width="3.02019" height="8.62911" transform="rotate(90 8.63281 0)" fill="#D32721"/>
    <rect x="8.63281" y="9.0625" width="3.02019" height="8.62911" transform="rotate(90 8.63281 9.0625)" fill="#EA8D8A"/>
    <rect x="8.62891" y="4.53125" width="3.02019" height="8.62911" transform="rotate(90 8.62891 4.53125)" fill="#E4625E"/>
  </svg>
  `;

function getImportationRiskIcon(riskLevel, isLocal) {
  if (isLocal) return ICON_PROXIMAL;

  switch (riskLevel) {
    case 0:
      return ICON_IMPORTATION_NONE;
    case 1:
      return ICON_IMPORTATION_LOW;
    case 2:
      return ICON_IMPORTATION_MED;
    case 3:
      return ICON_IMPORTATION_HIGH;
    default:
      return ICON_IMPORTATION_NONE;
  }
}

function getExportationRiskIcon(riskLevel) {
  switch (riskLevel) {
    case 0:
      return ICON_EXPORTATION_NONE;
    case 1:
      return ICON_EXPORTATION_LOW;
    case 2:
      return ICON_EXPORTATION_MED;
    case 3:
      return ICON_EXPORTATION_HIGH;
    default:
      return ICON_EXPORTATION_NONE;
  }
}

function getPopupContent(graphic, graphicIndex, geonameId) {
  // TODO: 97759341: replace the svgs within popup__date with <i class="icon bd-icon icon-calendar"></i>
  return graphic.attributes.sourceData.Events.map(
    (e, i) =>
      `
      <div class='popup__row' data-graphicindex=${graphicIndex} data-objectid=${graphic.attributes.ObjectID} data-eventindex=${i} data-eventid=${e.EventId}>
        <div class='popup__date'>
          <svg width='11' height='11' viewBox='0 0 11 11' fill='none' xmlns='http://www.w3.org/2000/svg'>
            <path d='M9.4 2.19989H1.6C1.26863 2.19989 1 2.46852 1 2.7999V9.39996C1 9.73134 1.26863 9.99997 1.6 9.99997H9.4C9.73137 9.99997 10 9.73134 10 9.39996V2.7999C10 2.46852 9.73137 2.19989 9.4 2.19989Z' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
            <path d='M1 4.59998H10' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
            <path d='M3.3999 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
            <path d='M7.6001 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
          </svg>
          <span class='popup__startDate'>${e.StartDate}</span>&nbsp;—&nbsp;<span class='popup__endDate'>${e.EndDate}</span>
        </div>
        <div class='popup__eventTitle'>${e.EventTitle}</div>
      </div>
    `
  )
    .reduce((a, b) => a + b, `<div class='popup__rowsWrapper'>`)
    .concat(`</div>`)
    .concat(
      `
      <div class='popup__details' style='display: none;'>
        <div class='popup__detailsSummary'>
          <div class='popup__date'>
            <svg width='11' height='11' viewBox='0 0 11 11' fill='none' xmlns='http://www.w3.org/2000/svg'>
              <path d='M9.4 2.19989H1.6C1.26863 2.19989 1 2.46852 1 2.7999V9.39996C1 9.73134 1.26863 9.99997 1.6 9.99997H9.4C9.73137 9.99997 10 9.73134 10 9.39996V2.7999C10 2.46852 9.73137 2.19989 9.4 2.19989Z' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
              <path d='M1 4.59998H10' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
              <path d='M3.3999 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
              <path d='M7.6001 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />
            </svg>
            <span class='popup__startDate'></span>&nbsp;—&nbsp;<span class='popup__endDate'></span>
          </div>
          <div class='popup__eventTitle'></div>
          <div class='popup__caseCounts'><span class='popup__repCases'></span>, <span class='popup__deaths'></span></div>
        </div>
        <div class="popup__importation ${
          !geonameId || geonameId === Geoname.GLOBAL_VIEW ? `hidden` : ``
        }">
          <div class="popup__importationTitle">Risk of importation to your locations</div>
          <div class="popup__importationRiskDetails">
            <div class="popup__importationRiskIcon"></div>
            <span class="popup__importationRiskText"></span>
          </div>
        </div>  
        <div class='popup__exportation'>
          <div class='popup__exportationTitle'>Risk of exportation</div>
          <div class='popup__exportationRiskDetails'>
            <div class='popup__exportationRiskIcon'></div>
            <span class='popup__exportationRiskText'></span>
          </div>
        </div>
        <button class='popup__openDetails' data-eventid='-1' class='btn'>Open details</button></div>
      </div>
    `
    );
}

function getPopupTitle(countryName, displayBackButton) {
  const callback =
    "window.jQuery('.popup__details').hide();" +
    "window.jQuery('.popup__rowsWrapper').show();" +
    "window.jQuery('.popup__back').addClass('popup__back--hide');" +
    "window.jQuery('.popup__back').removeClass('popup__back--show'); if (e.originalEvent) window.biod.map.gaEvent('RETURN_TO_EVENT_LIST_TOOLTIP'); ";

  return `<div class='popup__back popup__back--${
    displayBackButton ? `show` : `hide`
  }' onclick=${callback}>&nbsp;</div><div class='popup__titleText'>${countryName}</div>`;
}

function showPinPopup(popup, map, graphic, graphicIndex, sourceData) {
  const showEvent = popup.on('show', function() {
    setTimeout(function() {
      events.dimLayers();
      adjustPopupPosition(map);
      showEvent.remove();
    }, 100);
  });

  popup.setTitle(getPopupTitle(sourceData.CountryName, false));
  popup.setContent(getPopupContent(graphic, graphicIndex, sourceData.geonameId));

  const popupLocation = window.jQuery.extend(true, {}, graphic.geometry);
  if (map.geographicExtent.xmin > graphic.geometry.x) {
    popupLocation.x =
      graphic.geometry.x +
      Math.floor(Math.abs((map.geographicExtent.xmax - graphic.geometry.x) / 360)) * 360;
  } else if (map.geographicExtent.xmax < graphic.geometry.x) {
    popupLocation.x =
      graphic.geometry.x -
      Math.floor(Math.abs((map.geographicExtent.xmin - graphic.geometry.x) / 360)) * 360;
  }

  setPopupInnerEvents(popup, graphic, sourceData.geonameId);

  // open event details when only one row
  if (window.jQuery('.popup__row').length === 1) {
    window.jQuery('.popup__row').click();
  }

  popup.show(popupLocation);
}

function waitForElement(elementPath, callback, delay = 500) {
  window.setTimeout(() => {
    window.jQuery(elementPath).length
      ? callback(window.jQuery(elementPath))
      : waitForElement(elementPath, callback);
  }, delay);
}

function setPopupInnerEvents(popup, graphic, geonameId) {
  popup.resize(...POPUP_DIMENSIONS_LIST);

  window.jQuery('.popup__row').click(function(e) {
    window.jQuery('.popup__rowsWrapper').hide();
    popup.resize(...POPUP_DIMENSIONS_DETAILS);

    const { CountryGeonameId, CountryName, Events } = graphic.attributes.sourceData;
    popup.setTitle(getPopupTitle(CountryName, Events.length > 1));

    const $detailContainer = window.jQuery('.popup__details');
    $detailContainer.show();

    const eventId = parseInt(e.currentTarget.dataset.eventid);
    EventApi.getEvent(geonameId ? { eventId, geonameId } : { eventId }).then(
      ({ data: { eventInformation, isLocal, importationRisk, exportationRisk, caseCounts } }) => {
        const eventInfo = {
          DiseaseId: eventInformation.diseaseId,
          EventId: eventInformation.id,
          EventTitle: eventInformation.title,
          CountryName: CountryName,
          StartDate: eventInformation.startDate
            ? formatDate(eventInformation.startDate)
            : 'Unknown',
          EndDate: eventInformation.endDate ? formatDate(eventInformation.endDate) : 'Present',
          RepCases: caseCounts.reportedCases,
          Deaths: caseCounts.deaths,
          LocalSpread: isLocal,
          ImportationRiskLevel: importationRisk ? getRiskLevel(importationRisk.maxProbability) : -1,
          ImportationProbabilityString: isLocal
            ? 'In or proximal to your area(s) of interest'
            : importationRisk
            ? getInterval(
                importationRisk.minProbability,
                importationRisk.maxProbability,
                '%',
                importationRisk.isModelNotRun
              )
            : 'Unknown',
          ExportationRiskLevel: exportationRisk ? getRiskLevel(exportationRisk.maxProbability) : -1,
          ExportationProbabilityString: exportationRisk
            ? getInterval(
                exportationRisk.minProbability,
                exportationRisk.maxProbability,
                '%',
                exportationRisk.isModelNotRun
              )
            : 'Unknown'
        };

        $detailContainer.find('.popup__startDate').text(eventInfo.StartDate);
        $detailContainer.find('.popup__endDate').text(eventInfo.EndDate);
        $detailContainer.find('.popup__eventTitle').text(eventInfo.EventTitle);
        $detailContainer.find('.popup__repCases').text(formatNumber(eventInfo.RepCases, 'Case'));
        $detailContainer.find('.popup__deaths').text(formatNumber(eventInfo.Deaths, 'Death'));
        $detailContainer.find('.popup__importationRiskIcon').empty();
        $detailContainer
          .find('.popup__importationRiskIcon')
          .append(getImportationRiskIcon(eventInfo.ImportationRiskLevel, eventInfo.LocalSpread))
          .append('<i class="icon bd-icon icon-plane-arrival"></i>');
        $detailContainer
          .find('.popup__importationRiskText')
          .text(eventInfo.ImportationProbabilityString);
        $detailContainer.find('.popup__exportationRiskIcon').empty();
        $detailContainer
          .find('.popup__exportationRiskIcon')
          .append(getExportationRiskIcon(eventInfo.ExportationRiskLevel))
          .append('<i class="icon bd-icon icon-plane-departure"></i>');
        $detailContainer
          .find('.popup__exportationRiskText')
          .text(eventInfo.ExportationProbabilityString);

        window.jQuery('.popup__openDetails').attr('data-diseaseid', eventInfo.DiseaseId);
        window.jQuery('.popup__openDetails').attr('data-eventid', eventInfo.EventId);

        if (e.originalEvent) {
          // Only log on human-triggered clicks not synthetic clicks
          // TODO: window.biod.map.gaEvent('CLICK_EVENT_TOOLTIP', eventSourceData.EventId + ' | ' + eventSourceData.EventTitle);
        }
      }
    );
  });

  window.jQuery('.popup__back').click(function(e) {
    popup.resize(...POPUP_DIMENSIONS_LIST);
    window.jQuery('.popup__details').hide();
    window.jQuery('.popup__rowsWrapper').show();

    if (e.originalEvent) {
      // Only log on human-triggered clicks not synthetic clicks
      // TODO: window.biod.map.gaEvent('RETURN_TO_EVENT_LIST_TOOLTIP');
    }
  });

  window.jQuery('.popup__openDetails').click(function(e) {
    const eventId = e.currentTarget.getAttribute('data-eventid');
    const url = window.location.href;

    if (url.endsWith('/location')) {
      const minimizedDiseasePanelPath = 'div[data-sidebar="Diseases"]'; // TODO: Make the selector less breakable
      if (window.jQuery(minimizedDiseasePanelPath).length) {
        window.jQuery(minimizedDiseasePanelPath).click();
      }

      const diseaseId = e.currentTarget.getAttribute('data-diseaseid');
      const diseaseItemElement = window.jQuery(`div[role="listitem"][data-diseaseid=${diseaseId}]`);
      diseaseItemElement.click();
    } else {
      const minimizedEventsPanelPath = 'div[data-sidebar="My Events"]'; // TODO: Make the selector less breakable
      if (window.jQuery(minimizedEventsPanelPath).length) {
        window.jQuery(minimizedEventsPanelPath).click();
      }
    }

    const eventItemElementPath = `div[role="listitem"][data-eventid=${eventId}]`;
    waitForElement(eventItemElementPath, element => element.click());

    if (e.originalEvent) {
      const eventTitle = window
        .jQuery(e.currentTarget)
        .closest('.popup__details')
        .find('.popup__eventTitle')[0];
      // TODO: window.biod.map.gaEvent('OPEN_EVENT_DETAILS', eventId + ' | ' + eventTitle.innerText, parseInt(eventId));
    }
  });

  window.jQuery('.esriPopup .titleButton.close').unbind('click');
  window.jQuery('.esriPopup .titleButton.close').click(function(evt) {
    events.dimLayers(false);

    if (evt.originalEvent) {
      const countryName = window
        .jQuery(evt.currentTarget)
        .closest('.titlePane')
        .find('.title')[0];
      // TODO: window.biod.map.gaEvent('CLOSE_COUNTRY_TOOLTIP', countryName.innerHTML);
    }
  });
}

function adjustPopupPosition(map) {
  if (map.infoWindow.isShowing) {
    const scnPt = map.toScreen(map.infoWindow.location);
    const winH = Number(
      window
        .jQuery('.esriPopupWrapper')
        .css('height')
        .replace('px', '')
    );
    if (!map.extent.contains(map.toMap({ x: scnPt.x, y: scnPt.y - winH - 5 }))) {
      setTimeout(function() {
        map.centerAt(map.infoWindow.location);
      }, 400);
    }
  }
}

export default {
  showPinPopup
};