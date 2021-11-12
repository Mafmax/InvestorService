using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities.Users;

/// <summary>
/// User entity
/// </summary>
[Index(nameof(Login), IsUnique = true)]
public class UserEntity
{

    /// <summary>
    /// Customer unique identifier
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Customer login
    /// </summary>
    [Required]
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Customer password hash (SHA256)
    /// </summary>
    [Required]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
}