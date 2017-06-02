//Service para pasar el Id del cliente entre controllers in angular
app.factory('ventaService', function ($http, HOST) {

    var ventaServiceFactory = {};

    var _showId = function (url) {
        return $http.get(url)
             .success(function (results) {
                 return results;
             });
    };

    var _shareData = function (data) {
        return data;
    }

    ventaServiceFactory.showId = _showId;
    ventaServiceFactory.shareData = _shareData;

    return appServiceFactory;
})