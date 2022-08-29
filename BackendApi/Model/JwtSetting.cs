namespace BackendApi.Model;

public class JwtSetting
{
    public string Authority { get; set; }

    public string Secret { get; set; }

    public string Issuer { get; set; }

    public int ExpiresInMinute { get; set; }

    public int MobileExpiresInMinute { get; set; }
}