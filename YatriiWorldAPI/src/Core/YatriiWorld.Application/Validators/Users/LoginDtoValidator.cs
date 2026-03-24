using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Users;

namespace YatriiWorld.Application.Validators.Users
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
               .NotEmpty()
               .MinimumLength(3)
               .MaximumLength(256)
               .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ0-9\-\._@\+]+$");


            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        }
    }
}
