using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class UserRequest
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Sobrenome { get; set; }
    }
}
