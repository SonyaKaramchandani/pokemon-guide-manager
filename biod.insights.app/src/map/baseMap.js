export default {
  layers: [
    {
      paint: { 'background-color': '#cad2d3' },
      type: 'background',
      id: 'background',
      layout: {}
    },
    {
      layout: {},
      paint: { 'fill-color': '#EAEBEA' },
      source: 'esri',
      'source-layer': 'Land',
      type: 'fill',
      id: 'Land'
    },
    {
      layout: {},
      paint: { 'fill-color': '#dce2dc' },
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Urban area',
      maxzoom: 15,
      type: 'fill',
      id: 'Urban area'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#c0d1bb', 'fill-color': '#cdd9c9' },
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Openspace or forest',
      type: 'fill',
      id: 'Openspace or forest'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#cdd9c9', 'fill-color': '#dbe2d8' },
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Admin0 forest or park',
      type: 'fill',
      id: 'Admin0 forest or park'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#cdd9c9', 'fill-color': '#dbe2d8' },
      source: 'esri',
      minzoom: 8,
      'source-layer': 'Admin1 forest or park',
      type: 'fill',
      id: 'Admin1 forest or park'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cdd9c9' },
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Zoo',
      type: 'fill',
      id: 'Zoo'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Airport',
      type: 'fill',
      id: 'Airport/Airport property'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Airport',
      type: 'fill',
      id: 'Airport/Airport runway'
    },
    {
      layout: {},
      paint: { 'fill-color': '#ffffff' },
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Pedestrian',
      type: 'fill',
      id: 'Pedestrian'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cdd9c9' },
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Park or farming',
      type: 'fill',
      id: 'Park or farming'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Special area of interest/Sand',
        'fill-color': '#f6fff6'
      },
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Beach',
      type: 'fill',
      id: 'Beach'
    },
    {
      layout: { visibility: 'none' },
      paint: { 'fill-outline-color': '#b5c7b6', 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 12],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Garden path'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 15],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Parking'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cdd9c9' },
      filter: ['==', '_symbol', 11],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Green openspace'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cdd9c9' },
      filter: ['==', '_symbol', 8],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Grass'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Baseball field or other grounds'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Special area of interest/Groundcover',
        'fill-opacity': 0.5,
        'fill-color': '#c3d0c4'
      },
      filter: ['==', '_symbol', 13],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Groundcover'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 5],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Field or court exterior'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#b5c7b6', 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 4],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Football field or court'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#b5c7b6', 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 10],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Hardcourt'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 14],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Mulch or dirt'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#b5c7b6', 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Athletic track'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Special area of interest/Sand',
        'fill-color': '#c3d0c4'
      },
      filter: ['==', '_symbol', 6],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Sand'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Special area of interest/Rock or gravel',
        'fill-color': '#c3d0c4'
      },
      filter: ['==', '_symbol', 16],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Rock or gravel'
    },
    {
      layout: {},
      paint: { 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 2],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Bike, walk or pedestrian'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cad2d3' },
      filter: ['==', '_symbol', 7],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Water'
    },
    {
      layout: { 'line-join': 'round', visibility: 'none' },
      paint: { 'line-color': '#cad2d3', 'line-width': 0.5 },
      source: 'esri',
      minzoom: 1,
      'source-layer': 'Water line small scale',
      maxzoom: 5,
      type: 'line',
      id: 'Water line small scale'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-width': {
          base: 1.2,
          stops: [
            [5, 0.5],
            [7, 0.7]
          ]
        }
      },
      source: 'esri',
      minzoom: 6,
      'source-layer': 'Water line medium scale',
      maxzoom: 7,
      type: 'line',
      id: 'Water line medium scale'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-width': {
          base: 1.2,
          stops: [
            [7, 0.7],
            [11, 0.8]
          ]
        }
      },
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Water line large scale',
      maxzoom: 11,
      type: 'line',
      id: 'Water line large scale'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-dasharray': [5, 5],
        'line-width': 0.8
      },
      filter: ['==', '_symbol', 5],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line',
      type: 'line',
      id: 'Water line/Waterfall'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.7],
            [14, 0.7],
            [17, 2]
          ]
        }
      },
      filter: ['==', '_symbol', 2],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line',
      type: 'line',
      id: 'Water line/Dam or weir'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.7],
            [14, 0.7],
            [17, 2]
          ]
        }
      },
      filter: ['==', '_symbol', 3],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line',
      type: 'line',
      id: 'Water line/Levee/1'
    },
    {
      layout: {
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'icon-padding': 1,
        'icon-image': 'Water line/Levee/0',
        'icon-allow-overlap': true,
        'icon-rotation-alignment': 'map',
        'symbol-spacing': 15
      },
      paint: { 'text-color': '#707070', 'text-halo-color': '#fdfdfd' },
      filter: ['==', '_symbol', 3],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Water line',
      type: 'symbol',
      id: 'Water line/Levee/0'
    },
    {
      layout: { 'line-cap': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.8],
            [14, 0.8],
            [17, 2]
          ]
        }
      },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line',
      type: 'line',
      id: 'Water line/Canal or ditch'
    },
    {
      layout: {},
      paint: {
        'line-color': '#cad2d3',
        'line-dasharray': [7, 3],
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.8],
            [14, 0.8],
            [17, 2]
          ]
        }
      },
      filter: ['==', '_symbol', 4],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line',
      type: 'line',
      id: 'Water line/Stream or river intermittent'
    },
    {
      layout: { 'line-cap': 'round' },
      paint: {
        'line-color': '#cad2d3',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.8],
            [14, 0.8],
            [17, 2]
          ]
        }
      },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line',
      type: 'line',
      id: 'Water line/Stream or river'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cad2d3' },
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Marine area',
      type: 'fill',
      id: 'Marine area'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cad2d3' },
      source: 'esri',
      minzoom: 1,
      'source-layer': 'Water area small scale',
      maxzoom: 5,
      type: 'fill',
      id: 'Water area small scale'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Water area/Lake or river intermittent',
        'fill-color': '#cad2d3'
      },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Water area medium scale',
      maxzoom: 7,
      type: 'fill',
      id: 'Water area medium scale/Lake intermittent'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cad2d3' },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Water area medium scale',
      maxzoom: 7,
      type: 'fill',
      id: 'Water area medium scale/Lake or river'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Water area/Lake or river intermittent',
        'fill-color': '#cad2d3'
      },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Water area large scale',
      maxzoom: 11,
      type: 'fill',
      id: 'Water area large scale/Lake intermittent'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cad2d3' },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Water area large scale',
      maxzoom: 11,
      type: 'fill',
      id: 'Water area large scale/Lake or river'
    },
    {
      layout: {},
      paint: { 'fill-color': '#cad2d3' },
      filter: ['==', '_symbol', 7],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area',
      type: 'fill',
      id: 'Water area/Lake, river or bay'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Water area/Lake or river intermittent',
        'fill-color': '#cad2d3'
      },
      filter: ['==', '_symbol', 6],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area',
      type: 'fill',
      id: 'Water area/Lake or river intermittent'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Water area/Inundated area',
        'fill-color': '#cad2d3'
      },
      filter: ['==', '_symbol', 4],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area',
      type: 'fill',
      id: 'Water area/Inundated area'
    },
    {
      layout: {},
      paint: {
        'fill-pattern': 'Water area/Swamp or marsh',
        'fill-color': '#cad2d3'
      },
      filter: ['==', '_symbol', 3],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area',
      type: 'fill',
      id: 'Water area/Swamp or marsh'
    },
    {
      layout: {},
      paint: { 'fill-pattern': 'Water area/Playa', 'fill-color': '#cad2d3' },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area',
      type: 'fill',
      id: 'Water area/Playa'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#cad2d3', 'fill-color': '#cad2d3' },
      filter: ['==', '_symbol', 5],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area',
      type: 'fill',
      id: 'Water area/Dam or weir'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-width': {
          base: 1.2,
          stops: [
            [12, 1.5],
            [14, 2.5],
            [17, 3]
          ]
        }
      },
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Railroad',
      type: 'line',
      id: 'Railroad/2'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-width': {
          base: 1.2,
          stops: [
            [12, 0.5],
            [14, 1],
            [17, 1.5]
          ]
        }
      },
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Railroad',
      type: 'line',
      id: 'Railroad/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#dee6e7',
        'line-width': {
          base: 1.2,
          stops: [
            [12, 1.5],
            [14, 2.5],
            [17, 3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Ferry',
      type: 'line',
      id: 'Ferry/Rail ferry/2'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#dee6e7',
        'line-width': {
          base: 1.2,
          stops: [
            [12, 0.5],
            [14, 1],
            [17, 1.5]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Ferry',
      type: 'line',
      id: 'Ferry/Rail ferry/1'
    },
    {
      layout: {},
      paint: {
        'fill-translate': {
          stops: [
            [15, [0, 0]],
            [18, [2, 2]]
          ]
        },
        'fill-translate-anchor': 'viewport',
        'fill-color': '#b9b9ba'
      },
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Building',
      type: 'fill',
      id: 'Building/Shadow'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#dedfe0', 'fill-color': '#dedfe0' },
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Building',
      type: 'fill',
      id: 'Building'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-width': {
          base: 1.2,
          stops: [
            [15, 0.7],
            [17, 1.2]
          ]
        }
      },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Special area of interest line',
      type: 'line',
      id: 'Special area of interest line/Dock or pier'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-width': {
          base: 1.2,
          stops: [
            [15, 0.7],
            [17, 1.2]
          ]
        }
      },
      filter: ['==', '_symbol', 5],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Special area of interest line',
      type: 'line',
      id: 'Special area of interest line/Parking lot'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-width': {
          base: 1.2,
          stops: [
            [15, 0.7],
            [17, 1.2]
          ]
        }
      },
      filter: ['==', '_symbol', 6],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Special area of interest line',
      type: 'line',
      id: 'Special area of interest line/Sports field'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-width': {
          base: 1.2,
          stops: [
            [14, 1.5],
            [16, 3.3],
            [18, 4]
          ]
        }
      },
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Trail or path',
      type: 'line',
      id: 'Trail or path/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-dasharray': [2, 1],
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1.5],
            [14, 3.3],
            [18, 8.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 10], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/4WD/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1.5],
            [14, 3.3],
            [18, 8.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 8], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Service/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.4,
          stops: [
            [11, 1.5],
            [14, 4],
            [16, 6],
            [18, 17.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Local/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [14, 1.5],
            [16, 3.3],
            [18, 4]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 9], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Pedestrian/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1],
            [14, 4],
            [16, 9.6],
            [18, 17.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 6], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Minor, ramp or traffic circle/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 2.6],
            [14, 5.6],
            [16, 9.6],
            [18, 17.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Minor/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [9, 1.5],
            [14, 7.3],
            [16, 10.3],
            [18, 18]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Major, ramp or traffic circle/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1,
          stops: [
            [9, 1.5],
            [14, 7.3],
            [16, 10.3],
            [18, 18]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Major/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1,
          stops: [
            [9, 0.3],
            [14, 8.3],
            [16, 12.3],
            [18, 26]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Freeway Motorway, ramp or traffic circle/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [8, 0.3],
            [14, 8.3],
            [16, 12.3],
            [18, 26]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 8,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Highway/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#cacbca',
        'line-width': {
          base: 1.2,
          stops: [
            [5, 0.3],
            [14, 8.3],
            [16, 12.3],
            [18, 26]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Freeway Motorway/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#ffffff',
        'line-dasharray': [3, 1.5],
        'line-width': {
          base: 1.2,
          stops: [
            [14, 1.3],
            [16, 2],
            [18, 2.3]
          ]
        }
      },
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Trail or path',
      type: 'line',
      id: 'Trail or path/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-dasharray': [3, 1.5],
        'line-width': {
          base: 1.2,
          stops: [
            [14, 1.3],
            [16, 2],
            [18, 2.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 9], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Pedestrian/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.75],
            [14, 1.3],
            [18, 6.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 10], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/4WD/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.75],
            [14, 1.3],
            [18, 6.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 8], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Service/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.4,
          stops: [
            [11, 1.1],
            [14, 2],
            [16, 4],
            [18, 15.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Local/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.75],
            [14, 2],
            [16, 7.65],
            [18, 15.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 6], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Minor, ramp or traffic circle/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1.3],
            [14, 3.65],
            [16, 7.65],
            [18, 15.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Minor/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.75],
            [14, 5.3],
            [16, 8.3],
            [18, 16]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Major, ramp or traffic circle/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.75],
            [14, 5.3],
            [16, 8.3],
            [18, 16]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Major/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.3],
            [14, 6.3],
            [16, 10.3],
            [18, 24]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Freeway Motorway, ramp or traffic circle/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [8, 0.3],
            [14, 6.3],
            [16, 10.3],
            [18, 24]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 8,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Highway/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-width': {
          base: 1.2,
          stops: [
            [5, 0.3],
            [14, 6.3],
            [16, 10.3],
            [18, 24]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 6,
      'source-layer': 'Road',
      type: 'line',
      id: 'Road/Freeway Motorway/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-dasharray': [2, 1],
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1.5],
            [14, 3.3],
            [18, 8.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 10], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/4WD/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1.5],
            [14, 3.3],
            [18, 8.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 8], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Service/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.4,
          stops: [
            [11, 1.5],
            [14, 4],
            [16, 6],
            [18, 17.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Local/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [14, 1.5],
            [16, 3.3],
            [18, 4]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 9], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Pedestrian/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1],
            [14, 4],
            [16, 9.65],
            [18, 17.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 6], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Minor, ramp or traffic circle/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 2.6],
            [14, 5.65],
            [16, 9.65],
            [18, 17.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Minor/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [9, 1.5],
            [14, 7.3],
            [16, 10.3],
            [18, 18]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Major, ramp or traffic circle/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1,
          stops: [
            [9, 1.5],
            [14, 7.3],
            [16, 10.3],
            [18, 18]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Major/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.3],
            [14, 8.3],
            [16, 14.3],
            [18, 28]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Freeway Motorway, ramp or traffic circle/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [8, 0.3],
            [14, 8.3],
            [16, 14.3],
            [18, 28]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 8,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Highway/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [5, 0.3],
            [14, 8.3],
            [16, 14.3],
            [18, 28]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Freeway Motorway/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-dasharray': [3, 1.5],
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [14, 1.3],
            [16, 2],
            [18, 2.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 9], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Pedestrian/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.75],
            [14, 1.3],
            [18, 6.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 10], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/4WD/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.75],
            [14, 1.3],
            [18, 6.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 8], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Service/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.4,
          stops: [
            [11, 1.1],
            [14, 2],
            [16, 4],
            [18, 15.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 12,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Local/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 0.75],
            [14, 2],
            [16, 7.65],
            [18, 15.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 6], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Minor, ramp or traffic circle/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [11, 1.3],
            [14, 3.65],
            [16, 7.65],
            [18, 15.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Minor/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.75],
            [14, 5.3],
            [16, 8.3],
            [18, 16]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Major, ramp or traffic circle/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.75],
            [14, 5.3],
            [16, 8.3],
            [18, 16]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Major/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [9, 0.3],
            [14, 6.3],
            [16, 12.3],
            [18, 26]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Freeway Motorway, ramp or traffic circle/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [8, 0.3],
            [14, 6.3],
            [16, 12.3],
            [18, 26]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 8,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Highway/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#f0f1f0',
        'line-opacity': 0.5,
        'line-width': {
          base: 1.2,
          stops: [
            [5, 0.3],
            [14, 6.3],
            [16, 12.3],
            [18, 26]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 6,
      'source-layer': 'Road tunnel',
      type: 'line',
      id: 'Road tunnel/Freeway Motorway/0'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#b5c7b6', 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 9],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Gutter'
    },
    {
      layout: {},
      paint: { 'fill-outline-color': '#b5c7b6', 'fill-color': '#c3d0c4' },
      filter: ['==', '_symbol', 3],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Special area of interest',
      type: 'fill',
      id: 'Special area of interest/Curb'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-opacity': 0.95,
        'line-width': {
          base: 1,
          stops: [
            [4, 0.65],
            [14, 7],
            [17, 7]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 8], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Disputed admin2/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-dasharray': [7, 5],
        'line-width': {
          base: 1.2,
          stops: [
            [1, 0.65],
            [14, 1.3],
            [17, 2.65]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 8], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Disputed admin2/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-opacity': 0.95,
        'line-width': {
          base: 1,
          stops: [
            [4, 0.65],
            [14, 7]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Disputed admin1/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-opacity': 0.95,
        'line-width': {
          base: 1,
          stops: [
            [1, 0.65],
            [14, 9.3]
          ]
        }
      },
      filter: [
        'all',
        ['==', '_symbol', 6],
        ['!in', 'Viz', 3],
        ['!in', 'DisputeID', 8, 16, 90, 96, 0]
      ],
      source: 'esri',
      minzoom: 1,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Disputed admin0/1'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-dasharray': [7, 5],
        'line-width': {
          base: 1.2,
          stops: [
            [1, 0.65],
            [14, 1.3],
            [17, 2.65]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Disputed admin1/0'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-dasharray': [7, 5],
        'line-width': {
          base: 1.2,
          stops: [
            [1, 0.65],
            [14, 1.3],
            [17, 2.65]
          ]
        }
      },
      filter: [
        'all',
        ['==', '_symbol', 6],
        ['!in', 'Viz', 3],
        ['!in', 'DisputeID', 8, 16, 90, 96, 0]
      ],
      source: 'esri',
      minzoom: 1,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Disputed admin0/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-opacity': 0.6,
        'line-width': {
          base: 1.2,
          stops: [
            [8, 1.3],
            [14, 2.65]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin2/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-width': {
          base: 1,
          stops: [
            [4, 0.65],
            [14, 7]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin1/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-width': {
          base: 1,
          stops: [
            [1, 0.65],
            [14, 9.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 1,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin0/1'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: { 'line-color': '#d7d7d6', 'line-dasharray': [5, 3] },
      filter: ['all', ['==', '_symbol', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin5'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: { 'line-color': '#d7d7d6', 'line-dasharray': [5, 3] },
      filter: ['all', ['==', '_symbol', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin4'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: { 'line-color': '#d7d7d6', 'line-dasharray': [5, 3] },
      filter: ['all', ['==', '_symbol', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin3'
    },
    {
      layout: { 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-dasharray': [6, 4],
        'line-width': {
          base: 1.2,
          stops: [
            [8, 0.5],
            [14, 1]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin2/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-dasharray': [7, 5.3],
        'line-width': {
          base: 1,
          stops: [
            [7, 0.3],
            [14, 1.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin1/0'
    },
    {
      layout: { 'line-cap': 'round', 'line-join': 'round' },
      paint: {
        'line-color': '#d7d7d6',
        'line-dasharray': [7, 5.3],
        'line-width': {
          base: 1.2,
          stops: [
            [5, 0.7],
            [14, 1.3]
          ]
        }
      },
      filter: ['all', ['==', '_symbol', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Boundary line',
      type: 'line',
      id: 'Boundary line/Admin0/0'
    },
    {
      layout: {
        'text-letter-spacing': 0.3,
        'symbol-avoid-edges': true,
        'text-line-height': 1.6,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-padding': 15,
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 15.5]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Sea or ocean'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 7],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Island'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.7,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 5],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Dam or weir'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.7,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 6],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Playa'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 4],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Canal or ditch'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 3],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Stream or river'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 2],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Lake or reservoir'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Water point',
      type: 'symbol',
      id: 'Water point/Bay or inlet'
    },
    {
      layout: {
        'text-letter-spacing': 0.07,
        'symbol-avoid-edges': true,
        'text-max-angle': 18,
        'symbol-placement': 'line',
        'text-padding': 1,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-offset': [0, -0.5],
        'text-size': 9,
        'text-max-width': 6,
        'symbol-spacing': 800
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water line/label',
      type: 'symbol',
      id: 'Water line/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: { 'text-color': '#707070', 'text-halo-color': '#fdfdfd' },
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Marine park/label',
      type: 'symbol',
      id: 'Marine park/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 8],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Dam or weir'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 9],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Playa'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 5,
        'symbol-spacing': 1000
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 2],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Canal or ditch'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 8,
        'symbol-spacing': 1000
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 7],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Small river'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 10,
        'text-max-width': 8,
        'symbol-spacing': 1000
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 4],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Large river'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 6],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Small lake or reservoir'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'text-line-height': 1.15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-padding': 15,
        'text-size': 10,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 3],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Large lake or reservoir'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'text-line-height': 1.15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-padding': 15,
        'text-size': 11,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Bay or inlet'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Small island'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 10,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 5],
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Water area/label',
      type: 'symbol',
      id: 'Water area/label/Large island'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name}',
        'text-size': 9,
        'text-max-width': 4,
        'symbol-spacing': 1000
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Water area large scale/label',
      maxzoom: 11,
      type: 'symbol',
      id: 'Water area large scale/label/River'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name}',
        'text-size': 9.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 7,
      'source-layer': 'Water area large scale/label',
      maxzoom: 11,
      type: 'symbol',
      id: 'Water area large scale/label/Lake or lake intermittent'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name}',
        'text-size': 9,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Water area medium scale/label',
      maxzoom: 7,
      type: 'symbol',
      id: 'Water area medium scale/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name}',
        'text-size': 9,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      source: 'esri',
      minzoom: 1,
      'source-layer': 'Water area small scale/label',
      maxzoom: 5,
      type: 'symbol',
      id: 'Water area small scale/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-field': '{_name_global}',
        'text-size': 10,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      source: 'esri',
      minzoom: 11,
      'source-layer': 'Marine area/label',
      type: 'symbol',
      id: 'Marine area/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.12,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-size': {
          stops: [
            [1, 9],
            [6, 11]
          ]
        },
        'text-field': '{_name}',
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 4],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Marine waterbody/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Marine waterbody/label/small'
    },
    {
      layout: {
        'text-letter-spacing': 0.15,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-size': {
          stops: [
            [1, 9],
            [6, 11]
          ]
        },
        'text-field': '{_name}',
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 3],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Marine waterbody/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Marine waterbody/label/medium'
    },
    {
      layout: {
        'text-letter-spacing': 0.18,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-size': {
          stops: [
            [1, 9],
            [6, 12]
          ]
        },
        'text-field': '{_name}',
        'text-line-height': 1.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 2],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Marine waterbody/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Marine waterbody/label/large'
    },
    {
      layout: {
        'text-letter-spacing': 0.2,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-size': {
          stops: [
            [1, 10],
            [6, 13]
          ]
        },
        'text-field': '{_name}',
        'text-line-height': 1.5,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 3,
      'source-layer': 'Marine waterbody/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Marine waterbody/label/x large'
    },
    {
      layout: {
        'text-letter-spacing': 0.3,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Italic'],
        'text-size': {
          stops: [
            [1, 11],
            [4, 14]
          ]
        },
        'text-field': '{_name}',
        'text-line-height': 1.6,
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.5,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Marine waterbody/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Marine waterbody/label/2x large'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-offset': [0, -0.6],
        'text-size': 8.5,
        'text-max-width': 6,
        'symbol-spacing': 1000
      },
      paint: {
        'text-color': '#707070',
        'text-halo-width': 1,
        'text-halo-color': '#fdfdfd'
      },
      filter: ['all', ['==', '_label_class', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Ferry/label',
      type: 'symbol',
      id: 'Ferry/label/Rail ferry'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-offset': [0, -0.6],
        'text-size': 8.5,
        'text-max-width': 6,
        'symbol-spacing': 1000
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Railroad/label',
      type: 'symbol',
      id: 'Railroad/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.3,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 6], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Pedestrian'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Local, service, 4WD'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Minor'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Major, alt name'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Major'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 7], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Highway'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Freeway Motorway, alt name'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road tunnel/label',
      type: 'symbol',
      id: 'Road tunnel/label/Freeway Motorway'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#707070',
        'text-halo-width': 0.7,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Building/label',
      type: 'symbol',
      id: 'Building/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.3,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Trail or path/label',
      type: 'symbol',
      id: 'Trail or path/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.3,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 6], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Pedestrian'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 5], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Local'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 4], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Minor'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 3], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Major, alt name'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 2], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Major'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-padding': 10,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 75], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Highway'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 1], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Freeway Motorway, alt name'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'symbol-placement': 'line',
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 10.5,
        'text-max-width': 6,
        'symbol-spacing': 400
      },
      paint: {
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 0], ['!in', 'Viz', 3]],
      source: 'esri',
      minzoom: 13,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Freeway Motorway'
    },
    {
      layout: {
        'symbol-avoid-edges': true,
        'text-max-angle': 25,
        'symbol-placement': 'line',
        'text-padding': 30,
        'text-font': ['Ubuntu Regular'],
        'icon-image': 'Road/Rectangle white black (Alt)/{_len}',
        'text-field': '{_name}',
        'icon-rotation-alignment': 'viewport',
        'text-offset': [0, 0.2],
        'text-rotation-alignment': 'viewport',
        'text-size': 8.5,
        'text-max-width': 6,
        'symbol-spacing': 800
      },
      paint: { 'text-color': '#727272', 'text-halo-color': '#ffffff' },
      filter: [
        'all',
        [
          'in',
          '_label_class',
          8,
          10,
          12,
          14,
          16,
          20,
          22,
          24,
          26,
          28,
          30,
          32,
          34,
          36,
          38,
          40,
          42,
          44,
          46,
          48,
          50,
          52,
          54,
          56,
          58,
          60,
          62,
          64,
          66,
          68,
          70,
          72,
          74
        ],
        ['!in', 'Viz', 3]
      ],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Rectangle white black (Alt)'
    },
    {
      layout: {
        'symbol-avoid-edges': true,
        'text-max-angle': 25,
        'symbol-placement': 'line',
        'text-padding': 30,
        'text-font': ['Ubuntu Regular'],
        'icon-image': 'Road/Rectangle white black/{_len}',
        'text-field': '{_name}',
        'icon-rotation-alignment': 'viewport',
        'text-offset': [0, 0.2],
        'text-rotation-alignment': 'viewport',
        'text-size': 8.5,
        'text-max-width': 6,
        'symbol-spacing': 800
      },
      paint: { 'text-color': '#666666' },
      filter: [
        'all',
        [
          'in',
          '_label_class',
          7,
          9,
          11,
          13,
          15,
          17,
          19,
          21,
          23,
          25,
          27,
          29,
          31,
          33,
          35,
          37,
          39,
          41,
          43,
          45,
          47,
          49,
          51,
          53,
          55,
          57,
          59,
          61,
          63,
          65,
          67,
          69,
          71,
          73
        ],
        ['!in', 'Viz', 3]
      ],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Road/label',
      type: 'symbol',
      id: 'Road/label/Rectangle white black'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 16,
      'source-layer': 'Cemetery/label',
      type: 'symbol',
      id: 'Cemetery/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Freight/label',
      type: 'symbol',
      id: 'Freight/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Water and wastewater/label',
      type: 'symbol',
      id: 'Water and wastewater/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Port/label',
      type: 'symbol',
      id: 'Port/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Industry/label',
      type: 'symbol',
      id: 'Industry/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Government/label',
      type: 'symbol',
      id: 'Government/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Finance/label',
      type: 'symbol',
      id: 'Finance/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Emergency/label',
      type: 'symbol',
      id: 'Emergency/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Indigenous/label',
      type: 'symbol',
      id: 'Indigenous/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 25,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Military/label',
      type: 'symbol',
      id: 'Military/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Transportation/label',
      type: 'symbol',
      id: 'Transportation/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#727272',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Pedestrian/label',
      type: 'symbol',
      id: 'Pedestrian/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Beach/label',
      type: 'symbol',
      id: 'Beach/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Golf course/label',
      type: 'symbol',
      id: 'Golf course/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Zoo/label',
      type: 'symbol',
      id: 'Zoo/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Retail/label',
      type: 'symbol',
      id: 'Retail/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Landmark/label',
      type: 'symbol',
      id: 'Landmark/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'text-padding': 25,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Openspace or forest/label',
      type: 'symbol',
      id: 'Openspace or forest/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'text-padding': 25,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Park or farming/label',
      type: 'symbol',
      id: 'Park or farming/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 14,
      'source-layer': 'Point of interest',
      type: 'symbol',
      id: 'Point of interest/Park'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Education/label',
      type: 'symbol',
      id: 'Education/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Medical/label',
      type: 'symbol',
      id: 'Medical/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'text-padding': 25,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [12, 9.5]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Admin1 forest or park/label',
      type: 'symbol',
      id: 'Admin1 forest or park/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 25,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [12, 9.5]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Admin0 forest or park/label',
      type: 'symbol',
      id: 'Admin0 forest or park/label/Default'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [9, 8.5],
            [12, 9.5]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Airport/label',
      type: 'symbol',
      id: 'Airport/label/Airport property'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': 0.2,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': 9,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Admin2 area/label',
      maxzoom: 11,
      type: 'symbol',
      id: 'Admin2 area/label/small'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': 0.2,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 9,
      'source-layer': 'Admin2 area/label',
      maxzoom: 11,
      type: 'symbol',
      id: 'Admin2 area/label/large'
    },
    {
      layout: {
        'text-letter-spacing': {
          stops: [
            [4, 0.1],
            [8, 0.18]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-size': {
          stops: [
            [4, 9],
            [5, 9],
            [6, 9.5],
            [9, 10]
          ]
        },
        'text-field': '{_name}',
        'text-transform': 'uppercase',
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 5],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin1 area/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin1 area/label/x small'
    },
    {
      layout: {
        'text-letter-spacing': {
          stops: [
            [4, 0.1],
            [8, 0.18]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-size': {
          stops: [
            [4, 9],
            [5, 9],
            [6, 9.5],
            [9, 11.5]
          ]
        },
        'text-field': '{_name}',
        'text-transform': 'uppercase',
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 4],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin1 area/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin1 area/label/small'
    },
    {
      layout: {
        'text-letter-spacing': {
          stops: [
            [4, 0.1],
            [8, 0.18]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-size': {
          stops: [
            [4, 10],
            [5, 10],
            [6, 10.5],
            [9, 12]
          ]
        },
        'text-field': '{_name}',
        'text-transform': 'uppercase',
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 3],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin1 area/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin1 area/label/medium'
    },
    {
      layout: {
        'text-letter-spacing': {
          stops: [
            [4, 0.1],
            [8, 0.2]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-size': {
          stops: [
            [4, 10],
            [5, 10.5],
            [6, 11],
            [9, 15]
          ]
        },
        'text-field': '{_name}',
        'text-transform': 'uppercase',
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 2],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin1 area/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin1 area/label/large'
    },
    {
      layout: {
        'text-letter-spacing': {
          stops: [
            [4, 0.13],
            [8, 0.3]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-size': {
          stops: [
            [4, 10.5],
            [5, 11],
            [6, 11.5],
            [9, 17]
          ]
        },
        'text-field': '{_name}',
        'text-transform': 'uppercase',
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin1 area/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin1 area/label/x large'
    },
    {
      layout: {
        'text-size': {
          stops: [
            [4, 11],
            [5, 11.5],
            [6, 12],
            [9, 17.5]
          ]
        },
        'text-font': ['Ubuntu Regular'],
        'text-max-width': 6,
        'text-padding': 1,
        'text-letter-spacing': {
          stops: [
            [4, 0.13],
            [8, 0.4]
          ]
        },
        'symbol-avoid-edges': true,
        'text-field': '{_name}'
      },
      paint: {
        'text-color': '#434343',
        'text-halo-color': '#ffffff',
        'text-halo-width': 1,
        'text-halo-blur': 1
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin1 area/label',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin1 area/label/2x large'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': 9,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Point of interest',
      type: 'symbol',
      id: 'Point of interest/General'
    },
    {
      layout: {
        'text-letter-spacing': 0.04,
        'symbol-avoid-edges': true,
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-offset': [0, -0.9],
        'text-size': 9,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 2],
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Point of interest',
      type: 'symbol',
      id: 'Point of interest/Bus station'
    },
    {
      layout: {
        'text-letter-spacing': 0.04,
        'symbol-avoid-edges': true,
        'text-padding': 5,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-offset': [0, -0.9],
        'text-size': 9,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 3],
      source: 'esri',
      minzoom: 17,
      'source-layer': 'Point of interest',
      type: 'symbol',
      id: 'Point of interest/Rail station'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 10],
            [16, 13]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      source: 'esri',
      minzoom: 15,
      'source-layer': 'Neighborhood',
      maxzoom: 17,
      type: 'symbol',
      id: 'Neighborhood'
    },
    {
      layout: {
        'text-letter-spacing': 0.08,
        'symbol-avoid-edges': true,
        'text-padding': 15,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 10],
            [16, 13]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 5],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'City large scale',
      maxzoom: 17,
      type: 'symbol',
      id: 'City large scale/town small'
    },
    {
      layout: {
        'text-letter-spacing': 0.09,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 10],
            [16, 15]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 4],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'City large scale',
      maxzoom: 17,
      type: 'symbol',
      id: 'City large scale/town large'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 11],
            [16, 16]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 3],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'City large scale',
      maxzoom: 17,
      type: 'symbol',
      id: 'City large scale/small'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 11],
            [16, 18.5]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 2],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'City large scale',
      maxzoom: 17,
      type: 'symbol',
      id: 'City large scale/medium'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 14],
            [16, 22]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'City large scale',
      maxzoom: 17,
      type: 'symbol',
      id: 'City large scale/large'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name_global}',
        'text-size': {
          stops: [
            [10, 15],
            [16, 25]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 10,
      'source-layer': 'City large scale',
      maxzoom: 17,
      type: 'symbol',
      id: 'City large scale/x large'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 17],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/town small non capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 15],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/town large non capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 12],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/small non capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 9],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/medium non capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 18],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/other capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 14],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/town large other capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 11],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/small other capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Light Regular'],
        'text-field': '{_name}',
        'text-size': 10,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 8],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/medium other capital'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': {
          stops: [
            [5, 0.13],
            [8, 0.5]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Bold'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [5, 10],
            [10, 12.5]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 5],
      source: 'esri',
      minzoom: 5,
      'source-layer': 'Admin0 point',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin0 point/x small'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': {
          stops: [
            [4, 0.13],
            [8, 0.5]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Bold'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [4, 10],
            [10, 12.5]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 4],
      source: 'esri',
      minzoom: 4,
      'source-layer': 'Admin0 point',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin0 point/small'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': {
          stops: [
            [2, 0.13],
            [8, 0.5]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Bold'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [2, 8],
            [4, 10],
            [10, 16]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 3],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Admin0 point',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin0 point/medium'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': {
          stops: [
            [2, 0.13],
            [8, 0.5]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Bold'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [2, 9],
            [4, 10],
            [6, 16]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 2],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Admin0 point',
      maxzoom: 10,
      type: 'symbol',
      id: 'Admin0 point/large'
    },
    {
      layout: {
        'text-transform': 'uppercase',
        'text-letter-spacing': {
          stops: [
            [2, 0.15],
            [6, 0.5]
          ]
        },
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Bold'],
        'text-size': {
          stops: [
            [2, 9],
            [4, 10],
            [6, 16]
          ]
        },
        'text-field': '{_name}',
        'text-line-height': 1.5,
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_label_class', 1],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Admin0 point',
      maxzoom: 8,
      type: 'symbol',
      id: 'Admin0 point/x large'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 10.5],
            [8, 13]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 16],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/town small admin0 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 10.5],
            [8, 13]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 13],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/town large admin0 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 10.5],
            [8, 13]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 10],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/small admin0 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 10.5],
            [8, 13]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 7],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/medium admin0 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 11],
            [8, 14]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 5],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/large other capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 11],
            [6, 12],
            [8, 15]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 2],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/x large admin2 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 11],
            [8, 14]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 6],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/large non capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 10],
            [6, 11],
            [8, 14]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 4],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/large admin0 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 11],
            [6, 12],
            [8, 15]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 3],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/x large non capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 11],
            [6, 12],
            [8, 15]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 1],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/x large admin1 capital'
    },
    {
      layout: {
        'text-letter-spacing': 0.05,
        'symbol-avoid-edges': true,
        'text-padding': 1,
        'text-font': ['Ubuntu Regular'],
        'text-field': '{_name}',
        'text-size': {
          stops: [
            [3, 11],
            [6, 12],
            [8, 15]
          ]
        },
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['==', '_symbol', 0],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'City small scale',
      maxzoom: 10,
      type: 'symbol',
      id: 'City small scale/x large admin0 capital'
    },
    {
      layout: {
        'text-size': {
          stops: [
            [2, 12],
            [4, 15.5],
            [5, 18]
          ]
        },
        'text-font': ['Ubuntu Medium', 'Arial Unicode MS Regular'],
        'text-max-width': 6,
        'text-line-height': 1.7,
        'text-padding': 1,
        'text-letter-spacing': {
          base: 1,
          stops: [
            [2, 0.25],
            [5, 0.25]
          ]
        },
        'symbol-avoid-edges': true,
        'text-field': '{_name}',
        'text-transform': 'uppercase'
      },
      paint: {
        'text-color': '#6d6d6d',
        'text-halo-color': '#ffffff',
        'text-halo-width': 1,
        'text-halo-blur': 1
      },
      filter: ['==', '_label_class', 0],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Admin0 point',
      maxzoom: 6,
      type: 'symbol',
      id: 'Admin0 point/2x large'
    },
    {
      layout: {
        'text-letter-spacing': 0.13,
        'icon-allow-overlap': true,
        'text-font': ['Ubuntu Italic'],
        'icon-image': 'Disputed label point',
        'text-field': '{_name}',
        'text-optional': true,
        'text-size': {
          stops: [
            [6, 7],
            [15, 10]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 1], ['in', 'DisputeID', 0]],
      source: 'esri',
      minzoom: 6,
      'source-layer': 'Disputed label point',
      type: 'symbol',
      id: 'Disputed label point/Island'
    },
    {
      layout: {
        'text-letter-spacing': 0.1,
        'icon-allow-overlap': true,
        'text-font': ['Ubuntu Italic'],
        'icon-image': 'Disputed label point',
        'text-field': '{_name}',
        'text-optional': true,
        'text-size': {
          stops: [
            [2, 8],
            [6, 9.3]
          ]
        },
        'text-max-width': 4
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 0.5,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 0], ['in', 'DisputeID', 1006]],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Disputed label point',
      maxzoom: 10,
      type: 'symbol',
      id: 'Disputed label point/Waterbody'
    },
    {
      layout: {
        'text-letter-spacing': {
          stops: [
            [2, 0.13],
            [8, 0.5]
          ]
        },
        'icon-allow-overlap': true,
        'text-size': {
          stops: [
            [2, 8],
            [4, 10],
            [10, 16]
          ]
        },
        'text-font': ['Ubuntu Bold'],
        'icon-image': 'Disputed label point',
        'text-field': '{_name}',
        'text-optional': true,
        'text-transform': 'uppercase',
        'text-max-width': 6
      },
      paint: {
        'text-halo-blur': 1,
        'text-color': '#6d6d6d',
        'text-halo-width': 1,
        'text-halo-color': '#ffffff'
      },
      filter: ['all', ['==', '_label_class', 2], ['in', 'DisputeID', 1021]],
      source: 'esri',
      minzoom: 2,
      'source-layer': 'Disputed label point',
      type: 'symbol',
      id: 'Disputed label point/Admin0'
    }
  ],
  glyphs:
    'https://basemaps.arcgis.com/arcgis/rest/services/World_Basemap_v2/VectorTileServer/resources/fonts/{fontstack}/{range}.pbf',
  version: 8,
  sprite:
    'https://www.arcgis.com/sharing/rest/content/items/8a2cba3b0ebf4140b7c0dc5ee149549a/resources/styles/../sprites/sprite',
  sources: {
    esri: {
      url: 'https://basemaps.arcgis.com/arcgis/rest/services/World_Basemap_v2/VectorTileServer',
      type: 'vector'
    }
  },
  metadata: {
    arcgisStyleUrl:
      'https://www.arcgis.com/sharing/rest/content/items/3ad38f72915f426bb93b64828ae78670/resources/styles/root.json',
    arcgisOriginalItemTitle: 'Zebra Map',
    arcgisQuickEditor: {
      road: '#f0f1f0',
      building: '#dedfe0',
      boundaries: '#d7d7d6',
      land: '#eaebea',
      water: '#CAD2D3',
      nature: '#e8ebe7',
      labelTextColor: '#757776',
      labelHaloColor: '#f0f0f0',
      labelContrast: 4.5,
      labelColorMode: 'LABELS_MAP_COLORS'
    },
    arcgisQuickEditorWarning: true,
    arcgisEditorExtents: [
      {
        spatialReference: { wkid: 102100 },
        xmin: -16901953.51899815,
        ymin: -8456158.57935053,
        xmax: 23799235.302281942,
        ymax: 16355912.29823753
      },
      {
        spatialReference: { latestWkid: 3857, wkid: 102100 },
        xmin: 125437.3212165486,
        ymin: 6763811.340621513,
        xmax: 335792.0230573432,
        ymax: 6905678.465118793
      },
      {
        spatialReference: { latestWkid: 3857, wkid: 102100 },
        xmin: -8710828.164641865,
        ymin: 4628032.37571665,
        xmax: -8500473.462801069,
        ymax: 4769899.500213929
      },
      {
        spatialReference: { latestWkid: 3857, wkid: 102100 },
        xmin: 16826607.849773474,
        ymin: -4016564.07874888,
        xmax: 16839755.018638507,
        ymax: -4007697.383467811
      }
    ],
    arcgisMinimapVisibility: true
  }
};
