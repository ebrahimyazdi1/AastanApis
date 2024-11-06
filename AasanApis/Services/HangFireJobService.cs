using Newtonsoft.Json.Linq;

namespace AastanApis.Services;

public class HangFireJobService
{
    private readonly AastanClient _aastanService;

    public HangFireJobService(AastanClient service)
    {
        _aastanService = service;
    }

    public async Task CheckAndRefreshTokenAsync()
    {
        var expirationTime = _aastanService.GetPgsbTokenExpiration();
        var currentTime = DateTime.UtcNow;

        await RefreshExpiredPgsbToken();
        //if (expirationTime.HasValue && expirationTime.Value <= currentTime)
        //    await RefreshExpiredPgsbToken();
    }

    public async Task RefreshExpiredPgsbToken()
    {
        await _aastanService.RefreshExpiredPgsbToken();
    }
}

