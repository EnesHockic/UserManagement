using API.Application.Common.Exceptions;
using API.Application.Users.DTO;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Commands.EditUser
{
    public class EditUserCommand : IRequest<GetUserDTO>
    {
        public EditUserCommand(int id, string firstName, string lastName,
                               string email, string status)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = status;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, GetUserDTO>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        public async Task<GetUserDTO> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = _applicationDbContext.Users.Find(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist!");
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Status = request.Status;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GetUserDTO>(user);
        }
    }
}
