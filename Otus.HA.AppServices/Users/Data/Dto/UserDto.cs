using Otus.HA.AppServices.Users.Data.Enums;

namespace Otus.HA.AppServices.Users.Data.Dto;

/// <summary>
/// Пользователь.
/// </summary>
public class UserDto
{
    /// <summary>
    /// ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public Sex Sex { get; set; }

    /// <summary>
    /// Интересы.
    /// </summary>
    public string Biography { get; set; }

    /// <summary>
    /// Город.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Хеш пароля.
    /// </summary>
    public string PasswordHash { get; set; }
}