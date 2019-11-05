(function(model) {
  const $eventList = $('.eventlist');
  const children = model.DiseaseGroups.map(createDiseaseGroup).join('') + createNoResults();
  
  $eventList.append($(children));

  $(".eventlist__noresults--link").on("click", function (e) {
    window.FilterPanelMethods.open(e);
    window.gtagh(
      window.GoogleAnalytics.Action.CLICK_MODIFY_FILTERS,
      window.GoogleAnalytics.Category.FILTERS,
      'Modify filters from empty event list');
  });

  // Assume all groups are visible, when async calls return to hide, the count will decrement
  let visibleGroups = model.DiseaseGroups.length;
  if (visibleGroups === 0) {
    $eventList.addClass('eventlist--noresults');
    return;
  }
  
  // Initialize tooltips
  $('.eventlistitem__tooltip--report').kendoTooltip({
    content: 'Outlook report available',
    position: 'bottom',
    width: 180
  });
  $('.eventlistitem__tooltip--risk').kendoTooltip({
    content: getRiskTooltipContent,
    position: 'left',
    width: 180
  });
  
  // Attach on click event
  $('.eventlistitem').on('click', function (e) {
    $('.eventlistitem--active').removeClass('eventlistitem--active');
    $(this).addClass('eventlistitem--active');

    window.toggleSidebarLoadingOn();
    $("#gd-event-details").removeClass("show");
    $("#gd-sidebar-toggle").removeClass("metadata");

    const eventId = e.currentTarget.getAttribute("data-id");

    window.history.replaceState(null, null, "?eventId=" + eventId);
    window.getEventDetailPartialView(eventId);

    if (e.originalEvent) {
      // Only log on human-triggered clicks not synthetic clicks
      const eventTitle = $(this).find('.eventlistitem__title') && $(this).find('.eventlistitem__title')[0].innerHTML.trim();
      window.gtagh(window.GoogleAnalytics.Action.OPEN_EVENT_DETAILS, window.GoogleAnalytics.Category.EVENTS, `Open from list: ${eventId} | ${eventTitle}`, parseInt(eventId));
    }
  });

  // Collapse/Expand behaviours on groupings
  $('.eventlist__secondarylist').on('click', (e) => {
    const $target = $(e.target), $currentTarget = $(e.currentTarget);
    if ($target.hasClass('eventlist__togglebutton') || $target.closest('.eventlist__togglebutton').length > 0) {
      $currentTarget.toggleClass('eventlist__secondarylist--collapsed').toggleClass('eventlist__secondarylist--expanded');

      const isCollapsed = $currentTarget.hasClass('eventlist__secondarylist--collapsed');
      if (e.originalEvent) {
        // Only log on human-triggered clicks not synthetic clicks
        window.gtagh(
          isCollapsed ? window.GoogleAnalytics.Action.COLLAPSE_ADDITIONAL_EVENTS_EVENT_LIST : window.GoogleAnalytics.Action.EXPAND_ADDITIONAL_EVENTS_EVENT_LIST, 
          window.GoogleAnalytics.Category.EVENT_LIST,
          `${isCollapsed ? 'Collapse' : 'Expand'} ${$currentTarget.data('count')} additional outbreaks for ${$currentTarget.data('diseasename')} for ${window.FilterEventResults.geonameIds}`);
      }
    }
  });
  $('.eventlist__group').on('click', (e) => {
    const $target = $(e.target), $currentTarget = $(e.currentTarget);
    if (!$currentTarget.hasClass('eventlist__group--loading') && 
        ($target.hasClass('eventlist__groupheading') || $target.closest('.eventlist__groupheading').length > 0)) {
      $currentTarget.toggleClass('eventlist__group--collapsed').toggleClass('eventlist__group--expanded');

      const isCollapsed = $currentTarget.hasClass('eventlist__group--collapsed');
      if (e.originalEvent) {
        // Only log on human-triggered clicks not synthetic clicks
        window.gtagh(
          isCollapsed ? window.GoogleAnalytics.Action.COLLAPSE_DISEASE_GROUP_EVENT_LIST : window.GoogleAnalytics.Action.EXPAND_DISEASE_GROUP_EVENT_LIST,
          window.GoogleAnalytics.Category.EVENT_LIST,
          `${isCollapsed ? 'Collapse' : 'Expand'} ${$currentTarget.data('diseasename')}`);
      }
    }
  });
  
  function createNoResults() {
    return (
      `
        <div class="eventlist__noresults">
            <div>
                <div class="eventlist__noresults--text">No events found based on your query.</div>
                <div class="open-filter-panel eventlist__noresults--link">Modify your filters</div>
            </div>
        </div>
      `
    );
  }
  
  function createDiseaseGroup(diseaseGroup) {
    const sectionSelector = `.eventlist__group[data-id=${diseaseGroup.DiseaseId}]`;
    const alwaysVisible = (
         diseaseGroup.IsAllShown    // All items are visible, group must be visible
      || diseaseGroup.IsVisible     // Condition triggers the group to be visible (risk threshold or is local)
    );
    
    $.ajax({
      url: window.baseUrl + `/mvcapi/disease/AggregatedCaseCount?diseaseId=${diseaseGroup.DiseaseId || -1}&geonameIds=${model.FilterParams.geonameIds}`,
      method: 'GET',
      success: (data) => {
        
        if (!alwaysVisible && !data.IsVisible) {
          // Section will be hidden if does not exceed threshold as determined by the server-side logic
          $(`${sectionSelector}`).addClass('eventlist__group--hidden');
          decreaseVisibleGroups();
        } else {
          incrementEventsCount(diseaseGroup.ShownEvents.length + diseaseGroup.HiddenEvents.length);
        }
        
        // Update the values in the summary
        if (!data.TotalCases) {
          // No cases, summary text will reflect this
          $(`${sectionSelector} .eventlistsummary__cases`).addClass('eventlistsummary__cases--zero');
        }
        $(`${sectionSelector} .eventlistsummary__cases .eventlistsummary__valuetext`).text(data.TotalCasesText || 'No cases reported in or near your locations');
        $(`${sectionSelector} .eventlistsummary__travellers .eventlistsummary__valuetext`).text(diseaseGroup.TravellersText);
      },
      error: () => {
        $(`${sectionSelector} .eventlistsummary__cases .eventlistsummary__valuetext`).html('&mdash;');
        $(`${sectionSelector} .eventlistsummary__travellers .eventlistsummary__valuetext`).html('&mdash;');
      },
      complete: () => {
        $(`${sectionSelector}`).removeClass('eventlist__group--loading');
        $(`${sectionSelector} .eventlist__groupheadingtext`).text(diseaseGroup.DiseaseName);
      }
    });

    return (
      `
        <section class="eventlist__group eventlist__group--collapsed eventlist__group--loading"
                 data-id="${diseaseGroup.DiseaseId}"
                 data-diseasename="${diseaseGroup.DiseaseName}">
            <h3 class="eventlist__groupheading">
                <div class="eventlist__groupheadingtext">&nbsp;</div>
                <svg class="eventlistheading__icon" width="15" height="8" viewBox="0 0 15 8" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M1 1L7.08242 6.83393C7.13723 6.88658 7.20232 6.92834 7.27398 6.95684C7.34563 6.98533 7.42243 7 7.5 7C7.57757 7 7.65437 6.98533 7.72602 6.95684C7.79768 6.92834 7.86277 6.88658 7.91758 6.83393L14 1" stroke="#4F4F4F" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </h3>
            <div class="eventlist__groupsummary">
                <div class="eventlistsummary eventlistsummary__cases">
                    <p class="eventlistsummary__value">
                        <svg class="eventlistsummary__icon" width="10" height="15" viewBox="0 0 10 15" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M9.28571 4.28571C9.28571 1.92143 7.36429 0 5 0C2.63571 0 0.714286 1.92143 0.714286 4.28571C0.714286 7.5 5 12.1429 5 12.1429C5 12.1429 9.28571 7.5 9.28571 4.28571ZM3.57143 4.28571C3.57143 3.5 4.21429 2.85714 5 2.85714C5.78571 2.85714 6.42857 3.5 6.42857 4.28571C6.42857 5.07143 5.79286 5.71429 5 5.71429C4.21429 5.71429 3.57143 5.07143 3.57143 4.28571ZM0 12.8571V14.2857H10V12.8571H0Z" fill="#AAAAAA"/>
                        </svg>
                        <span class="eventlistsummary__valuetext">&nbsp;</span>
                    </p>
                    <p class="eventlistsummary__description">Total number of cases reported in or near your location(s)</p>
                </div>
                <span class="eventlistsummary__vertdivider" />
                <div class="eventlistsummary eventlistsummary__travellers">
                    <p class="eventlistsummary__value">
                        <svg class="eventlistsummary__icon" width="14" height="14" viewBox="0 0 14 14" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M0 12.5263H14V14H0V12.5263ZM5.29421 8.30421L8.49579 9.16263L12.4121 10.2126C13.0016 10.3711 13.6058 10.0211 13.7642 9.43158C13.9226 8.8421 13.5726 8.2379 12.9832 8.07947L9.06684 7.02947L7.03684 0.383158L5.61105 0V6.10105L1.95263 5.12105L1.26737 3.41158L0.198947 3.12421V6.93737L1.38158 7.25421L5.29421 8.30421Z" fill="#AAAAAA"/>
                        </svg>
                        <span class="eventlistsummary__valuetext">&nbsp;</span>
                    </p>
                    <p class="eventlistsummary__description">Total number of expected infected travellers (per month)</p>
                </div>
            </div>
            <div class="eventlist__lists">
                ${createPrimaryList(diseaseGroup.DiseaseName, diseaseGroup.ShownEvents, diseaseGroup.IsAllShown)}
                ${createSecondaryList(diseaseGroup.DiseaseName, diseaseGroup.HiddenEvents)}
            </div>
        </section>
      `
    );
  }
  
  function createPrimaryList(diseaseName, eventsList = [], allShown = false) {
    const isPlural = eventsList.length > 1;
    return (
      `
        <div class="eventlist__primarylist eventlist__primarylist${eventsList.length ? '' : '--empty'} eventlist__primarylist${allShown ? '--all' : '--some'}">
            <div class="eventlist__content">
                <p class="eventlist__countsummary">
                    <span class="eventlist__countsummary--some">Displaying <span class="eventlist__countsummary-bold">${eventsList.length}</span> ${diseaseName} outbreak${isPlural ? 's' : ''} with <span class="eventlist__countsummary-bold">&ge;1%</span> risk to your location</span>
                    <span class="eventlist__countsummary--all">Displaying all <span class="eventlist__countsummary-bold">${eventsList.length}</span> ${diseaseName} outbreak${isPlural ? 's' : ''}</span>
                </p>
                ${eventsList.map(createEventItem).join('')}
            </div>
        </div>
      `
    );
  }

  function createSecondaryList(diseaseName, eventsList = []) {
    const isPlural = eventsList.length > 1;
    return (
      `
        <div class="eventlist__secondarylist eventlist__secondarylist--collapsed eventlist__secondarylist${eventsList.length ? '' : '--empty'}"
             data-count="${eventsList.length}"
             data-diseasename="${diseaseName}">
            <div class="eventlist__content">
                <p class="eventlist__countsummary">
                    Displaying remaining <span class="eventlist__countsummary-bold">${eventsList.length}</span> outbreak${isPlural ? 's' : ''} with <span class="eventlist__countsummary-bold">&lt;1%</span> risk to your location
                </p>
                ${eventsList.map(createEventItem).join('')}
            </div>
            <div class="eventlist__countsummary--button">
                <div class="eventlist__togglebutton eventlist__togglebutton--collapsed">Show <span class="eventlist__countsummary-bold">${eventsList.length}</span> ${diseaseName} outbreak${isPlural ? 's' : ''} with <span class="eventlist__countsummary-bold">&lt;1%</span> risk to your location</div>
                <div class="eventlist__togglebutton eventlist__togglebutton--expanded">Collapse outbreaks with <span class="eventlist__countsummary-bold">&lt;1%</span> risk to your location</div>
            </div>
        </div>
      `
    );
  }
  
  function createEventItem(event) {
    const extraSourcesCount = event.ArticleSourceNames.slice(3).length;
    const extraSourcesText = extraSourcesCount ? `<span class="eventlistitem__source--extra">+ ${extraSourcesCount} source</span>` : '';
    
    return (
      `
        <article id="event-${event.EventId}" class="eventlistitem" data-id="${event.EventId}">
            ${getExportationRiskIcon(event)}
            ${getImportationRiskIcon(event)}
            <p class="eventlistitem__date">
                ${getOutlookReportIcon(event)}
                ${event.EventStartDate} — ${event.EventEndDate}
            </p>
            <h4 class="eventlistitem__title">${event.EventTitle}</h4>
            <p class="eventlistitem__source${event.ArticleSourceNames.length ? '' : '--empty'}">${event.ArticleSourceNames.slice(0, 3).join(' / ')} ${extraSourcesText}</p>
            <p class="eventlistitem__summary">${event.EventSummary}</p>
        </article>
      `
    );
  }
  
  function getOutlookReportIcon(event) {
    if (!event.HasOutlookReport) {
      return '';
    }
    
    return (
      `
        <span class="eventlistitem__tooltip--report">
          <img class="eventlistitem__icon--report" src="${window.ImagePaths.OutlookReportIcon}" alt=""/>
        </span>
      `
    )
  }
  
  function getImportationRiskIcon(event) {
    if (event.IsLocalSpread) {
      return (
        `
          <span class="eventlistitem__tooltip eventlistitem__tooltip--risk"
                data-risklevel="-1"
                data-risktitle="Outbreak is occurring in or proximal to your area(s) of interest">
              <img class="eventlistitem__icon eventlistitem__icon--risk" src="${window.ImagePaths.LocalSpreadIcon}" alt=""/>
          </span>
        `
      );
    }
    
    return (
      `
        <span class="eventlistitem__tooltip eventlistitem__tooltip--risk"
              data-risklevel="${event.ImportationRiskLevel}"
              data-risktitle="${getRiskTitle(event.ImportationRiskLevel)}"
              data-riskvalue="${event.ImportationRiskText || '&mdash;'}"
              data-risknote="Overall probability of at least one (1) imported infected traveller in one month">
            <img class="eventlistitem__icon eventlistitem__icon--risk" src="${getRiskIcon(event.ImportationRiskLevel)}" alt=""/>
            <img class="eventlistitem__icon eventlistitem__icon--import" src="${window.ImagePaths.ImportGreyIcon}" alt=""/>
        </span>
      `
    );
  }

  function getExportationRiskIcon(event) {
    return (
      `
        <span class="eventlistitem__tooltip eventlistitem__tooltip--risk"
              data-risklevel="${event.ExportationRiskLevel}"
              data-risktitle="${getRiskTitle(event.ExportationRiskLevel)}"
              data-riskvalue="${event.ExportationRiskText || '&mdash;'}"
              data-risknote="Overall probability of at least one (1) exported infected traveller in one month">
            <img class="eventlistitem__icon eventlistitem__icon--risk" src="${getRiskIcon(event.ExportationRiskLevel)}" alt=""/>
            <img class="eventlistitem__icon eventlistitem__icon--export" src="${window.ImagePaths.ExportGreyIcon}" alt=""/>
        </span>
      `
    );
  }
  
  function getRiskTooltipContent(e) {
    const target = e.target;
    const riskLevel = target.data('risklevel');
    if (riskLevel === -1) {
      // Local Spread
      return (
        `
          <div class="risktooltip">
              <p><img class="risktooltip__icon" src="${window.ImagePaths.LocalSpreadIcon}" alt=""/></p>
              <p class="risktooltip__title">${target.data('risktitle')}</p>
          </div>
        `
      );
    }
    
    return (
      `
        <div class="risktooltip">
            <p><img class="risktooltip__icon" src="${getRiskIcon(riskLevel)}" alt=""/></p>
            <p class="risktooltip__title">${target.data('risktitle')}</p>
            <p class="risktooltip__value">${target.data('riskvalue')}</p>
            <p class="risktooltip__note">${target.data('risknote')}</p>
        </div>
      `
    );
  }
  
  function decreaseVisibleGroups() {
    visibleGroups--;
    if (visibleGroups <= 0) {
      $eventList.addClass('eventlist--noresults');
    }
  }
  
  function incrementEventsCount(increment) {
    window.FilterEventResults.resultCount += increment;
    window.FilterSummaryMethods.updateSummaryText();
  }

  function getRiskIcon(riskLevel) {
    switch (riskLevel) {
      case 1:
        return window.ImagePaths.RiskLowIcon;
      case 2:
        return window.ImagePaths.RiskMediumIcon;
      case 3:
        return window.ImagePaths.RiskHighIcon;
      default:
        return window.ImagePaths.RiskNoneIcon;
    }
  }

  function getRiskTitle(riskLevel) {
    switch (riskLevel) {
      case 1:
        return "Low probability";
      case 2:
        return "Medium probability";
      case 3:
        return "High probability";
      default:
        return "No probability";
    }
  }
})(window.EventListResult);