app.controller('VentaController', function ($scope, $timeout, $http, appService, localStorageService) {

    $scope.mostrar = true;
   
    var url = '/Venta';
    $scope.resultados;
    $scope.object;
    $scope.lotes;
    $scope.lotes2;
    $scope.dependencia;

    $scope.financiamientoSelected;
    $scope.lotesSelected = { Precio: 0, TasaInteres: 0 };
    $scope.calculo = {total: 0, cuota: 0, monto: 0, interes: 0};
    $scope.finan = 0;

    $scope.model = {
        Id: '',
        FechaVenta: '',
        NumeroComprobante: '',
        CodigoEmpleado: '',
        ClienteId: '',
        TipoFinanciamientoId: '',
    }
   
    $scope.currentPage = 0;
    $scope.pageSize = 4;
    $scope.data = [];
    

    //LLama a la funcion show para cargar los registros 2 segundos despues de cargar la pagina(Asi no se rompe el datatable).
	$timeout(function () {
	    $scope.show();
	    $scope.dependencias();
	    $scope.verLotes();
	}, 1000);

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

                      $scope.resultados = $scope.resultados.filter(function (element) {
                          return element.FechaVenta = new Date(parseInt(
                              element.FechaVenta.replace("/Date(", "").replace(")/", ""), 10));
                      })

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
                    $scope.resultados = $scope.resultados.filter(function (element) {
                        return element.Id !== $scope.datos.Id;
                    });
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

	$scope.dependencias = function () {
	    appService.show(url + "/Dependencias/" + appService.getShare())
               .then(function (results) {
                   if (results.data !== '') {
                       $scope.dependencia = results.data;
                       console.log(results);
                   }
               },
               function (err) {
                   console.log(err);
               });
	};
    
	$scope.save = function () {

	    $scope.model.ClienteId = appService.getShare();
	    //console.log($scope.data[0].Id)

	    $http.post(url + "/Create/", {
	        Total: $scope.calculo.total,
            Cuota: $scope.calculo.cuota,
	        ClienteId: $scope.model.ClienteId,
	        TipoFinanciamientoId: $scope.model.TipoFinanciamientoId,
	        detalle: { LoteId: $scope.data[0].Id },
	    })
        .success(function (data, status, headers, config) {
            //console.log(headers);
            console.log(data.venta);
            console.log(status);
            if (data.venta !== null) {
                window.location.href = '../../DetalleVenta/ReportePdf/'+data.venta;
            }
        })
        .error(function (error, status, headers, config) {
            console.log(status);
            console.log(error);
        });
	}

	$scope.verLotes = function () {
	    appService.show("../Lote/ShowDisponibles")
               .then(function (results) {
                   if (results.data !== '') {
                       $scope.lotes = results.data;
                       $scope.lotes2 = results.data;
                       $scope.numberOfPages = function () {
                           return Math.ceil($scope.lotes.length / $scope.pageSize);
                       }
                   }
               },
               function (err) {
                   console.log(err);
               });
	};

    //Funcion que establece el tiempo de financiamiento
	$scope.setPlazo = function () {
	    var f = $scope.dependencia.Financiamiento.filter(function (element) {
	        return element.Id === $scope.model.TipoFinanciamientoId;
	    });
        
	    $scope.finan = f[0].Plazo;

	    //calc();
	}

	$scope.selectLote = function (id) {

	    if ($scope.finan !== 0) {

	        //iterar lista de lotes
	        iterarLote(id);

	        //Llama al metodo calc paara calcular las cuotas
	        calc();
	    }
	    else {
	        var snackbarContainer = document.querySelector('#snack-messaje');
	        var data = {message: 'SELECCIONE UN TIPO DE FINANCIAMIENTO'};
	        snackbarContainer.MaterialSnackbar.showSnackbar(data);
	    }
	};

	var iterarLote = function (id) {
	    //Seleccionar lote
	    $scope.data = $scope.lotes.filter(function (element) {
	        return element.Id === id;
	    });

	    $scope.lotesSelected.Precio = $scope.data[0].Precio;
	    $scope.lotesSelected.TasaInteres = $scope.data[0].Interes.TasaInteres;

	    $scope.lotes = $scope.lotes2;
        //Ocultar Lote
	    $scope.lotes = $scope.lotes.filter(function (element) {
	        return element.Id !== id;
	    });

	    $scope.numberOfPages = function () {
	        return Math.ceil($scope.lotes.length / $scope.pageSize);
	    }//*/
	}

	var calc = function () {

	    var i = ( ($scope.lotesSelected.TasaInteres / 100) / $scope.finan);

	    var x = (1 + i);
	    
	    var topExp = i * Math.pow(x, $scope.finan);

	    var downExp = Math.pow(x, $scope.finan) - 1;

	    var f = (topExp / downExp);

	    var p = $scope.lotesSelected.Precio * f;

	    $scope.calculo.total = p.toPrecision(4) * $scope.finan;
	    $scope.calculo.cuota = p.toPrecision(4);
	    $scope.calculo.monto = (p.toPrecision(4) * $scope.finan) - $scope.lotesSelected.Precio;

	    console.log(p);
	}

});


app.filter('startFrom', function () {
    return function (input, start) {
        if (!input || !input.length) { return; }
        start = +start; //parse to int
        return input.slice(start);
    }
});