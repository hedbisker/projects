require.config({
    paths: {
        'startup': '../scripts/startup',
        'main-module': '../scripts/main-module',
            'jquery':'../scripts/jquery/jquery-1.8.3.min',
            'angular':'../scripts/angular/angular',
            'angular-route': '../scripts/angular/angular-route',
            'setup-controller':'../scripts/setup-controller',
            'setup-config': '../scripts/setup-config',
            'bootstrap-ui': '../scripts/ui-bootstrap.min',
            'bootstrap': '../scripts/bootstrap/bootstrap.min'
    },
        shim:{ 
            'angular':{deps:['jquery']},
            'angular-route':{deps: ['angular']},
            'startup':{deps :['angular','main-module']}, 
            'main-module': { deps: ['angular', 'angular-route', 'bootstrap'] }
        }
});


require(['startup'], function () {
    angular.bootstrap(document, ['startup']);
});