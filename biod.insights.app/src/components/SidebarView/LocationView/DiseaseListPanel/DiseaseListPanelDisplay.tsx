/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import React, { useMemo, useState, useEffect } from 'react';
import { Input, List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { isMobile } from 'utils/responsive';
import { BdIcon } from 'components/_common/BdIcon';
import { IconButton } from 'components/_controls/IconButton';
import NotFoundMessage from 'components/_controls/Misc/NotFoundMessage';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';
import * as dto from 'client/dto';
import {
  DefaultSortOptionValue,
  DiseaseListGlobalViewSortOptions,
  DiseaseListLocationViewSortOptions
} from 'models/SortByOptions';

import DiseaseCard, { DiseaseCardProps } from './DiseaseCard';
import { BdSearch } from 'components/_controls/BdSearch';
import { sort } from 'utils/arrayHelpers';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';
import { Geoname } from 'utils/constants';
import { CaseCountModelMap } from 'models/DiseaseModels';

export type DiseaseListPanelDisplayProps = IPanelProps &
  ILoadableProps & {
    subtitleMobile: string;
    summaryTitle: string;
    geonameId: number;
    diseaseId: number;
    diseases: DiseaseCardProps[];
    subtitle: string;
    onDiseaseSelect: (disease: dto.DiseaseRiskModel) => void;
    onSettingsClick: () => void;
    onRetryClick: () => void;
    diseasesCaseCounts: CaseCountModelMap;
    hasError: boolean;
  };

const DiseaseListPanelDisplay: React.FC<DiseaseListPanelDisplayProps> = ({
  isLoading = false,
  geonameId,
  diseaseId,
  diseases,
  subtitle,
  subtitleMobile,
  summaryTitle,
  hasError,
  onDiseaseSelect,
  diseasesCaseCounts,

  onSettingsClick,
  onRetryClick,

  isMinimized, // TODO: 633056e0: group panel-related props (and similar)
  onMinimize,
  onClose
}) => {
  const isMobileDevice = isMobile(useBreakpointIndex());
  const [diseaseSearchFilterText, setDiseaseSearchFilterText] = useState('');
  // TODO: 3b381eba: move sorting login into DiseaseListPanelDisplay
  const [sortBy, setSortBy] = useState(DefaultSortOptionValue); // TODO: 597e3adc: right now we get away with the setter cast, because it thinks its a string
  const [sortOptions, setSortOptions] = useState(DiseaseListLocationViewSortOptions);

  useEffect(() => {
    if (geonameId === Geoname.GLOBAL_VIEW) {
      setSortOptions(DiseaseListGlobalViewSortOptions);
    } else {
      setSortOptions(DiseaseListLocationViewSortOptions);
    }
  }, [geonameId]);

  const processedDiseases: DiseaseCardProps[] = useMemo(
    () =>
      sort({
        items: (diseases || [])
          .filter(d => containsNoCaseNoLocale(d.diseaseInformation.name, diseaseSearchFilterText))
          .map(s =>
            geonameId === Geoname.GLOBAL_VIEW
              ? s
              : {
                  ...s,
                  caseCounts: diseasesCaseCounts[s.diseaseInformation.id]
                }
          ),
        sortOptions,
        sortBy
      }),
    [diseaseSearchFilterText, diseases, diseasesCaseCounts, geonameId, sortBy, sortOptions]
  );

  return (
    <Panel
      isAnimated
      isLoading={isLoading}
      title="Diseases"
      subtitle={subtitle}
      subtitleMobile={subtitleMobile}
      toolbar={
        <React.Fragment>
          <SortBy
            selectedValue={sortBy}
            options={sortOptions}
            onSelect={setSortBy}
            disabled={isLoading}
          />
          <BdSearch
            searchText={diseaseSearchFilterText}
            placeholder="Search for diseases"
            debounceDelay={200}
            onSearchTextChange={setDiseaseSearchFilterText}
          />
        </React.Fragment>
      }
      headerActions={
        !isMobileDevice && (
          <IconButton
            icon="icon-cog"
            color="sea100"
            bold
            tooltipText="Modify the diseases in this list"
            nomargin
            onClick={onSettingsClick}
          />
        )
      }
      summary={<MobilePanelSummary onClick={onClose} summaryTitle={summaryTitle} />}
      isMinimized={isMinimized}
      onMinimize={onMinimize}
      onClose={onClose}
    >
      <List>
        {hasError ? (
          <Error
            title="Something went wrong."
            subtitle="Please check your network connectivity and try again."
            linkText="Click here to retry"
            linkCallback={onRetryClick}
          />
        ) : !(diseases || []).length ? (
          <Error
            title="No relevant diseases to your location."
            subtitle="Change your disease settings to Always of Interest to make them relevant to your location."
            linkText="Click here to customize your settings"
            linkCallback={onSettingsClick}
          />
        ) : (
          <React.Fragment>
            {!processedDiseases.length && <NotFoundMessage text="Disease not found" />}
            {processedDiseases.map(disease => (
              <DiseaseCard
                key={disease.diseaseInformation.id}
                selected={diseaseId}
                geonameId={geonameId}
                {...disease}
                onSelect={() => onDiseaseSelect && onDiseaseSelect(disease)}
              />
            ))}
          </React.Fragment>
        )}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
