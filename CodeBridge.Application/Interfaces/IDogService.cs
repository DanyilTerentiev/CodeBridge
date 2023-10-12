using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Models;

namespace CodeBridge.Application.Interfaces;

public interface IDogService
{
    Task<List<Dog>> GetAllDogsAsync(SortParameters sortParameters, PagingParameters pagingParameters);

    Task CreateDogAsync(Dog dog);
}