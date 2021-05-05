// Functions to control google maps
function initialize() {
	var mapOptions = {
		zoom: 4,
		center: new google.maps.LatLng(-34.397, 150.644)
	};

	var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

	var marker = new google.maps.Marker({
		position: map.getCenter(),
		map: map,
		title: 'Click to zoom'
	});

	//google.maps.event.addListener(map, 'center_changed', function() {
	//	// 3 seconds after the center of the map has changed, pan back to the
	//	// marker.
	//	window.setTimeout(function ()
	//	{
	//		map.panTo(marker.getPosition());
	//	}, 3000);
	//});


	//google.maps.event.addListener(map, 'click', showAlert);

	google.maps.event.addListener(map, 'click', function (event) {
		showAlert(event.latLng);
	});

}

function showPoint(point)
{
	alert(point);
}

function showAlert(location) {
	 //alert(location);

	window.external.Test(location)

	var mapOptions = {
		zoom: 4,
		center: new google.maps.LatLng(-34.397, 150.644)
	};

	var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

	var marker = new google.maps.Marker({
		position: location,
		map: map,
		title: 'Meu ponto'
	});


	if (marker) {
			marker.setPosition(location);
		}

}


//function placeMarker(location) {
//	if (marker) {
//		marker.setPosition(location);
//	} else {
//		marker = new google.maps.Marker({
//			position: location,
//			map: map,
//			title: 'My point',
//			draggable: true,
//		});
//		// IF I REMOVE THIS PART -> IT WORKS, BUT WITHOUT INFOWINDOW
//		google.maps.event.addListener(marker, 'click', function () {
//			infowindow.open(map, marker);
//		});
//	}
//}

google.maps.event.addDomListener(window, 'load', initialize);

