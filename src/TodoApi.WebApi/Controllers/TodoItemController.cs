using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Abstractions.Dto.TodoItemDtos;
using TodoApi.BusinessLogic.TodoItem.Commands.CreateTodoItem;
using TodoApi.BusinessLogic.TodoItem.Commands.DeleteTodoItem;
using TodoApi.BusinessLogic.TodoItem.Commands.UpdateTodoItems;
using TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItem;
using TodoApi.BusinessLogic.TodoItem.Queries.GetTodoItems;

namespace TodoApi.WebApi.Controllers
{
    /// <summary>
    ///     The controller is responsible for working with tasks list.
    /// </summary>
    [ApiController]
    [Route(template: "[controller]")]
    [Produces(contentType: MediaTypeNames.Application.Json)]
    public class TodoItemController : ControllerBase
    {
        /// <summary>
        ///     Default mediator implementation.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoApi.WebApi.Controllers.TodoItemController"/> class.
        /// </summary>
        /// <param name="mediator">Default mediator implementation.</param>
        public TodoItemController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(paramName: nameof(mediator));
        }

        /// <summary>
        ///     Gets the task lists.
        /// </summary>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <returns>The list of tasks.</returns>
        /// <response code="200">Returns the list of tasks.</response>
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(value: await _mediator.Send(request: new GetTodoItemsQuery(), cancellationToken: cancellationToken));
        }

        /// <summary>
        ///     Gets the task by id.
        /// </summary>
        /// <param name="id">The primary key for this task.</param>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <returns>The concrete task.</returns>
        /// <response code="200">Return the task.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If task not found.</response>
        [HttpGet(template: "{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(value: await _mediator.Send(request: new GetTodoItemQuery(id: id), cancellationToken: cancellationToken));
        }

        /// <summary>
        ///     Creates a new task.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <returns>A newly created task.</returns>
        /// <response code="201">Returns the newly created task.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(
            [FromBody] CreateTodoItemCommand command,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await _mediator.Send(request: command, cancellationToken: cancellationToken);
            return CreatedAtAction(actionName: nameof(GetTodoItem), routeValues: new {id = response.Id}, value: response);
        }

        /// <summary>
        ///     Updates the task.
        /// </summary>
        /// <param name="command">Request body.</param>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <returns>No content.</returns>
        /// <response code="204">No content.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If task not found.</response>
        [HttpPut]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(
            [FromBody] UpdateTodoItemCommand command,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _mediator.Send(request: command, cancellationToken: cancellationToken);
            return NoContent();
        }

        /// <summary>
        ///     Delete the task.
        /// </summary>
        /// <param name="id">The primary key for this task.</param>
        /// <param name="cancellationToken">Client closed request.</param>
        /// <returns>Nothing.</returns>
        /// <returns>No content.</returns>
        /// <response code="204">No content.</response>
        /// <response code="400">If the request body will not pass validation.</response>
        /// <response code="404">If task not found.</response>
        [HttpDelete(template: "{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _mediator.Send(request: new DeleteTodoItemCommand(id: id), cancellationToken: cancellationToken);
            return NoContent();
        }
    }
}