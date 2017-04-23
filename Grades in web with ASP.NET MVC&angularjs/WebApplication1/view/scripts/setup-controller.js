define(['controler-names'], function (ctrlNames) {
    'use strict';
    function setupController($http) {
        var scope = this;
        var loginCtrl = ctrlNames.loginCtrl;
        document.getElementById("navHome").classList.add("active");

        scope.logout = function () {
            $http.post("/../" + loginCtrl.loginName + "/" + loginCtrl.logout).
				success(function (data) {
				    scope.user = "";
				    scope.course = "";
					location.reload();
				})
        }

        scope.activeRefresh = function (id) {
			document.getElementById("navHome").classList.remove("active");
			document.getElementById("navRegister").classList.remove("active");
			document.getElementById("navLogin").classList.remove("active");
			document.getElementById("navCourse").classList.remove("active");
			document.getElementById("navDraw").classList.remove("active");
			document.getElementById(id).classList.add("active");
			scope.initUser();
		}

        scope.initUser = function () {
            $http.post("/../" + loginCtrl.loginName + "/" + loginCtrl.getUserName).
				success(function (data) {
				    scope.user = data;
				})
		}
	}
    return ['$http', setupController];
});