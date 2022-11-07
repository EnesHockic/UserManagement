using FluentValidation;

namespace API.Application.Users.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required!");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required!");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address!");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Please provide a valid username!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please provide a valid password!");
        }
    }
}
