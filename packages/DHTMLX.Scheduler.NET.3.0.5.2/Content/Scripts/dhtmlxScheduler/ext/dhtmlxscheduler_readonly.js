/*
Copyright Dinamenta, UAB. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){a.attachEvent("onTemplatesReady",function(){function e(a,b,c,d){for(var j=b.getElementsByTagName(a),i=c.getElementsByTagName(a),h=i.length-1;h>=0;h--)if(c=i[h],d){var f=document.createElement("SPAN");f.className="dhx_text_disabled";f.innerHTML=d(j[h]);c.parentNode.insertBefore(f,c);c.parentNode.removeChild(c)}else if(c.disabled=!0,b.checked)c.checked=!0}var s=a.config.lightbox.sections,l=null,o=a.config.buttons_left.slice(),p=a.config.buttons_right.slice();a.attachEvent("onBeforeLightbox",
function(g){if(this.config.readonly_form||this.getEvent(g).readonly){this.config.readonly_active=!0;for(var b=0;b<this.config.lightbox.sections.length;b++)this.config.lightbox.sections[b].focus=!1}else this.config.readonly_active=!1,a.config.buttons_left=o.slice(),a.config.buttons_right=p.slice();var c=this.config.lightbox.sections;if(this.config.readonly_active){for(var d=!1,b=0;b<c.length;b++)if(c[b].type=="recurring"){l=c[b];this.config.readonly_active&&c.splice(b,1);break}!d&&!this.config.readonly_active&&
l&&c.splice(c.length-2,0,l);for(var j=["dhx_delete_btn","dhx_save_btn"],i=[a.config.buttons_left,a.config.buttons_right],b=0;b<j.length;b++)for(var h=j[b],f=0;f<i.length;f++){for(var e=i[f],m=-1,k=0;k<e.length;k++)if(e[k]==h){m=k;break}m!=-1&&e.splice(m,1)}}this.resetLightbox();return!0});var q=a._fill_lightbox;a._fill_lightbox=function(){var g=this.getLightbox();if(this.config.readonly_active)g.style.visibility="hidden",g.style.display="block";var b=q.apply(this,arguments);if(this.config.readonly_active)g.style.visibility=
"",g.style.display="none";if(this.config.readonly_active){var c=this.getLightbox(),d=this._lightbox_r=c.cloneNode(!0);d.id=a.uid();e("textarea",c,d,function(a){return a.value});e("input",c,d,!1);e("select",c,d,function(a){return!a.options.length?"":a.options[Math.max(a.selectedIndex||0,0)].text});c.parentNode.insertBefore(d,c);n.call(this,d);a._lightbox&&a._lightbox.parentNode.removeChild(a._lightbox);this._lightbox=d;if(a.config.drag_lightbox)d.firstChild.onmousedown=a._ready_to_dnd;this.setLightboxSize();
d.onclick=function(c){var b=c?c.target:event.srcElement;if(!b.className)b=b.previousSibling;if(b&&b.className)switch(b.className){case "dhx_cancel_btn":a.callEvent("onEventCancel",[a._lightbox_id]),a._edit_stop_event(a.getEvent(a._lightbox_id),!1),a.hide_lightbox()}}}return b};var n=a.showCover;a.showCover=function(){this.config.readonly_active||n.apply(this,arguments)};var r=a.hide_lightbox;a.hide_lightbox=function(){if(this._lightbox_r)this._lightbox_r.parentNode.removeChild(this._lightbox_r),this._lightbox_r=
this._lightbox=null;return r.apply(this,arguments)}})});
