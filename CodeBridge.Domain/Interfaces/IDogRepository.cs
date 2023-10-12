using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Specifications;

namespace CodeBridge.Domain.Interfaces;

public interface IDogRepository
{
    Task<List<Dog>> GetAllDogsAsync(Specification<Dog> specification);
    Task CreateDogAsync(Dog dog);
}