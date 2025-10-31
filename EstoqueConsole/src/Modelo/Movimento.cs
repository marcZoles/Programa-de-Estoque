using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueConsole.src.Modelo
{
    internal class Movimento
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; } // "Entrada" ou "Saída"
        public Movimento(int id, Produto produto, int quantidade, DateTime dataMovimento, string tipoMovimento)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
        }
    }
}
