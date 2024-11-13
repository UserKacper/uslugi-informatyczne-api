using Azure.Core;
using DnsClient;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;

public class EmailValidation : IEmailValidation
{
    private readonly IMemoryCache _memoryCache;
    private const int MaxRequests = 2;
    private const int TimeWindowInMinutes = 60;

    public EmailValidation(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<bool> IsValidEmailAsync(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            var domain = addr.Host;

            var cacheKey = $"dnsLookup_{domain}";
            if (!_memoryCache.TryGetValue(cacheKey, out bool hasMxRecords))
            {
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domain, QueryType.MX);
                var mxRecords = result.Answers.MxRecords();
                hasMxRecords = mxRecords.Any();

                _memoryCache.Set(cacheKey, hasMxRecords, TimeSpan.FromMinutes(5));
            }

            return hasMxRecords;
        }
        catch
        {
            return false;
        }
    }

    public bool IsRateLimitReached(string ipAddress)
    {
        var cacheKey = $"emailLimit_{ipAddress}";
        var requests = _memoryCache.Get<EmailRequestInfo>(cacheKey);

        if (requests == null)
        {
            _memoryCache.Set(cacheKey, new EmailRequestInfo { RequestCount = 1, LastRequestTime = DateTime.UtcNow }, TimeSpan.FromMinutes(TimeWindowInMinutes));
            return true;
        }

        if (requests.RequestCount >= MaxRequests && (DateTime.UtcNow - requests.LastRequestTime).TotalMinutes < TimeWindowInMinutes)
        {
            return false;
        }

        requests.RequestCount++;
        requests.LastRequestTime = DateTime.UtcNow;
        _memoryCache.Set(cacheKey, requests, TimeSpan.FromMinutes(TimeWindowInMinutes));

        return true;
    }
}

