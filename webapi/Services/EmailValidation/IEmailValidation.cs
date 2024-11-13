using System;

public interface IEmailValidation
{
    Task<bool> IsValidEmailAsync(string emailAddress);
    public bool IsRateLimitReached(string ipAddress);

}
