window.initializeMap = (() => {
    let map;
    let routeLayerGroup;

    return (rawJson) => {
        const data = JSON.parse(rawJson);

        if (!data.successed) {
            return;
        }

        const routeData = JSON.parse(data.response);
        const points = routeData.routes[0].legs[0].points.map(x => [x.latitude, x.longitude]);

        if (!map) {
            map = L.map('map').setView([53.90004, 27.56675], 13);

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            }).addTo(map);

            routeLayerGroup = L.layerGroup().addTo(map); 
        }

        routeLayerGroup.clearLayers();

        if (points.length < 2) {
            return;
        }

        const polyline = L.polyline(points, { color: 'blue' });
        routeLayerGroup.addLayer(polyline);

        map.fitBounds(polyline.getBounds());

        const start = points[0];
        const end = points[points.length - 1];
        routeLayerGroup.addLayer(L.marker(start).bindPopup('Start Point').openPopup());
        routeLayerGroup.addLayer(L.marker(end).bindPopup('End Point'));
    };
})();
