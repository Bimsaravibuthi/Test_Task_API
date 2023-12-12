using System.ComponentModel.DataAnnotations;

namespace Test_Task_API.Models.User
{
    public class UpdateUser
    {
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name should only contains letters")]
        [MaxLength(20)]
        public string? USR_NAME { get; set; }
        [EmailAddress(ErrorMessage = "Enter a valid Email address")]
        [MaxLength(30)]
        public string? USR_EMAIL { get; set; } 
        public string? USR_PASSWORD { get; set; }
        [RegularExpression("^[^\\s]+$", ErrorMessage = "Username should not contains spaces")]
        [MaxLength(20)]
        public string? USR_USERNAME { get; set; }
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string? USR_TPN { get; set; }
        public bool USR_ACTIVESTATUS { get; set; }
        [EnumDataType(typeof(RoleStatus), ErrorMessage = "User status should be 'User' or 'Admin'")]
        public RoleStatus USR_STATUS { get; set; }
    }
}
