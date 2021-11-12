using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Mafmax.InvestorService.Services;

/// <summary>
/// Service
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract class ServiceBase<TContext> where TContext : DbContext
{
    /// <summary>
    /// Application main context
    /// </summary>
    protected readonly TContext Db;

    /// <summary>
    /// Mapper instance
    /// </summary>
    protected readonly IMapper Mapper;

    /// <summary>
    /// Creates service
    /// </summary>
    /// <param name="db"></param>
    /// <param name="mapper"></param>
    protected ServiceBase(TContext db, IMapper mapper)
    {
        Db = db;
        Mapper = mapper;
    }
}