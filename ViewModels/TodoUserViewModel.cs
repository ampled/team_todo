using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamTodo.ViewModels
{
    public class TodoUserViewModel
    {
        public int Id { get;set; }
        [Required]
        public string Name { get;set; }        
        public string Email { get;set; }
        public IEnumerable<NestedTodoViewModel> Todos { get;set; }
        
    }
}