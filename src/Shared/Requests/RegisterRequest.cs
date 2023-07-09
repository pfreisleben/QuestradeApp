using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma senha")]
        [Compare(nameof(Senha), ErrorMessage = "A senha e a confirmação de senha não são iguais")]
        public string ConfirmaSenha { get; set; }
    }
}
