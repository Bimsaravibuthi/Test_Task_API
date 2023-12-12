using Test_Task_API.DAL;
using Test_Task_API.BOL;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using Test_Task_API.Shared;

namespace Test_Task_API.BLL
{
    public class UserLogic : IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly HttpStatus _httpStatus;
        public UserLogic() 
        {
            _dbContext = new();
            _httpStatus = new();
        }

        public Status? UserView(int? Id)
        {
            try
            {
                if (Id is not null)
                {
                    Tbl_User? user = _dbContext?.Tbl_Users?.SingleOrDefault(u => u.ID == Id);
                    if(user is not null)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, user);
                    }
                }
                else
                {
                    List<Tbl_User>? users = _dbContext?.Tbl_Users?.ToList();
                    if (users is not null)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, users);
                    }
                }          
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "User(s) not found");
            }
            catch (SqlException)
            {
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception)
            {
                return null;
            }
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
            catch (Exception)
            {
                return null;
            }
        }
    
        public Status? UserUpdate(int Id, string? Name, string? Email, string? Password, string? Username,
            string? Telephone, bool activeStatus, string? UserRole)
        {
            try
            {
                var user = _dbContext?.Tbl_Users?.SingleOrDefault(u => u.ID == Id);
                if (user is not null)
                {
                    user.USR_NAME = string.IsNullOrEmpty(Name) ? user.USR_NAME : Name;
                    user.USR_EMAIL = string.IsNullOrEmpty(Email) ? user.USR_EMAIL : Email;
                    user.USR_PASSWORD = string.IsNullOrEmpty(Password) ? user.USR_PASSWORD : Password;
                    user.USR_ACTIVESTATUS = string.IsNullOrEmpty(activeStatus.ToString()) ? user.USR_ACTIVESTATUS : activeStatus;
                    user.USR_USERNAME = string.IsNullOrEmpty(Username) ? user.USR_USERNAME : Username;
                    user.USR_TPN = string.IsNullOrEmpty(Telephone) ? user.USR_TPN : Telephone;
                    user.USR_STATUS = string.IsNullOrEmpty(UserRole) ? user.USR_STATUS : UserRole;

                    int? OPState = _dbContext?.SaveChanges();
                    if (OPState >= 1)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, "User updated successfully");
                    }
                    return _httpStatus.StatusCodeWithContent(HttpStatusCode.InternalServerError);
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "User not found");
            }
            catch (SqlException)
            {
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception)
            {
                return null;
            }
        }
    
        public Status? UserPatch(int Id, JsonPatchDocument update)
        {
            try
            {
                var user = _dbContext?.Tbl_Users?.SingleOrDefault(u => u.ID == Id);
                if (user is not null)
                {
                    update.ApplyTo(user);
                    var OPState = _dbContext?.SaveChanges();
                    if(OPState >= 1)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, "User updated successfully");
                    }
                    return _httpStatus.StatusCodeWithContent(HttpStatusCode.InternalServerError);
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "User not found");
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
    }
}