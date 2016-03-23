var iniHeaderSettings = function ($rootScope, $http) {
    $rootScope.user = {};
    $rootScope.permissions = {};
    $rootScope.validated = false;
    $rootScope.isAuthenticated = false;

    $http.get('/Account/Validate').success(function (result) {
        if (result) {
            $rootScope.isAuthenticated = true;
            $rootScope.validated = true;
            $rootScope.permissions = result.Permissions;
            $rootScope.user = result.User;            
        }
    }).error(function () {
        console.log('No User Authenticated');
        $rootScope.validated = true;
    });
};

var HeaderController = function ($rootScope, $scope, $timeout) {
    var ctrl = this;
    ctrl.header = {};

    ctrl.hideAlert = function () {
        ctrl.header.hasAlert = false;
    };

    $rootScope.$watch('header', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            ctrl.header = newVal;
            $timeout(function () {
                $rootScope.$apply();
            });
        }        
    });
};

var HomeController = function ($rootScope, $location) {
    var ctrl = this;
    ctrl.goHome = function () {
        $rootScope.header = {
            showHeader: true,
            headerTitle: "Home Page",
            hasAlert: false,
            alertType: "",
            signType: "",
            message: ""
        };
        $location.path('/');
    }
};

var AccessDeniedController = function ($rootScope) {
    $rootScope.accessDenied = true;

    $rootScope.header = {
        showHeader: true,
        headerTitle: "Access Denied",
        hasAlert: true,
        alertType: "danger",
        signType: "exclamation",
        message: "Sorry! You don't have the rights to access this function"
    };
};

var ModalController = function ($rootScope, $modalInstance) {
    this.element = $rootScope.item;

    this.ok = function () {
        $modalInstance.close(true);
    };

    this.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};