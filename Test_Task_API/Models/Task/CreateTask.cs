using System.ComponentModel.DataAnnotations;

namespace Test_Task_API.Models.Task
{
    public class CreateTask
    {
        [Required(ErrorMessage ="Task name is required")]
        public string? TSK_NAME { get; set; }
        public string? TSK_DESCRIPTION { get; set; }
        [Required(ErrorMessage ="Priority is required")]
        public int TSK_PRIORITY { get; set; }
        public DateTime? TSK_END { get; set; }
        [Required(ErrorMessage ="User Id is required")]
        public int USR_ID { get; set; }
    }
}
