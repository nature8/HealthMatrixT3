using DoctorService.Api.DTOs;
using FluentValidation;

namespace DoctorService.Api.Validators;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Department).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Specialization).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Experience).GreaterThanOrEqualTo(0).LessThan(70);
        RuleFor(x => x.Fee).GreaterThan(0);
    }
}