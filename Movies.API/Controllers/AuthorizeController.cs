using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models.Request;
using Movies.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IConfiguration _configuration;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthorizeController(ITenantRepository tenantRepository, IConfiguration configuration)
    {
        _tenantRepository = tenantRepository;
        _configuration = configuration;

        var key = configuration["Jwt:Key"];
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        _jwtTokenGenerator = new JwtTokenGenerator(key, issuer, audience);
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateClient([FromBody] ClientCredentials credentials)
    {
        if (credentials == null || string.IsNullOrWhiteSpace(credentials.ClientId) || string.IsNullOrWhiteSpace(credentials.ClientSecret))
        {
            return BadRequest(new { message = "ClientId and ClientSecret are required." });
        }

        bool isValid = await _tenantRepository.ValidateClientAsync(credentials.ClientId, credentials.ClientSecret);

        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid ClientId or ClientSecret." });
        }

        return Ok(new
        {
            access_token = CreateToken(credentials.ClientId),
            expires_at = DateTime.UtcNow.AddMinutes(30)
        });
    }

    private string CreateToken(string clientId)
    {
        return _jwtTokenGenerator.GenerateToken(clientId);
    }
}