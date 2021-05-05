
var map;
var markers = [];

// Initialize map
function initialize()
{
	var lat = window.external.GetLatitude();
	var long = window.external.GetLongitude();
	//var zoom = window.external.GetZoom();

	var mapOptions = {
		center: new google.maps.LatLng(lat, long),
		zoom: 12,
	    zoomControl: true,
	    mapTypeControl: true,
	    scaleControl: true,
	    streetViewControl: true,
	    rotateControl: true,
	    fullscreenControl: true
	};



	map = new google.maps.Map(document.getElementById('map-canvas'),  mapOptions);

	var marker = new google.maps.Marker
                    (
                        {
                            position: new google.maps.LatLng(lat, long),
                            map: map,
                            title: 'Clique aqui'
                        }
                    );
	markers.push(marker);

    // This event listener will call addMarker() when the map is clicked.
	map.addListener('click', function (event) {
	    addMarker(event.latLng);
	});

}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}


// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}

// Adds a marker to the map and push to the array.
function addMarker(location) {
    deleteMarkers();
    var marker = new google.maps.Marker({
        position: location,
        map: map,
        title: 'Local DLN'
    });

    markers.push(marker);
    window.external.PontoCriado(location.lat().toString(), location.lng().toString());
}



function getMapCenter(dummy)
{
	return map.getCenter().toString();
}

function getMapZoom(dummy) {
	return map.getZoom().toString();
}


function CenterMap(lat, lng)
{
	try
	{
		map.setCenter(new google.maps.LatLng(lat, lng));
	}
	catch (err)
	{
		alert(err.message);
	}
}


google.maps.event.addDomListener(window, 'load', initialize);
