using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Reviews;

namespace YatriiWorld.Application.Validators.Reviews
{
    public class ReviewCreateDtoValidator : AbstractValidator<ReviewCreateDto>
    {
        public ReviewCreateDtoValidator()
        {
            
             
            RuleFor(x => x.Rating)
                    
                .NotEmpty().WithMessage("Rating is required.")
                    
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

              
            RuleFor(x => x.Comment)
                    .NotEmpty().WithMessage("Comment cannot be empty.")
                   
                    .MinimumLength(10).WithMessage("Comment must be at least 10 characters long.")
                    
                    .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");


              
            RuleFor(x => x.TourId)
                   
                .GreaterThan(0).WithMessage("Invalid Tour ID.");
          
        }
    }
}
