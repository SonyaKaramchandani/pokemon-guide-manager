/** @jsx jsx */
import { useBreakpointIndex } from '@theme-ui/match-media';
import * as dto from 'client/dto';
import { Formik, FormikHelpers } from 'formik';
import { NumericDictionary } from 'lodash';
import React, { useCallback, useContext, useEffect, useMemo, useState } from 'react';
import { Button, Dropdown, DropdownItemProps, Grid } from 'semantic-ui-react';
import { jsx } from 'theme-ui';
import * as Yup from 'yup';

import { hasIntersection, mapToDictionary } from 'utils/arrayHelpers';
import { sxtheme, sxMixinActiveHover } from 'utils/cssHelpers';
import { isMobile, isNonMobile } from 'utils/responsive';
import { nameof } from 'utils/typeHelpers';

import { FlexGroup } from 'components/_common/FlexGroup';
import { BdParagraph } from 'components/_common/SectionHeader';
import { Typography } from 'components/_common/Typography';
import { BdSearch } from 'components/_controls/BdSearch';

import { FormikSemanticDropDown } from '../FormikControls/FormikSemanticDropDown';
import { Map2DiseaseGroupingVM } from './CustomSettingsHelpers';
import {
  CustomSettingsFM,
  CustomSettingsGeoname,
  CustomSettingsSubmitData,
  DiseaseGroupingVM,
  DiseaseNotificationLevelDict,
  RoleAndItsPresets
} from './CustomSettingsModels';
import { UserAoiMultiselectFormikControl } from './FormikControls.Aoi';
import {
  DiseaseRowFormikControl,
  DiseaseRowGroupAccordian,
  DiseaseTableHeading
} from './FormikControls.DiseaseTable';
import { BdDropdown } from 'components/_controls/BdDropdown/BdDropdown';

const PageHeading: React.FC = ({ children }) => (
  <BdParagraph>
    <Typography variant="h2" color="stone90">
      {children}
    </Typography>
  </BdParagraph>
);

const PageParagraph: React.FC = ({ children }) => (
  <BdParagraph>
    <Typography variant="body1" color="stone90">
      {children}
    </Typography>
  </BdParagraph>
);

//=====================================================================================================================================

type CustomSettingsFormProps = {
  rolesAndPresets: RoleAndItsPresets[];
  diseases: NumericDictionary<dto.DiseaseInformationModel>;
  diseaseGroupings: dto.DiseaseGroupModel[];
  geonames: dto.GetGeonameModel[];
  userRoleIdInitial: string;
  userDiseaseRelevanceSetting: DiseaseNotificationLevelDict;
  onSubmit: (
    data: CustomSettingsSubmitData,
    setSubmitting: (isSubmitting: boolean) => void
  ) => void;
};

//=====================================================================================================================================

