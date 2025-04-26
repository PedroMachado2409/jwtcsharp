namespace jwt.Models.Response
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }
        // Podemos adicionar outras informações na resposta de autenticação, como:
        // public DateTime Expiration { get; set; }
        // public string? RefreshToken { get; set; }
    }
}