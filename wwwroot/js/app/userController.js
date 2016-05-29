// userController.js
(function () {

    "use strict";

    angular.module("app-todos").controller("userController", userController);

    function userController($http, $filter) {

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

        vm.addUser = function () {
            // use this function with a form bound to vm.newTodo

            vm.isBusy = true;

            $http.post("/api/users", vm.newUser)
                .then(function (response) {
                    vm.users.push(response.data);
                    vm.newUser = {};
                }, function () {
                    vm.errorMessage = "Failed to add new user";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };

        vm.addBlankUser = function () {
            vm.isBusy = true;

            var newUser = { "name": "New user" }

            $http.post("/api/users", newUser)
                .then(function (response) {
                    console.log(response.data);
                    vm.users.push(response.data);
                }, function () {
                    vm.errorMessage = "Failed to add new user";
                    console.log(vm.errorMessage)
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        vm.removeUser = function (user) {
            vm.isBusy = true;

            $http.delete("/api/users/" + user.id)
                .then(function (response) {
                    var index = vm.users.indexOf(user)
                    vm.users.splice(index, 1)
                }, function () {
                    vm.errorMessage = "Failed to remove user";
                })
                .finally(function () {
                    vm.isBusy = false;
                })
        };

        vm.updateUser = function (user) {
            vm.isBusy = true;

            $http.put("/api/users/" + user.id, user)
                .then(function (response) {

                }, function () {
                    vm.errorMessage = "Failed to update user";
                    console.log(vm.errorMessage)
                })
                .finally(function () {
                    vm.isBusy = false;
                })
        };
        
        
        vm.showUserTodosCount = function (user) {
            var todos = $filter('filter')(vm.todos, { user: user.name })
            return todos.length
        }
        

        vm.showCompleteTodosCount = function (user) {
            var completedItems = $filter('filter')(vm.todos, { user: user.name, isComplete: true })
            return completedItems.length
        }
        
        vm.showIncompleteTodosCount = function (user) {
            var incompleteItems = $filter('filter')(vm.todos, { user: user.name, isComplete: false })
            return incompleteItems.length
        }

        vm.setComplete = function (todo, status) {
            todo.isComplete = status;
            var index = vm.todos.indexOf(todo)
            console.log(status)
            vm.updateTodo(todo)
            vm.todos[index].isComplete = status;
        }

    };

})();