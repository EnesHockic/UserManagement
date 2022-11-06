using API.Application.Users.DTO;
using API.Domain.Entities;
using API.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Commands.AddUser
{
    public class AddUserCommand : IRequest<GetUserDTO>
    {
        public AddUserCommand(string firstName, string lastName,
                              string email, string status,
                              string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Status = status;
            Username = username;
            Password = password;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, GetUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<GetUserDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Status = request.Status,
                Username = request.Username,
                Password = request.Password
            };
            _userRepository.Add(newUser);
            await _userRepository.SaveChanges(cancellationToken);
            return _mapper.Map<GetUserDTO>(newUser);
        }
    }
}
