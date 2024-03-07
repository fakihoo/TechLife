using System.ComponentModel.DataAnnotations;

namespace TechLife.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoListId { get; set; }
        [Required]
        public string ToDoListText { get; set; }
        public int ToDoListDuration { get; set; }
    }
}
