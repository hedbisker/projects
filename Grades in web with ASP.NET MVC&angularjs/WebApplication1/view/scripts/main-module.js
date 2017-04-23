define(['bootstrap-ui', 'setup-config', 'setup-controller', 'login-controller', 'course-controller', 'course-directive', 'draw-service','get-courses-service','draw-controller']
    , function (bu,setupConf, setupCtrl, loginCtrl, courseCtrl,courseDir,drawService,getCoursesService,drawCtrl) {
        return angular.module('main-module', ['ui.bootstrap', 'ngRoute'])
        .config(setupConf)
        .service("drawService", drawService)
        .service("getCoursesService", getCoursesService)
        .controller("drawController", drawCtrl)
        .controller("setupController", setupCtrl)
        .controller("loginController", loginCtrl)
        .controller("courseController", courseCtrl)
        .directive("courseDirective", courseDir);
});