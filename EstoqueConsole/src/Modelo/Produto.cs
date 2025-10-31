using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueConsole.src.Modelo
{
    internal class Produto
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public int QuantidadeEstoqueProduto { get; set; }
        public int EstoqueMinimoProduto { get; set; }
        public string CategoriaProduto { get; set; }
        public int saldoProduto { get; set; }
        public Produto(int idProduto, string nomeProduto, string descricaoProduto, int quantidadeEstoqueProduto, int estoqueMinimoProduto)
        {
            IdProduto = idProduto;
            NomeProduto = nomeProduto;
            DescricaoProduto = descricaoProduto;
            QuantidadeEstoqueProduto = quantidadeEstoqueProduto;
            EstoqueMinimoProduto = estoqueMinimoProduto;
        }
    }
}
