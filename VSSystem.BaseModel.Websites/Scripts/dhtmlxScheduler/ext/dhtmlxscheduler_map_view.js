/*
Copyright Dinamenta, UAB. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){a.xy.map_date_width=188;a.xy.map_description_width=400;a.config.map_resolve_event_location=!0;a.config.map_resolve_user_location=!0;a.config.map_initial_position=new google.maps.LatLng(48.724,8.215);a.config.map_error_position=new google.maps.LatLng(15,15);a.config.map_infowindow_max_width=300;a.config.map_type=google.maps.MapTypeId.ROADMAP;a.config.map_zoom_after_resolve=15;a.locale.labels.marker_geo_success="It seems you are here.";a.locale.labels.marker_geo_fail="Sorry, could not get your current position using geolocation.";
a.templates.marker_date=a.date.date_to_str("%Y-%m-%d %H:%i");a.templates.marker_text=function(i,j,g){return"<div><b>"+g.text+"</b><br/><br/>"+(g.event_location||"")+"<br/><br/>"+a.templates.marker_date(i)+" - "+a.templates.marker_date(j)+"</div>"};a.dblclick_dhx_map_area=function(){!this.config.readonly&&this.config.dblclick_create&&this.addEventNow({start_date:a._date,end_date:a.date.add(a._date,a.config.time_step,"minute")})};a.templates.map_time=function(i,j,g){return g._timed?this.day_date(g.start_date,
g.end_date,g)+" "+this.event_date(i):a.templates.day_date(i)+" &ndash; "+a.templates.day_date(j)};a.templates.map_text=function(a,j,g){return g.text};a.date.map_start=function(a){return a};a.date.add_map=function(a){return new Date(a.valueOf())};a.templates.map_date=function(){return""};a._latLngUpdate=!1;a.attachEvent("onSchedulerReady",function(){function i(b){if(b){var c=a.locale.labels;a._els.dhx_cal_header[0].innerHTML="<div class='dhx_map_line' style='width: "+(a.xy.map_date_width+a.xy.map_description_width+
2)+"px;' ><div class='headline_date' style='width: "+a.xy.map_date_width+"px;'>"+c.date+"</div><div class='headline_description' style='width: "+a.xy.map_description_width+"px;'>"+c.description+"</div></div>";a._table_view=!0;a.set_sizes()}}function j(){a._selected_event_id=null;a.map._infowindow.close();var b=a.map._markers,c;for(c in b)b.hasOwnProperty(c)&&(b[c].setMap(null),delete a.map._markers[c],a.map._infowindows_content[c]&&delete a.map._infowindows_content[c])}function g(){var b=a.get_visible_events();
b.sort(function(a,b){return a.start_date.valueOf()==b.start_date.valueOf()?a.id>b.id?1:-1:a.start_date>b.start_date?1:-1});for(var c="<div class='dhx_map_area'>",e=0;e<b.length;e++){var d=b[e],f=d.id==a._selected_event_id?"dhx_map_line highlight":"dhx_map_line",g=d.color?"background:"+d.color+";":"",h=d.textColor?"color:"+d.textColor+";":"";c+="<div class='"+f+"' event_id='"+d.id+"' style='"+g+""+h+""+(d._text_style||"")+" width: "+(a.xy.map_date_width+a.xy.map_description_width+2)+"px;'><div style='width: "+
a.xy.map_date_width+"px;' >"+a.templates.map_time(d.start_date,d.end_date,d)+"</div>";c+="<div class='dhx_event_icon icon_details'>&nbsp</div>";c+="<div class='line_description' style='width:"+(a.xy.map_description_width-25)+"px;'>"+a.templates.map_text(d.start_date,d.end_date,d)+"</div></div>"}c+="<div class='dhx_v_border' style='left: "+(a.xy.map_date_width-2)+"px;'></div><div class='dhx_v_border_description'></div></div>";a._els.dhx_cal_data[0].scrollTop=0;a._els.dhx_cal_data[0].innerHTML=c;a._els.dhx_cal_data[0].style.width=
a.xy.map_date_width+a.xy.map_description_width+1+"px";var i=a._els.dhx_cal_data[0].firstChild.childNodes;a._els.dhx_cal_date[0].innerHTML=a.templates[a._mode+"_date"](a._min_date,a._max_date,a._mode);a._rendered=[];for(e=0;e<i.length-2;e++)a._rendered[e]=i[e]}function m(b){var c=document.getElementById(b),e=a._y-a.xy.nav_height;e<0&&(e=0);var d=a._x-a.xy.map_date_width-a.xy.map_description_width-1;d<0&&(d=0);c.style.height=e+"px";c.style.width=d+"px";c.style.marginLeft=a.xy.map_date_width+a.xy.map_description_width+
1+"px";c.style.marginTop=a.xy.nav_height+2+"px"}a._isMapPositionSet=!1;var h=document.createElement("div");h.className="dhx_map";h.id="dhx_gmap";h.style.dispay="none";var o=a._obj;o.appendChild(h);a._els.dhx_gmap=[];a._els.dhx_gmap.push(h);m("dhx_gmap");var p={zoom:a.config.map_inital_zoom||10,center:a.config.map_initial_position,mapTypeId:a.config.map_type||google.maps.MapTypeId.ROADMAP},f=new google.maps.Map(document.getElementById("dhx_gmap"),p);f.disableDefaultUI=!1;f.disableDoubleClickZoom=!a.config.readonly;
google.maps.event.addListener(f,"dblclick",function(b){if(!a.config.readonly&&a.config.dblclick_create){var c=b.latLng;geocoder.geocode({latLng:c},function(b,d){if(d==google.maps.GeocoderStatus.OK)c=b[0].geometry.location,a.addEventNow({lat:c.lat(),lng:c.lng(),event_location:b[0].formatted_address,start_date:a._date,end_date:a.date.add(a._date,a.config.time_step,"minute")})})}});var n={content:""};if(a.config.map_infowindow_max_width)n.maxWidth=a.config.map_infowindow_max_width;a.map={_points:[],
_markers:[],_infowindow:new google.maps.InfoWindow(n),_infowindows_content:[],_initialization_count:-1,_obj:f};geocoder=new google.maps.Geocoder;a.config.map_resolve_user_location&&navigator.geolocation&&(a._isMapPositionSet||navigator.geolocation.getCurrentPosition(function(b){var c=new google.maps.LatLng(b.coords.latitude,b.coords.longitude);f.setCenter(c);f.setZoom(a.config.map_zoom_after_resolve||10);a.map._infowindow.setContent(a.locale.labels.marker_geo_success);a.map._infowindow.position=f.getCenter();
a.map._infowindow.open(f);a._isMapPositionSet=!0},function(){a.map._infowindow.setContent(a.locale.labels.marker_geo_fail);a.map._infowindow.setPosition(f.getCenter());a.map._infowindow.open(f);a._isMapPositionSet=!0}));google.maps.event.addListener(f,"resize",function(){h.style.zIndex="5";f.setZoom(f.getZoom())});google.maps.event.addListener(f,"tilesloaded",function(){h.style.zIndex="5"});h.style.display="none";a.attachEvent("onSchedulerResize",function(){return this._mode=="map"?(this.map_view(!0),
!1):!0});var q=a.render_data;a.render_data=function(b,c){if(this._mode=="map"){g();for(var e=a.get_visible_events(),d=0;d<e.length;d++)a.map._markers[e[d].id]||k(e[d],!1,!1)}else return q.apply(this,arguments)};a.map_view=function(b){a.map._initialization_count++;var c=a._els.dhx_gmap[0];a._els.dhx_cal_data[0].style.width=a.xy.map_date_width+a.xy.map_description_width+1+"px";a._min_date=a.config.map_start||a._currentDate();a._max_date=a.config.map_end||a.date.add(a._currentDate(),1,"year");a._table_view=
!0;i(b);if(b){j();g();c.style.display="block";m("dhx_gmap");for(var e=a.map._obj.getCenter(),d=a.get_visible_events(),f=0;f<d.length;f++)a.map._markers[d[f].id]||k(d[f])}else c.style.display="none";google.maps.event.trigger(a.map._obj,"resize");a.map._initialization_count===0&&e&&a.map._obj.setCenter(e);a._selected_event_id&&r(a._selected_event_id)};var r=function(b){a.map._obj.setCenter(a.map._points[b]);a.callEvent("onClick",[b])},k=function(b,c,e){var d=a.config.map_error_position;b.lat&&b.lng&&
(d=new google.maps.LatLng(b.lat,b.lng));var f=a.templates.marker_text(b.start_date,b.end_date,b);a._new_event||(a.map._infowindows_content[b.id]=f,a.map._markers[b.id]&&a.map._markers[b.id].setMap(null),a.map._markers[b.id]=new google.maps.Marker({position:d,map:a.map._obj}),google.maps.event.addListener(a.map._markers[b.id],"click",function(){a.map._infowindow.setContent(a.map._infowindows_content[b.id]);a.map._infowindow.open(a.map._obj,a.map._markers[b.id]);a._selected_event_id=b.id;a.render_data()}),
a.map._points[b.id]=d,c&&a.map._obj.setCenter(a.map._points[b.id]),e&&a.callEvent("onClick",[b.id]))};a.attachEvent("onClick",function(b){if(this._mode=="map"){a._selected_event_id=b;for(var c=0;c<a._rendered.length;c++)a._rendered[c].className="dhx_map_line",a._rendered[c].getAttribute("event_id")==b&&(a._rendered[c].className+=" highlight");a.map._points[b]&&a.map._markers[b]&&(a.map._obj.setCenter(a.map._points[b]),google.maps.event.trigger(a.map._markers[b],"click"))}return!0});var l=function(b){b.event_location&&
geocoder?geocoder.geocode({address:b.event_location,language:a.uid().toString()},function(c,e){var d={};if(e!=google.maps.GeocoderStatus.OK){if(d=a.callEvent("onLocationError",[b.id]),!d||d===!0)d=a.config.map_error_position}else d=c[0].geometry.location;b.lat=d.lat();b.lng=d.lng();a._selected_event_id=b.id;a._latLngUpdate=!0;a.callEvent("onEventChanged",[b.id,b]);k(b,!0,!0)}):k(b,!0,!0)},s=function(b){b.event_location&&geocoder&&geocoder.geocode({address:b.event_location,language:a.uid().toString()},
function(c,e){var d={};if(e!=google.maps.GeocoderStatus.OK){if(d=a.callEvent("onLocationError",[b.id]),!d||d===!0)d=a.config.map_error_position}else d=c[0].geometry.location;b.lat=d.lat();b.lng=d.lng();a._latLngUpdate=!0;a.callEvent("onEventChanged",[b.id,b])})},t=function(a,c,e,d){setTimeout(function(){var d=a.apply(c,e);a=c=e=null;return d},d||1)};a.attachEvent("onEventChanged",function(b){if(this._latLngUpdate)this._latLngUpdate=!1;else{var c=a.getEvent(b);c.start_date<a._min_date&&c.end_date>
a._min_date||c.start_date<a._max_date&&c.end_date>a._max_date||c.start_date.valueOf()>=a._min_date&&c.end_date.valueOf()<=a._max_date?(a.map._markers[b]&&a.map._markers[b].setMap(null),l(c)):(a._selected_event_id=null,a.map._infowindow.close(),a.map._markers[b]&&a.map._markers[b].setMap(null))}return!0});a.attachEvent("onEventIdChange",function(b,c){var e=a.getEvent(c);if(e.start_date<a._min_date&&e.end_date>a._min_date||e.start_date<a._max_date&&e.end_date>a._max_date||e.start_date.valueOf()>=a._min_date&&
e.end_date.valueOf()<=a._max_date)a.map._markers[b]&&(a.map._markers[b].setMap(null),delete a.map._markers[b]),a.map._infowindows_content[b]&&delete a.map._infowindows_content[b],l(e);return!0});a.attachEvent("onEventAdded",function(b,c){if(!a._dataprocessor&&(c.start_date<a._min_date&&c.end_date>a._min_date||c.start_date<a._max_date&&c.end_date>a._max_date||c.start_date.valueOf()>=a._min_date&&c.end_date.valueOf()<=a._max_date))a.map._markers[b]&&a.map._markers[b].setMap(null),l(c);return!0});a.attachEvent("onBeforeEventDelete",
function(b){a.map._markers[b]&&a.map._markers[b].setMap(null);a._selected_event_id=null;a.map._infowindow.close();return!0});a._event_resolve_delay=1500;a.attachEvent("onEventLoading",function(b){a.config.map_resolve_event_location&&b.event_location&&!b.lat&&!b.lng&&(a._event_resolve_delay+=1500,t(s,this,[b],a._event_resolve_delay));return!0});a.attachEvent("onEventCancel",function(b,c){c&&(a.map._markers[b]&&a.map._markers[b].setMap(null),a.map._infowindow.close());return!0})})});
