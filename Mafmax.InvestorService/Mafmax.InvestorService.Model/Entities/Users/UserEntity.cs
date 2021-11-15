using System;
using System.ComponentModel.DataAnnotations;
using Mafmax.InvestorService.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Mafmax.InvestorService.Model.Entities.Users;

/// <summary>
/// User entity
/// </summary>
[Index(nameof(Login), IsUnique = true)]
public class UserEntity : IHasId<int>
{

    /// <summary>
    /// Customer unique identifier
    /// </summary>
    [Key]
    public int Id { get; protected set; }

    /// <summary>
    /// Customer login
    /// </summary>
    public string Login { get; protected set; } = string.Empty;

    /// <summary>
    /// Customer password hash (SHA256)
    /// </summary>
    public byte[] PasswordHash { get; protected set; } = Array.Empty<byte>();
}