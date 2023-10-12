using CodeBridge.Domain.Entities;
using FluentValidation;

namespace CodeBridge.WebAPI.FluentValidation;

public class DogValidator : AbstractValidator<Dog>
{
    public DogValidator()
    {
        RuleFor(dog => dog.Weight).NotEmpty().GreaterThan(0);
        RuleFor(dog => dog.TailLength).NotEmpty().GreaterThan(0);
        RuleFor(dog => dog.Color).NotEmpty();
        RuleFor(dog => dog.Name).NotEmpty();
    }
}