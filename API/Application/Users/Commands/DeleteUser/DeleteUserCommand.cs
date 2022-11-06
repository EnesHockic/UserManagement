using API.Application.Common.Exceptions;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
        public int UserId{ get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _applicationDbContext.Users.Find(request.UserId);
            if(user == null)
            {
                throw new NotFoundException("User doesn't exist");
            }
            _applicationDbContext.Users.Remove(user);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
