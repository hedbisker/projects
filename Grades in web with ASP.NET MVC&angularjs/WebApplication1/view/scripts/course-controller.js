
define(['controler-names'], function (ctrlNames) {
    function courseController($http, getCoursesService, $modal) {
        var scope = this;
        var coursesCtrl = ctrlNames.coursesCtrl;
        scope.makeAvrg = function () {
            $http.post("/../" + coursesCtrl.coursesName + "/" + coursesCtrl.getAverage).
           success(function (data) {
               if (data != null) {
                   document.getElementById("average").innerText = data.average;
                   document.getElementById("points").innerText = data.points;
               } else {
                   document.getElementById("average").innerText = document.getElementById("points").innerText = "---";
               }
           })
        }

        scope.deleteCourse = function (name) {
            if (confirm("you sure you want to delete this " + name + " course?") == true) {
                $http.post("/../" + coursesCtrl.coursesName + "/" + coursesCtrl.deleteCourse, { "name": name }).
              success(function (data) {
                  scope.makeAvrg();
                  scope.fetchData();
                  alert("Your course deleted successfully");
              })
            } else
                alert("it did nothing");
        }

        scope.editCourse = function (name, grade, points, semester ,exempt) {
            $http.post("/../" + coursesCtrl.coursesName + "/" + coursesCtrl.overrideCourse, {
                "oldName": scope.oldName, "name": name, "grade": grade, "points": points, "semester": semester, "exempt": exempt
            }).success(function (data) {
               scope.makeAvrg();
               scope.fetchData();
               alert("You Edited your course successfully");
           });
        }

        scope.editMode = function (name) {
            scope.editCourses = scope.courses.filter(function (course) {
                return course.Name === name
            });
            scope.oldName = scope.editCourses[0].Name;
        }

        scope.addCourseByEnter = function ($event, name, grade, points, semester, exempt) {
            if (13 == $event.keyCode) {
                scope.addCourse(name, grade, points, semester, exempt);
            }

        }
        scope.addCourse = function (name, grade, points, semester, exempt) {
            $http.post("/../" + coursesCtrl.coursesName + "/" + coursesCtrl.addCourse, { "name": name, "grade": grade, "points": points, "semester": semester, "exempt": exempt }).
            success(function (data) {
                if (data != -1) {
                    scope.makeAvrg();
                    scope.fetchData();
                    alert("You added your course successfully");
                } else {
                    alert("The course is already exist");
                }
            })
        }
        scope.fetchData = function () {
            getCoursesService.getCourses().then(function (result) {
                scope.courses = result.data;
                scope.data = {
                    repeatSelect: null,
                    availableOptions: [
                        { id: '1', name: 'Name' },
                        { id: '2', name: 'Grade' },
                        { id: '3', name: 'Points' },
                        { id: '4', name: 'Semester' },
                        { id: '5', name: 'Exempt' }
                    ],
                };
            });
        }
    }
    return ["$http", "getCoursesService", courseController];
});