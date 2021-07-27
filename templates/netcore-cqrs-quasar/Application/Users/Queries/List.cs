using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;
using X.PagedList;

namespace Application.Users
{
    public class List
    {
        public class Query : IRequest<List<ApplicationUser>>
        {
            public int Page { get; set; }

            public bool Descending { get; set; }

            public string SortBy { get; set; }
            public string Search { get; set; }

            public int Size { get; set; }

            public string Role { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ApplicationUser>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;

            }
            public async Task<List<ApplicationUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    user.Role = String.Join(",", roles.ToArray());
                }
                users = FilterUsers(request.Search, users);

                users = SortUsers(request.SortBy, request.Descending, users);

                if (request.Page <= 0) request.Page = 1; // make sure request.Page is  > 0
                if (request.Size <= 0) request.Size = users.Count; // retrieve all if invalid request.Size

                if (!String.IsNullOrEmpty(request.Role)) users = await GetUsersInRole(request.Role, users);

                return users.ToPagedList(request.Page, request.Size).ToList();
            }

            private async Task<List<ApplicationUser>> GetUsersInRole(string role, List<ApplicationUser> users)
            {
                List<ApplicationUser> usersInRole = new List<ApplicationUser>();
                foreach (var u in users)
                {
                    if (await IsInRole(u, role))
                        usersInRole.Add(u);
                }
                return usersInRole;
            }
            private async Task<bool> IsInRole(ApplicationUser user, string role)
            {
                return await _userManager.IsInRoleAsync(user, role);
            }

            private List<ApplicationUser> FilterUsers(string search, List<ApplicationUser> users)
            {
                if (String.IsNullOrEmpty(search))
                {
                    return users;
                }
                return users.FindAll(i =>
                    i.UserName != null && i.UserName.ToLower().Contains(search.ToLower())
                    || i.Email != null && i.Email.ToLower().Contains(search.ToLower())
                    || i.PhoneNumber != null && i.PhoneNumber.ToLower().Contains(search.ToLower())
                    || i.FullName != null && i.FullName.ToLower().Contains(search.ToLower()))
                    .ToList();
            }
            private List<ApplicationUser> SortUsers(string sort, bool descending, List<ApplicationUser> users)
            {
                switch (sort)
                {
                    case "userName":
                        if (descending)
                        {
                            users = users.OrderByDescending(i => i.UserName).ToList();
                        }
                        else
                        {
                            users = users.OrderBy(i => i.UserName).ToList();
                        }
                        break;
                    case "email":
                        if (descending)
                        {
                            users = users.OrderByDescending(i => i.Email).ToList();
                        }
                        else
                        {
                            users = users.OrderBy(i => i.Email).ToList();
                        }
                        break;
                    case "phoneNumber":
                        if (descending)
                        {
                            users = users.OrderByDescending(i => i.PhoneNumber).ToList();
                        }
                        else
                        {
                            users = users.OrderBy(i => i.PhoneNumber).ToList();
                        }
                        break;
                    default:
                        if (descending)
                        {
                            users = users.OrderByDescending(i => i.FullName).ToList();
                        }
                        else
                        {
                            users = users.OrderBy(i => i.FullName).ToList();
                        }
                        break;
                }
                return users;
            }
        }
    }
}