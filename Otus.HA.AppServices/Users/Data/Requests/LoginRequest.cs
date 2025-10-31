namespace Otus.HA.AppServices.Users.Data.Requests;

/// <summary>
/// Запрос на аутентификацию пользователя.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string Password { get; set; }
}