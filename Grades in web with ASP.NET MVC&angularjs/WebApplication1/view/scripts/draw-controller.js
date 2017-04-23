define(['math'], function (math) {
    function drawController(UserService,getCoursesService) {
        var scope = this;

        var normalAvg = function (avgArr, exemptArr, semArr) {
            var notExemptArr = new Array();
            var j = 0;
            for (var i = 1; i < semArr.length; i++) {
                if (semArr[i].exempt == false) {
                    notExemptArr[j++] = semArr[i];
                }
            }
            var total = 0;
            var sum = 0;
            for (var i = 0; i < notExemptArr.length; i++) {
                sum += parseFloat(notExemptArr[i].rlvPtn);
                total += (parseFloat(notExemptArr[i].avg) *
                    parseFloat(notExemptArr[i].rlvPtn));
            }
            var avg = (parseFloat(total) / sum).toFixed(2);
            for (var i = 0; i < exemptArr.length; i++) {
                avgArr[exemptArr[i].semester] = avg;
            }
            return avgArr;
        }
        var writerObj = function (border, c, canvas, semesters, semArr, valueArr, title) {
            //build the Box
            c.beginPath();
            c.lineWidth = "0";
            c.shadowColor = "black";
            c.fillStyle = "#FFFFFF";
            c.fillRect(0, border.boxHeight, canvas.width, canvas.height - border.boxHeight);
            //c.rect(border.intervalWG, border.height, canvas.width, canvas.height - border.height));
            c.stroke();

            var N = 7;
            var iIntervalInGraph = 5
            var spaceFromHeight = (border.graphHeight - border.boxHeight) * (border.boxHeight / border.graphHeight);
            var intervalHeight = (canvas.height - border.graphHeight) / N;
            var intervalAvg = (border.maxValue - border.minValue) / N;
            //build the Graphe
            c.beginPath();
            c.lineWidth = "1";
            c.shadowColor = "black";
            c.fillStyle = "gray";
            c.rect(border.intervalWG, border.graphHeight - spaceFromHeight, canvas.width - border.intervalWG, canvas.height - border.graphHeight);
            c.stroke();


            var move = (canvas.height - border.graphHeight) / (border.maxValue - border.minValue);
            c.font = "25px Arial";
            c.fillStyle = "black";
            c.fillText(title, (canvas.width / 2) - (5 * title.length), border.boxHeight - 10);

            for (var i = 0 ; i <= N ; i++) {
                c.beginPath();
                /**grades interval from min to max**/
                c.font = "20px Arial";
                c.fillStyle = "black";
                var avg = (border.minValue + (intervalAvg * i)).toFixed(2);
                var height = (canvas.height - (intervalHeight * i)) - spaceFromHeight;
                c.fillText(avg, 3, height);

                c.strokeStyle = '#32b910';//horizontal lines
                c.moveTo(0, height + 2);
                c.lineTo(canvas.width, height + 3);
                c.stroke();
            }

            for (var i = 1 ; i < semesters; i++) {
                c.beginPath()
                c.strokeStyle = 'red';
                var diff1 = (border.maxValue - valueArr[i]) * move;
                var diff2 = (border.maxValue - valueArr[i + 1]) * move;
                c.moveTo(border.intervalWG * (i), border.graphHeight + diff1 - canvas.height * (1 / 20));
                c.lineTo(border.intervalWG * (i + 1), border.graphHeight + diff2 - canvas.height * (1 / 20));
                c.stroke();
            }

            for (var i = 1 ; i <= semesters; i++) {
                c.beginPath();
                //the blue lines
                c.strokeStyle = '#013ADF';
                c.moveTo(border.intervalWG * i, canvas.height);
                c.lineTo(border.intervalWG * i, border.boxHeight);
                c.stroke();

                // the avgs
                if (semArr[i].exempt == true) {
                    c.beginPath();
                    c.rect(border.intervalWG * i, border.boxHeight,
                        border.intervalWG, spaceFromHeight);
                    c.fillStyle = 'yellow';
                    c.fill();
                    c.stroke();
                }
                c.font = "15px Arial";
                c.fillStyle = "black";
                c.fillText(valueArr[i], (border.intervalWG * i) + 2, border.boxHeight + 10);

            }
        }

        scope.changePaper = function () {
            switch (localStorage.getItem("Graph")) {
                case "grade":
                    scope.writeGradeGraph();
                    localStorage.setItem("Graph", "points");
                    $('#writeContinuousGraph').text("To ponts statistic");
                    break;
                case "points":
                    scope.writePointsGraph();
                    localStorage.setItem("Graph", "coursesNum");
                    $('#writeContinuousGraph').text("To courses number statistic");
                    break;
                case "coursesNum":
                    scope.writeCourseNumberGraph();
                    localStorage.setItem("Graph", "grade");
                    $('#writeContinuousGraph').text("To grades statistic");
                    break;
                default:
                    localStorage.setItem("Graph", "grade");
            }
        }

        scope.drawPaper = function () {
            switch(localStorage.getItem("Graph"))
            {
                case "coursesNum":
                    scope.writePointsGraph();
                    break;
                case "grade":
                    scope.writeCourseNumberGraph();
                    break;
                case "points":
                    scope.writeGradeGraph();
                    break;
                default:
                    localStorage.setItem("Graph", "points");
            }
        }

        UserService.setCP(scope.drawPaper);

        scope.writeGraphe = function () {
            scope.changePaper();
        };

        scope.writeGradeGraph = function () {
            getCoursesService.getCourses().then(function (result) {
                var courses = result.data;
                var semArr = math.toSomeArr(function (course) { return course.Semester }, courses);
                var semesters = Math.max.apply(null, semArr);
                var coursesArr = new Array(semesters);
                for (var i = 0 ; i < semesters ; i++) {
                    coursesArr[i] = new Array();
                }
                var semArr = new Array();
                semArr[0] = -1;
                for (var i = 0; i < semesters ; i++) {
                    semArr[i + 1] = math.SemesterI(courses, i + 1);
                }
                var canvas = document.getElementById("paper");
                if (canvas == null)
                    return;
                var exemptArr = new Array();
                var j = 0;
                for (var i = 1; i < semArr.length; i++) {
                    if (semArr[i].exempt == true) {
                        exemptArr[j++] = semArr[i];
                    }
                }

                var avgArr = math.toSomeArr(function (sem) { return sem.avg }, semArr);
                avgArr = normalAvg(avgArr, exemptArr, semArr);

                canvas.height = (window.innerHeight * 0.4);//$scope.screenH;
                canvas.width = window.innerWidth;//$scope.screenW;
                var c = canvas.getContext('2d');

                var border = (function () {
                    return {
                        "GrapheWidth": canvas.width * 0.8,
                        "intervalWG": (canvas.width * 0.89) / semesters,
                        "boxHeight": canvas.height * 0.1,//90%
                        "graphHeight": canvas.height * 0.2,//80%
                        "intervalWidth": canvas.width / semesters,
                        "minValue": (function () {
                            var min = 101;
                            for (var i = 0; i < avgArr.length ; i++) {
                                min = (parseFloat(avgArr[i]) < parseFloat(min) && parseFloat(avgArr[i]) != -1) ?
                                    parseFloat(avgArr[i]) : parseFloat(min);
                            }
                            return min;
                        })(),
                        "maxValue": (function () {
                            var max = -1;
                            for (var i = 0; i < avgArr.length ; i++) {
                                max = (parseFloat(max) < parseFloat(avgArr[i])) ?
                                    parseFloat(avgArr[i]) : parseFloat(max);
                            }
                            return max;
                        })()
                    };
                })();

                writerObj(border, c, canvas, semesters, semArr, avgArr, "Grade Statistic");

                    });
        }
        scope.writeCourseNumberGraph = function () {
            getCoursesService.getCourses().then(function (result) {
                    var courses = result.data;
                    var semsArr = math.toSomeArr(function (course) { return course.Semester }, courses);
                    var semesters = Math.max.apply(null, semsArr);
                    var semArr = new Array();
                    semArr[0] = -1;
                    for (var i = 0; i < semesters ; i++) {
                        semArr[i + 1] = math.SemesterI(courses, i + 1);
                    }
                    var canvas = document.getElementById("paper");
                    if (canvas == null)
                        return;
                    canvas.height = (window.innerHeight * 0.4);//$scope.screenH;
                    canvas.width = window.innerWidth;//$scope.screenW;
                    var c = canvas.getContext('2d');
                    var border = (function () {
                        return {
                            "GrapheWidth": canvas.width * 0.8,
                            "intervalWG": (canvas.width * 0.89) / semesters,
                            "boxHeight": canvas.height * 0.1,//90%
                            "graphHeight": canvas.height * 0.2,//80%
                            "intervalWidth": canvas.width / semesters,
                            "minValue": (function () {
                                var min = 100;
                                for (var i = 0; i < semArr.length ; i++) {
                                    min = (parseFloat(semArr[i].coursesNumber) < parseFloat(min)) ?
                                            parseFloat(semArr[i].coursesNumber) : parseFloat(min);
                                }
                                return min;
                            })(),
                            "maxValue": (function () {
                                var max = -1;
                                for (var i = 0; i < semArr.length ; i++) {
                                    max = (parseFloat(max) < parseFloat(semArr[i].coursesNumber)) ?
                                        parseFloat(semArr[i].coursesNumber) : parseFloat(max);
                                }
                                return max;
                            })()
                        };
                    })();
                    var ptnArr = math.toSomeArr(function (sem) { return sem.coursesNumber }, semArr);
                    writerObj(border, c, canvas, semesters, semArr, ptnArr, "Courses Statistic");

                });


        }
        scope.writePointsGraph = function () {
            getCoursesService.getCourses().then(function (result) {
                   var courses = result.data;
                   var semsArr = math.toSomeArr(function (course) { return course.Semester }, courses);
                   var semesters = Math.max.apply(null, semsArr);
                   var semArr = new Array();
                   semArr[0] = -1;
                   for (var i = 0; i < semesters ; i++) {
                       semArr[i + 1] = math.SemesterI(courses, i + 1);
                   }
                   var canvas = document.getElementById("paper");
                   if (canvas == null)
                       return;
                   canvas.height = (window.innerHeight * 0.4);//$scope.screenH;
                   canvas.width = window.innerWidth;//$scope.screenW;
                   var c = canvas.getContext('2d');
                   var border = (function () {
                       return {
                           "GrapheWidth": canvas.width * 0.8,
                           "intervalWG": (canvas.width * 0.89) / semesters,
                           "boxHeight": canvas.height * 0.1,//90%
                           "graphHeight": canvas.height * 0.2,//80%
                           "intervalWidth": canvas.width / semesters,
                           "minValue": (function () {
                               var min = 10000;
                               for (var i = 0; i < semArr.length ; i++) {
                                   min = (parseFloat(semArr[i].ptn) < parseFloat(min)) ?
                                           parseFloat(semArr[i].ptn) : parseFloat(min);
                               }
                               return min;
                           })(),
                           "maxValue": (function () {
                               var max = -1;
                               for (var i = 0; i < semArr.length ; i++) {
                                   max = (parseFloat(max) < parseFloat(semArr[i].ptn)) ?
                                       parseFloat(semArr[i].ptn) : parseFloat(max);
                               }
                               return max;
                           })()
                       };
                   })();
                   var ptnArr = math.toSomeArr(function (sem) { return sem.ptn }, semArr);
                   writerObj(border, c, canvas, semesters, semArr, ptnArr, "Points Statistic");

               });



        }
        scope.writeStatistic = function () {
            var canvas = document.getElementById("statistic");
            if (canvas == null)
                return;
            canvas.height = (window.innerHeight * 0.4);//$scope.screenH;
            canvas.width = window.innerWidth;//$scope.screenW;
        }


    }
    return ["drawService","getCoursesService", drawController];
});