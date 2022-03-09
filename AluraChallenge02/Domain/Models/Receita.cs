using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge02.Models
{
    [Table("Receita")]
    public class Receita
    {
        #region construtor

        public Receita(string descricao, decimal valor, DateTime data)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
        }

        #endregion

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Entre com a descrição.")]
        [MaxLength(40)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Entre com o valor.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Entre com a data.")]
        public DateTime Data { get; set; }

    }
}