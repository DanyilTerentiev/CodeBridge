using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Exceptions;
using CodeBridge.Domain.Interfaces;
using CodeBridge.Domain.Specifications;
using CodeBridge.Infrastructure.Data;
using ExceptionHandler;
using Microsoft.EntityFrameworkCore;

namespace CodeBridge.Infrastructure.Repositories;

public class DogRepository : IDogRepository
{
    private readonly AppDbContext _dbContext;

    public DogRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Dog>> GetAllDogsAsync(Specification<Dog> specification)
    {
        return await SpecificationEvaluator.GetQuery(_dbContext.Dogs, specification).ToListAsync();
    }

    public async Task CreateDogAsync(Dog dog)
    {
        if (await _dbContext.Dogs.FirstOrDefaultAsync(d => d.Name == dog.Name) is not null)
        {
            throw new ValidationException(new List<AppError> {new AppError(nameof(dog.Name),"The dog with such name already exists!")});
        }
            
        await _dbContext.Dogs.AddAsync(dog);
        await _dbContext.SaveChangesAsync();
    }
}