var HeaderInfo = function () {
    return {
        restrict: 'E',
        templateUrl: 'Scripts/Templates/HeaderInfo.html',
        controller: HeaderController,
        controllerAs: 'headerCtrl',
        scope: true,
        bindToController: true
    };
};

var LoginPartial = function () {
    return {
        restrict: 'E',
        templateUrl: 'Scripts/Templates/LoginPartial.html',
        controller: AccountController,
        controllerAs: 'userCtrl'
    };
};