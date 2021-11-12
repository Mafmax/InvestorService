using AutoMapper;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Commands.Portfolios;

namespace Mafmax.InvestorService.Services.Profiles;

/// <summary>
/// Provides mapper configuration
/// </summary>
// ReSharper disable once UnusedType.Global
public class MappingProfile : Profile
{

    /// <summary>
    /// Create profile
    /// </summary>
    public MappingProfile()
    {
        CreateMap<AssetEntity, ShortAssetDto>()
            .ForCtorParam(nameof(ShortAssetDto.ExchangeStockCode), opt => opt.MapFrom(src => src.Stock.Key))
            .ForCtorParam(nameof(ShortAssetDto.IssuerName), opt => opt.MapFrom(src => src.Issuer.Name));

        CreateMap<IssuerEntity, IssuerDto>()
            .ForCtorParam(nameof(IssuerDto.Country), opt => opt.MapFrom(src => src.Country.Name))
            .ForCtorParam(nameof(IssuerDto.Industry), opt => opt.MapFrom(src => src.Industry.Name));

        CreateMap<CirculationPeriodEntity, CirculationPeriodDto>();

        CreateMap<StockExchangeEntity, StockExchangeDto>();

        CreateMap<AssetEntity, AssetDto>();

        CreateMap<ExchangeTransactionEntity, ExchangeTransactionDto>()
            .ForCtorParam(nameof(ExchangeTransactionDto.Price),
                opt => opt.MapFrom(src => src.OneLotPrice));

        CreateMap<InvestmentPortfolioEntity, PortfolioShortInfoDto>();

        CreateMap<InvestmentPortfolioEntity, PortfolioDetailedInfoDto>();

        CreateMap<CreatePortfolioCommand, InvestmentPortfolioEntity>();
    }
}