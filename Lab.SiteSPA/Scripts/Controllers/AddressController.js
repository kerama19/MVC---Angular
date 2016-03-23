var AddressListController = function ($http, $rootScope, $modal, $scope) {
    var ctrl = this;

    ctrl.addresses = {};
    ctrl.isEmpty = false;

    ctrl.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Addresses" });

    $http({
        url: '/Address/GetAddressList',
        method: 'POST'
    }).success(function (result) {
        if (!$rootScope.accessDenied) {
            ctrl.addresses = result;

            ctrl.isEmpty = ctrl.addresses.length === 0 ? true : false;

            $rootScope.header = {
                showHeader: true,
                headerTitle: "Address List",
                hasAlert: ctrl.isEmpty,
                alertType: "warning",
                signType: "warning",
                message: "There are no addresses to be displayed"
            };
        }
        $rootScope.accessDenied = false;        
    }).error(function () {
        if (!$rootScope.accessDenied) {
            $rootScope.header = {
                showHeader: true,
                headerTitle: "Address List",
                hasAlert: true,
                alertType: "danger",
                signType: "exclamation",
                message: "An Error has occured, the address list cannot be displayed"
            };
        }
        $rootScope.accessDenied = false;        
    });

    ctrl.openModal = function (id) {
        $rootScope.item = "address";
        var modalInstance = $modal.open({
            templateUrl: 'Scripts/Templates/DeleteModal.html',
            controller: 'ModalController',
            controllerAs: 'modalCtrl'
        });

        modalInstance.result.then(function (response) {
            $http({
                url: '/Address/DeleteAddress',
                method: 'POST',
                params: {
                    id: id
                }
            }).success(function (result) {
                ctrl.addresses = result;

                ctrl.isEmpty = ctrl.addresses.length === 0 ? true : false;

                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Address List",
                    hasAlert: true,
                    alertType: "success",
                    signType: "ok",
                    message: "The address was deleted successfully"
                };
            }).error(function () {
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Address List",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the address could not be deleted"
                };
            });
        });
    };
};

var EditAddressController = function ($http, $rootScope, $scope, $route, $routeParams) {
    var ctrl = this;

    ctrl.address = {
        CompanyName: "",
        AddressLine1: "",
        AddressLine2: "",
        State: "",
        City: "",
        Country: "",
        Zip: ""
    };

    ctrl.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Addresses" });

    ctrl.IsNew = true;

    if ($routeParams.Id) {
        $http({
            url: '/Address/GetAddress',
            method: 'POST',
            params: {
                id: $routeParams.Id
            }
        })
        .success(function (result) {
            if (!$rootScope.accessDenied) {
                ctrl.address = result;
                ctrl.IsNew = false;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Edit Address",
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
                    headerTitle: "Edit Address",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the address details cannot be displayed"
                };
            }
            $rootScope.accessDenied = false;            
        });
    }
    else {
        $rootScope.header = {
            showHeader: true,
            headerTitle: "Add Address",
            hasAlert: false,
            alertType: "",
            signType: "",
            message: ""
        };
    }

    ctrl.saveAddress = function (address) {
        var methodUrl = (ctrl.IsNew ? 'Address/CreateAddress' : 'Address/UpdateAddress');

        $http({ url: methodUrl, method: 'POST', params: { Data: JSON.stringify(address) } }).success(function (result) {
            ctrl.address = result;
            ctrl.IsNew = false;
            $rootScope.header = {
                showHeader: true,
                headerTitle: (ctrl.IsNew ? "Add" : "Edit") + " Address",
                hasAlert: true,
                alertType: "success",
                signType: "ok",
                message: "The address was saved succesfully"
            };
        }).error(function () {
            $rootScope.header = {
                showHeader: true,
                headerTitle: (ctrl.IsNew ? "Add" : "Edit") + " Address",
                hasAlert: true,
                alertType: "danger",
                signType: "exclamation",
                message: "An error has occured, the address could not be saved"
            };
        });
    };
};