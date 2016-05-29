using System.ComponentModel.DataAnnotations.Schema;

namespace TeamTodo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get;set; }
        public bool IsComplete { get;set; }
        
        
        public int TodoTypeId { get;set; }
        public TodoType TodoType { get;set; }
        // String field to work around viewmodel not correctly returning TodoTypeId
        public string Type { get;set; }
        
        public int TodoUserId { get; set; }
        public TodoUser TodoUser { get; set; }
        // String field to work around viewmodel not correctly returning TodoUserId
        public string User { get;set; }

    }
}
