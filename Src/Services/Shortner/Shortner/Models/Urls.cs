using MongoDB.EntityFrameworkCore;

namespace Shortner.Models;

[Collection("Urls")]
public sealed class Urls
{
    public Guid Id { get; set; }
    public string ShortUrl { get; set; }
    public string LongUrl { get; set; }
    public DateTime InsertDate { get; set; } = DateTime.Now;
}