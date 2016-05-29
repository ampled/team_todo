// app-todos.js
var app = angular.module("app-todos", ["ngRoute", "simpleControls", "xeditable"]);

app.run(function(editableOptions) {
    editableOptions.theme = 'bs3'
});

app.config(function($routeProvider) {
    $routeProvider.when("/", {
                controller: "todoController",
                controllerAs: "vm",
                templateUrl: "/views/todoView.html"
            });
            
            $routeProvider.when("/users", {
                controller: "userController",
                controllerAs: "vm",
                templateUrl: "/views/userView.html"
            });
            
            $routeProvider.when("/users/:userName", {
                controller: "userDetailController",
                controllerAs: "vm",
                templateUrl: "/views/userDetailView.html" 
            });
            
            $routeProvider.when("/categories", {
                controller: "typeController",
                controllerAs: "vm",
                templateUrl: "/views/typeView.html"
            })
            
            $routeProvider.otherwise({ redirectTo: "/" })
});