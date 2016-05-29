// typeController.js
(function () {

    "use strict";

    angular.module("app-todos").controller("typeController", typeController);

    function typeController($http, $filter) {

        var vm = this;

        vm.todos = []
        vm.users = []
        vm.types = []

        vm.newType = {};

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

        vm.addType = function () {
            // use this function with a form bound to vm.newType

            vm.isBusy = true;

            $http.post("/api/types", vm.newType)
                .then(function (response) {
                    vm.types.push(response.data);
                    vm.newType = {};
                }, function () {
                    vm.errorMessage = "Failed to save new todo";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.addBlankType = function () {
            vm.isBusy = true;

            var newType = { "name": "New category" }

            $http.post("/api/types", newType)
                .then(function (response) {
                    console.log(response.data);
                    vm.types.push(response.data);
                }, function () {
                    vm.errorMessage = "Failed to add new type";
                    console.log(vm.errorMessage)
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
        
        vm.addBlankTodo = function (type) {
            vm.isBusy = true;

            var newTodo = { "name": "New item", "type": type.name }

            $http.post("/api/todos", newTodo)
                .then(function (response) {
                    console.log(response.data);
                    vm.todos.push(response.data);
                }, function () {
                    vm.errorMessage = "Failed to add new type";
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

        vm.removeType = function (type) {
            vm.isBusy = true;

            $http.delete("/api/types/" + type.id)
                .then(function (response) {
                    var index = vm.types.indexOf(type)
                    vm.types.splice(index, 1)
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

        vm.updateType = function (type) {
            vm.isBusy = true;

            $http.put("/api/types/" + type.id, type)
                .then(function (response) {

                }, function () {
                    vm.errorMessage = "Failed to update todo";
                    console.log(vm.errorMessage)
                })
                .finally(function () {
                    vm.isBusy = false;
                })
        };
        
        vm.typeTodos = function (type) {
            return $filter('filter')(vm.todos, { type: type.name})
        }

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
            var index = vm.todos.indexOf(todo)
            console.log(status)
            vm.updateTodo(todo)
            vm.todos[index].isComplete = status;
        }
        
        vm.modalTodo = {}
        vm.openDescModal = function (todo) {
            vm.modalTodo = todo;
            $('#todoDescModal').modal('show');
        }
        
        vm.setDescription = function (todo) {
            var index = vm.todos.indexOf(todo);
            vm.todos[index].description = todo.description;
            vm.updateTodo(vm.todos[index]);
        }

    };

})();