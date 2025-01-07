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

            // Get the roles associated with the user
            var roles = await _mediator.Send(new GetRolesForUserQuery(id));
            var allRoles = await _mediator.Send(new GetRolesQuery());
            var viewModel = _mapper.Map<EditUserDto>(user);

            // Retrieve and set the selected roles for the user
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
                    TempData["Success"] = "User created successfully";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Message);
                }
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
                // Send the update command to update the user's details
                var result = await _mediator.Send(new UpdateUserCommand { UserDto = model });

                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            // If the model is invalid or failed, re-fetch roles and return to the view
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
                var firstReason = result.Reasons.FirstOrDefault();
              
                if (firstReason != null && !string.IsNullOrEmpty(firstReason.Message))
                {
                    TempData["Error"] = firstReason.Message.ToString();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
