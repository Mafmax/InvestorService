using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Mafmax.InvestorService.Model.Entities;
using Mafmax.InvestorService.Model.Entities.Assets;
using Mafmax.InvestorService.Model.Entities.ExchangeTransaction;
using Mafmax.InvestorService.Model.Entities.Users;
using Mafmax.InvestorService.Services.DTOs;
using Mafmax.InvestorService.Services.Services.Commands.ExchangeTransactions;
using Mafmax.InvestorService.Services.Services.Commands.Login;
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

        CreateMap<CreateExchangeTransactionCommand, ExchangeTransactionEntity>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.OrderToBuy ? ExchangeTransactionType.Buy : ExchangeTransactionType.Sell));

        CreateMap<RegisterInvestorCommand, InvestorEntity>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => SHA256.HashData(Encoding.Default.GetBytes(src.Password))));
    }
}