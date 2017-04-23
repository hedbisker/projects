define(['controler-names'], function (ctrlNames) {
    var coursesCtrl = ctrlNames.coursesCtrl;
    function getCoursesService($http) {
        return {
            getCourses: function () {
                var promise = $http.post("/../" + coursesCtrl.coursesName + "/" + coursesCtrl.fetchCourses).
                    success(function (res) {
                        return res.data;
                    });
                return promise;
            }
        };
    }
    return ['$http', getCoursesService];
})
