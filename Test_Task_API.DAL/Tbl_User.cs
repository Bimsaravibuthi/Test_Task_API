using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Task_API.DAL
{
    public class Tbl_User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(20)")]
        public string? USR_NAME { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(30)")]
        public string? USR_EMAIL { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(MAX)")]
        public string? USR_PASSWORD { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(20)")]
        public string? USR_USERNAME {  get; set; }
        [Column(TypeName ="nvarchar(12)")]
        public string? USR_TPN {  get; set; }
        public bool USR_ACTIVESTATUS { get; set; } = false;
        [Required]
        [Column(TypeName ="nvarchar(5)")]
        public string? USR_STATUS { get; set; }
        [Required]
        public DateTime? USR_CREATED { get; set; }
        public List<Tbl_Task>? Tasks { get; set; }
    }
}
