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
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Search all games paged
        /// </summary>
        /// <remarks>
        /// It is not possible to return games without pagination
        /// </remarks>
        /// <param name="page">Indicates which page is being consulted. At least 1</param>
        /// <param name="quantity">Indicates the number of registers per page. Minimum 1 and maximum 50</param>
        /// <response code="200">Return to game list</response>
        /// <response code="204">If there are no games</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obtain([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.Obtain(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Search a game by id
        /// </summary>
        /// <param name="idGame">Fetched game id</param>
        /// <response code="200">Return the filtered game</response>
        /// <response code="204">If there is no game with this id</response>

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Obtain([FromRoute] Guid idGame)
        {
            var game = await _gameService.Obtain(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>
        /// Insert a new game
        /// </summary>
        /// <param name="gameInputModel">New game parameters</param>
        /// <response code="200">Insert the new game and return it</response>
        /// <response code="204">If there is already a game with this name for this producer</response>

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
            var game = await _gameService.Insert(gameInputModel);

            return Ok(game);
            }
            catch (GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("There is already a game with this name for this producer");
            }
        }

        /// <summary>
        /// Update all game parameters
        /// </summary>
        /// <param name="idGame">Fetched game id</param>
        /// <param name="gameInputModel">Updated parameters</param>
        /// <response code="200">Update the new parameters and return it</response>
        /// <response code="204">If there is no game with this id</response>

        //update all the things
        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
            await _gameService.Update(idGame, gameInputModel);

            return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("There is no such game");
            }
        }

        /// <summary>
        /// Only update the price parameter
        /// </summary>
        /// <param name="idGame">Fetched game id</param>
        /// <param name="price">New game price</param>
        /// <response code="200">Update the price and return it</response>
        /// <response code="204">If there is no game with this id</response>

        //update a specific thing
        [HttpPatch("{idGame:guid}/price{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
            await _gameService.Update(idGame, price);

            return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("There is no such game");
            }
        }

        
        
        
        ///<summary>
        /// Delete the selected game
        /// </summary>
        /// <param name="idGame">Fetched game id</param>
        /// <response code="200">Delete the selected game</response>
        /// <response code="204">If there is no game with this id</response>

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
            await _gameService.Remove(idGame);

            return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("There is no such game");
            }
        }

    }
}
