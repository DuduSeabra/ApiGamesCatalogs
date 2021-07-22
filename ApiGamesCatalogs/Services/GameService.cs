using ApiGamesCatalogs.Entities;
using ApiGamesCatalogs.Exceptions;
using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.Repositories;
using ApiGamesCatalogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Obtain(game.Name, game.Producer);

            if (gameEntity.Count > 0)
                throw new GameAlreadyRegisteredException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<List<GameViewModel>> Obtain(int page, int quantity)
        {
            var games = await _gameRepository.Obtain(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Obtain(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.Obtain(id);

            if (game == null)
                throw new GameNotRegisteredException();

            await _gameRepository.Delete(id);
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.Obtain(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Name = game.Name;
            gameEntity.Producer = game.Producer;
            gameEntity.Price = game.Price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double price)
        {
            var gameEntity = await _gameRepository.Obtain(id);

            if (gameEntity == null)
                throw new GameNotRegisteredException();

            gameEntity.Price = price;

            await _gameRepository.Update(gameEntity);
        }
    }
}
