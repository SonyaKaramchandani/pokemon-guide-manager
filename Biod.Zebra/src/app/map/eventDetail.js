import utils from './utils';
import mapApi from '../../api/mapApi';
import AirportLayer from "./airportLayer";

import {
    featureOutbreakPointCityCollection,
    featureOutbreakPointProvinceCollection,
    featureOutbreakPointCountryCollection,
    featureOutbreakPolygonCollection,
    outbreakPointLabelClassObjectLocation,
    outbreakPointLabelClassObjectCasesLeft,
    outbreakPointLabelClassObjectCasesCenter,
    outbreakPointLabelClassObjectDeaths,
} from './config';

let esriHelper = null;
let map = null;

let destinationAirportLayer = null;

let eventDetailPinsCityLayer = null;
let eventDetailPinsCountryLayer = null;
let eventDetailPinsProvinceLayer = null;

let eventDetailOutlineCountryLayer = null;
let eventDetailOutlineProvinceLayer = null;

let polygonFeaturesCountry = [];
let polygonFeaturesProvince = [];

let pointFeaturesCity = [];
let pointFeaturesCountry = [];
let pointFeaturesProvince = [];

function init({ esriHelper: _esriHelper, map: _map }) {
    esriHelper = _esriHelper;
    map = _map;

    eventDetailPinsCityLayer = new esriHelper.FeatureLayer(featureOutbreakPointCityCollection, {
        id: 'eventDetailPinsCityLayer',
        featureReduction: {
            type: "cluster"
        },
        infoTemplate: new esriHelper.PopupTemplate({
            title: "{location}",
            description: "{cases} Case(s), {deaths} Death(s)"
        }),
        outFields: ["*"]
    });

    eventDetailPinsCountryLayer = new esriHelper.FeatureLayer(featureOutbreakPointCountryCollection, {
        id: 'eventDetailPinsCountryLayer',
        outFields: ["*"]
    });

    eventDetailPinsProvinceLayer = new esriHelper.FeatureLayer(featureOutbreakPointProvinceCollection, {
        id: 'eventDetailPinsProvinceLayer',
        outFields: ["*"]
    });

    eventDetailOutlineCountryLayer = new esriHelper.FeatureLayer(featureOutbreakPolygonCollection, {
        id: 'eventDetailOutlineCountryLayer',
        outFields: ["*"]
    });

    eventDetailOutlineProvinceLayer = new esriHelper.FeatureLayer(featureOutbreakPolygonCollection, {
        id: 'eventDetailOutlineProvinceLayer',
        outFields: ["*"]
    });

    destinationAirportLayer = new AirportLayer(esriHelper);

    map.addLayer(eventDetailOutlineCountryLayer);
    map.addLayer(eventDetailOutlineProvinceLayer);
    map.addLayer(eventDetailPinsCountryLayer);
    map.addLayer(eventDetailPinsProvinceLayer);
    map.addLayer(eventDetailPinsCityLayer);
    destinationAirportLayer.initializeOnMap(map);

    const eventDetailPointLabelClassLocation = new esriHelper.LabelClass(outbreakPointLabelClassObjectLocation);
    const eventDetailPointLabelClassCasesLeft = new esriHelper.LabelClass(outbreakPointLabelClassObjectCasesLeft);
    const eventDetailPointLabelClassCasesCenter = new esriHelper.LabelClass(outbreakPointLabelClassObjectCasesCenter);
    const eventDetailPointLabelClassDeaths = new esriHelper.LabelClass(outbreakPointLabelClassObjectDeaths);
    const labelClasses = [eventDetailPointLabelClassLocation, eventDetailPointLabelClassCasesLeft, eventDetailPointLabelClassCasesCenter, eventDetailPointLabelClassDeaths];
    eventDetailPinsCityLayer.setLabelingInfo(labelClasses);
    eventDetailPinsProvinceLayer.setLabelingInfo(labelClasses);
    eventDetailPinsCountryLayer.setLabelingInfo(labelClasses);
}

function render({ EventCaseCounts, EventInfo, FilterParams }) {
    polygonFeaturesCountry = [];
    polygonFeaturesProvince = [];
    pointFeaturesCity = [];
    pointFeaturesProvince = [];
    pointFeaturesCountry = [];

    const geonameIds = EventCaseCounts.map(e => e.GeonameId);
    mapApi.getGeonameShapes(geonameIds)
        .then(({ data: shapes }) => {
            geonameIds.forEach((geonameId, index) => {
                const eventData = EventCaseCounts.find(evt => evt.GeonameId === geonameId);

                const _shape = shapes.find(s => s.GeonameId === geonameId)
                const { Longitude: x, Latitude: y } = _shape;
                const shapeData = _shape.Shape;

                let retVal = null;
                if (shapeData && shapeData.length) {
                    // MULTIPOLYGON
                    if (shapeData.substring(0, 4).toLowerCase() === "mult") {
                        retVal = utils.parseShape(
                            shapeData.substring(15, shapeData.length - 2).split("), ("),
                            function (val) {
                                return val.replace(/\(|\)/g, "").split(", ");
                            }
                        );
                        addOutline({ "GeonameId": geonameId, "Shape": retVal, eventData });
                    }
                    else if (shapeData.substring(0, 4).toLowerCase() === "poly") {
                        // POLYGON 
                        retVal = utils.parseShape(
                            shapeData.substring(10, shapeData.length - 2).split("), ("),
                            function (val) {
                                return val.split(", ");
                            }
                        );
                        addOutline({ "GeonameId": geonameId, "Shape": retVal, eventData });
                    }
                }
                
                addPoint({ "GeonameId": geonameId, x, y, eventData });
            });

            eventDetailOutlineCountryLayer.applyEdits(polygonFeaturesCountry, null, eventDetailOutlineCountryLayer.graphics);
            eventDetailOutlineProvinceLayer.applyEdits(polygonFeaturesProvince, null, eventDetailOutlineProvinceLayer.graphics);
            eventDetailPinsCityLayer.applyEdits(pointFeaturesCity, null, eventDetailPinsCityLayer.graphics);
            eventDetailPinsCountryLayer.applyEdits(pointFeaturesCountry, null, eventDetailPinsCountryLayer.graphics);
            eventDetailPinsProvinceLayer.applyEdits(pointFeaturesProvince, null, eventDetailPinsProvinceLayer.graphics);

            onZoomEnd({ level: map.getZoom() });
        });
    
    destinationAirportLayer.addAirportPoints(EventInfo.EventId);
}

