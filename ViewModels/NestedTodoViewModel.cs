using System;
using System.ComponentModel.DataAnnotations;

namespace TeamTodo.ViewModels
{
    public class NestedTodoViewModel
    {
        public int Id { get;set; }
        
        [Required]
        public string Name { get;set; }
        
        public string Description { get;set; }
        public bool IsComplete { get;set; } = false;
        
    }
}