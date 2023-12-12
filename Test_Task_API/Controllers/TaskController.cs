using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Test_Task_API.Models.Task;
using Test_Task_API.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Test_Task_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskLogic;
        public TaskController(ITaskRepository taskRepository)
        {
            _taskLogic = taskRepository;
        }

        [HttpGet("TaskView/{Id?}")]
        public IActionResult TaskView(int? Id)
        {
            var result = _taskLogic.TaskView(Id);
            if (result is not null)
            {
                if (result.status == HttpStatusCode.OK)
                {
                    return StatusCode((int)result.status, result.Content);
                }
                if (result.Content is not null)
                {
                    return StatusCode((int)result.status, result);
                }
                return StatusCode((int)result.status, new { result.status, result.ReasonPhrase });
            }
            return BadRequest("😕 Bad Input");
        }

        [HttpPost("TaskCreate")]
        public IActionResult TaskCreate([FromBody] CreateTask createTask)
        {
            var OPState = _taskLogic.TaskCreate(createTask.TSK_NAME, createTask.TSK_DESCRIPTION,
                createTask.TSK_PRIORITY, createTask.TSK_END, createTask.USR_ID);

            if(OPState is not null)
            {
                if(OPState.Content is not null)
                {
                    return StatusCode((int)OPState.status, OPState);
                }
                return StatusCode((int)OPState.status, new { OPState.status, OPState.ReasonPhrase });
            }
            return BadRequest("😕 Bad Input");
        }

        [HttpPut("TaskUpdate/{Id}")]
        public IActionResult TaskUpdate([FromBody] UpdateTask updateTask, [FromRoute] int Id)
        {
            var OPState = _taskLogic.TaskUpdate(Id, updateTask.TSK_NAME, updateTask.TSK_DESCRIPTION,
                updateTask.TSK_PRIORITY, updateTask.TSK_END);

            if (OPState is not null)
            {
                if (OPState.Content is not null)
                {
                    return StatusCode((int)OPState.status, OPState);
                }
                return StatusCode((int)OPState.status, new { OPState.status, OPState.ReasonPhrase });
            }
            return BadRequest("😕 Bad Input");
        }

        [HttpPatch("TaskPatch/{Id}")]
        public IActionResult TaskPatch(JsonPatchDocument updateTask, [FromRoute] int Id)
        {
            var OPStatus = _taskLogic.TaskPatch(Id, updateTask);
            if (OPStatus is not null)
            {
                if(OPStatus.Content is not null)
                {
                    return StatusCode((int)OPStatus.status, OPStatus);
                }
                return StatusCode((int)OPStatus.status, new {OPStatus.status, OPStatus.ReasonPhrase });
            }
            return BadRequest("😕 Bad Input");
        }
    }
}
