using Otus.HA.Api.Contracts.Users.Enums;

namespace Otus.HA.Api.Contracts.Users.Responses;

/// <summary>
/// Анкета пользователя.
/// </summary>
public class UserProfileResponse
{
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
}