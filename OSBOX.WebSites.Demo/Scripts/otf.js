$(function () {

    var AjaxFormSubmit = function () {


        var $form = $(this);
        var options =
            {
                url: $form.attr("action"),
                type: $form.attr("method"),
                LoadingElementId: $form.attr("progress"),
                data: $form.serialize()
            };

        $.ajax(options).done(function (data) {

            var $target = $($form.attr("data-otf-Target"));
            $target.replace(data);
        });
        return false;

    }



    var CreateAutoComplete = function () {
        var $input = $(this);
        var options = {

            source: $input.attr("data-otf-autocomplete")
        };

        $input.autocomplete(options);

    };


    $("input[data-otf-autocomplete]").each(CreateAutoComplete);
    $("form[data-otf-ajax='true']").submit(AjaxFormSubmit);






    /**
 * This tiny script just helps us demonstrate
 * what the various example callbacks are doing
 */
   















})

//Bootbox.js for modal//////////////////////
/////////////////////////////////////////////
var Notification = (function () {
    "use strict";

    var elem,
        hideHandler,
        that = {};

    that.init = function (options) {
        elem = $(options.selector);
    };

    that.show = function (text) {
        clearTimeout(hideHandler);

        elem.find("span").html(text);
        elem.delay(200).fadeIn().delay(4000).fadeOut();
    };

    return that;
}());


$(function () {
    Notification.init({
        "selector": ".bb-alert"
    });
});
////////////////////////////////////////////////////


//////////////////// Purr Notification plugin //////////////////

//var PurrNotice = '<div class="alert alert-info alert">'
//      //+ '<div class="notice-body">'
//         //+ '<img src="../static/images/icons/info.png" />'
//         + 'Headline'
//         + '<p>Message</p>'
//      + '</div>'
      //+ '<div class="notice-bottom">'
    //  + '</div>'
//+ '</div>';
//var PurrNotice = '<div class="alertNotification alert-info">2</div>';



//////////////////////////loading animation/////////////
var LoadingModal = function () {
    if($(this).valid()) {
        $.blockUI({
            message: '<i class="fa fa-spinner fa-spin fa-4x"></i> Processing data, please wait...',
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .6,
                color: '#fff'
            }
        })
    }
        
    
};


//http://stackoverflow.com/questions/9446318/bootstrap-tooltips-not-working
//For the tooltip menu to show
$("[data-toggle=\"tooltip\"]").tooltip();