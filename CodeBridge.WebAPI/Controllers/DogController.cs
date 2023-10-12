using CodeBridge.Application.Interfaces;
using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CodeBridge.WebAPI.Controllers;

[ApiController]
[EnableRateLimiting("fixed")]
[Route("api/[controller]")]
public class DogController : ControllerBase
{
    private readonly IDogService _dogService;

    private readonly IValidator<Dog> _dogValidator;
    
    public DogController(IDogService dogService, IValidator<Dog> dogValidator)
    {
        _dogService = dogService;
        _dogValidator = dogValidator;
    }

    [HttpGet("ping")]
    [ProducesResponseType(typeof(Ok), 200)]
    public IActionResult Ping()
    {
        return Ok("Dogshouseservice.Version1.0.1");
    }

    [HttpGet]
    [ProducesResponseType(typeof(Ok), 200)]
    public async Task<IActionResult> GetAllDogs([FromQuery]SortParameters sortParameters, [FromQuery]PagingParameters pagingParameters)
    {
        var dogs = await _dogService.GetAllDogsAsync(sortParameters, pagingParameters);
        
        return Ok(dogs);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Ok), 200)]
    [ProducesResponseType(typeof(BadRequest), 400)]
    public async Task<IActionResult> CreateDog(Dog dog)
    {
        var validationResult = await _dogValidator.ValidateAsync(dog);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        await _dogService.CreateDogAsync(dog);

        return Ok();
    }
}