function addOutline(input) {
    const attr = {};
    attr["sourceData"] = { "GeonameId": input.GeonameId };

    const polygonJson = {
        "rings": input.Shape,
        "spatialReference": { "wkid": 4326 }
    };

    const geometry = new esriHelper.Polygon(polygonJson);
    const graphic = new esriHelper.Graphic(geometry);
    graphic.setAttributes(attr);
    
    switch(input.eventData.LocationType) {
        case "Province/State":
            polygonFeaturesProvince.push(graphic);
            break;
        case "Country":
            polygonFeaturesCountry.push(graphic);
            break;
    }
}

function addPoint(input) {
    const attr = {};
    attr["sourceData"] = { "GeonameId": input.GeonameId };
    attr["cases"] = input.eventData.RepCases;
    attr["deaths"] = input.eventData.Deaths;
    attr["location"] = input.eventData.LocationName;

    const geometry = new esriHelper.Point(input);
    const graphic = new esriHelper.Graphic(geometry);
    graphic.setAttributes(attr);

    switch(input.eventData.LocationType) {
        case "City/Township":
            pointFeaturesCity.push(graphic);
            break;
        case "Province/State":
            pointFeaturesProvince.push(graphic);
            break;
        case "Country":
            pointFeaturesCountry.push(graphic);
            break;
    }
}

function setExtentToEventDetail() {
    const features = [...pointFeaturesCity, ...pointFeaturesProvince,, ...pointFeaturesCountry, ...polygonFeaturesCountry, ...polygonFeaturesProvince];
    let layerExtent = null;

    features.forEach(feature => {
        let extent = feature.geometry.getExtent() || new esriHelper.Extent(feature.geometry.x - 1,
                feature.geometry.y - 1,
                feature.geometry.x + 1,
                feature.geometry.y + 1,
                feature.geometry.SpatialReference);
        
        layerExtent = !!layerExtent ? layerExtent.union(extent) : extent;
    });

    layerExtent && map.setExtent(layerExtent, true);
}

function show() {
    map.getLayer('eventDetailOutlineCountryLayer').show();
    map.getLayer('eventDetailOutlineProvinceLayer').show();
    map.getLayer('eventDetailPinsCityLayer').show();
    map.getLayer('eventDetailPinsProvinceLayer').show();
    map.getLayer('eventDetailPinsCountryLayer').show();
}

function hide() {
    utils.clearLayer(eventDetailOutlineCountryLayer);
    utils.clearLayer(eventDetailOutlineProvinceLayer);
    utils.clearLayer(eventDetailPinsCityLayer);
    utils.clearLayer(eventDetailPinsProvinceLayer);
    utils.clearLayer(eventDetailPinsCountryLayer);
    destinationAirportLayer.clearAirportPoints();

    map.getLayer('eventDetailOutlineCountryLayer').hide();
    map.getLayer('eventDetailOutlineProvinceLayer').hide();
    map.getLayer('eventDetailPinsCityLayer').hide();
    map.getLayer('eventDetailPinsProvinceLayer').hide();
    map.getLayer('eventDetailPinsCountryLayer').hide();
}

function onZoomEnd({ level }) {
    // always show country
    map.getLayer('eventDetailOutlineCountryLayer').show();
    map.getLayer('eventDetailPinsCountryLayer').show();

    map.getLayer('eventDetailOutlineProvinceLayer').hide();    
    map.getLayer('eventDetailPinsCityLayer').hide();
    map.getLayer('eventDetailPinsProvinceLayer').hide();

    if (level >= 8) {
        map.getLayer('eventDetailPinsCityLayer').show();
        map.getLayer('eventDetailPinsProvinceLayer').show();
        map.getLayer('eventDetailOutlineProvinceLayer').show();
    }
    else if (level >= 6) {
        map.getLayer('eventDetailPinsProvinceLayer').show();
        map.getLayer('eventDetailPinsCountryLayer').show();
        map.getLayer('eventDetailOutlineProvinceLayer').show();
    }
}

export default {
    init,
    render,
    show,
    hide,
    setExtentToEventDetail,
    onZoomEnd
}
