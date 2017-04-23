
define(['controler-names'], function (ctrlNames) {
    function loginController($http, $uibModal) {
        var scope = this;
        var loginCtrl = ctrlNames.loginCtrl;
        scope.f = function () {
			$uibModal.open({
				templateUrl: '../template/editCourseModeModal.html',
			});
		}

        scope.alertRun = function (msg, newClass) {
            $("#login_alert").removeClass();
            $("#login_alert").addClass(newClass);
            $("#alert_msg").html(msg);
            $("#login_alert").show("slow", function () {});
            setTimeout(function () {
                $("#login_alert").hide( "slow", function(){});
            }, 10000);
        }


        scope.submit = function (user, pass) {
            $http.post("/../" + loginCtrl.loginName + "/" + loginCtrl.saveUser + "?user=" + user + "&pass=" + pass).
				success(function (data) {
				    if (data == 1) {
				        scope.user = user;
				        scope.alertRun("You loged in successful", "alert alert-success");
				    } else {
				        scope.alertRun("one or more of your data are incorrect", "alert alert-danger");
				    }
				});
		}

        scope.register = function (user, pass) {
            $http.post("/../" + loginCtrl.loginName + "/" + loginCtrl.registerNewAccount + "?user=" + user + "&pass=" + pass).
				success(function (data) {
					if (data != -1) {
						alert("Your account is registered successfully")
					} else {
						alert("This account is already exist");
					}
				})
		}
	}
return ["$http","$uibModal",loginController];
});
