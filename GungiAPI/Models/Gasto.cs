using System.ComponentModel.DataAnnotations;

namespace GungiAPI.Models;

public class Gasto
{
    public int Id { get; set; }

    [Required] public DateTime DataLancamento { get; set; }

    [Required] [MaxLength(100)] public string Descricao { get; set; }

    [Required] public string Categoria { get; set; }

    [Required] public decimal Valor { get; set; }

    [Required] public string FormaPagamento { get; set; }

    public bool StatusPagamento { get; set; }
    public string Observacao { get; set; }
}