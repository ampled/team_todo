using System;
using System.ComponentModel.DataAnnotations;

namespace TeamTodo.ViewModels
{
    public class TodoViewModel
    {
        public int Id { get;set; }
        
        [Required]
        public string Name { get;set; }
        public string Description { get;set; }
        public bool IsComplete { get;set; } = false;        

        public String Type { get;set; }
        public String User { get;set; }
        
        // public int TodoTypeId { get;set; } 
    }
}