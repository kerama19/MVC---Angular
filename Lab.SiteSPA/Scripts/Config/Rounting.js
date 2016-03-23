var Routing = function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/UsersList', { templateUrl: '/User/UserList', controller: 'UserListController', controllerAs: 'userCtrl' })
        .when('/EditUser/:UserName', { templateUrl: '/User/EditUser', controller: 'EditUserController' })
        .when('/AddUser', { templateUrl: '/User/AddUser', controller: 'EditUserController' })
        .when('/ViewPermissions/:UserName', { templateUrl: '/Permission/ViewPermissions', controller: 'ViewPermissionsController' })
        .when('/PermissionsList', { templateUrl: '/Permission/ViewPermissionsList', controller: 'ViewPermissionsController' })
        .when('/EditPermissions/:UserName/:Function/:From', { templateUrl: '/Permission/EditPermissions', controller: 'EditPermissionsController' })
        .when('/CarsList', { templateUrl: '/Car/CarList', controller: 'CarListController' })
        .when('/EditCar/:Id', { templateUrl: '/Car/EditCar', controller: 'EditCarController' })
        .when('/AddCar', { templateUrl: '/Car/AddCar', controller: 'EditCarController' })
        .when('/AddressesList', { templateUrl: '/Address/AddressList', controller: 'AddressListController' })
        .when('/EditAddress/:Id', { templateUrl: '/Address/EditAddress', controller: 'EditAddressController' })
        .when('/AddAddress', { templateUrl: '/Address/AddAddress', controller: 'EditAddressController' })
        .when('/AccessDenied', { templateUrl: '/Account/AccessDenied', controller: 'AccessDeniedController' });
    $locationProvider.html5Mode(false).hashPrefix('!');
};