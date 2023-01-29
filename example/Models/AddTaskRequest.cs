using System.ComponentModel.DataAnnotations;

namespace tasks.Models
{
    public class AddTaskRequest
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string Priority { get; set; }
    }
}
