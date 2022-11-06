using API.Interfaces.Persistence;
using FluentValidation;

namespace API.Application.Users.DTO
{
    public class EditUserDTOValidator : AbstractValidator<EditUserDTO>
    {
        public EditUserDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required!");
        }
    }
}
