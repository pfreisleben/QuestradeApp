using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Senha { get; set; }

    }
}
