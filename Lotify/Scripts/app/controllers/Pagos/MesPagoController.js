app.controller('MesPagoController', function ($scope, $timeout, $http, appService) {

    $scope.mostrar = true;
   
	var url = '/MesPago';
	$scope.resultados;
	$scope.object;

	$scope.datos = {
	    Id: '',
        NombreMes: '',
	}
   
    //LLama a la funcion show para cargar los registros 2 segundos despues de cargar la pagina(Asi no se rompe el datatable).
	$timeout(function () {
	    $scope.show();
	}, 2000);

    //Muestra un registro en base al Id.
	$scope.showId = function (Id) {
	    appService.showId(url + "/ShowId/"+Id)
            .then(function (results) {
                if (results.data !== '') {
                    $scope.object = results.data;
                }
            },
                function (err) {
                    console.log(err);
                });
	}

    //Carga la lista de todos los registros de una determinada entidad.
	$scope.show = function(){
	    appService.show(url + "/Show")
               .then(function(results){
                  if(results.data !== ''){
                      $scope.resultados = results.data;
                      $scope.mostrar = false;
                  }
               },
               function (err){
                   console.log(err);
               });
	};

    //Confirma la acción eliminar para un determinado registro
	$scope.delete = function () {
	    appService.delete(url + "/Delete", $scope.datos)
            .then(function (results) {
                if (results.status === 200) {

                    /*
                        *Cualquiera de los dos funciona, utilizar el que mejor les parezca 
                    */

                    /* 
                        *Itera los arreglos ya cargados en memoria y elimina el que se ha eliminado en la base de datos
                    */
                    $scope.resultados = $scope.resultados.filter(function (element) {
                        return element.Id !== $scope.datos.Id;
                    })

                    /*
                        *Llama a la funcion show despues de haber eliminado los arreglos.
                    */

                    //$scope.show();
                }
            },
            function (err) {
                console.log(err);
            })
	}

    //Establece el Id del registro que se va a eliminar.
	$scope.selectElementDelete = function (id) {

	    var dialog = document.querySelector('dialog');
	    
	    if (!dialog.showModal) {
	        dialogPolyfill.registerDialog(dialog);
	    }
	    //Abre el Dialogo
	    dialog.showModal();

        //Establecemos el Id del registro a eliminar
        $scope.datos.Id = id;
	};
    

});