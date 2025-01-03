﻿using BlackJack.API.Services.GameSessionService;
using BlackJack.Domain.Entities;
using BlackJack.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [AllowAnonymous]
    public class GameSessionController : ControllerBase
    {
        private readonly IGameSessionService _gameSessionService;
        public GameSessionController(IGameSessionService gameService)
        {
            _gameSessionService = gameService;
        }

        [HttpGet("games")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseData<List<GameSession>>>> GetGames(int pageNo = 1,
                                                                                      int pageSize = 3)
        {
            return Ok(await _gameSessionService.GetGameSessionsListAsync(pageNo, pageSize));
        }
    }
}
