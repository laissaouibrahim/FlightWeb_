// code to load Route details
var geocoder;
var map;
function initialize(departure, destination) {
    var map = new google.maps.Map(
        document.getElementById("map_canvas"), {
            //center: new google.maps.LatLng(37.4419, -122.1419),
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
    var directionService = new google.maps.DirectionsService();
    var directionsDisplay = new google.maps.DirectionsRenderer({
        map: map
    });
    directionService.route({
        origin: departure,
        destination: destination,
        travelMode: google.maps.TravelMode.DRIVING
    },
        function (response, status) {
            if (status === google.maps.DirectionsStatus.OK) {
                console.log(response);
                directionsDisplay.setDirections(response);


                var summaryPanel = $("#directions_panel");
                summaryPanel.empty();
                // For each route, display summary information.
                var route = response.routes[0];
                for (var i = 0; i < route.legs.length; i++) {

                    var routeSegment = i + 1;
                    summaryPanel.append('<b>Route Segment: ' + routeSegment + '</b><br>');
                    summaryPanel.append(' <b>From</b> : ' + route.legs[i].start_address + '<br>');
                    summaryPanel.append(' <b>To</b> : ' + route.legs[i].end_address + '<br>');
                    summaryPanel.append(' <b>Duration</b> : ' + route.legs[i].duration.text + '<br>');
                    summaryPanel.append(' <b>Distance</b> : ' + route.legs[i].distance.text + '<br><br>');

                    $("#FlightTime").val(route.legs[i].duration.value);
                    $("#FlightDistance").val(route.legs[i].distance.value);
                }

            } else {
                window.alert('Directions request failed due to ' + status);
            }
        });
}
  //  google.maps.event.addDomListener(window, "load", initialize);
