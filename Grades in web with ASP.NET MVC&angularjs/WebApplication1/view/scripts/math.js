define([], function () {
    var SemesterI = function (arr, j) {
        ret = { "semester": j, "avg": 0, "ptn": 0, "rlvPtn":0, "exempt": true,"coursesNumber":0 };
        var gradeSum = 0, ptn = 0, rlvPtn = 0;
        for (var i = 0 ; i < arr.length ; i++) {
            if (arr[i].Semester == j) {
                ret.coursesNumber++;
                ret.ptn += parseFloat(arr[i].Points);
                if (arr[i].Exempt == false) {
                    gradeSum += (parseFloat(arr[i].Points) * parseInt(arr[i].Grade));
                    ret.rlvPtn += parseFloat(arr[i].Points);
                    ret.exempt = false;
                }
            }
        }
        if (ret.rlvPtn == 0)
            ret.avg = 0;
        else {
            ret.avg = (gradeSum / ret.rlvPtn).toFixed(2);
        }return ret;
    }

    var toSomeArr = function (type, arr) {
        var someArr = new Array();
        for (var i = 0; i < arr.length; i++) {
            someArr[i] = type(arr[i]);
        }
        return someArr;
    }

    var dataSaver = function (name, grade, points, semester) {
        var ret = {
            "code": true,
            "what": "The course update correctly"
        };
        if (name == "") {
            ret = {
                "code": false,
                "what": "The name you entered is illegal"
            };
        }
        if ((isNaN(parseFloat(grade)) && ((parseFloat(grade) % parseInt(grade)) != 0)) ||
               (parseInt(grade) > 100) || (parseInt(grade) < 0)) {
            ret = {
                "code": false,
                "what": "The grade you entered is must be a integer not negative and not higher then 100"
            };
        }

        if (isNaN(parseFloat(points)) || (parseFloat(points) < 0)) {
            ret = {
                "code": false,
                "what": "The points you entered is must be number not negative"
            };
        }
        if (isNaN(parseFloat(semester)) && ((parseFloat(semester) % parseInt(semester)) != 0) ||
                 (parseInt(semester) > 14) || (parseInt(semester) < 0)) {
            ret = {
                "code": false,
                "what": "The semester you entered is must be not negative and lower then 14"
            };
        }
        return ret;
    }

    var makeAvg = function (courses, F) {
        var gradeSum = 0, ptn = 0, rlvPtn = 0;
        for (var i = 0 ; i < courses.length ; i++) {
            ptn += parseFloat(courses[i].Points);
            if (courses[i].Exempt == false) {
                gradeSum += (parseFloat(courses[i].Points) * parseInt(courses[i].Grade));
                rlvPtn += parseFloat(courses[i].Points);
            }
        }
        F((gradeSum / rlvPtn).toFixed(2), parseFloat(ptn));
    }
    return { "SemesterI": SemesterI, "toSomeArr": toSomeArr, "dataSaver": dataSaver, "makeAvg": makeAvg };

});