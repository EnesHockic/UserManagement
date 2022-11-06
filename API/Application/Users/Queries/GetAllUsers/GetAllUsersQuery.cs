using API.Application.Common.Models;
using API.Application.Users.DTO;
using API.Domain.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PaginatedList<GetUserDTO>>
    {
        public GetAllUsersQuery(int pageSize = 0, int pageNumber = 0,
                            string sortOrder = "LastNameAsc", string searchString = "")
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? DefaultPageSize : pageSize;
            SortOrder = sortOrder != null ? sortOrder : "LastNameAsc";
            SearchString = searchString != null ? searchString : "";
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int DefaultPageSize { get; set; } = 10;
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<GetUserDTO>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetUserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _applicationDbContext.Users.AsQueryable();
            if (!String.IsNullOrEmpty(request.SearchString))
            {
                users = FilterUsers(users, request.SearchString);
            }
            users = AdjustUsersOrder(users, request.SortOrder);
            var usersList = users.ToList();
            return PaginatedList<GetUserDTO>.Create(_mapper.Map<List<GetUserDTO>>(usersList), request.PageNumber, request.PageSize);
        }
        private IQueryable<User> FilterUsers(IQueryable<User> users, string searchString)
        {
            var a = users.Where(u => u.FirstName!.Contains(searchString) ||
                        u.LastName!.Contains(searchString) ||
                        u.Email!.Contains(searchString) ||
                        u.Username!.Contains(searchString) ||
                        u.Status!.Contains(searchString) ||
                        u.Password!.Contains(searchString));
            return a;
        }
        private IQueryable<User> AdjustUsersOrder(IQueryable<User> users, string sortOrder)
        {
            switch (sortOrder.ToLower())
            {
                case "firstnameasc":
                    users = users.OrderBy(u => u.FirstName);
                    break;
                case "firstnamedesc":
                    users = users.OrderByDescending(u => u.FirstName);
                    break;
                case "lastnameasc":
                    users = users.OrderBy(u => u.LastName);
                    break;
                case "lastnamedesc":
                    users = users.OrderByDescending(u => u.LastName);
                    break;
                case "emailasc":
                    users = users.OrderBy(u => u.Email);
                    break;
                case "emaildesc":
                    users = users.OrderByDescending(u => u.Email);
                    break;
                case "usernameasc":
                    users = users.OrderBy(u => u.Username);
                    break;
                case "usernamedesc":
                    users = users.OrderByDescending(u => u.Username);
                    break;
                case "passwordasc":
                    users = users.OrderBy(u => u.Password);
                    break;
                case "passworddesc":
                    users = users.OrderByDescending(u => u.Password);
                    break;
                case "statusdasc":
                    users = users.OrderBy(u => u.Status);
                    break;
                case "statusdesc":
                    users = users.OrderByDescending(u => u.Status);
                    break;

                default:
                    break;
            }
            return users;
        }
    }
}
