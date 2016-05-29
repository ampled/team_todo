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
    [Route("api/todos")]
    public class TodoController : Controller
    {
        private ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }


        // GET api/todos
        [HttpGet]
        public IActionResult Get()
        {
            // return _repository.GetAllWithRelated();
            var results = Mapper.Map<IEnumerable<TodoViewModel>>(_repository.GetAll());
            return new ObjectResult(results);
        }

        // GET api/todos/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<TodoViewModel>(item);
            return new ObjectResult(result);
        }

        // POST api/todos
        [HttpPost]
        public IActionResult Post([FromBody]TodoViewModel item)
        {
            if (ModelState.IsValid)
            {
                var newItem = Mapper.Map<Todo>(item);
                _repository.Add(newItem);
                if (_repository.SaveAll()) {
                    return new CreatedResult("api/todos", Mapper.Map<TodoViewModel>(newItem));
                };                
            }
            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TodoViewModel item)
        {
            if (ModelState.IsValid)
            {
                if (item == null || item.Id != id)
                {
                    return BadRequest();
                }
                var todo = Mapper.Map<Todo>(item);
                _repository.Update(todo);
                if (_repository.SaveAll())
                {
                    return new NoContentResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
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
