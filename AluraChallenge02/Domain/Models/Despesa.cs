using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge02.Models
{
    [Table("Despesa")]
    public class Despesa
    {
        public Despesa(string descricao, decimal valor, DateTime data, Categoria categoria = Categoria.Saúde)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Entre com a descrição.")]
        [MaxLength(100)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Entre com o valor.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Entre com a data.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Entre com a categoria.")]
        public Categoria Categoria { get; set; }

    }

    public enum Categoria
    { Alimentação, Saúde, Moradia, Tansporte, Educação, Lazer, Imprevistos, Outras };
}