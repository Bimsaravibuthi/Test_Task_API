using Microsoft.AspNetCore.JsonPatch;
using Test_Task_API.BOL;

namespace Test_Task_API.Shared
{
    public interface IUserRepository
    {
        public Status? UserView(int? Id);
        public Status? UserLogin(string Username, string Password);
        public Status? UserRegister(string? Name, string? Email, string? Password, string? Username,
            string? Telephone, bool activeStatus, string? UserRole);
        public Status? UserUpdate(int Id, string? Name, string? Email, string? Password, string? Username,
            string? Telephone, bool activeStatus, string? UserRole, DateTime Created);
        public Status? UserPatch(int Id, JsonPatchDocument update);
    }
}
