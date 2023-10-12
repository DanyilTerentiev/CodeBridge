using CodeBridge.Application.Services;
using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Interfaces;
using CodeBridge.Domain.Models;
using CodeBridge.Domain.Specifications;
using Moq;

namespace CodeBridge.UnitTests;

public class DogServiceGetAllDogsUnitTest
{
    [Test]
    public async Task GetAllDogsAsync_ShouldReturnListOfDogs_WithoutPagingAndSorting()
    {
        //Arrange
        var dogMock = new Mock<IDogRepository>();
        
        var dogs = new List<Dog>()
        {
            new() { Name = "a" },
            new() { Name = "b" },
        };
        
        dogMock.Setup(repository => repository
                .GetAllDogsAsync(It.IsAny<Specification<Dog>>()))
                .ReturnsAsync(dogs);

        var dogService = new DogService(dogMock.Object);
        
        var sortParams = new SortParameters();
        var pagingParams = new PagingParameters();

        //Act
        var result = await dogService.GetAllDogsAsync(sortParams, pagingParams);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf(typeof(List<Dog>)));
        });
        
        dogMock.Verify(repository => repository.GetAllDogsAsync(
                It.Is<Specification<Dog>>(spec =>
                    spec.OrderByExpression == null &&
                    spec.OrderByDescendingExpression == null &&
                    spec.AsNoTracking == true &&
                    spec.IsPagingEnabled == false )),
            Times.Once);
    }

    [Test]
    public async Task GetAllDogsAsync_WithSortingAsc()
    {
        var dogMock = new Mock<IDogRepository>();
        
        var dogs = new List<Dog>()
        {
            new() { Name = "a" },
            new() { Name = "b" },
        };
        
        dogMock.Setup(repository => repository
                .GetAllDogsAsync(It.IsAny<Specification<Dog>>()))
            .ReturnsAsync(dogs);
        
        var dogService = new DogService(dogMock.Object);
        
        var sortParams = new SortParameters {Attribute = "name", Order = "asc"};
        var pagingParams = new PagingParameters();
        
        //Act
        var result = await dogService.GetAllDogsAsync(sortParams, pagingParams);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf(typeof(List<Dog>)));
        });
        
        dogMock.Verify(repository => repository.GetAllDogsAsync(
                It.Is<Specification<Dog>>(spec =>
                    spec.OrderByExpression != null &&
                    spec.AsNoTracking == true)),
            Times.Once);
    }
    
    [Test]
    public async Task GetAllDogsAsync_WithSortingDesc()
    {
        var dogMock = new Mock<IDogRepository>();
        
        var dogs = new List<Dog>()
        {
            new() { Name = "b" },
            new() { Name = "a" },
        };
        
        dogMock.Setup(repository => repository
                .GetAllDogsAsync(It.IsAny<Specification<Dog>>()))
            .ReturnsAsync(dogs);
        
        var dogService = new DogService(dogMock.Object);
        
        var sortParams = new SortParameters {Attribute = "name", Order = "desc"};
        var pagingParams = new PagingParameters();
        
        //Act
        var result = await dogService.GetAllDogsAsync(sortParams, pagingParams);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf(typeof(List<Dog>)));
        });
        
        dogMock.Verify(repository => repository.GetAllDogsAsync(
                It.Is<Specification<Dog>>(spec =>
                    spec.OrderByExpression == null &&
                    spec.OrderByDescendingExpression != null &&
                    spec.AsNoTracking == true)),
            Times.Once);
    }
    
    [Test]
    public async Task GetAllDogsAsync_WithPagingAndSorting()
    {
        var dogMock = new Mock<IDogRepository>();
        
        var dogs = new List<Dog>()
        {
            new() { Name = "a" },
            new() { Name = "b" },
        };
        
        dogMock.Setup(repository => repository
                .GetAllDogsAsync(It.IsAny<Specification<Dog>>()))
            .ReturnsAsync(dogs);
        
        var dogService = new DogService(dogMock.Object);

        var sortParams = new SortParameters {Attribute = "name", Order = "desc"};
        var pagingParams = new PagingParameters {PageNumber = 1, PageSize = 2};
        
        //Act
        var result = await dogService.GetAllDogsAsync(sortParams, pagingParams);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf(typeof(List<Dog>)));
        });
        
        dogMock.Verify(repository => repository.GetAllDogsAsync(
                It.Is<Specification<Dog>>(spec =>
                    spec.OrderByExpression == null &&
                    spec.OrderByDescendingExpression != null &&
                    spec.AsNoTracking == true &&
                    spec.IsPagingEnabled == true)),
            Times.Once);
    }
    
    [Test]
    public async Task GetAllDogsAsync_WithPaging()
    {
        var dogMock = new Mock<IDogRepository>();
        
        var dogs = new List<Dog>()
        {
            new() { Name = "b" },
            new() { Name = "a" },
        };
        
        dogMock.Setup(repository => repository
                .GetAllDogsAsync(It.IsAny<Specification<Dog>>()))
            .ReturnsAsync(dogs);
        
        var dogService = new DogService(dogMock.Object);
        
        var sortParams = new SortParameters();
        var pagingParams = new PagingParameters {PageNumber = 1, PageSize = 2};
        
        //Act
        var result = await dogService.GetAllDogsAsync(sortParams, pagingParams);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Is.TypeOf(typeof(List<Dog>)));
        });
        
        dogMock.Verify(repository => repository.GetAllDogsAsync(
                It.Is<Specification<Dog>>(spec =>
                    spec.OrderByExpression == null &&
                    spec.OrderByDescendingExpression == null &&
                    spec.AsNoTracking == true &&
                    spec.IsPagingEnabled == true)),
            Times.Once);
    }
}