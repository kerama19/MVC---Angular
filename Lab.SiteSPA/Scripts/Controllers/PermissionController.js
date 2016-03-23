var ViewPermissionsController = function ($http, $rootScope, $scope, $route, $routeParams) {
    var ctrl = this;

    ctrl.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Permissions" });

    ctrl.users = {};

    ctrl.fromUser = $routeParams.UserName ? true : false;

    $http({
        url: '/Permission/GetPermissions',
        method: 'POST',
        params: {
            username: $routeParams.UserName
        }
    }).success(function (result) {
        ctrl.users = result.Users;
        $rootScope.header = {
            showHeader: true,
            headerTitle: "User Permissions",
            hasAlert: false,
            alertType: "",
            signType: "",
            message: ""
        };
    }).error(function () {
        $rootScope.header = {
            showHeader: true,
            headerTitle: "User Permissions",
            hasAlert: true,
            alertType: "danger",
            signType: "exclamation",
            message: "An Error has occured, the user permissions list cannot be displayed"
        };
    });
};

var EditPermissionsController = function ($http, $rootScope, $scope, $route, $routeParams) {
    var ctrl = this;
    ctrl.permissions = {};
    ctrl.fromUser = $routeParams.From === "true" ? true : false;
    ctrl.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Permissions" });

    if ($routeParams.UserName && $routeParams.Function) {
        $http({
            url: '/Permission/GetPermissionsByUserFunction',
            method: 'POST',
            params: {
                username: $routeParams.UserName,
                currentFunction: $routeParams.Function
            }
        })
        .success(function (result) {
            if (!$rootScope.accessDenied) {
                ctrl.permissions = result;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Edit Permission",
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
                    headerTitle: "Edit Permission",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the permission details cannot be displayed"
                };
            }
            $rootScope.accessDenied = false;
        });
    }

    ctrl.savePermissions = function (permission) {
        $http({ url: '/Permission/SavePermissions', method: 'POST', params: { Data: JSON.stringify(permission) } }).success(function (result) {
            ctrl.permission = result;
            ctrl.IsNew = false;
            $rootScope.header = {
                showHeader: true,
                headerTitle: "Edit Permission",
                hasAlert: true,
                alertType: "success",
                signType: "ok",
                message: "The user permissions were saved succesfully"
            };
        }).error(function () {
            $rootScope.header = {
                showHeader: true,
                headerTitle: "Edit Permission",
                hasAlert: true,
                alertType: "danger",
                signType: "exclamation",
                message: "An error has occured, the user permissions could not be saved"
            };
        });
    }
};