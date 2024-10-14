using BuildingBlocks.CQRS.Command;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shortner.Data;
using Shortner.Models;
using Shortner.Utilities;

namespace Shortner.UrlActions.CreateShortnerUrl;

public record CreateShortnerUrlCommand(string orginalUrl) : ICommand<string>;

public class CreateShortnerUrlCommandValidator : AbstractValidator<CreateShortnerUrlCommand>
{
    public CreateShortnerUrlCommandValidator()
    {
        RuleFor(x => x.orginalUrl)
            .NotEmpty().WithMessage("URL is required.")
            .Must(BeAValidUrl).WithMessage("Invalid URL format.");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}


public sealed class CreateShortnerUrlCommandHandler(AppDbContext _dbContext)
    : ICommandHandler<CreateShortnerUrlCommand, string>
{
    public async Task<string> Handle(CreateShortnerUrlCommand request, CancellationToken cancellationToken)
    {
        var shortUrl = new UrlShortnerUtility().ShortenUrl(request.orginalUrl);

        var findDuplicateLongUrl = await _dbContext.Urls
            .Where(x => x.LongUrl.Contains(request.orginalUrl))
            .ToListAsync();

        if (findDuplicateLongUrl.Count > 0)
            return findDuplicateLongUrl.FirstOrDefault().ShortUrl;
        
        Urls urls = new()
        {
            Id = new Guid(),
            InsertDate = DateTime.Now,
            LongUrl = request.orginalUrl,
            ShortUrl = shortUrl
        };

        await _dbContext.Urls.AddAsync(urls);
        await _dbContext.SaveChangesAsync();

        return urls.ShortUrl;
    }
}