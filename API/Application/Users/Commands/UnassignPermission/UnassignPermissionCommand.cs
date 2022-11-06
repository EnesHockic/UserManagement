using API.Application.Common.Exceptions;
using API.Application.Users.DTO;
using API.Interfaces;
using API.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Commands.UnassignPermission
{
    public class UnassignPermissionCommand : IRequest<bool>
    {
        public UnassignPermissionCommand(int userId, int permissionId)
        {
            UserId = userId;
            PermissionId = permissionId;
        }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
    public class UnassignPermissionCommandHandler : IRequestHandler<UnassignPermissionCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UnassignPermissionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> Handle(UnassignPermissionCommand request, CancellationToken cancellationToken)
        {
            var userPermission = _applicationDbContext.UserPermissions
                .FirstOrDefault(x => x.UserId == request.UserId && x.PermissionId == request.PermissionId);
            if (userPermission == null)
            {
                throw new NotFoundException("The user doesn't have this permission!");
            }

            _applicationDbContext.UserPermissions.Remove(userPermission);

            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
