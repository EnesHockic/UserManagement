using FluentValidation;

namespace API.Application.Users.DTO
{
    public class AddUserDTOValidator : AbstractValidator<AddUserDTO>
    {
        public AddUserDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required!");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required!");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required!");
            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required!");
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required!");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required!");
        }
    }
}
