﻿using AutoMapper;
using BlogApp.Core.Entities;
using BlogApp.Core.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace BlogApp.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Map UserDto to ApplicationUser
                    var user = _mapper.Map<ApplicationUser>(request.UserDto);
                    var passwordHasher = new PasswordHasher<ApplicationUser>();
                    user.PasswordHash = passwordHasher.HashPassword(user, request.UserDto.Password);
                    // Create the user
                    var createdUser = await _userRepository.CreateUserAsync(user);
                  
                    // Add roles to the user
                    if (request.UserDto.SelectedRoles != null && request.UserDto.SelectedRoles.Any())
                    {
                        await _userRepository.AddRolesToUserAsync(createdUser, request.UserDto.SelectedRoles);
                    }

                    // Commit the transaction
                    transaction.Complete();

                    return Result.Ok(createdUser.Id);
                }
                catch (Exception ex)
                {
                    // Log the exception (optional)
                    // Rollback is implicit if `Complete()` is not called
                    return Result.Fail($"An error occurred while creating the user: {ex.Message}");
                }
            }
        }
    }
}