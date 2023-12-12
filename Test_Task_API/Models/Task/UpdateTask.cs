using System.ComponentModel.DataAnnotations;

namespace Test_Task_API.Models.Task
{
    public class UpdateTask
    {
        public string? TSK_NAME { get; set; }
        public string? TSK_DESCRIPTION { get; set; }
        public int TSK_PRIORITY { get; set; }
        public DateTime? TSK_END { get; set; }
    }
}
