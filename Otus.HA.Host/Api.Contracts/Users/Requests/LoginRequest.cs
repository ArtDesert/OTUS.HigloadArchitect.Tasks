using System.ComponentModel.DataAnnotations;

namespace Otus.HA.Api.Contracts.Users.Requests;

/// <summary>
/// Запрос на аутентификацию пользователя.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    [Required]
    public string Id { get; set; }
    
    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    [Required]
    public string Password { get; set; }
}