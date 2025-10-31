using System.ComponentModel.DataAnnotations;
using Otus.HA.Api.Contracts.Users.Enums;

namespace Otus.HA.Api.Contracts.Users.Requests;

/// <summary>
/// Запрос на регистрацию пользователя.
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// Имя.
    /// </summary>
    [Required, MaxLength(30)]
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    [Required, MaxLength(30)]
    public string LastName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    [Required]
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    [Required]
    public Sex Sex { get; set; }

    /// <summary>
    /// Интересы.
    /// </summary>
    public string Biography { get; set; }

    /// <summary>
    /// Город.
    /// </summary>
    [Required, MaxLength(30)]
    public string City { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    [Required]
    public string Password { get; set; }
}