using System.Linq.Expressions;
using CodeBridge.Application.Interfaces;
using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Interfaces;
using CodeBridge.Domain.Models;
using CodeBridge.Domain.Specifications;

namespace CodeBridge.Application.Services;

public class DogService : IDogService
{
    private readonly IDogRepository _dogRepository;

    private readonly DogSpecification _dogSpecification;
    
    public DogService(IDogRepository dogRepository)
    {
        _dogRepository = dogRepository;
        _dogSpecification = new DogSpecification();
    }

    public async Task<List<Dog>> GetAllDogsAsync(SortParameters sortParameters, PagingParameters pagingParameters)
    {
        if (sortParameters is not {Order: null, Attribute: null})
        {
            if(sortParameters.Order == "asc")
                _dogSpecification.AddOrderBy(GetSortingProperty(sortParameters));
            else if (sortParameters.Order == "desc")
                _dogSpecification.AddOrderByDescending(GetSortingProperty(sortParameters));
        }
        
        if (pagingParameters is not {PageNumber: 0, PageSize: 0})
        {
            _dogSpecification.ApplyPaging(pagingParameters);
        }
        
        return await _dogRepository.GetAllDogsAsync(_dogSpecification);
    }

    public async Task CreateDogAsync(Dog dog)
    {
        await _dogRepository.CreateDogAsync(dog);
    }


    private Expression<Func<Dog, object>> GetSortingProperty(SortParameters parameters)
    {
        return parameters.Attribute!.ToLower() switch
        {
            "name" => dog => dog.Name,
            "color" => dog => dog.Color,
            "tail_length" => dog => dog.TailLength,
            "weight" => dog => dog.TailLength,
            _ => dog => dog.Name
        };
    }
    
    
}