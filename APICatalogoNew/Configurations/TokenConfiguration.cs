namespace APICatalogoNew.Configurations;

public class TokenConfiguration
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int ExpireHours { get; set; }
    public string? Key { get; set; }
}