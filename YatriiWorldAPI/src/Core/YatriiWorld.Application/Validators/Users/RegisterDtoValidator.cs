using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Users;

namespace YatriiWorld.Application.Validators.User
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Name can only contain letters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MinimumLength(3).WithMessage("UserName must be at least 2 characters long.")
                .MaximumLength(256).WithMessage("UserName cannot exceed 50 characters.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("UserName can only contain letters.");


            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Surname is required.")
                .MinimumLength(3).WithMessage("Surname must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Surname cannot exceed 256 characters.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$").WithMessage("Surname can only contain letters.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                 .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            RuleFor(x => x).Must(x => x.ConfirmPassword == x.Password);

            
        }
    }
}
