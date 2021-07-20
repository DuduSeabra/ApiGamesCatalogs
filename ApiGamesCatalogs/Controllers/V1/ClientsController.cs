using ApiGamesCatalogs.Exceptions;
using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.Services;
using ApiGamesCatalogs.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Search all clients paged
        /// </summary>
        /// <remarks>
        /// It is not possible to return clients without pagination
        /// </remarks>
        /// <param name="page">Indicates which page is being consulted. At least 1</param>
        /// <param name="quantity">Indicates the number of registers per page. Minimum 1 and maximum 50</param>
        /// <response code="200">Return to client list</response>
        /// <response code="204">If there are no clients</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> Obtain([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var clients = await _clientService.Obtain(page, quantity);

            if (clients.Count() == 0)
                return NoContent();

            return Ok(clients);
        }

        /// <summary>
        /// Search a client by id
        /// </summary>
        /// <param name="idClient">Fetched client id</param>
        /// <response code="200">Return the filtered client</response>
        /// <response code="204">If there is no client with this id</response>

        [HttpGet("{idClient}")]
        public async Task<ActionResult<ClientViewModel>> Obtain([FromRoute] string idClient)
        {
            var client = await _clientService.Obtain(idClient);

            if (client == null)
                return NoContent();

            return Ok(client);
        }

        /// <summary>
        /// Insert a new order
        /// </summary>
        /// <param name="clientInputModel">New client parameters</param>
        /// <response code="200">Insert the new client and return it</response>
        /// <response code="204">If there is already a client with this name for this producer</response>

        [HttpPost]
        public async Task<ActionResult<ClientViewModel>> InsertClient([FromBody] ClientInputModel clientInputModel)
        {
            try
            {
                var client = await _clientService.Insert(clientInputModel);

                return Ok(client);
            }
            catch (ClientAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("There is already a client with this name");
            }
        }

        /// <summary>
        /// Delete the selected client
        /// </summary>
        /// <param name="idClient">Fetched client id</param>
        /// <response code="200">Delete the selected client</response>
        /// <response code="204">If there is no client with this id</response>

        [HttpDelete("{idClient}")]
        public async Task<ActionResult> DeleteClient([FromRoute] string idClient)
        {
            try
            {
                await _clientService.Remove(idClient);

                return Ok();
            }
            catch (ClientNotRegisteredException)
            {
                return NotFound("There is no such client");
            }
        }
    }
}
//Ja fiz a viewmodel, e as Inputmodel faltam os repositories e controller e services.