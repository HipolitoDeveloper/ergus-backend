using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ergus.Backend.Infrastructure.Helpers
{
    #region [ Anuncio ]

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
        [Description("nenhum")]
        Nenhum = 0,
        [Description("ativo")]
        Ativo = 1,
        [Description("inativo")]
        Inativo = 2,
        [Description("pausado")]
        Pausado = 3,
    }

    #endregion [ FIM - Anuncio ]

    #region [ Lista Preço ]

    public enum TipoAjuste
    {
        [Description("nenhum")]
        Nenhum = 0,
        [Description("percentual")]
        Percentual = 1,
        [Description("valorfixo")]
        ValorFixo = 2,
    }

    public enum TipoListaPreco
    {
        [Description("nenhum")]
        Nenhum = 0,
        [Description("dinamico")]
        Dinamico = 1,
        [Description("estatico")]
        Estatico = 2,
    }

    public enum TipoOperacao
    {
        [Description("nenhum")]
        Nenhum = 0,
        [Description("adicao")]
        Adicao = 1,
        [Description("subtracao")]
        Subtracao = 2,
    }

    #endregion [ FIM - Lista Preço ]

    #region [ Token ]

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

    #endregion [ FIM - Token ]
}
