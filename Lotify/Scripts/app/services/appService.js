app.factory('appService', function ($http, HOST, localStorageService) {

    var appServiceFactory = {};

   //Funcion para buscar un registro.
   var _show = function (url) {

       return $http.get(url)
         .success(function (results) {
            return results;
         });
   };

   var _showId = function (url) {
       return $http.get(url)
            .success(function (results) {
                return results;
            });
   };

   //Funcion para guardar un registro.
   var _save = function(data, url){
       return $http.post(url, data)
         .success(function (results){
            return results;
         });
   };

   var _delete = function (url, data) {
       return $http.post(url, data)
         .success(function (results) {
             return results;
         });
   };

   var _setShare = function (datos) {
       localStorageService.remove("ClienteId")
       localStorageService.set("ClienteId", datos);
   };

   var _getShare = function(){
       return localStorageService.get("ClienteId");
   }; 

      appServiceFactory.showId = _showId;
      appServiceFactory.show = _show;
      appServiceFactory.save = _save;
      appServiceFactory.delete = _delete;
      appServiceFactory.setShare = _setShare;
      appServiceFactory.getShare = _getShare;

      return appServiceFactory;
})