using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TeamTodo.Models
{
    public class TodoUserRepository : ITodoUserRepository
    {
        private TodoContext _context;
        
        public TodoUserRepository(TodoContext context)
        {
            _context = context;
        }
        
        public void Add(TodoUser newUser)
        {
            _context.TodoUsers.Add(newUser);
        }

        public TodoUser Get(int id)
        {
            return _context.TodoUsers.FirstOrDefault(user => user.Id == id);
        }
        
        public TodoUser GetByName(string name) {
            return _context.TodoUsers.Where(tu => tu.Name == name).FirstOrDefault();
        }
        
        public TodoUser GetWithTodos(int id) {
            return _context.TodoUsers.Include(tu => tu.Todos)
                                     .Where(tu => tu.Id == id)
                                     .FirstOrDefault();
        }

        public IEnumerable<TodoUser> GetAll()
        {
            return _context.TodoUsers.ToList();
        }
        
        public IEnumerable<TodoUser> GetAllWithTodos() {
            var allTodos = _context.Todos;
            
            return _context.TodoUsers
                .Include(tu => tu.Todos)
                .ToList();
        }

        public void Remove(TodoUser user)
        {
            _context.TodoUsers.Remove(user);
        }

        public bool SaveAll()
        {
            // returns true if rows saved/updated is more than 0
            return _context.SaveChanges() > 0;
        }

        public void Update(TodoUser user)
        {
            _context.TodoUsers.Update(user);
        }
    }
    
    public class TodoTypeRepository : ITodoTypeRepository
    {
        private TodoContext _context;
        
        public TodoTypeRepository(TodoContext context)
        {
            _context = context;
        }
        
        public void Add(TodoType newType)
        {
            _context.TodoTypes.Add(newType);
        }

        public TodoType Get(int id)
        {
            return _context.TodoTypes.FirstOrDefault(type => type.Id == id);
        }
        
        public IEnumerable<TodoType> GetAllWithTodos() {
            return _context.TodoTypes
                .Include(tt => tt.Todos)
                .ToList();
        }
        
        public TodoType GetWithTodos(int id) {
            return _context.TodoTypes.Include(tt => tt.Todos)
                                     .Where(tt => tt.Id == id)
                                     .FirstOrDefault();
        }
        

        public IEnumerable<TodoType> GetAll()
        {
            return _context.TodoTypes.ToList();
        }

        public void Remove(TodoType type)
        {
            _context.TodoTypes.Remove(type);
        }

        public bool SaveAll()
        {
            // returns true if rows saved/updated is more than 0
            return _context.SaveChanges() > 0;
        }

        public void Update(TodoType type)
        {
            _context.TodoTypes.Update(type);
        }
    }
    
    public class TodoRepository : ITodoRepository
    {
        private TodoContext _context;
        
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        
        public void Add(Todo newTodo)
        {
            _context.Todos.Add(newTodo);
        }

        public Todo Get(int id)
        {
            return _context.Todos.FirstOrDefault(user => user.Id == id);
        }
        
        public IEnumerable<Todo> GetAllWithRelated() 
        {  
            return _context.Todos
                .Include(t => t.TodoType)
                .Include(t => t.TodoUser)
                .ToList();   
        }
        
        public IEnumerable<Todo> GetByUser(string username) {
            return _context.Todos.Where(t => t.User == username);
        }
        

        public IEnumerable<Todo> GetAll()
        {
            return _context.Todos.ToList();
        }

        public void Remove(Todo item)
        {
            _context.Todos.Remove(item);
        }

        public bool SaveAll()
        {
            // returns true if rows saved/updated is more than 0
            return _context.SaveChanges() > 0;
        }

        public void Update(Todo item)
        {
            _context.Todos.Update(item);
        }
    }
}
