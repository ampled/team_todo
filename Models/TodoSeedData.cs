using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTodo.Models
{
    public class TodoSeedData
    {
        private TodoContext _context;
        public TodoSeedData(TodoContext context)
        {
            _context = context;
        }

        public void EnsureSeedData()
        {

            if (!_context.TodoUsers.Any())
            {
                _context.TodoUsers.Add(new TodoUser()
                {
                    Name = "John",
                    Email = "john@teamto.do"
                });
                _context.TodoUsers.Add(new TodoUser()
                {
                    Name = "Paul",
                    Email = "paul@teamto.do"
                });
                _context.TodoUsers.Add(new TodoUser()
                {
                    Name = "George",
                    Email = "george@teamto.do"
                });
                _context.TodoUsers.Add(new TodoUser()
                {
                    Name = "Ringo",
                    Email = "ringo@teamto.do"
                });

                _context.SaveChanges();
            }

            /*  
            This correctly set the foreign key on the nested todo-items to the user,
            but the TodoViewModel will not correctly return the foreign key when using AutoMapper.

            var pete = new TodoUser() {
                Name = "Pete",
                Email = "pete@teamto.do",
                Todos = new List<Todo> {
                    new Todo() { Name="Create VM", User="Pete", Type="Deployment" }
                }
            };

            _context.Add(pete);
            _context.AddRange(pete.Todos);
           */

            if (!_context.TodoTypes.Any())
            {
                var backend = new TodoType()
                {
                    Name = "Backend",
                    //Todos = new List<Todo>()
                    //{
                    //    new Todo() { Name="Set up models", Type="Backend" },
                    //    new Todo() { Name="Create a DbContext", Type="Backend" }
                    //}
                };

                _context.TodoTypes.Add(backend);
                //_context.Todos.AddRange(backend.Todos);

                _context.TodoTypes.Add(new TodoType()
                {
                    Name = "Frontend"
                });

                _context.SaveChanges();

            }

            if (!_context.Todos.Any())
            {
                _context.Todos.Add(new Todo() { Name = "Set up models", Type = "Backend", User = "Ringo", IsComplete = true });
                _context.Todos.Add(new Todo() { Name = "Create a DbContext", Type = "Backend", User = "Ringo", IsComplete = true });
                _context.Todos.Add(new Todo() { Name = "Ensure unique usernames", Type = "Backend", User = "George" });

                _context.Todos.Add(new Todo() { Name = "Create controller for users", Type = "Frontend", User = "Paul", IsComplete = true });
                _context.Todos.Add(new Todo()
                {
                    Name = "Validate usernames",
                    Description = "Usernames should be longer than three characters and contain only regular characters and numbers",
                    Type = "Frontend",
                    User = "John"
                });

                _context.Todos.Add(new Todo() { Name = "Create a login-view", User = "Paul" });

                _context.SaveChanges();

            }

        }
    }
}
