using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTodo.Models
{
    public interface ITodoUserRepository
    {
        IEnumerable<TodoUser> GetAll();
        IEnumerable<TodoUser> GetAllWithTodos();
        TodoUser Get(int id);
        TodoUser GetByName(string name);
        TodoUser GetWithTodos(int id);
        void Add(TodoUser newUser);
        void Update(TodoUser user);
        void Remove(TodoUser user);
        bool SaveAll();
    }
    
    public interface ITodoTypeRepository
    {
        IEnumerable<TodoType> GetAll();
        IEnumerable<TodoType> GetAllWithTodos();
        
        TodoType GetWithTodos(int id);
        TodoType Get(int id);
        void Add(TodoType newType);
        void Update(TodoType user);
        void Remove(TodoType user);
        bool SaveAll();
    }
    
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        IEnumerable<Todo> GetAllWithRelated();
        IEnumerable<Todo> GetByUser(string username);
        Todo Get(int id);
        void Add(Todo newTodo);
        void Update(Todo item);
        void Remove(Todo item);
        bool SaveAll();
    }
}
