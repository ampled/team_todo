using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamTodo.Models;
using TeamTodo.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamTodo.Controllers.Api
{
    [Route("api/users")]
    public class TodoUserController : Controller
    {
        private ITodoUserRepository _repository;
        private ITodoRepository _todorepository;

        public TodoUserController(ITodoUserRepository repository, ITodoRepository todorepository)
        {
            _repository = repository;
            _todorepository = todorepository;
        }


        // GET api/users
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(Mapper.Map<IEnumerable<TodoUserViewModel>>(_repository.GetAllWithTodos()));
        }

        // GET api/users/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var item = _repository.GetWithTodos(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<TodoUserViewModel>(item);
            return new ObjectResult(result);
        }
        
        // GET api/users/Pete
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var item = _repository.GetByName(name);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<TodoUserViewModel>(item);
            return new ObjectResult(result);
        }
        
        // get api/users/1/todos
        [HttpGet("{id:int}/todos")]
        public IActionResult GetTodosByUser(int id)
        {
            var item = _repository.GetWithTodos(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(Mapper.Map<IEnumerable<TodoViewModel>>(item.Todos));
        }
        
        // get api/users/Pete/todos
        [HttpGet("{name}/todos")]
        public IActionResult GetTodosByUserName(string name)
        {
            var item = _todorepository.GetByUser(name);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(Mapper.Map<IEnumerable<TodoViewModel>>(item));
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody]TodoUserViewModel item)
        {
            if (ModelState.IsValid) 
            {
                var newUser = Mapper.Map<TodoUser>(item);
                _repository.Add(newUser);
                if (_repository.SaveAll()) {
                    return new CreatedResult("api/todos", Mapper.Map<TodoUserViewModel>(newUser));
                };
            }
            return BadRequest();
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TodoUserViewModel item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var user = Mapper.Map<TodoUser>(item);
            _repository.Update(user);
            if (_repository.SaveAll())
            {
                return new NoContentResult();
            }
            return new NoContentResult();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _repository.Get(id);
            if (item == null)
            {
                return BadRequest();
            }
            _repository.Remove(item);
            _repository.SaveAll();
            return new OkResult();
        }
    }
}
