var obj;
var map;
var dir;

/*  Handling search boxes (Add, Remove) */
jQuery(document).ready(function($){
	$('.my-form .add-box').click(function(){
		var n = $('.text-box').length + 1;
		var box_html = $('<p class="text-box"><label for="box' + n + '">Box <span class="box-number">' + n + '</span></label> <input type="text" name="addr" value="" id="addr" size="10" onKeyUp="addr_search(this);" /> <a href="#" class="remove-box">Remove</a></p>');
		box_html.hide();
		$('.my-form p.text-box:last').after(box_html);
		box_html.fadeIn('slow');
		return false;
	});
	
	$('.my-form').on('click', '.remove-box', function(){
		$(this).parent().css( 'background-color', '#FF6C6C' );
		$(this).parent().fadeOut("slow", function() {
			$(this).remove();
			$('.box-number').each(function(index){
				$(this).text( index + 1 );
			});
		});
		return false;
	});
});

window.onload = function() {
		var locationsJSON = '[]';
		obj = JSON.parse(locationsJSON);
		//Here we need to read last location and set marker for it.
		//If this is the first post of the trip - ask user to enter it manually in text box
		obj.push({'city':'Zagreb'});
		locationsJSON = JSON.stringify(obj);

		map = L.map('map', {
			layers: MQ.mapLayer(),
			center: [ 45.8167, 15.9833 ],
			zoom: 8
		});
	
		map.on('click', onClick);
		function onClick(event)
			{
			var latt = event.latlng.lat;
			var lngt = event.latlng.lng;
			obj.push({'latLng':{'lat':latt,'lng':lngt}});
			drawRoute();
			}

		dir = MQ.routing.directions()
			.on('success', function(data) {
				var legs = data.route.legs,
					html = '',
					maneuvers,
					i;

				if (legs && legs.length) {
					var dist =0;
					legs.forEach(function(entry) {
						dist+= entry.distance*1.60934;
					});                       

					html+=dist;

					L.DomUtil.get('route-narrative').innerHTML = html;
				}
			});

		drawRoute();	
	}

/* Draw route on the map */
var drawRoute =	function ()
		{
			//alert();
			dir.route({
					  locations:obj,
					options: {
					  avoids: [ 'toll road' ],
					  routeType:'bicycle'
					},
				});

			map.addLayer(MQ.routing.routeLayer({
				directions: dir,
				fitBounds: true
			}));
		}	

/* Is called on choosing the location from results */
function chooseAddr(lat, lng) {
	obj.push({'latLng':{'lat':lat,'lng':lng}});
	drawRoute();
	
}

/* Is called on typing in location search box */
function addr_search(inp) {

    $.getJSON('http://nominatim.openstreetmap.org/search?format=json&limit=5&q=' + inp.value, function(data) {
        var items = [];

        $.each(data, function(key, val) {
            bb = val.boundingbox;
            items.push("<li><a href='#' onclick='chooseAddr(" + val.lat + ", " + val.lon + "\);return false;'>" + val.display_name + '</a></li>');
        });

		$('#results').empty();
        if (items.length != 0) {
            $('<p>', { html: "Search results:" }).appendTo('#results');
            $('<ul/>', {
                'class': 'my-new-list',
                html: items.join('')
            }).appendTo('#results');
        } else {
            $('<p>', { html: "No results found" }).appendTo('#results');
        }
    });
}
