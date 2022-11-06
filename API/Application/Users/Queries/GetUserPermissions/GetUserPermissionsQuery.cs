using API.Application.Permissions.DTO;
using API.Domain.Entities;
using API.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Queries.GetUserPermissions
{
    public class GetUserPermissionsQuery : IRequest<List<PermissionDTO>>
    {
        public GetUserPermissionsQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }
    }
    public class GetUserPermissionQueryHandler : IRequestHandler<GetUserPermissionsQuery, List<PermissionDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserPermissionQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<PermissionDTO>> Handle (GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = _userRepository.GetUserPermissions(request.UserId);
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }
    }
}
