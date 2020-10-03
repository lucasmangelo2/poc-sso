using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SSO.ViewModel
{
    public class ApplicationViewModel
    {
        [Required, Display(Name = "Identificador")]
        public string ClientId { get; set; }

        [Required, Display(Name = "Nome")]
        public string ClientName { get; set; }

        [Required, Display(Name = "Tipo de autenticação")]
        public string AllowedGrantTypes { get; set; }

        [Display(Name = "Exige consentimento")]
        public bool RequireConsent { get; set; } = false;

        [Display(Name = "Sempre incluir permissões de usuários e token")]
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = true;

        [Required, Display(Name = "Urls de redirecionamento")]
        public string RedirectUris { get; set; }

        [Required, Display(Name = "Urls de redirecionamento após logout")]
        public string PostLogoutRedirectUris { get; set; }

        [Required, Display(Name = "Chave secreta")]
        public string ClientSecrets { get; set; }

        [Display(Name = "Escopos permitidos")]
        public List<string> AllowedScopes { get; set; }

        [Required, Display(Name = "Tempo da sessão")]
        public int AccessTokenLifetime { get; set; } = 3600;
    }
}
