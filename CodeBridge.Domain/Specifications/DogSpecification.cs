using CodeBridge.Domain.Entities;

namespace CodeBridge.Domain.Specifications;

public sealed class DogSpecification : Specification<Dog>
{
    public DogSpecification()
    {
        AddAsNoTracking();
    }
}