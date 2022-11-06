using API.Application.Permissions.DTO;
using API.Domain.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Commands.AssignPermission
{
    public class AssignPermissionCommand : IRequest<bool>
    {
        public AssignPermissionCommand(int userId, int permissionId)
        {
            UserId = userId;
            PermissionId = permissionId;
        }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
    public class AssignPermissionCommandHandler : IRequestHandler<AssignPermissionCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AssignPermissionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> Handle(AssignPermissionCommand request, CancellationToken cancellationToken)
        {
            var userPermission = new UserPermission()
            {
                PermissionId = request.PermissionId,
                UserId = request.UserId
            };
            _applicationDbContext.UserPermissions.Add(userPermission);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
