using NSE.Core.Comunication;
using NSE.Core.DomainObjects;
using NSE.Core.Validations;
using NSE.WebApp.MVC.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class UsuarioRegistro
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        [DisplayName("Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [DisplayName("CPF")]
        [Cpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [EmailAddress(ErrorMessage = ValidationMessages.Invalid)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(100, ErrorMessage = ValidationMessages.StringLength, MinimumLength = 6)]
        public string Senha { get; set; }

        [DisplayName("Confirme sua senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string SenhaConfirmacao { get; set; }
    }

    public class UsuarioLogin
    {
        [Required(ErrorMessage = ValidationMessages.Required)]
        [EmailAddress(ErrorMessage = ValidationMessages.Invalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.Required)]
        [StringLength(100, ErrorMessage = ValidationMessages.StringLength, MinimumLength = 6)]
        public string Senha { get; set; }
    }

    public class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}