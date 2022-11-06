using API.Application.Common.Exceptions;
using API.Application.Users.DTO;
using API.Interfaces.Persistence;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<GetUserDTO> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserById(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User doesn't exist!");
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Status = request.Status;

            await _userRepository.SaveChanges(cancellationToken);

            return _mapper.Map<GetUserDTO>(user);
        }
    }
}
