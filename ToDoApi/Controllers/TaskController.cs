using Domain;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Repositories;
using SharpCompress.Common;

namespace ToDoApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TaskController : ControllerBase
  {
    private readonly TaskRepositorie taskRepositorie = new TaskRepositorie();
    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask(Domain.Task task)
    {
      string taskId = await this.taskRepositorie.CreateAsync(task);
      return this.Ok(taskId);
    }

    [HttpGet("GetTask/{taskId}")]
    public async Task<IActionResult> GetTask(string taskId)
    {
      Domain.Task task = await this.taskRepositorie.GetByIdAsync(taskId);
      return this.Ok(task);
    }

    [HttpGet("GetTasksForUser/{userId}")]
    public async Task<IActionResult> GetTasksForUser(string userId)
    {
      var tasks = await this.taskRepositorie.GetTasksForUserAsync(userId);
      return this.Ok(tasks);
    }

    [HttpPut("MarkTaskAsCompleted/{task_id}")]
    public async Task<IActionResult> MarkTaskAsCompleted(string task_id)
    {
      bool success = await this.taskRepositorie.MarkTaskAsCompleted(task_id);
      if (success)
      {
        return this.Ok("success");
      }
      else
      {
        return this.BadRequest("fail");
      }
    }

    [HttpDelete("DeleteTask/{taskId}")]
    public async Task<IActionResult> DeleteUser(string taskId)
    {
      bool deleted = await this.taskRepositorie.DeleteByIdAsync(taskId);
      if (deleted == true)
      {
        return this.Ok("Success");
      }
      else
      {
        return this.BadRequest("Fail");
      }
    }

    [HttpPut("UpdateTask")]
    public async Task<IActionResult> UpdateUser(Domain.Task task)
    {
      bool update = await this.taskRepositorie.UpdateAsync(task);
      if (update == true)
      {
        return this.Ok("Success");
      }
      else
      {
        return this.BadRequest("Fail");
      }
    }
  }
}
