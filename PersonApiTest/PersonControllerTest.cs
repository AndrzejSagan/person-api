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
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetPeople();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Person>>>(okResult.Result);
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

    }
}
