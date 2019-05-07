using Microsoft.AspNetCore.Mvc;
using PersonApi.Controllers;
using PersonApi.Models;
using PersonApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PersonApiTest
{
    public class PersonControllerTest
    {
        PersonController _controller;
        IPersonService _service;


        public PersonControllerTest()
        {
            _service = new PersonServiceFake();
            _controller = new PersonController(_service);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.GetPeople();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _controller.GetPeople();
            var  okObjectResult =  okResult.Result as OkObjectResult;
  
            // Assert
            var items = Assert.IsType<List<Person>>(okObjectResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.GetPerson(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }


        [Fact] 
        public async Task GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Act
            var okResult = await _controller.GetPerson(1);
            var okObjectResult = okResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<Person>(okObjectResult.Value);
            Assert.Equal(1, (okObjectResult.Value as Person).id);
        }


        [Fact]
        public async Task GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.GetPerson(100);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingPerson = new Person()
            {
                id = 3,
                birthdate = new System.DateTime(1991,5,4),
                phone = 123456123,
                email = "noname@gmail.com"

            };
            _controller.ModelState.AddModelError("name", "Required");

            // Act
            var badResponse = await _controller.PostPerson(nameMissingPerson);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse.Result);
        }


        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var validPerson = new Person()
            {
                id = 3,
                name = "Basia",
                birthdate = new System.DateTime(1991, 5, 4),
                phone = 123456123,
                email = "basia@gmail.com"

            };

            // Act
            var createdResponse =  await _controller.PostPerson(validPerson);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var validPerson = new Person()
            {
                id = 3,
                name = "Basia",
                birthdate = new System.DateTime(1991, 5, 4),
                phone = 123456123,
                email = "basia@gmail.com"

            };

            // Act
            var createdResponse = await _controller.PostPerson(validPerson);
            var createdAtActionResult = createdResponse.Result as CreatedAtActionResult;
            var person = createdAtActionResult.Value as Person;

            // Assert
            Assert.IsType<Person>(person);
            Assert.Equal(3, person.id);
        }

        [Fact]
        public async Task Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Act
            var notFoundResponse = await _controller.DeletePerson(10);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResponse);
        }

        [Fact]
        public async Task Remove_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Act
            var noContentResponse = await _controller.DeletePerson(1);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);
        }
        [Fact]
        public async Task Remove_ExistingIdPassed_RemovesOneItem()
        {
            // Act
            var okResponse = await _controller.DeletePerson(2);

            // Assert
            Assert.Single(_service.GetAll().Result);
        }


        [Fact]
        public async Task Update_ValidObjectPassed_ReturnsNoContentResult()
        {
            // Arrange
            var validPerson = new Person()
            {
                id = 1,
                name = "Marta",
                birthdate = new System.DateTime(1991, 5, 4),
                phone = 123456123,
                email = "marta@gmail.com"

            };

            // Act
            var noContentResponse = await _controller.PutPerson(1, validPerson);

            // Assert
            Assert.IsType<NoContentResult>(noContentResponse);      
        }


        [Fact]
        public async Task Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var invalidPerson = new Person()
            {
                id = 2,
                name = "Gosia",
                birthdate = new System.DateTime(1991, 5, 4),
                phone = 123456123,
                email = "gosia@gmail.com"

            };

            // Act
            var badRequestResponse = await _controller.PutPerson(1, invalidPerson);

            // Assert
            Assert.IsType<BadRequestResult>(badRequestResponse);
        }

        [Fact]
        public async Task Update_NotExistingIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var invalidPerson = new Person()
            {
                id = 10,
                name = "Maciej",
                birthdate = new System.DateTime(1991, 5, 4),
                phone = 123456123,
                email = "maciej@gmail.com"

            };

            // Act
            var badRequestResponse = await _controller.PutPerson(10, invalidPerson);

            // Assert
            Assert.IsType<BadRequestResult>(badRequestResponse);
        }

    }
}
