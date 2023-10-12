using CodeBridge.Application.Services;
using CodeBridge.Domain.Entities;
using CodeBridge.Domain.Interfaces;
using Moq;

namespace CodeBridge.UnitTests;

public class DogServiceCreateDogUnitTest
{
    [Test]
    public void CreateDogShouldReturnTask()
    {
        //Arrange
        var dog = new Dog{Name = "a"};
        
        var dogMock = new Mock<IDogRepository>();
        dogMock.Setup(repository => repository.CreateDogAsync(It.IsAny<Dog>())).Returns(Task.CompletedTask);
        
        var service = new DogService(dogMock.Object);
        
        //Act
        var result = service.CreateDogAsync(dog);
        
        //Assert
        Assert.That(result, Is.EqualTo(Task.CompletedTask));
    }
}