// global dependency: window.biod.map.gaEvent
// global dependency: window.filterParams.geonameIds
// global provider: window.biod.map.updatePoints

import helpers from './helpers';
import mapApi from '../../api/mapApi';

import {
    featureCountryPolygonCollection,
    featureCountryPointCollection,
    featureAirportPointCollection,
    countryPointFlagOutlookReportPictureMarkerSymbolObject,
    countryPointLabelClassObject
} from './config';

function renderMap({
    getCountriesAndEvents,
    baseMapJson
}) {
    helpers.whenEsriReady(({
        FeatureLayer, Point, Graphic, Popup, LabelClass, PictureMarkerSymbol,
        domClass, domConstruct, array, Polygon, Map, VectorTileLayer
    }) => {
        let countryOutlineLayer = null;
        let countryPinsLayer = null;
        let destinationAirportPointsLayer = null;
        let map = null;
        let currentZoom = 2;
        let popup = null;

        const basemap = new VectorTileLayer(baseMapJson);
        const countryPointFlagOutlookReportPictureMarkerSymbol = new PictureMarkerSymbol(countryPointFlagOutlookReportPictureMarkerSymbolObject);
        const countryPointLabelClass = new LabelClass(countryPointLabelClassObject);

        function addDestinationAirportPoints(inputArr) {
            const features = [];
            array.forEach(inputArr, function (item) {
                const attr = {
                    "sourceData": item
                };

                const geometry = new Point(item);
                const graphic = new Graphic(geometry);
                graphic.setAttributes(attr);
                features.push(graphic);
            });

            destinationAirportPointsLayer.applyEdits(features, null, null);
        }

        function addDestinationAirportsForEvent(eventId) {
            function parseAirportData(inputArr) {
                const retArr = [];
                for (let i = 0; i < inputArr.length; i++) {
                    const item = inputArr[i];
                    if (!isNaN(item.Latitude) && !isNaN(item.Longitude) &&
                        item.Latitude !== 0 && item.Latitude !== 0) {
                        retArr.push({
                            StationName: item.StationName,
                            CityDisplayName: item.CityDisplayName,
                            StationCode: item.StationCode,
                            x: Number(item.Longitude),
                            y: Number(item.Latitude),
                        });
                    }
                }
                return retArr;
            }

            if (eventId) {
                mapApi.getDestinationAirport(eventId, window.filterParams.geonameIds)
                    .then(({ data }) => {
                        if (data.length) {
                            if (!(data.length === 1 && data[0].CityDisplayName === "-")) {
                                addDestinationAirportPoints(parseAirportData(data));
                            }
                        }
                    }).catch(() => {
                        console.log('Failed to get destination airport');
                    });
            }
        }

        function removeAllDestinationAirportPoints() {
            if (destinationAirportPointsLayer.graphics && destinationAirportPointsLayer.graphics.length) {
                destinationAirportPointsLayer.applyEdits(null, null, destinationAirportPointsLayer.graphics);
            }
        }

        function initDestinationAirportPointsLayer() {
            destinationAirportPointsLayer = new FeatureLayer(featureAirportPointCollection, {
                id: 'destinationAirportPointsLayer',
                outFields: ["*"]
            });
            map.addLayer(destinationAirportPointsLayer);
        }

        function addCountryPins(inputArr) {
            var features = [];
            array.forEach(inputArr, function (item) {
                var attr = {};
                attr["eventCount"] = (item.NumOfEvents > 9 ? "9+" : item.NumOfEvents.toString());
                attr["sourceData"] = item;

                var geometry = new Point(item);
                var graphic = new Graphic(geometry);
                graphic.setAttributes(attr);

                if (item.FlagOutlookReport) {
                    graphic.setSymbol(countryPointFlagOutlookReportPictureMarkerSymbol);
                }

                features.push(graphic);
            });

            countryPinsLayer.applyEdits(features, null, countryPinsLayer.graphics);
        }

        function initCountryPinsLayer() {
            countryPinsLayer = new FeatureLayer(featureCountryPointCollection, {
                id: 'countryPinsLayer',
                outFields: ["*"]
            });

            countryPinsLayer.setLabelingInfo([countryPointLabelClass]);
            countryPinsLayer.on("click", function (evt) {
                const graphic = evt.graphic;
                const sourceData = evt.graphic.attributes.sourceData;

                function showPopup() {
                    helpers.showPinPopup(popup, map, graphic, countryPinsLayer, sourceData,
                        countryGeonameId => {
                            // on popup show
                            addCountryOutline(countryGeonameId);
                        }, eventId => {
                            // on popup row click
                            addDestinationAirportsForEvent(eventId);
                            dimLayers(true);
                        }, () => {
                            // on popup back click
                            removeAllDestinationAirportPoints();
                            dimLayers(false);
                        }, () => {
                            // on popup close
                            removeAllCountryOutlines();
                            removeAllDestinationAirportPoints();
                            dimLayers(false);
                        }
                    );
                }

                function dimLayers(isDim) {
                    if (isDim) {
                        countryPinsLayer.setOpacity(0.25);
                        map.getLayer(map.layerIds[0]).setOpacity(0.25);
                    }
                    else {
                        countryPinsLayer.setOpacity(1);
                        map.getLayer(map.layerIds[0]).setOpacity(1);
                    }
                }

                removeAllCountryOutlines();
                removeAllDestinationAirportPoints();
                dimLayers(false);

                if ($(".esriPopup").hasClass("esriPopupHidden")) {
                    showPopup();
                }
                else {
                    const hideEvent = popup.on("hide", function () {
                        showPopup();
                        hideEvent.remove();
                    });
                    popup.hide();
                }

                window.biod.map.gaEvent('CLICK_MAP_PIN', sourceData.CountryName);
            });

            const layerAddResultEventHandler = map.on("layer-add-result", function (results) {
                if (results.layer.id === "countryPinsLayer") {
                    const countriesAndEvents = getCountriesAndEvents();
                    addCountryPins(helpers.groupEventsByCountries(countriesAndEvents));
                    layerAddResultEventHandler.remove(); // after the event handler gets fired, remove it
                }
            });

            map.addLayer(countryPinsLayer);
        }

        function addCountryOutline(geonameId) {
            function addCountryData(input) {
                const features = [];
                const attr = {};
                attr["sourceData"] = { "GeonameId": input.GeonameId };

                const polygonJson = {
                    "rings": input.Shape,
                    "spatialReference": { "wkid": 4326 }
                };

                const geometry = new Polygon(polygonJson);
                const graphic = new Graphic(geometry);
                graphic.setAttributes(attr);
                features.push(graphic);
                countryOutlineLayer.applyEdits(features, null, countryOutlineLayer.graphics);//3rd parameter is fail proof way to remove the previous graphic
            }

            if (geonameId) {
                mapApi.getCountryShape(geonameId).then(({ data }) => {
                    let retVal = null;
                    if (data.length) {
                        // MULTIPOLYGON
                        if (data.substring(0, 4).toLowerCase() === "mult") {
                            retVal = helpers.parseCountryShape(
                                data.substring(15, data.length - 2).split("), ("),
                                function (val) {
                                    return val.replace(/\(|\)/g, "").split(", ");
                                }
                            );
                        }
                        else {
                            // POLYGON 
                            retVal = helpers.parseCountryShape(
                                data.substring(10, data.length - 2).split("), ("),
                                function (val) {
                                    return val.split(", ");
                                }
                            );
                        }
                        addCountryData({ "GeonameId": geonameId, "Shape": retVal });
                    }
                });
            }
        }

        function removeAllCountryOutlines() {
            if (countryOutlineLayer.graphics && countryOutlineLayer.graphics.length) {
                countryOutlineLayer.applyEdits(null, null, countryOutlineLayer.graphics);
            }
        }

        function initCountryOutlineLayer() {
            countryOutlineLayer = new FeatureLayer(featureCountryPolygonCollection, {
                id: 'countryOutlineLayer',
                outFields: ["*"]
            });
            map.addLayer(countryOutlineLayer);
        }

        map = new Map("map-div", {
            center: [-46.807, 32.553],
            zoom: currentZoom,
            minZoom: 2,
            showLabels: true //very important that this must be set to true!
        });

        map.addLayers([basemap]);

        initCountryOutlineLayer();
        initCountryPinsLayer();
        initDestinationAirportPointsLayer();

        popup = new Popup({
            highlight: false,
            offsetY: -8,
            anchor: "top"
        }, domConstruct.create("div"));
        popup.resize(280, 210);
        domClass.add(popup.domNode, "light");
        map.infoWindow = popup;

        //hide the popup if its outside the map's extent
        map.on("pan-end", function (evt) {
            let loopEvt = null;
            function startRepositionLoop() {
                endRepositionLoop();
                loopEvt = setInterval(function () {
                    if (map.infoWindow.isShowing) {
                        map.infoWindow.reposition();
                    }
                    else {
                        endRepositionLoop();
                    }
                }, 5000);
            }

            function endRepositionLoop() {
                clearInterval(loopEvt)
            }

            window.biod.map.gaEvent('PAN_MAP');

            if (map.infoWindow.isShowing) {
                var xMin = map.geographicExtent.xmin;
                var xMax = map.geographicExtent.xmax;
                var x = map.infoWindow.location.x;

                if (xMin > x) {
                    var nX = x + 360;
                    if (xMax >= nX && xMin <= nX) {
                        map.infoWindow.location.x = nX;
                        map.infoWindow.reposition();
                    }
                }
                else if (xMax < x) {
                    var nX = x - 360;
                    if (xMax >= nX && xMin <= nX) {
                        map.infoWindow.location.x = nX;
                        map.infoWindow.reposition();
                    }
                }
            }

            startRepositionLoop();
        });
        map.on("zoom-end", function (e) {
            if (currentZoom < e.level) {
                window.biod.map.gaEvent('CLICK_ZOOM_IN', null, e.level);
            } else if (currentZoom > e.level) {
                window.biod.map.gaEvent('CLICK_ZOOM_OUT', null, e.level);
            }
            currentZoom = e.level;
        });

        window.biod.map.updatePoints = function (eventsMap, eventsInfo) {
            const countryArr = eventsMap.map(function (m) {
                return {
                    CountryGeonameId: m.CountryGeonameId,
                    CountryName: m.CountryName,
                    CountryPoint: m.CountryPoint
                };
            });

            const eventArr = [];
            const eventSet = new Set();
            eventsInfo.forEach(function (e) {
                // filter out duplicate events
                if (!eventSet.has(e.EventId)) {
                    eventArr.push({
                        EventId: e.EventId,
                        EventTitle: e.EventTitle,
                        Summary: e.Summary.replace(/\r?\n/, ' '),
                        CountryName: e.CountryName,
                        StartDate: e.StartDate,
                        EndDate: e.EndDate,
                        PriorityTitle: e.ExportationPriorityTitle
                    });
                    eventSet.add(e.EventId);
                }
            });

            const preParsedData = { countryArray: countryArr, eventArray: eventArr };
            const parsedData = helpers.groupEventsByCountries(preParsedData);

            addCountryPins(parsedData);
        };
    }
    );
}

export default {
    renderMap
};