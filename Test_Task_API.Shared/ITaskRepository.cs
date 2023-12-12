using Microsoft.AspNetCore.JsonPatch;
using Test_Task_API.BOL;

namespace Test_Task_API.Shared
{
    public interface ITaskRepository
    {
        public Status? TaskView(int? Id);
        public Status? TaskCreate(string? Name, string? Description, int Priority, DateTime? End, int CreatedUser);
        public Status? TaskUpdate(int Id, string? Name, string? Description, int? Priority, DateTime? End);
        public Status? TaskPatch(int Id, JsonPatchDocument update);
    }
}
