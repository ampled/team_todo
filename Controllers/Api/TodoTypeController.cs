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
    [Route("api/types")]
    public class TodoTypeController : Controller
    {
        private ITodoTypeRepository _repository;

        public TodoTypeController(ITodoTypeRepository repository)
        {
            _repository = repository;
        }


        // GET api/types
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(Mapper.Map<IEnumerable<TodoTypeViewModel>>(_repository.GetAllWithTodos()));
        }

        // GET api/types/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<TodoTypeViewModel>(item);
            return new ObjectResult(result);
        }
        
        // GET api/types/5/todos
        [HttpGet("{id}/todos")]
        public IActionResult GetTodosByType(int id)
        {
            var item = _repository.GetWithTodos(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(Mapper.Map<IEnumerable<TodoViewModel>>(item.Todos));
        }

        // POST api/types
        [HttpPost]
        public IActionResult Post([FromBody]TodoTypeViewModel item)
        {
            if (ModelState.IsValid) 
            {
                var newType = Mapper.Map<TodoType>(item);
                _repository.Add(newType);
                if (_repository.SaveAll()) {
                    
                }
                return new CreatedResult("api/users", Mapper.Map<TodoTypeViewModel>(newType));
            }
            return BadRequest();
        }

        // PUT api/types/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TodoTypeViewModel item)
        {
            if (ModelState.IsValid)
            {
                if (item == null || item.Id != id)
                {
                    return BadRequest();
                }
                var type = Mapper.Map<TodoType>(item);
                _repository.Update(type);
                if (_repository.SaveAll())
                {
                    return new NoContentResult();
                }
                return new NoContentResult();
            }
            return new NoContentResult();
        }

        // DELETE api/types/5
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
