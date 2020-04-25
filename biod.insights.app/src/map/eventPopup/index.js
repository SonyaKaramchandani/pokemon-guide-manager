import events from 'map/events';
import './style.scss';
import { navigate } from '@reach/router';
import EventApi from 'api/EventApi';
import { formatDate } from 'utils/dateTimeHelpers';
import { formatNumber } from 'utils/stringFormatingHelpers';
import { getRiskLikelihood } from 'utils/modelHelpers';
import { Geoname } from 'utils/constants';
import { getImportationRiskIcon, getExportationRiskIcon } from 'utils/assetUtils.map';

const POPUP_DIMENSIONS_LIST = [280, 285];
const POPUP_DIMENSIONS_DETAILS = [280, 400];

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
      <div class='popup__details-loading' style='display: none;'>
        <svg width="60" height="65" viewBox="0 0 14 15" fill="none" xmlns="http://www.w3.org/2000/svg" style="">
          <path d="M0.5 7.32666C0.5 10.8654 3.40454 13.7441 7 13.7441C10.5955 13.7441 13.5 10.8654 13.5 7.32666C13.5 3.78796 10.5955 0.90918 7 0.90918C3.40454 0.90918 0.5 3.78796 0.5 7.32666Z" stroke="#B0BDCA" stroke-linecap="round" class="rZvBjWTH_1 xxxCHOyztwz_0"></path>
          <path d="M4.89364 11.8269V6.82632C4.89364 6.15957 4.30328 5.57617 3.62858 5.57617C2.95388 5.57617 2.36352 6.15957 2.36352 6.82632C2.36352 10.0767 11.6406 10.0767 11.6406 6.82632C11.6406 6.15957 11.0503 5.57617 10.3756 5.57617C9.70087 5.57617 9.1105 6.15957 9.1105 6.82632V11.8269" stroke="#B0BDCA" stroke-width="1.2" stroke-linecap="round" class="rZvBjWTH_2 CHOyztwz_1"></path>
          <style data-made-with="vivus-instant">.CHOyztwz_0{stroke-dasharray:41 43;stroke-dashoffset:42;animation:CHOyztwz_draw_0 800ms ease 0ms infinite,CHOyztwz_fade 800ms linear 0ms infinite;}.CHOyztwz_1{stroke-dasharray:30 32;stroke-dashoffset:31;animation:CHOyztwz_draw_1 800ms ease 0ms infinite,CHOyztwz_fade 800ms linear 0ms infinite;}@keyframes CHOyztwz_draw{100%{stroke-dashoffset:0;}}@keyframes CHOyztwz_fade{0%{stroke-opacity:1;}62.5%{stroke-opacity:1;}100%{stroke-opacity:0;}}@keyframes CHOyztwz_draw_0{0%{stroke-dashoffset: 42}62.5%{ stroke-dashoffset: 0;}100%{ stroke-dashoffset: 0;}}@keyframes CHOyztwz_draw_1{0%{stroke-dashoffset: 31}62.5%{ stroke-dashoffset: 0;}100%{ stroke-dashoffset: 0;}}</style>
        </svg>
      </div>
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

    const $loaderContainer = window.jQuery('.popup__details-loading');
    $loaderContainer.show();

    const eventId = parseInt(e.currentTarget.dataset.eventid);
    EventApi.getEvent(geonameId ? { eventId, geonameId } : { eventId }).then(
      ({ data: { eventInformation, isLocal, importationRisk, exportationRisk, caseCounts } }) => {
        const eventInfo = {
          GeonameId: geonameId,
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
          ImportationRiskLevel: getRiskLikelihood(importationRisk),
          ImportationProbabilityString: getRiskLikelihood(importationRisk),
          ExportationRiskLevel: getRiskLikelihood(exportationRisk)
        };

        const $detailContainer = window.jQuery('.popup__details');
        $detailContainer.find('.popup__startDate').text(eventInfo.StartDate);
        $detailContainer.find('.popup__endDate').text(eventInfo.EndDate);
        $detailContainer.find('.popup__eventTitle').text(eventInfo.EventTitle);
        $detailContainer.find('.popup__repCases').text(formatNumber(eventInfo.RepCases, 'Case'));
        $detailContainer.find('.popup__deaths').text(formatNumber(eventInfo.Deaths, 'Death'));
        $detailContainer.find('.popup__importationRiskIcon').empty();
        eventInfo.LocalSpread
          ? $detailContainer
              .find('.popup__importationRiskIcon')
              .append('<i class="icon bd-icon icon-pin"></i>')
              .append(getImportationRiskIcon(eventInfo.ImportationRiskLevel))
              .append('<i class="icon bd-icon icon-plane-import"></i>')
          : $detailContainer
              .find('.popup__importationRiskIcon')
              .append(getImportationRiskIcon(eventInfo.ImportationRiskLevel))
              .append('<i class="icon bd-icon icon-plane-import"></i>');
        $detailContainer
          .find('.popup__importationRiskText')
          .text(eventInfo.ImportationProbabilityString);
        $detailContainer.find('.popup__exportationRiskIcon').empty();
        $detailContainer
          .find('.popup__exportationRiskIcon')
          .append(getExportationRiskIcon(eventInfo.ExportationRiskLevel))
          .append('<i class="icon bd-icon icon-plane-export"></i>');
        $detailContainer.find('.popup__exportationRiskText').text(eventInfo.ExportationRiskLevel);

        $loaderContainer.hide();
        $detailContainer.show();

        window.jQuery('.popup__openDetails').attr('data-geonameid', eventInfo.GeonameId);
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

    if (window.location.pathname.startsWith('/location')) {
      const geonameId = e.currentTarget.getAttribute('data-geonameid');
      const diseaseId = e.currentTarget.getAttribute('data-diseaseid');
      navigate(`/location/${geonameId}/disease/${diseaseId}/event/${eventId}`);
    } else {
      navigate(`/event/${eventId}`);
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
