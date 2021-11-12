using Mafmax.InvestorService.Model.Entities.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mafmax.InvestorService.Model.EntitiesConfiguration.Assets;

/// <summary>
/// Class for AssetEntity configuration
/// </summary>
public class AssetEntityTypeConfiguration : IEntityTypeConfiguration<AssetEntity>
{

    /// <summary>
    /// Configures AssetEntity
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<AssetEntity> builder) =>
        builder.HasDiscriminator(x => x.Class)
            .HasValue<BondEntity>("Облигация")
            .HasValue<ShareEntity>("Акция");
}