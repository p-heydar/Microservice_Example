using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Shortner.Models;

namespace Shortner.Data.Configuration;

public sealed class UrlsConfiguration:IEntityTypeConfiguration<Urls>
{
    public void Configure(EntityTypeBuilder<Urls> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x=>new {x.ShortUrl, x.LongUrl});
        builder.ToCollection("Urls");
    }
}