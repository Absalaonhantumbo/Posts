using Aplication.Accounts;
using Aplication.Dtos;
using Aplication.Interfaces;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {

        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login(Login.LoginCommand command)
    {
        return await _mediator.Send(command);
    }
}