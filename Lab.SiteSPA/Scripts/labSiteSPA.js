(function () {
    var labSiteSPA = angular.module('labSiteSPA', ['ngRoute', 'ui.bootstrap']).config(['$routeProvider', '$locationProvider', Routing]).run(['$rootScope', '$http', iniHeaderSettings]);

    //Controllers
    labSiteSPA.controller('HeaderController', ['$rootScope', '$scope', '$timeout', HeaderController]);

    labSiteSPA.controller('ModalController', ['$rootScope', '$modalInstance', ModalController]);

    labSiteSPA.controller('HomeController', ['$rootScope', '$location', HomeController]);

    labSiteSPA.controller('AccessDeniedController', ['$rootScope', AccessDeniedController]);

    labSiteSPA.controller('AccountController', ['$http', '$rootScope', '$location', '$scope', '$timeout', AccountController]);
    
    labSiteSPA.controller('UserListController', ['$http', '$rootScope', '$modal', '$scope', UserListController]);

    labSiteSPA.controller('CarListController', ['$http', '$rootScope', '$modal', '$scope', CarListController]);

    labSiteSPA.controller('AddressListController', ['$http', '$rootScope', '$modal', '$scope', AddressListController]);

    labSiteSPA.controller('EditUserController', ['$http', '$rootScope', '$scope', '$route', '$routeParams', EditUserController]);

    labSiteSPA.controller('EditPermissionsController', ['$http', '$rootScope', '$scope', '$route', '$routeParams', EditPermissionsController]);

    labSiteSPA.controller('EditCarController', ['$http', '$rootScope', '$scope', '$route', '$routeParams', EditCarController]);

    labSiteSPA.controller('EditAddressController', ['$http', '$rootScope', '$scope', '$route', '$routeParams', EditAddressController]);

    labSiteSPA.controller('ViewPermissionsController', ['$http', '$rootScope', '$scope', '$route', '$routeParams', ViewPermissionsController]);

    //Diretives
    labSiteSPA.directive('loginPartial', LoginPartial);

    labSiteSPA.directive('headerInfo', HeaderInfo);

})();
