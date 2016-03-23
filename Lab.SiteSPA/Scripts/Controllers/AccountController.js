var AccountController = function ($http, $rootScope, $location, $scope, $timeout) {
    var account = this;

    account.isAuthenticated = $rootScope.isAuthenticated;
    account.validated = $rootScope.validated;

    account.tabs = $rootScope.user;
    account.user = $rootScope.permissions;

    $rootScope.$watch('user', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            account.user = newVal;
            $timeout(function () {
                $rootScope.$apply();
            });
        }        
    });

    $rootScope.$watch('validated', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            account.validated = newVal;
            $timeout(function () {
                $rootScope.$apply();
            });
        }        
    });

    $rootScope.$watch('isAuthenticated', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            account.isAuthenticated = newVal;
            $timeout(function () {
                $rootScope.$apply();
            });
        }        
    });

    $rootScope.$watch('permissions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            account.tabs = newVal;
            $timeout(function () {
                $rootScope.$apply();
            });
        }        
    });

    
    account.login = function (logUser) {
        var resultInfo = {};

        $http({ url: '/Account/Login', method: 'POST', params: { Data: JSON.stringify(logUser) } }).success(function (result) {
            account.user = result.User;
            account.tabs = result.Permissions;
            account.isAuthenticated = true;
            account.validated = true;

            $rootScope.user = account.user;
            $rootScope.permissions = account.tabs;

            $rootScope.header = {
                showHeader: true,
                headerTitle: "Home Page",
                hasAlert: true,
                alertType: "info",
                signType: "info",
                message: "Welcome " + account.user.UserName
            };
            $location.path('/');
            
        }).error(function () {
            $rootScope.header = {
                showHeader: true,
                headerTitle: "Error",
                hasAlert: true,
                alertType: "danger",
                signType: "exclamation",
                message: "The user name or password provided is incorrect."
            };
        });
    };

    account.logoff = function () {
        $http({ url: '/Account/LogOff', method: 'POST' }).success(function (result) {
            if (result) {
                account.isAuthenticated = false;
                $rootScope.user = {};
                account.user = {};
                $rootScope.header = {
                    showHeader: false,
                    headerTitle: "",
                    hasAlert: false,
                    alertType: "",
                    signType: "",
                    message: ""
                };
                $location.path('/');
            }
        }).error(function () {
            $rootScope.header = {
                showHeader: false,
                headerTitle: "",
                hasAlert: false,
                alertType: "",
                signType: "",
                message: ""
            };
        });
    };    
};

