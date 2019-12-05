import { $, jQuery } from 'jquery';

function getPopupContent(graphic, eventsCountryPinsLayer) {
  var retVal = '';

  //set short event list
  var gIdx = eventsCountryPinsLayer.graphics.indexOf(graphic);
  var gEvts = graphic.attributes.sourceData.Events;

  for (var i = 0; i < gEvts.length; i++) {
    retVal +=
      "<div class='popup-row-style' data-graphicindex=" +
      gIdx +
      ' data-objectid=' +
      graphic.attributes.ObjectID +
      ' data-eventindex=' +
      i +
      ' data-eventid=' +
      gEvts[i].EventId +
      '>';
    retVal += "<div class='popup-date'>";
    retVal +=
      "<svg width='11' height='11' viewBox='0 0 11 11' fill='none' xmlns='http://www.w3.org/2000/svg'>";
    retVal +=
      "<path d='M9.4 2.19989H1.6C1.26863 2.19989 1 2.46852 1 2.7999V9.39996C1 9.73134 1.26863 9.99997 1.6 9.99997H9.4C9.73137 9.99997 10 9.73134 10 9.39996V2.7999C10 2.46852 9.73137 2.19989 9.4 2.19989Z' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
    retVal +=
      "<path d='M1 4.59998H10' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
    retVal +=
      "<path d='M3.3999 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
    retVal +=
      "<path d='M7.6001 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
    retVal += '</svg>';
    retVal += gEvts[i].StartDate + ' - ' + gEvts[i].EndDate + '</div>';

    retVal += "<div class='popup-event-title'>" + gEvts[i].EventTitle + '</div>';
    retVal += '</div>';
  }

  retVal = "<div id='popup-row-container'>" + retVal + '</div>';
  retVal += "<div id='popup-detail-container' style='display: none;'>";
  retVal += "<div id='sp-prioritytitle'>";
  retVal += '</div>';
  retVal += "<div class='popup-date'>";
  retVal +=
    "<svg width='11' height='11' viewBox='0 0 11 11' fill='none' xmlns='http://www.w3.org/2000/svg'>";
  retVal +=
    "<path d='M9.4 2.19989H1.6C1.26863 2.19989 1 2.46852 1 2.7999V9.39996C1 9.73134 1.26863 9.99997 1.6 9.99997H9.4C9.73137 9.99997 10 9.73134 10 9.39996V2.7999C10 2.46852 9.73137 2.19989 9.4 2.19989Z' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
  retVal +=
    "<path d='M1 4.59998H10' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
  retVal +=
    "<path d='M3.3999 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
  retVal +=
    "<path d='M7.6001 3.10002V1' stroke='#AAAAAA' stroke-linecap='round' stroke-linejoin='round' />";
  retVal += '</svg>';
  retVal += "<span id='sp-startdate'></span> - <span id='sp-enddate'></span></div>";

  retVal += "<div><div id='sp-eventtitle'></span></div>";
  retVal += "<div style='line-height: 1rem;'><p id='sp-summary' /></div>";

  retVal +=
    "<div class='popup-btn-container'><button id='popup-back-btn' class='btn'>Back</button>";
  retVal +=
    "<button id='popup-open-details-btn' data-eventid='-1' class='btn'>Open details</button></div>";

  retVal += '</div>';

  return retVal;
}

