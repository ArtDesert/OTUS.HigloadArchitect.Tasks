namespace Otus.HA.Domain.Entities;

/// <summary>
/// Сущность пользователя.
/// </summary>
public class UserEntity
{
    /// <summary>
    /// ID.
    /// </summary>
    public string Id { get; set; }
    
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
    public string Sex { get; set; }

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