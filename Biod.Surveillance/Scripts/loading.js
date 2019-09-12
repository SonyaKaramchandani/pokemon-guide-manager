/// <reference path="../typings/tsd.d.ts" />
/// <reference path="../BiodWeb/Common.ts" />
(function ($) {
    var counter = 0;
    $.fn.loading = function (toLoad) {
        if (toLoad) {
            if (0 < counter) {
                counter++;
                return;
            }
            counter++;
            //$.ajaxSetup({
            //    global: false
            //});
        }
        else {
            if (1 < counter) {
                counter--;
                return;
            }
            counter--;
            //$.ajaxSetup({
            //    global: true
            //});
        }
        var element = $(this);
        var mask = element.find("#div-loading");
        //toLoad ? mask.length || (
        //    mask = $("<div id='div-loading' class='k-loading-mask'><span class='k-loading-text'>Loading...</span><div class='k-loading-image' style='z-index:100000;' /><div class='k-loading-color'/></div>")
        //    .width(element.outerWidth()).height(element.outerHeight()).prependTo(element)) : mask && mask.remove();
        if (toLoad) {
            mask.length ||
                (mask = $("<div id='div-loading' class='k-loading-mask'><span class='k-loading-text'>Loading...</span><div class='k-loading-image' style='z-index:100000;' /><div class='k-loading-color'/></div>")
                    .width(element.outerWidth()).height(element.outerHeight()).prependTo(element));
        }
        else {
            mask && mask.remove();
        }
    };
})(jQuery);
(function ($) {
    $.fn.loading2 = function (toLoad) {
        var element = $(this);
        var mask = element.find(".k-loading-mask");
        toLoad ? mask.length || (mask = $("<div class='k-loading-mask'><span class='k-loading-text'>Loading...</span><div class='k-loading-image'/><div class='k-loading-color'/></div>")
            .width(element.outerWidth()).height(element.outerHeight()).prependTo(element)) : mask && mask.remove();
    };
})(jQuery);
//(function () {
//    $.ajaxSetup({
//        global: true
//    });
//    $(document).ajaxSend(function (event, jqXHR, settings)
//    {        
//        var cookieToken = biodWeb.func.common.utility.getVariableValue("token");
//        jqXHR.setRequestHeader('Authorization', cookieToken);
//    });
//    //$.extend($.ajaxSettings, {
//    //    headers: {
//    //        'Authorization': sessionStorage.token
//    //    }
//    //});
//    $(document).ajaxStart(function (event, jqxhr, settings)
//    {
//        //$(document.body).loading2(true);
//    });
//    $(document).ajaxComplete(function (event, request, settings)
//    {
//        //$(document.body).loading2(false);
//        if (request.status == 200)
//        {
//            var token = request.getResponseHeader('Authorization');
//            if (token)
//            {
//                biodWeb.func.common.utility.setVariableValue("token", token, "both");
//            }
//        }
//    });
//    $(document).ajaxError(function (event, jqxhr, settings, thrownError)
//    {
//        var isRefreshPage = false;
//        isRefreshPage = jqxhr.getResponseHeader('X-RefreshPage');
//        var errorMessage = jqxhr.getResponseHeader('X-ErrorMessage');           
//        if (isRefreshPage)
//        {
//            if (window === top)
//            {
//                location.href = biodWeb.constant.indexUrl;
//            }
//        }
//    });
//})(); 
//# sourceMappingURL=loading.js.map