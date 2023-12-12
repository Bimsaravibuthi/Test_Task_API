using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using System.Net;
using Test_Task_API.BOL;
using Test_Task_API.DAL;
using Test_Task_API.Shared;

namespace Test_Task_API.BLL
{
    public class TaskLogic : ITaskRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly HttpStatus _httpStatus;
        public TaskLogic() 
        {
            _dbContext = new();
            _httpStatus = new();
        }

        public Status? TaskView(int? Id)
        {
            try
            {
                if (Id is not null)
                {
                    var result = _dbContext?.Tbl_Tasks?.FirstOrDefault(t => t.ID == Id);
                    if (result is not null)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, result);
                    }
                }
                else
                {
                    var result = _dbContext?.Tbl_Tasks?.ToList();
                    if (result is not null)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, result);
                    }
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "Task(s) not found");
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
    
        public Status? TaskCreate(string? Name, string? Description, int Priority, DateTime? End, int CreatedUser)
        {
            try
            {
                Tbl_User? user = _dbContext?.Tbl_Users?.FirstOrDefault(u => u.ID == CreatedUser);
                if(user is not null)
                {
                    Tbl_Task tbl_Task = new()
                    {
                        TSK_NAME = Name,
                        TSK_DESCRIPTION = Description,
                        TSK_PRIORITY = Priority,
                        TSK_CREATED = DateTime.Now,
                        TSK_END = End,
                        User = user
                    };
                    _dbContext?.Tbl_Tasks?.Add(tbl_Task);
                    var OPState = _dbContext?.SaveChanges();
                    if(OPState is not null)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, "Task created successfully");
                    }
                    return _httpStatus.StatusCodeWithContent(HttpStatusCode.InternalServerError);
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "User not found");
            }
            catch(SqlException)
            {
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.ServiceUnavailable);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public Status? TaskUpdate(int Id, string? Name, string? Description, int? Priority, DateTime? End)
        {
            try
            {
                var task = _dbContext?.Tbl_Tasks?.FirstOrDefault(t => t.ID == Id);
                if(task is not null)
                {
                    task.ID = Id;
                    task.TSK_NAME = string.IsNullOrEmpty(Name) ? task.TSK_NAME : Name;
                    task.TSK_DESCRIPTION = string.IsNullOrEmpty(Description) ? task.TSK_DESCRIPTION : Description;
                    task.TSK_PRIORITY = Priority ?? task.TSK_PRIORITY;
                    task.TSK_END = string.IsNullOrEmpty(End.ToString()) ? task.TSK_END : End;

                    var OPState = _dbContext?.SaveChanges();
                    if(OPState >= 1)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, "Task updated successfully");
                    }
                    return _httpStatus.StatusCodeWithContent(HttpStatusCode.InternalServerError);
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "Task not found");
            }
            catch (SqlException)
            {
                return _httpStatus?.StatusCodeWithContent(HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public Status? TaskPatch(int Id, JsonPatchDocument update)
        {
            try
            {
                var task = _dbContext?.Tbl_Tasks?.FirstOrDefault(t =>t.ID == Id);
                if(task is not null)
                {
                    update.ApplyTo(task);
                    var OPStatus = _dbContext?.SaveChanges();
                    if(OPStatus >= 1)
                    {
                        return _httpStatus.StatusCodeWithContent(HttpStatusCode.OK, "Task updated successfully");
                    }
                    return _httpStatus.StatusCodeWithContent(HttpStatusCode.InternalServerError);
                }
                return _httpStatus.StatusCodeWithContent(HttpStatusCode.NotFound, "Task not found");
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
    }
}
