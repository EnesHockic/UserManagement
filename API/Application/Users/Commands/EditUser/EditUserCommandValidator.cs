using API.Interfaces.Persistence;
using FluentValidation;

namespace API.Application.Users.Commands.EditUser
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public EditUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(x => x.Id)
                .Must(UserExists)
                .WithMessage("User does not exist!");

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
                .WithMessage("Please provide a valid email address!")
                .When(x => !string.IsNullOrEmpty(x.Email));

        }
        public bool UserExists(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            return user != null;
        }
    }
}