export const CustomSettingsForm: React.FC<CustomSettingsFormProps> = ({
  rolesAndPresets,
  diseases,
  diseaseGroupings,
  geonames,
  userRoleIdInitial,
  userDiseaseRelevanceSetting,
  onSubmit
}) => {
  const refCustomDiseasesButtonDiv = React.useRef<HTMLDivElement>();
  const [isCustomizeDiseasesOpen, setIsCustomizeDiseasesOpen] = useState(false);
  const [diseaseSearchFilterText, setDiseaseSearchFilterText] = useState('');
  const [selectedDiseaseGrouping, setSelectedDiseaseGrouping] = useState<dto.DiseaseGroupModel>(
    null
  );
  const isNonMobileDevice = isNonMobile(useBreakpointIndex());
  const isMobileDevice = isMobile(useBreakpointIndex());

  useEffect(() => {
    diseaseGroupings && diseaseGroupings[0] && setSelectedDiseaseGrouping(diseaseGroupings[0]);
  }, [diseaseGroupings]);

  function Map2SubmitData(
    values: CustomSettingsFM,
    optimize: 'quick' | 'full'
  ): CustomSettingsSubmitData {
    const diseaseMatrix = values.diseasesPerRole[values.roleId];
    const activeRoleBucket =
      optimize === 'full' && rolesAndPresets && rolesAndPresets.find(x => x.id === values.roleId);
    return {
      roleId: values.roleId,
      geonames: values.geonames,
      diseaseMatrix: diseaseMatrix,
      ...(optimize === 'full' && {
        isPresetAltered: ComputeIsDiseasematrixAltered(
          activeRoleBucket && activeRoleBucket.preset,
          diseaseMatrix
        )
      })
    };
  }

  const formikSeedValue = useMemo<CustomSettingsFM>(
    () => ({
      roleId: userRoleIdInitial,
      geonames:
        geonames &&
        geonames.map(x => ({
          geonameId: x.geonameId,
          name: x.fullDisplayName
        })),
      diseasesPerRole: {
        ...mapToDictionary(
          rolesAndPresets,
          x => x.id,
          x => ({ ...x.preset })
        ),
        ...(userRoleIdInitial &&
          userDiseaseRelevanceSetting && {
            [userRoleIdInitial]: { ...userDiseaseRelevanceSetting }
          })
      }
    }),
    [userRoleIdInitial, geonames, rolesAndPresets, userDiseaseRelevanceSetting]
  );

  const formikSeedValueJson = useMemo(
    () => JSON.stringify(Map2SubmitData(formikSeedValue, 'quick')),
    [formikSeedValue]
  );

  const vmDiseaseGroupRoot: DiseaseGroupingVM = useMemo(() => {
    return (
      selectedDiseaseGrouping &&
      diseases &&
      Map2DiseaseGroupingVM(selectedDiseaseGrouping.subGroups, diseases, diseaseSearchFilterText)
    );
  }, [selectedDiseaseGrouping, diseases, diseaseSearchFilterText]);

  const vmDiseaseGroupRootHasResults = useMemo(() => {
    function ComputeHasValues(group: DiseaseGroupingVM): boolean {
      if (group.diseases && group.diseases && group.diseases.length > 0) return true;
      return !!(group.subgroups && group.subgroups.find(x => ComputeHasValues(x)));
    }
    return !!(vmDiseaseGroupRoot && ComputeHasValues(vmDiseaseGroupRoot));
  }, [vmDiseaseGroupRoot]);

  const roleOptions = useMemo<DropdownItemProps[]>(
    () =>
      rolesAndPresets &&
      rolesAndPresets.map(x => ({
        text: x.name || 'Select a role',
        value: x.id
      })),
    [rolesAndPresets]
  );

  const scrollToCustomizedDiseases = () => {
    if (
      refCustomDiseasesButtonDiv &&
      refCustomDiseasesButtonDiv.current &&
      refCustomDiseasesButtonDiv.current.scrollIntoView
    ) {
      setIsCustomizeDiseasesOpen(true);
      setTimeout(() => {
        refCustomDiseasesButtonDiv.current.scrollIntoView();
      }, 10);
    }
  };

  const handleFormSubmit = useCallback(
    (values: CustomSettingsFM, formikHelpers: FormikHelpers<CustomSettingsFM>) => {
      const { setSubmitting } = formikHelpers;
      const submitData = Map2SubmitData(values, 'full');
      onSubmit && onSubmit(submitData, setSubmitting);
    },
    [onSubmit, rolesAndPresets]
  );

  const ComputeIsDiseasematrixAltered = (
    matrix1: DiseaseNotificationLevelDict,
    matrix2: DiseaseNotificationLevelDict
  ) => (matrix1 && matrix2 && JSON.stringify(matrix1) !== JSON.stringify(matrix2)) || false;

  if (!rolesAndPresets || !diseases || !diseaseGroupings) {
    return <div />;
  }

  return (
    <Formik<CustomSettingsFM>
      enableReinitialize
      initialValues={formikSeedValue}
      onSubmit={handleFormSubmit}
      validationSchema={Yup.object().shape<Partial<CustomSettingsFM>>({
        geonames: Yup.array<CustomSettingsGeoname>().required('Please enter at least one location')
      })}
    >
      {({
        values,
        errors,
        touched,
        getFieldHelpers,
        handleChange,
        handleBlur,
        handleSubmit,
        isSubmitting
      }) => {
        const hasErrors = hasIntersection(Object.keys(errors), Object.keys(touched));
        const activeRoleBucket =
          rolesAndPresets && rolesAndPresets.find(x => x.id === values.roleId);
        const resetFormikToSelectedRolePreset = () => {
          const { setValue } = getFieldHelpers(
            `${nameof<CustomSettingsFM>('diseasesPerRole')}.${values.roleId}`
          );
          setValue({ ...activeRoleBucket.preset });
          // scrollToCustomizedDiseases();
        };
        const isDiseasesAltered =
          activeRoleBucket &&
          ComputeIsDiseasematrixAltered(
            activeRoleBucket.preset,
            values.diseasesPerRole[values.roleId]
          );
        const isFormAltered =
          formikSeedValueJson !== JSON.stringify(Map2SubmitData(values, 'quick'));
        return (
          <form onSubmit={handleSubmit}>
            {hasErrors && (
              <Typography variant="body1" color="clay100">
                <p>The information is incomplete.</p>
                <ul>
                  {Object.values(errors).map(errorText => (
                    <li>{errorText}</li>
                  ))}
                </ul>
              </Typography>
            )}
            <PageHeading>My Locations</PageHeading>
            <PageParagraph>
              See your default locations within the Custom View on your dashboard. You will also
              receive notifications about risk to these locations.
            </PageParagraph>
            <PageParagraph>Select up to 50 locations:</PageParagraph>
            <PageParagraph>
              <UserAoiMultiselectFormikControl
                name={nameof<CustomSettingsFM>('geonames')}
                error={touched.geonames && !!errors.geonames}
                maxAoi={50}
              />
            </PageParagraph>
            <PageHeading>My Role</PageHeading>
            <PageParagraph>
              See disease risks most likely to affect you within the Custom View on your dashboard.
              You will also receive notifications about these diseases.
            </PageParagraph>
            <PageParagraph>
              You can further customize your disease list below by clicking on "Customize Diseases."
            </PageParagraph>
            <PageParagraph>
              <FlexGroup prefix="I work in">
                <FormikSemanticDropDown
                  name={nameof<CustomSettingsFM>('roleId')}
                  placeholder="Select a role"
                  options={roleOptions}
                  error={touched.roleId && !!errors.roleId}
                  // onValueChange={val => setSelectedUserRoleId(val as string)}
                />
              </FlexGroup>
            </PageParagraph>
            {activeRoleBucket && activeRoleBucket.notificationDescription && (
              <PageParagraph>
                <span
                  dangerouslySetInnerHTML={{ __html: activeRoleBucket.notificationDescription }}
                />
              </PageParagraph>
            )}
            {isDiseasesAltered && (
              <PageParagraph>
                Your disease list has been customized —{' '}
                <a
                  onClick={scrollToCustomizedDiseases}
                  sx={{
                    color: 'black',
                    textDecoration: 'underline',
                    cursor: 'pointer'
                  }}
                >
                  Edit customized diseases
                </a>
              </PageParagraph>
            )}
            <PageParagraph>
              <FlexGroup
                suffix={
                  isDiseasesAltered &&
                  activeRoleBucket && (
                    <a onClick={resetFormikToSelectedRolePreset} sx={{ cursor: 'pointer' }}>
                      {`Reset to ${activeRoleBucket.name} presets`}
                    </a>
                  )
                }
              >
                <Typography variant="h2" color="stone90">
                  Customize Diseases
                </Typography>
              </FlexGroup>
            </PageParagraph>
            {isMobileDevice && (
              <PageParagraph>
                <Typography variant="body1" color="stone90">
                  To edit your customized diseases, visit your custom settings on your desktop.
                </Typography>
              </PageParagraph>
            )}
            {isNonMobileDevice && (
              <React.Fragment>
                <div ref={refCustomDiseasesButtonDiv}>
                  <Button
                    type="button" // LESSON: form will submit on ANY button press unless its marked with type="button" (LINK: https://github.com/jaredpalmer/formik/issues/1610#issuecomment-502831705)
                    onClick={() => setIsCustomizeDiseasesOpen(!isCustomizeDiseasesOpen)}
                    sx={{
                      '&.ui.button': {
                        width: '100%',
                        background: 'none',
                        border: '1px solid #364E78',
                        fontSize: '16px',
                        lineHeight: '19px',
                        borderRadius: '3px',
                        padding: '10px',
                        cursor: 'pointer',
                        ...sxMixinActiveHover()
                      }
                    }}
                  >
                    <Typography variant="button" color="stone90">
                      {isCustomizeDiseasesOpen
                        ? 'Hide Customized Diseases'
                        : 'Customize My Diseases'}
                    </Typography>
                  </Button>
                </div>
                {isCustomizeDiseasesOpen && (
                  <Grid>
                    <Grid.Column width={16}>
                      <div
                        sx={{
                          margin: `10px 0`,
                          width: `100%`,
                          minHeight: `770px`,
                          // maxWidth: `600px`,
                          backgroundColor: `#fff`,
                          padding: `20px 15px`,
                          border: `1px solid #E9E9E9`,
                          borderRadius: `6px`,
                          overflow: `hidden`,

                          // CODE: ed8635ac: see FormikControls.DiseaseTable for className's
                          '& .disease-row': {
                            borderBottom: `1px solid #EAEAEA`,
                            '&.disease-group-row': {
                              backgroundColor: `#F9F9F9`,
                              borderTop: `2px solid #D2D2D2`,
                              cursor: `pointer`,
                              '&.secondary': {
                                backgroundColor: `#fff`,
                                borderTop: `2px solid #D2D2D2`
                              }
                            },
                            '&.disease-table-header': {
                              borderTop: `2px solid #D2D2D2`,
                              borderBottom: `none`,
                              position: 'sticky', // TODO: 452806ed: sticky header does not work, possible cause: overflow-x is in on but no scrollbar on page...
                              '& .disease-header-text': {
                                width: `100%`,
                                display: `table-cell`,
                                color: `#2D3040`,
                                fontStyle: `normal`,
                                fontWeight: `normal`,
                                fontSize: `14px`,
                                lineHeight: `17px`,
                                padding: `15px 0 10px`,
                                verticalAlign: `bottom`
                              },
                              '& .disease-option': {
                                verticalAlign: `bottom`
                              }
                            },
                            '& .disease-name': {
                              width: `100%`,
                              display: `table-cell`,
                              color: `#8C8C8C`,
                              fontSize: `16px`,
                              lineHeight: `20px`,
                              padding: `10px 15px`,
                              verticalAlign: `middle`
                            },
                            '& .disease-option': {
                              minWidth: `75px`,
                              borderLeft: `1px solid #EAEAEA`,
                              display: `table-cell`,
                              textAlign: `center`,
                              verticalAlign: `middle`,
                              cursor: 'pointer',

                              '& .label': {
                                fontSize: '12px',
                                fontWeight: 400,
                                display: 'block'
                              }
                            }
                          },
                          '& .not-found': {
                            my: '240px',
                            textAlign: 'center'
                          }
                        }}
                      >
                        <BdSearch
                          placeholder='E.g. "measles", "ebola", "zika"'
                          debounceDelay={200}
                          onSearchTextChange={setDiseaseSearchFilterText}
                        />
                        <FlexGroup
                          prefix={
                            <Typography variant="subtitle2" color="stone90">
                              Group by
                            </Typography>
                          }
                        >
                          <BdDropdown
                            placeholder="Select a role"
                            options={diseaseGroupings.map((dg, dgIndex) => ({
                              text: dg.groupName,
                              value: dgIndex
                            }))}
                            value={diseaseGroupings.indexOf(selectedDiseaseGrouping)}
                            onChange={value =>
                              setSelectedDiseaseGrouping(diseaseGroupings[value as number])
                            }
                          />
                          <Dropdown
                            fluid
                            placeholder="Select a role"
                            selection
                            options={diseaseGroupings.map((dg, dgIndex) => ({
                              text: dg.groupName,
                              value: dgIndex
                            }))}
                            value={diseaseGroupings.indexOf(selectedDiseaseGrouping)}
                            onChange={(_, { value }) =>
                              setSelectedDiseaseGrouping(diseaseGroupings[value as number])
                            }
                            sx={{
                              '&.ui.selection.dropdown': {
                                fontSize: '14px',
                                height: '40px',
                                borderBottom: `none`,
                                mt: '5px',
                                '&:not(.error)': {
                                  bg: 'white',
                                  borderBottom: `none`,
                                  '& > input.search': {}
                                }
                              }
                            }}
                          />
                        </FlexGroup>
                        <DiseaseTableHeading
                          diseaseGroupRoot={vmDiseaseGroupRoot}
                          diseaseFormData={values.diseasesPerRole[values.roleId]}
                          getFieldHelpers={getFieldHelpers}
                          activeRoleId={values.roleId}
                          umbrellaSelectionDisabled={
                            !!diseaseSearchFilterText && diseaseSearchFilterText.length > 0
                          }
                        />
                        {vmDiseaseGroupRoot &&
                          vmDiseaseGroupRoot.subgroups.map(
                            diseaseGroup =>
                              !!(
                                (diseaseGroup.diseases && diseaseGroup.diseases.length) ||
                                (diseaseGroup.subgroups && diseaseGroup.subgroups.length)
                              ) && (
                                <DiseaseRowGroupAccordian
                                  key={`diseaseGroup-${diseaseGroup.name}`}
                                  title={diseaseGroup.name}
                                  diseaseGroup={diseaseGroup}
                                  diseaseFormData={values.diseasesPerRole[values.roleId]}
                                  getFieldHelpers={getFieldHelpers}
                                  activeRoleId={values.roleId}
                                  umbrellaSelectionDisabled={
                                    !!diseaseSearchFilterText && diseaseSearchFilterText.length > 0
                                  }
                                >
                                  {diseaseGroup.subgroups &&
                                    diseaseGroup.subgroups.map(diseaseGroup2 => (
                                      <DiseaseRowGroupAccordian
                                        key={`diseaseGroup-${diseaseGroup2.name}`}
                                        isSecondary
                                        title={diseaseGroup2.name}
                                        diseaseGroup={diseaseGroup2}
                                        diseaseFormData={values.diseasesPerRole[values.roleId]}
                                        getFieldHelpers={getFieldHelpers}
                                        activeRoleId={values.roleId}
                                        umbrellaSelectionDisabled={
                                          !!diseaseSearchFilterText &&
                                          diseaseSearchFilterText.length > 0
                                        }
                                      >
                                        {diseaseGroup2.diseases &&
                                          diseaseGroup2.diseases.map(disease => (
                                            <DiseaseRowFormikControl
                                              key={`diseaseGroup-${diseaseGroup2.name}.${disease.id}`}
                                              //prettier-ignore
                                              name={`${nameof<CustomSettingsFM>('diseasesPerRole')}.${values.roleId}.${disease.id}`}
                                              disease={disease}
                                            />
                                          ))}
                                      </DiseaseRowGroupAccordian>
                                    ))}
                                  {/* DESIGN: 859d1084: if subgroups present do NOT show immediate children diseases, show subgroups only */}
                                  {!diseaseGroup.subgroups &&
                                    diseaseGroup.diseases &&
                                    diseaseGroup.diseases.map(disease => (
                                      <DiseaseRowFormikControl
                                        key={`diseaseGroup-${diseaseGroup.name}.${disease.id}`}
                                        //prettier-ignore
                                        name={`${nameof<CustomSettingsFM>('diseasesPerRole')}.${values.roleId}.${disease.id}`}
                                        disease={disease}
                                      />
                                    ))}
                                </DiseaseRowGroupAccordian>
                              )
                          )}
                        {!vmDiseaseGroupRootHasResults && (
                          <div className="not-found">
                            <Typography variant="h1" color="stone90">
                              No matching diseases.
                            </Typography>
                          </div>
                        )}
                      </div>
                    </Grid.Column>
                    {/* <Grid.Column width={4}>
                    <pre sx={{ fontSize: '8px' }}>{JSON.stringify(values, null, 2)}</pre>
                  </Grid.Column> */}
                  </Grid>
                )}
              </React.Fragment>
            )}

            <div
              sx={{
                py: '20px',
                textAlign: 'center',
                background: sxtheme(t => t.colors.stone10)
                // position: `sticky`,
                // bottom: `0`,
                // position: `fixed`,
                // width: `100%`,
                // left: `0`,
                // boxShadow: `0px -4px 4px rgba(0, 0, 0, 0.15)`
              }}
              // onScroll={onScroll} TODO: make a sticky/style alterating directive with "marker" phantom element to track scroll visibility state and toggle box shadown and position
            >
              <Button type="submit" disabled={!isFormAltered || hasErrors || isSubmitting}>
                Update Settings
              </Button>
            </div>
          </form>
        );
      }}
    </Formik>
  );
};
