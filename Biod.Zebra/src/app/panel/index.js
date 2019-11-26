import './style.scss';

const STATE_OPEN = 0;
const STATE_CLOSED = 1;
const STATE_COLLAPSED = 2;

let $panelWrapper = null;
let $filterPanel = null;
let $eventListPanel = null;
let $eventDetailsPanel = null;

let filterPanelState = STATE_CLOSED;
let eventListPanelState = STATE_CLOSED;
let eventDetailsPanelState = STATE_CLOSED;

// Tracks whether the panels have been toggled by the user
let initialState = true;

// Tracks whether the blue toggle button was clicked to minimize/restore all the panels
let allPanelsState = STATE_OPEN;

function initializePanels(panelWrapper, filterPanel, eventListPanel, eventDetailsPanel) {
  $panelWrapper = panelWrapper;
  $filterPanel = filterPanel;
  $eventListPanel = eventListPanel;
  $eventDetailsPanel = eventDetailsPanel;
}

function updatePanelState() {
  initialState = false;
  if ($eventListPanel) {
    $eventListPanel.toggleClass('collapsible', canCollapseEventsListPanel());
  }
}

function openFilterPanel() {
  if ($filterPanel) {
    if (!$filterPanel.hasClass("show")) {
      $filterPanel.addClass("show");
    } else {
      $filterPanel.addClass("blink");
      $filterPanel[0].t || ($filterPanel[0].t = setTimeout(function () {
        $filterPanel.removeClass("blink");
        $filterPanel[0].t = null;
      }, 1000));
    }
    filterPanelState = STATE_OPEN;
    updatePanelState();
  }
}

function closeFilterPanel() {
  if ($filterPanel) {
    $filterPanel.removeClass("show");
    filterPanelState = STATE_CLOSED;
    updatePanelState();
  }
}

function openEventsListPanel() {
  if ($eventListPanel) {
    $eventListPanel.addClass('show').removeClass('collapsed closed');
    eventListPanelState = STATE_OPEN;
    updatePanelState();
  }
}

function closeEventsListPanel() {
  if ($eventListPanel) {
    $eventListPanel.addClass('closed').removeClass('show collapsed');
    eventListPanelState = STATE_CLOSED;
    updatePanelState();
  }
}

function collapseEventsListPanel() {
  if ($eventListPanel && canCollapseEventsListPanel()) {
    if (eventDetailsPanelState === STATE_OPEN) {
      $eventListPanel.addClass('collapsed').removeClass('show closed');
      eventListPanelState = STATE_COLLAPSED;
      closeFilterPanel();
    } else {
      closeEventsListPanel();
      closeFilterPanel();
      initialState = true;
    }
  }
}

function openEventDetailsPanel() {
  if ($eventDetailsPanel) {
    $eventDetailsPanel.addClass('show').removeClass('collapsed');
    eventDetailsPanelState = STATE_OPEN;
    if (eventListPanelState === STATE_CLOSED) {
      openEventsListPanel();
    }
    updatePanelState();
  }
}

function closeEventDetailsPanel() {
  if ($eventDetailsPanel) {
    $eventDetailsPanel.addClass('closed').removeClass('show');
    eventDetailsPanelState = STATE_CLOSED;
    if (eventListPanelState === STATE_COLLAPSED) {
      openEventsListPanel();
    }
    updatePanelState();
  }
}

function togglePanelsOpen() {
  $panelWrapper.removeClass('minimized');
  allPanelsState = STATE_OPEN;
}

function togglePanelsClosed() {
  $panelWrapper.addClass('minimized');
  allPanelsState = STATE_CLOSED;
}

function togglePanelsButton() {
  if (initialState) {
    openEventsListPanel();
    updatePanelState();
  } else if (allPanelsState !== STATE_OPEN) {
    togglePanelsOpen();
  } else {
    togglePanelsClosed();
  }
}

function closeAllPanels() {
  closeFilterPanel();
  closeEventsListPanel();
  closeEventDetailsPanel();
  initialState = true;
}

function canCollapseEventsListPanel() {
  return eventListPanelState === STATE_OPEN && (filterPanelState === STATE_OPEN || eventDetailsPanelState === STATE_OPEN);
}

export default {
  initializePanels,
  openFilterPanel,
  closeFilterPanel,
  openEventsListPanel,
  closeEventsListPanel,
  collapseEventsListPanel,
  openEventDetailsPanel,
  closeEventDetailsPanel,
  togglePanelsButton
};