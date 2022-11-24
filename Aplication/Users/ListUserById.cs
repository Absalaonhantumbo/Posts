using Aplication.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Users;

public class ListUserById
{
    public class ListUserByIdQuery: IRequest<UserDto>
    {
        public string Id { get; set; }
    }
    
    public class ListUserByIdHandler: IRequestHandler<ListUserByIdQuery, UserDto>
    {
        private readonly UserManager<User> _manager;
        private readonly IMapper _mapper;

        public ListUserByIdHandler(UserManager<User> manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(ListUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user2 = await _manager.FindByIdAsync(request.Id);
            if (user2 is null)
            {
                throw new Exception("User not found");
            }

            return _mapper.Map<UserDto>(user2);
        }
    }
}