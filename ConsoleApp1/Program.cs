using System;
using BlogApp.Application.DTOs.User; // Adjust namespace for CreateUserDto
using BlogApp.Application.Validators; // Adjust namespace for CreateUserDtoValidator

class Program
{
    static void Main(string[] args)
    {
        var validator = new CreateUserDtoValidator();

        // Test a valid password
        var result = validator.Validate(new CreateUserDto
        {
            UserName= "xxxxxx",
            Email="x@gmail.com",
            SelectedRoles = new List<string> { "Admin" },
            Password = "Password123!" // Replace with your test password
        });

        if (!result.IsValid)
        {
            Console.WriteLine("Validation failed with the following errors:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
        }
        else
        {
            Console.WriteLine("Password is valid.");
        }

        Console.ReadKey();
    }
}
