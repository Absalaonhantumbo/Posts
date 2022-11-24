using Aplication.Dtos;
using Aplication.Interfaces;
using Aplication.Users;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController: BaseApiController
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly IUserNamesList _userNamesList;

    public UsersController(IMediator mediator, UserManager<User> userManager, IUserNamesList userNamesList)
    {
        _mediator = mediator;
        _userManager = userManager;
        _userNamesList = userNamesList;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUser.CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        return await _mediator.Send(new ListUsers.ListUsersQuery());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(string id)
    {
        return await _mediator.Send(new ListUserById.ListUserByIdQuery{Id = id});
    }

    [HttpGet("usernames")]
    public async Task<ActionResult<List<string>>> GetUsernames()
    {
        return await _mediator.Send(new UsernameList.UsernameListQuery());
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<string>>> GetUserNames2()
    {
        var userList = await _userManager.Users.ToListAsync();
        return _userNamesList.GetUserNames(userList);
    }


}