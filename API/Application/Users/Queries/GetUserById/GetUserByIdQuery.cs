using API.Application.Users.DTO;
using API.Interfaces.Persistence;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserDTO>
    {
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<GetUserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserById(request.UserId);
            //Throw exception
            return _mapper.Map<GetUserDTO>(user);
        }
    }
}
