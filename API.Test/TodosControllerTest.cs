using Api.Controllers;
using Api.Dtos;
using Api.Helper;
using AutoMapper;
using Entity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Runtime.CompilerServices;
using static Api.Controllers.TodosController;

namespace Api.Test
{
    [TestClass]
    public class TodosControllerTest
    {
        private Mock<ITodoRepository> _todoRepositoryMock;
        private Mock<ILogger<TodosController>> _loggerMock;
        private TodosController _controller;


        [TestInitialize]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            IMapper _mapper = config.CreateMapper();

            _todoRepositoryMock = new Mock<ITodoRepository>();
            _loggerMock = new Mock<ILogger<TodosController>>();

                _controller = new TodosController(
               _todoRepositoryMock.Object,
               _mapper,
               _loggerMock.Object
           );
        }

        [TestMethod]
        public async Task GetTodosShuldReturnAllTodos()
        {
            //Arrange
            var sampleTodos = new List<Todo>
            {
                new Todo { Id = 1, Title = "Todo 1", Completed = false},
                new Todo { Id = 2, Title = "Todo 2", Completed = false }
            };

            _todoRepositoryMock.Setup(repo => repo.GetTodos())
               .ReturnsAsync(sampleTodos);
           
            //Act
            var result = await _controller.GetTodos() as ActionResult<List<TodoDto>>;

            //Assert

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var todoDtosResult = okResult.Value as List<TodoDto>;

            Assert.IsNotNull(todoDtosResult);
            Assert.AreEqual(sampleTodos.Count, todoDtosResult.Count);
        }


        [TestMethod]

        public async Task GetTodosWhenNotFoundShuldReturnNotFound() 
        {
            //Arange
            var sampleTodos = new List<Todo>
            {
               
            };

            _todoRepositoryMock.Setup(repo => repo.GetTodos())
               .ReturnsAsync(sampleTodos);

            //Act
            var result = await _controller.GetTodos() as ActionResult<List<TodoDto>>;

            //Assert
            Assert.IsNotInstanceOfType(result, typeof(NotFoundObjectResult));
         
        }

        [TestMethod]

        public async Task GetTodoShuldReturnTodoWithId() 
        {
            //Arrange
            var todoId = 1;
            var sampleTodo = new Todo { Id = todoId, Title = "Shopping", Completed = false };
            
            _todoRepositoryMock.Setup(repo => repo.GetTodo(todoId))
                .ReturnsAsync(sampleTodo);
            //Act  

            var result = await _controller.GetTodo(todoId) as ActionResult<TodoDto>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var todoDtoResult = okResult.Value as TodoDto;

            Assert.IsNotNull(todoDtoResult);
            Assert.AreEqual(sampleTodo.Id, todoDtoResult.Id);
        }

        [TestMethod]

        public async Task GetTodoWhitoutIdShuldReturnNotFound()
        {
            //Arange
            var todoId = 0;
            var sampleTodo = new Todo { Id = todoId, Title = "Shopping", Completed = false };

            _todoRepositoryMock.Setup(repo => repo.GetTodo(todoId))
               .ReturnsAsync(sampleTodo);

            //Act
            var result = await _controller.GetTodo(id: 1) as ActionResult<TodoDto>;

            //Assert
            Assert.IsNotInstanceOfType(result.Result, typeof(NotFoundObjectResult));

        }

        [TestMethod]

        public async Task PostTodoShuldCreateATodo() 
        {
            //Arrange
            var createdTodo = new Todo { Id = 1, Title = "New Post", Completed = false };

            _todoRepositoryMock.Setup(repo => repo.AddTodo(createdTodo))
                .ReturnsAsync(createdTodo);

            //Act  
            var result = await _controller.PostTodo(createdTodo);

            //Assert
            Assert.IsNotNull(result);

            var createdResult = result.Result as CreatedAtActionResult;
            var todoResult = createdResult.Value as Todo;

            Assert.AreEqual(createdTodo, todoResult);

        }

        [TestMethod]
        public async Task DeleteTodo_ShuldDeleteTodoByIdAndReturnOk()
        {
            //Arrange
            var deleteTodo = new Todo { Id = 1, Title = "Deleted Post" };

            _todoRepositoryMock.Setup(repo => repo.DeleteTodoById(1))
                .ReturnsAsync(deleteTodo);

            //Act  
            var result = await _controller.DeleteTodo(1);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

    }
}