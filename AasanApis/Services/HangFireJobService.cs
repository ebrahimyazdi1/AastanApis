using Newtonsoft.Json.Linq;

namespace AastanApis.Services;

public class HangFireJobService
{
    private readonly AastanClient _aastanService;

    public HangFireJobService(AastanClient service)
    {
        _aastanService = service;
    }

    public async Task CheckAndRefreshShakarTokenAsync()
    {
        var expirationTime = _aastanService.GetShahkerTokenExpiration();
        var currentTime = DateTime.UtcNow;

        if (expirationTime.HasValue && expirationTime.Value <= currentTime)
            await RefreshExpiredShakarToken();
    }

    public async Task CheckAndRefreshPSGBTokenAsync()
    {
        var expirationTime = _aastanService.GetPgsbTokenExpiration();
        var currentTime = DateTime.UtcNow;

        if (expirationTime.HasValue && expirationTime.Value <= currentTime)
            await RefreshExpiredPgsbToken();
    }

    public async Task RefreshExpiredPgsbToken()
    {
        await _aastanService.RefreshExpiredPgsbToken();
    }

    public async Task RefreshExpiredShakarToken()
    {
        await _aastanService.RefreshExpiredShahkarToken();
    }
}

