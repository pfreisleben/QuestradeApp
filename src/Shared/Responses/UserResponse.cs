namespace Shared.Responses;

public class UserResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; } = true;
    public string PhoneNumber { get; set; }
}