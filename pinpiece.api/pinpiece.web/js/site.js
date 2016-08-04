
$(document).ready(initialize);

function initialize() {

    var latlng = new google.maps.LatLng(-37.813611, 144.963056); // initial melbourne
    var howclose = 1000;
    //var endpoint = "http://localhost:698";
    var endpoint = "http://pinpieceapi.azurewebsites.net";

    var mapOptions = {
        zoom: 13,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map($('#map')[0], mapOptions);
    var infowindow = new google.maps.InfoWindow();
    var markersArray = [];
    var bounds = new google.maps.LatLngBounds();

    // current pin blue
    var pinColor = "0073e5";
    var pinImage = new google.maps.MarkerImage("http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|" + pinColor,
            null, /* size is determined at runtime */
            null, /* origin is 0,0 */
            null, /* anchor is bottom center of the scaled image */
            new google.maps.Size(31.5, 51)
        );

    var meMarker = new google.maps.Marker({
        position: latlng,
        map: map,
        draggable: true,
        icon: pinImage
    });
    // end current pin blue

    // drag - relocation
    google.maps.event.addListener(meMarker, "dragend", function (event) {
        bounds = new google.maps.LatLngBounds();
        var lat = event.latLng.lat();
        var lng = event.latLng.lng();
        toastr.success('Lat: ' + lat + ', Lng:' + lng);

        clearOverlays(); // clean markers

        var reload = {
            "UserId": 23232322,
            "Token": "cwancwancwancwan0",
            "Coord": {
                "lng": lng,
                "lat": lat
            }
        };

        $.post(endpoint + "/mongo/reload", reload, function (data) {

            if (data.length > 0) {
                for (i = 0; i < data.length; i++) {
                    addMarker(data[i]);
                }
                map.setCenter(bounds.getCenter());
                map.fitBounds(bounds);
                // map.setZoom(map.getZoom() - 1);
            }
          
        });

    });

    // initial screen 
    
    $.get(endpoint + "/mongo/all", function (data) {
        for (i = 0; i < data.length; i++) {
            addMarker(data[i]);
        }
    });
    

    // add markers (pin)
    function addMarker(pin) {

        var markerLatLng = new google.maps.LatLng(pin.Latitude, pin.Longitude);
        bounds.extend(markerLatLng);

        var tipHtml = "<h4>Pin Piece:</h4> " + pin.UserId + "<h5>Token:</h5><br/>Text: " + pin.Token + "What been told: " + pin.Text;

        var marker = new google.maps.Marker({
            position: markerLatLng,
            map: map,
            title: pin.Token,
            infowindow: infowindow
        });

        google.maps.event.addListener(marker, 'click', function () {
            this.infowindow.setContent(tipHtml);
            this.infowindow.open(map, this);
        });

        markersArray.push(marker);

    }

    // clean markers
    function clearOverlays() {
        for (var i = 0; i < markersArray.length; i++) {
            markersArray[i].setMap(null);
        }
        markersArray.length = 0;
    }

}
