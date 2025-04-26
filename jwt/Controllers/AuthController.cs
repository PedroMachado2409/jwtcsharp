using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using jwt.Models.Request;
using jwt.Models.Response;
using jwt.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace jwt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;

        public AuthController(IAuthService authService, IUserService userService,
                              IValidator<RegisterRequest> registerValidator,
                              IValidator<LoginRequest> loginValidator)
        {
            _authService = authService;
            _userService = userService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="model">Dados para o registro do usuário.</param>
        /// <returns>Retorna um status 201 Created em caso de sucesso.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var validationResult = await _registerValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var existingUserByUsername = await _userService.GetUserByUsernameAsync(model.Username!);
            if (existingUserByUsername != null)
            {
                return Conflict($"Nome de usuário '{model.Username}' já está em uso.");
            }

            var existingUserByEmail = await _userService.GetUserByEmailAsync(model.Email!);
            if (existingUserByEmail != null)
            {
                return Conflict($"O e-mail '{model.Email}' já está em uso.");
            }

            var newUser = new Data.Entities.User
            {
                Username = model.Username!,
                Email = model.Email!,
                CreatedAt = DateTime.UtcNow
            };

            await _userService.RegisterAsync(newUser, model.Password!);

            return StatusCode(StatusCodes.Status201Created); // Created
        }

        /// <summary>
        /// Realiza a autenticação do usuário e retorna um token JWT.
        /// </summary>
        /// <param name="model">Credenciais de login do usuário.</param>
        /// <returns>Retorna um token JWT em caso de sucesso (200 OK) ou erro de autenticação (401 Unauthorized).</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var validationResult = await _loginValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var authResponse = await _authService.Authenticate(model);

            if (authResponse == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            return Ok(authResponse);
        }
    }
}