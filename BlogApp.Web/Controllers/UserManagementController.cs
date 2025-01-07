using AutoMapper;
using BlogApp.Application.Users.Commands.CreateUser;
using BlogApp.Application.Users.Commands.DeleteUser;
using BlogApp.Application.Users.Commands.UpdateUser;
using BlogApp.Application.Users.Queries.GetRoles;
using BlogApp.Application.Users.Queries.GetRolesForUser;
using BlogApp.Application.Users.Queries.GetUsers;
using BlogApp.Dto.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserManagementController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            if (!result.IsSuccess)
            {
                TempData["Error"] = "An error occurred while retrieving the user list.";
            }
            return View(result.Value);
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _mediator.Send(new GetRolesQuery());
            var viewModel = new CreateUserDto
            {
                SelectedRoles = new List<string>(),
                Roles = roles
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            // Retrieve user by ID
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
            {
                TempData["Error"] = "User not found";
                return RedirectToAction(nameof(Index));
            }

            var roles = await _mediator.Send(new GetRolesForUserQuery(id));
            var allRoles = await _mediator.Send(new GetRolesQuery());
            var viewModel = _mapper.Map<EditUserDto>(user);

            viewModel.SelectedRoles = roles.Select(r => r.Name).ToList();
            viewModel.Roles = allRoles;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateUserCommand { UserDto = model });
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = result.Errors[0].Message;
            }

            model.Roles = await _mediator.Send(new GetRolesQuery());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserDto model)
        {
            if (ModelState.IsValid)
            {
             
                var result = await _mediator.Send(new UpdateUserCommand { UserDto = model });

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = result.Errors[0].Message;
            }

            model.Roles = await _mediator.Send(new GetRolesQuery());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = id });

            if (!result.IsSuccess)
            {
                foreach (var reason in result.Reasons)
                {
                    TempData["Error"] = reason.Message;
                }
            }
           
            return RedirectToAction(nameof(Index));
        }
    }
}
