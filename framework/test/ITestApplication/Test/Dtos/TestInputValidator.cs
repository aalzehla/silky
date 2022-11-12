using FluentValidation;

namespace ITestApplication.Test.Dtos
{
    public class TestInputValidator : AbstractValidator<TestInput>
    {
        public TestInputValidator()
        {
            RuleFor(x => x.Name).Length(3, 10).WithMessage("Name length is3~10bit(Fluent)");
            // RuleFor(x => x.Phone).Matches("^1[3-9]\\d{9}$").WithMessage("Mobile number format is incorrect");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is not allowed to be empty(Fluent)");
        }
    }
}