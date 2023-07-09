using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Finance;

public record NewLoanRequest
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    [Range(1, 12, ErrorMessage = "Quantidade de parcelas deve estar entre 1 e 12.")]
    public int NumberOfPortions { get; set; }
    
    [Required]
    [Range(1, 5000, ErrorMessage = "Valor do empréstimo deve estar entre 1 e 5000.")]
    public decimal TotalAmount { get; set; }
    
    [Required]
    [StringLength(20, ErrorMessage = "Nome não pode ser maior que 20 caracteres")]
    public string Description { get; set; }

    public NewLoanRequest(string userId, int numberOfPortions, decimal totalAmount, string description)
    {
        UserId = userId;
        NumberOfPortions = numberOfPortions;
        TotalAmount = totalAmount;
        Description = description;
    }
}