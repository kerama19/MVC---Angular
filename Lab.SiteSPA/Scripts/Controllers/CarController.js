var CarListController = function ($http, $rootScope, $modal, $scope) {
    var ctrl = this;

    ctrl.cars = {};
    ctrl.isEmpty = false;

    ctrl.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Cars" });

   $http({
        url: '/Car/GetCarList',
        method: 'POST' 
   }).success(function (result) {
       if (!$rootScope.accessDenied) {
           ctrl.cars = result;

           ctrl.isEmpty = ctrl.cars.length === 0 ? true : false;

           $rootScope.header = {
               showHeader: true,
               headerTitle: "Car List",
               hasAlert: ctrl.isEmpty,
               alertType: "warning",
               signType: "warning",
               message: "There are no cars to be displayed"
           };
       }
       $rootScope.accessDenied = false;       
    }).error(function () {
        if (!$rootScope.accessDenied) {
            $rootScope.header = {
                showHeader: true,
                headerTitle: "Car List",
                hasAlert: true,
                alertType: "danger",
                signType: "exclamation",
                message: "An Error has occured, the car list cannot be displayed"
            };
        }
        $rootScope.accessDenied = false;        
    });

    ctrl.openModal = function (id) {
        $rootScope.item = "car";
        var modalInstance = $modal.open({
            templateUrl: 'Scripts/Templates/DeleteModal.html',
            controller: 'ModalController',
            controllerAs: 'modalCtrl'
        });

        modalInstance.result.then(function (response) {
            $http({
                url: '/Car/DeleteCar',
                method: 'POST',
                params: {
                    id: id
                }
            }).success(function (result) {
                ctrl.cars = result;

                ctrl.isEmpty = ctrl.cars.length === 0 ? true : false;

                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Car List",
                    hasAlert: true,
                    alertType: "success",
                    signType: "ok",
                    message: "The car was deleted successfully"
                };
            }).error(function () {
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Car List",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the car could not be deleted"
                };
            });
        });
    };
};

var EditCarController = function ($http, $rootScope, $scope, $route, $routeParams) {
    var ctrl = this;

    ctrl.car = {
        Manufacturer: "",
        Model: "",
        Color: "",
        Year: null,
        Category: ""
    };

    ctrl.IsNew = true;

    ctrl.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Cars" });

    if ($routeParams.Id) {
        $http({
            url: '/Car/GetCar',
            method: 'POST',
            params: {
                Id: $routeParams.Id
            }
        })
        .success(function (result) {
            if (!$rootScope.accessDenied) {
                ctrl.car = result;
                ctrl.IsNew = false;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Edit Car",
                    hasAlert: false,
                    alertType: "",
                    signType: "",
                    message: ""
                };
            }
            $rootScope.accessDenied = false;
        })
        .error(function () {
            if (!$rootScope.accessDenied) {
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Edit Car",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the car details cannot be displayed"
                };
            }
            $rootScope.accessDenied = false;
        });
    }
    else {
        $rootScope.header = {
            showHeader: true,
            headerTitle: "Add Car",
            hasAlert: false,
            alertType: "",
            signType: "",
            message: ""
        };
    }

    ctrl.saveCar = function (car) {
        var methodUrl = (ctrl.IsNew ? 'Car/CreateCar' : 'Car/UpdateCar');

        $http({ url: methodUrl, method: 'POST', params: { Data: JSON.stringify(car) } }).success(function (result) {
            if (!$rootScope.accessDenied) {
                ctrl.car = result;
                ctrl.IsNew = false;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: (ctrl.IsNew ? "Add" : "Edit") + " Car",
                    hasAlert: true,
                    alertType: "success",
                    signType: "ok",
                    message: "The car was saved succesfully"
                };
            }
            $rootScope.accessDenied = false;            
        }).error(function () {
            if (!$rootScope.accessDenied) {
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: (ctrl.IsNew ? "Add" : "Edit") + " Car",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the car could not be saved"
                };
            }
            $rootScope.accessDenied = false;            
        });
    };
};