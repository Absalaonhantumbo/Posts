using Aplication.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Users;

public class ListUsers
{
    public class ListUsersQuery: IRequest<List<UserDto>>
    {
    }
    
    public class ListUsersHandler: IRequestHandler<ListUsersQuery, List<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ListUsersHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _userManager.Users.ToListAsync(cancellationToken);

            return _mapper.Map<List<UserDto>>(usersList);
        }
    }
}