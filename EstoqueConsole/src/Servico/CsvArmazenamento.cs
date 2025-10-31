using EstoqueConsole.src.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueConsole.src.Servico
{
    internal class CsvArmazenamento
    {
        private readonly string produtosPath;
        private readonly string movimentosPath;

        public CsvArmazenamento(string produtosPath, string movimentosPath)
        {
            this.produtosPath = produtosPath;
            this.movimentosPath = movimentosPath;

            Directory.CreateDirectory(Path.GetDirectoryName(produtosPath));
           if(!File.Exists(produtosPath))
            File.WriteAllText(produtosPath, 
            "IdProduto,NomeProduto,DescricaoProduto,QuantidadeEstoqueProduto,EstoqueMinimoProduto,CategoriaProduto\n");
        }
        public List<Produto> CarregarProdutos()
        {
            return File.ReadAllLines(produtosPath)
                .Skip(1) // Pular o cabeçalho
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Produto(
                        int.Parse(parts[0]),
                        parts[1],
                        parts[2],
                        int.Parse(parts[3]),
                        int.Parse(parts[4])
                    )
                    { CategoriaProduto = parts[5] };
                })
                .ToList();
        }
        public List<Movimento> CarregarMovimentos()
        {
            return File.ReadAllLines(movimentosPath)
                .Skip(1) // Pular o cabeçalho
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split(',');
                    var produto = CarregarProdutos().FirstOrDefault(p => p.IdProduto == int.Parse(parts[1]));
                    return new Movimento(
                        int.Parse(parts[0]),
                        produto,
                        int.Parse(parts[2]),
                        DateTime.Parse(parts[3]),
                        parts[4]
                    );
                })
                .ToList();
        }
        public void SalvarProdutos(List<Produto> produtos)
        {
            var temp = produtosPath + ".tmp";
            File.WriteAllLines(temp, new[] {"IdProduto,NomeProduto,DescricaoProduto,QuantidadeEstoqueProduto," +
                "EstoqueMinimoProduto,CategoriaProduto" } .Concat(produtos.Select(p => $"{p.IdProduto},{p.NomeProduto}," +
                $"{p.DescricaoProduto},{p.QuantidadeEstoqueProduto},{p.EstoqueMinimoProduto},{p.CategoriaProduto}")));
        }
    }
}
