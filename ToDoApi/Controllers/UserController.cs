using Domain;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ToDoApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly UserRepositorie userRepositorie = new UserRepositorie();
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(User user)
    {
      string userId =await this.userRepositorie.CreateAsync(user);
      return this.Ok(userId);
    }

    [HttpGet("GetUser/{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
      User user = await this.userRepositorie.GetByIdAsync(userId);
      return this.Ok(user);
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
      List<User> users = await this.userRepositorie.GetAllAsync();
      return this.Ok(users);
    }

    [HttpDelete("DeleteUser/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
      bool deleted = await this.userRepositorie.DeleteByIdAsync(userId);
      if (deleted == true)
      {
        return this.Ok("Success");
      }
      else
      {
        return this.Ok("Fail");
      }
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(User user)
    {
      bool update = await this.userRepositorie.UpdateAsync(user);
      if(update==true)
      {
        return this.Ok("Success");
      }
      else
      {
        return this.Ok("Fail");
      }
    }
  }
}
