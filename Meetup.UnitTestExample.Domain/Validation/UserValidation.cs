using FluentValidation;
using Meetup.UnitTestExample.Domain.Model;

namespace Meetup.UnitTestExample.Domain.Validation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            ValidateFirstName();
        }

        protected void ValidateFirstName()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("Please ensure you have entered the Name")
                .WithErrorCode("UserError1")
                .Length(2, 150)
                .WithMessage("The Name must have between 2 and 150 characters");
        }
    }
}
