using Test_Task_API.DAL;
using Test_Task_API.BOL;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Test_Task_API.BLL
{
    public class UserLogic
    {
        private readonly DatabaseContext _dbContext;
        private readonly HttpStatus _httpStatus;
        public UserLogic() 
        {
            _dbContext = new();
            _httpStatus = new();
        }

        public Status? UserLogin(string Username, string Password)
        {
            try
            {
                var result = _dbContext?.Tbl_Users?.FirstOrDefault(c => c.USR_USERNAME == Username);
                if (result != null)
                {
                    if (result.USR_PASSWORD == Password)
                    {
                        if (result.USR_ACTIVESTATUS)
                        {
                            string[] user =
                            [
                                result.USR_USERNAME ?? "",
                                result.USR_NAME ?? "",
                                result.USR_EMAIL ?? "",
                                result.USR_STATUS ?? "",
                            ];
                            return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, user);
                        }
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.Forbidden, "User deactivated");
                    }                   
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.Unauthorized, "Username or Password incorrect");
            }
            catch(SqlException)
            {
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception)
            {
                return null;
            }
        }
    
        public Status? UserRegister(string? Name, string? Email, string? Password, string? Username,
            string? Telephone, bool activeStatus, string? UserRole)
        {
            try
            {
                Tbl_User user = new()
                {
                    USR_NAME = Name,
                    USR_EMAIL = Email,
                    USR_PASSWORD = Password,
                    USR_ACTIVESTATUS = activeStatus,
                    USR_USERNAME = Username,
                    USR_TPN = Telephone,
                    USR_STATUS = UserRole,
                    USR_CREATED = DateTime.Now,
                };
                _dbContext?.Tbl_Users?.Add(user);
                int? OPState = _dbContext?.SaveChanges();
                if (OPState >= 1)
                {
                    return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, "User created successfully");
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.InternalServerError);
            }
            catch (SqlException)
            {
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.ServiceUnavailable);
            }
            catch(Exception)
            {
                return null;
            }
        }
    
        public Status? UserUpdate(int Id, string? Name, string? Email, string? Password, string? Username,
            string? Telephone, bool activeStatus, string? UserRole)
        {           
            Tbl_User user = new()
            {
                ID = Id,
                USR_NAME = Name,
                USR_EMAIL = Email,
                USR_PASSWORD = Password,
                USR_ACTIVESTATUS = activeStatus,
                USR_USERNAME = Username,
                USR_TPN = Telephone,
                USR_STATUS = UserRole,
            };

            _dbContext?.Tbl_Users?.Update(user);
            int? OPstate = _dbContext?.SaveChanges();
            return null;
        }
    }
}