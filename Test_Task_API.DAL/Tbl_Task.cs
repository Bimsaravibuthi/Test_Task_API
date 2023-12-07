using System.ComponentModel.DataAnnotations;

namespace Test_Task_API.DAL
{
    public class Tbl_Task
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? TSK_NAME { get; set; }
        public string? TSK_DESCRIPTION { get; set; }
        public int TSK_PRIORITY {  get; set; }
        [Required]
        public DateTime? TSK_CREATED { get; set;}
        public DateTime? TSK_END { get; set;}
        [Required]
        public Tbl_User? User { get; set; }
    }
}
