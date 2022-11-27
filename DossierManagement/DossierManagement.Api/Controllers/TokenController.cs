using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DossierManagement.Api.DTOs;
using DossierManagement.Api.Services;
using System.Drawing;

namespace DossierManagement.Api.Controllers
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
            var tokenDto = _tokenService.Generate();
            return Ok(tokenDto);
        }

    }
}
