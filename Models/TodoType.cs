using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamTodo.Models
{
    public class TodoType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Todo> Todos { get;set; } 
    }
}
