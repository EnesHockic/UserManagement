using API.Application.Permissions.DTO;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public GetUserPermissionQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        public async Task<List<PermissionDTO>> Handle (GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = _applicationDbContext.UserPermissions
                .Include(x => x.Permission).Where(x => x.UserId == request.UserId)
                .Select(x => x.Permission).ToList();
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }
    }
}
