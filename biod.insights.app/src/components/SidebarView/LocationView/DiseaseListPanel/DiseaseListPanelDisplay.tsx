/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import React, { useContext, useEffect, useMemo, useState } from 'react';
import { List } from 'semantic-ui-react';
import { jsx } from 'theme-ui';

import { AppStateContext } from 'api/AppStateContext';
import { DiseaseAndProximalRiskVM } from 'models/DiseaseModels';
import {
  DefaultSortOptionValue,
  DiseaseListGlobalViewSortOptions,
  DiseaseListLocationViewSortOptions
} from 'models/SortByOptions';
import { sort } from 'utils/arrayHelpers';
import { Geoname } from 'utils/constants';
import { isMobile } from 'utils/responsive';
import { containsNoCaseNoLocale } from 'utils/stringHelpers';

import { BdSearch } from 'components/_controls/BdSearch';
import { IconButton } from 'components/_controls/IconButton';
import NotFoundMessage from 'components/_controls/Misc/NotFoundMessage';
import { Error } from 'components/Error';
import { MobilePanelSummary } from 'components/MobilePanelSummary';
import { ILoadableProps, IPanelProps, Panel } from 'components/Panel';
import { SortBy } from 'components/SortBy';

import DiseaseCard from './DiseaseCard';

export type DiseaseListPanelDisplayProps = IPanelProps &
  ILoadableProps & {
    subtitleMobile: string;
    summaryTitle: string;
    isGlobal?: boolean;
    diseaseId: number;
    diseases: dto.DiseaseRiskModel[];
    subtitle: string;
    onDiseaseSelect: (disease: dto.DiseaseRiskModel) => void;
    onSettingsClick: () => void;
    onRetryClick: () => void;
    hasError: boolean;
  };

const DiseaseListPanelDisplay: React.FC<DiseaseListPanelDisplayProps> = ({
  isLoading = false,
  isGlobal = false,
  diseaseId,
  diseases,
  subtitle,
  subtitleMobile,
  summaryTitle,
  hasError,
  onDiseaseSelect,

  onSettingsClick,
  onRetryClick,

  isMinimized, // TODO: 633056e0: group panel-related props (and similar)
  onMinimize,
  onClose
}) => {
  const { appState } = useContext(AppStateContext);
  const isMobileDevice = isMobile(useBreakpointIndex());
  const [diseaseSearchFilterText, setDiseaseSearchFilterText] = useState('');
  const [sortBy, setSortBy] = useState(DefaultSortOptionValue); // TODO: 597e3adc: right now we get away with the setter cast, because it thinks its a string
  const [sortOptions, setSortOptions] = useState(DiseaseListLocationViewSortOptions);

  const { proximalData } = appState;

  useEffect(() => {
    if (isGlobal) {
      setSortOptions(DiseaseListGlobalViewSortOptions);
    } else {
      setSortOptions(DiseaseListLocationViewSortOptions);
    }
  }, [isGlobal]);

  const processedDiseases: DiseaseAndProximalRiskVM[] = useMemo(
    () =>
      sort({
        items: (diseases || [])
          .filter(d => containsNoCaseNoLocale(d.diseaseInformation.name, diseaseSearchFilterText))
          .map(
            disease =>
              ({
                disease: disease,
                proximalVM: isGlobal
                  ? null
                  : proximalData && proximalData[disease.diseaseInformation.id],
                caseCounts: disease.caseCounts // Used for global case counts
              } as DiseaseAndProximalRiskVM)
          ),
        sortOptions,
        sortBy
      }),
    [diseaseSearchFilterText, diseases, proximalData, isGlobal, sortBy, sortOptions]
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
            {processedDiseases.map(diseaseVM => (
              <DiseaseCard
                key={diseaseVM.disease.diseaseInformation.id}
                vm={diseaseVM}
                selectedId={diseaseId}
                isGlobal={isGlobal}
                onSelect={() => onDiseaseSelect && onDiseaseSelect(diseaseVM.disease)}
              />
            ))}
          </React.Fragment>
        )}
      </List>
    </Panel>
  );
};

export default DiseaseListPanelDisplay;
