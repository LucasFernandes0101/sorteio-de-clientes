using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSorteio.Models.Entidades
{
    [Table("tb_cliente")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        public int NumeroSorteado { get; set; }

        [Display(Description = "Nome")]
        [Required(ErrorMessage = "Preencha este campo")]
        [MinLength(3, ErrorMessage = "O nome deve possuir entre 2 e 40 caracteres!")]
        [MaxLength(40, ErrorMessage = "O nome deve possuir entre 2 e 40 caracteres!")]
        public string Nome { get; set; }

        [Display(Description = "Telefone")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Telefone { get; set; }

        [Display(Description = "Email")]
        [Required(ErrorMessage = "Preencha este campo")]
        public string Email { get; set; }
    }
}