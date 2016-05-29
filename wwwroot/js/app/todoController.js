// todoController.js
(function () {

    "use strict";

    angular.module("app-todos").controller("todoController", todoController);

    function todoController($http, $filter) {

        var vm = this;

        vm.todos = []
        vm.users = []
        vm.types = []

        vm.newTodo = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/todos")
            .then(function (response) {
                angular.copy(response.data, vm.todos);
            }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        $http.get("/api/users")
            .then(function (response) {
                angular.copy(response.data, vm.users);
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

            var newTodo = { "name": "New todo" }

            $http.post("/api/todos", newTodo)
                .then(function (response) {
                    console.log(response.data);
                    vm.todos.push(response.data);
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
                    var index = vm.todos.indexOf(todo)
                    vm.todos.splice(index, 1)
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
            var selected = $filter('filter')(vm.users, { name: todo.user })
            return (todo.name && selected.length) ? selected[0].name : 'No user'
        }

        vm.setComplete = function (todo, status) {
            todo.isComplete = status;
            var index = vm.todos.indexOf(todo);
            console.log(status);
            vm.updateTodo(todo);
            vm.todos[index].isComplete = status;
        }
        
        vm.modalTodo = {}
        vm.openDescModal = function (todo) {
            vm.modalTodo = todo;
            $('#todoDescModal').modal('show');
        }
        
        vm.setDescription = function (todo) {
            console.log(vm.todos.indexOf(vm.modalTodo))
            var index = vm.todos.indexOf(todo);
            console.log(index);
            vm.todos[index].description = todo.description;
            vm.updateTodo(vm.todos[index]);
        }

    };

})();