function showPinPopup(
  popup,
  map,
  graphic,
  eventsCountryPinsLayer,
  sourceData,
  onPopupShow,
  onPopupRowClick,
  onPopupBackClick,
  onPopupClose
) {
  const showEvent = popup.on('show', function() {
    setTimeout(function() {
      onPopupShow(sourceData.CountryGeonameId);
      adjustPopupPosition(map);
      showEvent.remove();
    }, 100);
  });

  const htmlContent = getPopupContent(graphic, eventsCountryPinsLayer);
  popup.setTitle(sourceData.CountryName);
  popup.setContent(htmlContent);

  const popupLocation = jQuery.extend(true, {}, graphic.geometry);
  if (map.geographicExtent.xmin > graphic.geometry.x) {
    popupLocation.x =
      graphic.geometry.x +
      Math.floor(Math.abs((map.geographicExtent.xmax - graphic.geometry.x) / 360)) * 360;
  } else if (map.geographicExtent.xmax < graphic.geometry.x) {
    popupLocation.x =
      graphic.geometry.x -
      Math.floor(Math.abs((map.geographicExtent.xmin - graphic.geometry.x) / 360)) * 360;
  }

  setPopupInnerEvents(eventsCountryPinsLayer, onPopupRowClick, onPopupBackClick, onPopupClose);

  // open event details when only one row
  if ($('.popup-row-style').length === 1) {
    $('.popup-row-style').click();
  }

  popup.show(popupLocation);
}

function setPopupInnerEvents(
  eventsCountryPinsLayer,
  onPopupRowClick,
  onPopupBackClick,
  onPopupClose
) {
  $('.popup-row-style').click(function(e) {
    var $elm = $(e.currentTarget);

    var srcData =
      eventsCountryPinsLayer.graphics[Number($elm.attr('data-graphicindex'))].attributes.sourceData
        .Events[Number($elm.attr('data-eventindex'))];
    var $detailContainer = $('#popup-detail-container');

    $detailContainer.find('#sp-startdate').text(srcData.StartDate);
    $detailContainer.find('#sp-enddate').text(srcData.EndDate);
    $detailContainer
      .find('#sp-prioritytitle')
      .find('.gd-priority')
      .attr('class', 'gd-priority ' + srcData.PriorityTitle.toLowerCase());
    $detailContainer.find('#sp-startdate').text(srcData.StartDate);
    $detailContainer.find('#sp-eventtitle').text(srcData.EventTitle);
    $detailContainer
      .find('#sp-summary')
      .html(
        srcData.Summary.length > 200
          ? "<p style='line-height: 1rem;'>" + srcData.Summary.substring(0, 200) + '...</p>'
          : "<p style='line-height: 1rem;'>" + srcData.Summary + '</p>'
      );
    $('#popup-open-details-btn').attr('data-eventid', srcData.EventId);
    $('#popup-row-container').hide();
    $detailContainer.show();

    onPopupRowClick(srcData.EventId);

    if (e.originalEvent) {
      // Only log on human-triggered clicks not synthetic clicks
      window.biod.map.gaEvent('CLICK_EVENT_TOOLTIP', srcData.EventId + ' | ' + srcData.EventTitle);
    }
  });

  $('#popup-back-btn').click(function(e) {
    $('#popup-detail-container').hide();
    $('#popup-row-container').show();

    onPopupBackClick();

    if (e.originalEvent) {
      // Only log on human-triggered clicks not synthetic clicks
      window.biod.map.gaEvent('RETURN_TO_EVENT_LIST_TOOLTIP');
    }
  });

  $('#popup-open-details-btn').click(function(e) {
    window.biod.dashboard.panel.openEventsListPanel();

    const eventId = e.currentTarget.getAttribute('data-eventid');
    $(`#event-${eventId}`).click();

    if (e.originalEvent) {
      const eventTitle = $(e.currentTarget)
        .closest('#popup-detail-container')
        .find('#sp-eventtitle')[0];
      window.biod.map.gaEvent(
        'OPEN_EVENT_DETAILS',
        eventId + ' | ' + eventTitle.innerText,
        parseInt(eventId)
      );
    }
  });

  $('.esriPopup .titleButton.close').unbind('click');
  $('.esriPopup .titleButton.close').click(function(evt) {
    onPopupClose();

    if (evt.originalEvent) {
      const countryName = $(evt.currentTarget)
        .closest('.titlePane')
        .find('.title')[0];
      window.biod.map.gaEvent('CLOSE_COUNTRY_TOOLTIP', countryName.innerHTML);
    }
  });
}

function adjustPopupPosition(map) {
  if (map.infoWindow.isShowing) {
    var scnPt = map.toScreen(map.infoWindow.location);
    var winH = Number(
      $('.esriPopupWrapper')
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
