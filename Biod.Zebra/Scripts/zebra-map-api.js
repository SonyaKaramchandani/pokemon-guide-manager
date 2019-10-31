(() => {
        function GetDestinationAirport(id, geonameIds) {
            return axios.get(window.biod.Urls.GetDestinationAirports, {
                params: {
                    'EventId': id,
                    'GeonameIds': geonameIds || '-1'
                }
            })
        }

        function GetCountryShape(id) {
            return axios.get(window.biod.Urls.GetCountryShapeAsText, {
                params: {
                    'GeonameId': id
                }
            });
        }

        window.biod = window.biod || {};
        window.biod.ZebraMapApi = window.biod.ZebraMapApi || {
            GetDestinationAirport,
            GetCountryShape
        };
    }
)();

