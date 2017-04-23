define([],function() {
    function courseDirective(){
        return {
            scope: {
                course: '=',
            },
            restrict: 'EA',
            templateUrl: "../template/courseRowTemplete.html"
        };
    }
    return [courseDirective];
});