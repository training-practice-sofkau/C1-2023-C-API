using System.ComponentModel.DataAnnotations;

namespace tasks.Models
{
    public class UpdateTaskRequest
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string CreatedBy { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
    }
}
