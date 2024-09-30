namespace Shortner.Models;

public sealed class Urls
{
    public Guid Id { get; set; }
    public string ShortUrl { get; set; }
    public string LongUrl { get; set; }
    public DateTime InsertDate { get; set; } = DateTime.Now;
}