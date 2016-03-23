var UserListController = function ($http, $rootScope, $modal) {
    var site = this;

    site.users = {};

    site.UserFunctions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Users" });
    site.currentLoggedUser = $rootScope.user.CurrentUserName;

    $http({
        url: '/User/GetUserList',
        method: 'POST'
    })
    .success(function (result) {
        if (!$rootScope.accessDenied) {
            site.users = result;
            $rootScope.header = {
                showHeader: true,
                headerTitle: "User List",
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
            site.users = {};
            $rootScope.header = {
                showHeader: true,
                headerTitle: "User List",
                hasAlert: true,
                alertType: "danger",
                signType: "exclamation",
                message: "An error has occured, the user list cannot be displayed"
            };
        }
        $rootScope.accessDenied = false;
    });    

    site.openModal = function (userName) {
        $rootScope.item = "user";
        var modalInstance = $modal.open({
            templateUrl: 'Scripts/Templates/DeleteModal.html',
            controller: 'ModalController',
            controllerAs: 'modalCtrl'
        });

        modalInstance.result.then(function (response) {
            $http({
                url: '/User/DeleteUser',
                method: 'POST',
                params: {
                    username: userName
                }
            }).success(function (result) {
                site.users = result;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "User List",
                    hasAlert: true,
                    alertType: "success",
                    signType: "ok",
                    message: "The user was deleted successfully"
                };
            }).error(function () {
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "User List",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the user could not be deleted"
                };
            });            
        });
    };
};

var EditUserController = function ($http, $rootScope, $scope, $route, $routeParams) {
    var ctrl = this;

    ctrl.rightsPermissions = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Permissions" });
    ctrl.rightsUsers = $.grep($rootScope.permissions, function (e) { return e.currentFunction === "Users" });

    ctrl.user = {
        Id: 0,
        UserName: "",
        Password: null,
        Email: "",
        Active: false
    };

    ctrl.IsNew = true;

    if ($routeParams.UserName) {
        $http({
            url: '/User/GetUser',
            method: 'POST',
            params: {
                username: $routeParams.UserName
            }
        })
        .success(function (result) {
            if (!$rootScope.accessDenied) {
                ctrl.user = result;
                ctrl.IsNew = false;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: "Edit User",
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
                    headerTitle: "Edit User",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the user details cannot be displayed"
                };
            }
            $rootScope.accessDenied = false;
        });        
    }
    else {
        $rootScope.header = {
            showHeader: true,
            headerTitle: "Add User",
            hasAlert: false,
            alertType: "",
            signType: "",
            message: ""
        };
    }

    ctrl.saveUser = function (user) {
        var methodUrl = (ctrl.IsNew ? 'User/CreateUser' : 'User/UpdateUser');
        
        $http({ url: methodUrl, method: 'POST', params: { Data: JSON.stringify(user) } }).success(function (result) {
            if (!$rootScope.accessDenied) {
                ctrl.user = result;
                ctrl.IsNew = false;
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: (ctrl.IsNew ? "Add" : "Edit") + " User",
                    hasAlert: true,
                    alertType: "success",
                    signType: "ok",
                    message: "The user was saved succesfully"
                };
            }
            $rootScope.accessDenied = false;            
        }).error(function () {
            if (!$rootScope.accessDenied) {
                $rootScope.header = {
                    showHeader: true,
                    headerTitle: (ctrl.IsNew ? "Add" : "Edit") + " User",
                    hasAlert: true,
                    alertType: "danger",
                    signType: "exclamation",
                    message: "An error has occured, the user could not be saved"
                };
            }
            $rootScope.accessDenied = false;            
        });
    };
};