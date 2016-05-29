// userDetailController.js
(function () {

    "use strict";

    angular.module("app-todos").controller("userDetailController", userDetailController);

    function userDetailController($http, $filter, $routeParams) {

        var vm = this;
        vm.userName = $routeParams.userName;
        
        vm.todos = []
        vm.user = []
        vm.userTodos = []
        vm.types = []

        vm.newUser = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/todos")
            .then(function (response) {
                angular.copy(response.data, vm.todos);
                vm.userTodos = $filter('filter')(vm.todos, { user: vm.userName })
            }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        $http.get("/api/users/" + vm.userName)
            .then(function (response) {
                angular.copy(response.data, vm.user);
            }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        $http.get("/api/types")
            .then(function (response) {
                angular.copy(response.data, vm.types);
            }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTodo = function () {
            // use this function with a form bound to vm.newTodo

            vm.isBusy = true;

            $http.post("/api/todos", vm.newTodo)
                .then(function (response) {
                    vm.todos.push(response.data);
                    vm.newTodo = {};
                }, function () {
                    vm.errorMessage = "Failed to save new todo";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.addBlankTodo = function () {
            vm.isBusy = true;

            var newTodo = { "name": "New todo", "user": vm.user.name }

            $http.post("/api/todos", newTodo)
                .then(function (response) {
                    console.log(response.data);
                    vm.userTodos.push(response.data);
                }, function () {
                    vm.errorMessage = "Failed to add new todo";
                    console.log(vm.errorMessage)
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        vm.removeTodo = function (todo) {
            vm.isBusy = true;

            $http.delete("/api/todos/" + todo.id)
                .then(function (response) {
                    var index = vm.userTodos.indexOf(todo)
                    vm.userTodos.splice(index, 1)
                }, function () {
                    vm.errorMessage = "Failed to remove todo";
                })
                .finally(function () {
                    vm.isBusy = false;
                })
        };

        vm.updateTodo = function (todo) {
            vm.isBusy = true;

            $http.put("/api/todos/" + todo.id, todo)
                .then(function (response) {

                }, function () {
                    vm.errorMessage = "Failed to update todo";
                    console.log(vm.errorMessage)
                })
                .finally(function () {
                    vm.isBusy = false;
                })
        };

        vm.showType = function (todo) {
            var selected = $filter('filter')(vm.types, { name: todo.type })
            return (todo.type && selected.length) ? selected[0].name : 'Uncategorized';
        }

        vm.showUser = function (todo) {
            return vm.user.name
        }

        vm.setComplete = function (todo, status) {
            todo.isComplete = status;
            var index = vm.userTodos.indexOf(todo)
            console.log(status)
            vm.updateTodo(todo)
            vm.userTodos[index].isComplete = status;
        }
        
        vm.modalTodo = {}
        vm.openDescModal = function (todo) {
            vm.modalTodo = todo;
            $('#todoDescModal').modal('show');
        }
        
        vm.setDescription = function (todo) {
            var index = vm.userTodos.indexOf(todo);
            vm.userTodos[index].description = todo.description;
            vm.updateTodo(vm.userTodos[index]);
        }

    };

})();