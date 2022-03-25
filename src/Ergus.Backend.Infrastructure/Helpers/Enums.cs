using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ergus.Backend.Infrastructure.Helpers
{
    public enum AuthSchemaType 
    {
        [Description("basic")]
        Basic,
        [Description("bearer")]
        Bearer
    }

    public enum TokenType 
    {
        [Description("access_token")]
        AccessToken,
        [Description("refresh_token")]
        RefreshToken 
    }

    public enum TipoAnuncio
    {
        [Description("none")]
        [EnumMember(Value = "Nenhum")]
        None = 0,

        [Description("gold_special")]
        [EnumMember(Value = "Clássico")]
        GoldSpecial = 1,

        [Description("gold_pro")]
        [EnumMember(Value = "Premium")]
        GoldPro = 2,

        [Description("gold_premium")]
        [EnumMember(Value = "Diamante")]
        GoldPremium = 3,

        [Description("free")]
        [EnumMember(Value = "Grátis")]
        Free = 4,

        [Description("bronze")]
        [EnumMember(Value = "Bronze")]
        Bronze = 5,

        [Description("silver")]
        [EnumMember(Value = "Prata")]
        Silver = 6,

        [Description("gold")]
        [EnumMember(Value = "Ouro")]
        Gold = 7,
    }

    public enum TipoStatusAnuncio
    {
        [Description("ativo")]
        Ativo = 0,
        [Description("inativo")]
        Inativo = 1,
        [Description("pausado")]
        Pausado = 2,
    }
}
