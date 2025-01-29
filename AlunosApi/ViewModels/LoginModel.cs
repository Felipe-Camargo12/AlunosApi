using System.ComponentModel.DataAnnotations;

namespace AlunosApi.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]

        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatório")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} e no" +
            "máximo {1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
