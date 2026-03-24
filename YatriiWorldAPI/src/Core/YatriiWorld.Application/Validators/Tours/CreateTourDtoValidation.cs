using FluentValidation;
using YatriiWorld.Application.DTOs.Tours;

namespace YatriiWorld.Application.Validators
{
    public class CreateTourDtoValidator : AbstractValidator<TourCreateDto>
    {
        public CreateTourDtoValidator()
        {
            // Title Validation
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tour title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            // Description Validation
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            // Price Validation
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be a positive value.");

            // Capacity Validation
            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 100).WithMessage("Capacity must be between 1 and 100 people.");

            // Date Validation
            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.Now).WithMessage("Start date must be in the future.");

            // Category Validation
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category must be selected.")
                .GreaterThan(0).WithMessage("Invalid Category ID.");

            // Image List Validation (One-to-Many)
            RuleFor(x => x.ImageUrls)
                .NotEmpty().WithMessage("At least one image is required.")
                .Must(list => list != null && list.Count > 0).WithMessage("Image list cannot be empty.");

            // Tag List Validation (Many-to-Many)
            RuleFor(x => x.SelectedTagIds)
                .Must(tags => tags != null && tags.Count > 0).WithMessage("Please select at least one tag.");
        }


    }
}