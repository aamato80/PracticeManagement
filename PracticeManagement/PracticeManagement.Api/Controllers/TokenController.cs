using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Api.Services;
using System.Drawing;

namespace PracticeManagement.Api.Controllers
{
    [ApiController]
    [Route("/api/Token")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenService _tokenService;

        public TokenController(ILogger<TokenController> logger,ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpGet()]
        public ActionResult<TokenDto> GetToken()
        {
         _tokenService.Validate("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2Njg5NDkyODgsImV4cCI6MTY2ODk1MDQ4OCwiaWF0IjoxNjY4OTQ5Mjg4fQ.6GKFcnAmIyidO5xzwIEQMxxTgDvWFqB8DtwLCI2X1I8");
            var tokenDto = _tokenService.Generate();
            return Ok(tokenDto);
        }
    }
}
