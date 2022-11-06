using API.Application.Common.Exceptions;
using API.Application.Users.DTO;
using API.Interfaces;
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
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        public async Task<GetUserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _applicationDbContext.Users.Find(request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User was not found!");
            }
            return _mapper.Map<GetUserDTO>(user);
        }
    }
}
