using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Infrastructure.Authentication.Common;

public class JwtOptions
{
    public const string Section = "JwtOptions";
    public const string IssuerSection = "JwtOptions:Issuer";
    public const string AudienceSection = "JwtOptions:Audience";
    public const string IssuerSigningKeySection = "JwtOptions:SecretKey";

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int Expire { get; set; }
    public int ExpireRefreshToken { get; set; }
    public string SecretKey { get; set; } = string.Empty;
}