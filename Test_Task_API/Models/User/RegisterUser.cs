using System.ComponentModel.DataAnnotations;

namespace Test_Task_API.Models.User
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="Name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage ="Name should only contains letters")]
        [MaxLength(20)]
        public string? USR_NAME { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Enter a valid Email address")]
        [MaxLength(30)]
        public string? USR_EMAIL { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? USR_PASSWORD { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[^\\s]+$", ErrorMessage ="Username should not contains spaces")]
        [MaxLength(20)]
        public string? USR_USERNAME { get; set; }
        [Phone(ErrorMessage ="Phone number is not valid")]
        public string? USR_TPN { get; set; }
        public bool USR_ACTIVESTATUS { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(RoleStatus), ErrorMessage ="User status should be 'User' or 'Admin'")]
        public RoleStatus USR_STATUS { get; set; }
    }

    public enum RoleStatus
    {
        Admin,
        User
    }
}
