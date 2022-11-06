using API.Application.Permissions.DTO;
using API.Interfaces;
using API.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace API.Application.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsQuery : IRequest<List<PermissionDTO>>
    {
        public GetAllPermissionsQuery()
        {

        }
    }
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, List<PermissionDTO>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetAllPermissionsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<PermissionDTO>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = _applicationDbContext.Permissions.ToList();
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }
    }
}
