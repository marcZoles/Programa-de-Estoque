using EstoqueConsole.src.Modelo;
using System;
using System.Collections.Generic;
using System.IO;


namespace EstoqueConsole.src.Armazenamento
{
    public class CsvArmazenamento
    {
        public static void ProcessaArquivoCSV(string caminhoArquivo)
        {

            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    Console.WriteLine("Arquivo encontrado.");
                    
                }
                else
                {
                    CriaArquivoCSV(caminhoArquivo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar o arquivo: \n\n" + ex.ToString());
            }
        }
        public static void CriaArquivoCSV(string caminhoArquivo)
        {
            try
            {
                using (File.Create(caminhoArquivo)) { }
                Console.WriteLine("Arquivo criado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar o arquivo: \n\n" + ex.ToString());
            }
        }
        public static List<Produto> CarregarProdutos(string caminhoArquivo)
        {
            List<Produto> produtos = new List<Produto>();

            ProcessaArquivoCSV(caminhoArquivo);

            string[] linhas = File.ReadAllLines(caminhoArquivo);

            foreach (string linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha))
                    continue;

                string[] dados = linha.Split(';');

                // Ignorar cabeçalho
                if (dados[0] == "id")
                    continue;

                Produto p = new Produto
                {

                    produtoId = int.Parse(dados[0]),
                    produtoNome = dados[1],
                    produtoCategoria = dados[2],
                    produtoEstoqueMinimo = int.Parse(dados[3]),
                    produtoSaldo = int.Parse(dados[4]),
                    produtoObservacao = dados[5]
                };

                produtos.Add(p);
            }

            return produtos;
        }
        public static void SalvarProdutos(string caminho, List<Produto> lista)
        {
            try
            {
                using (var writer = new StreamWriter(caminho, false))
                {
                    writer.WriteLine("id;nome;categoria;estoqueMinimo;saldo;obs");

                    foreach (var p in lista)
                    {
                        writer.WriteLine($"{p.produtoId};{p.produtoNome};{p.produtoCategoria};{p.produtoEstoqueMinimo};{p.produtoSaldo};{p.produtoObservacao};");
                    }
                }
                Console.WriteLine("Produto salvo com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar produtos: \n\n" + ex.Message);
            }
        }

        public static List<Movimento> CarregarMovimentos(string caminhoMovimentos)
        {
            List<Movimento> movimentos = new List<Movimento>();

            try
            {
                if (!File.Exists(caminhoMovimentos))
                {
                    // cria arquivo com cabeçalho
                    using (var w = new StreamWriter(caminhoMovimentos, false))
                    {
                        w.WriteLine("movimentoId;produtoId;movimentoTipo;movimentoQuantidade;movimentoData;movimentoObservacao");
                    }
                    return movimentos;
                }

                string[] linhas = File.ReadAllLines(caminhoMovimentos);

                foreach (var linha in linhas)
                {
                    if (string.IsNullOrWhiteSpace(linha))
                        continue;

                    var dados = linha.Split(';');

                    if (dados.Length < 6)
                        continue;

                    if (dados[0] == "movimentoId")
                        continue;

                    if (!int.TryParse(dados[0], out int movId)) continue;
                    if (!int.TryParse(dados[1], out int prodId)) continue;
                    if (!int.TryParse(dados[3], out int qtd)) continue;
                    if (!DateTime.TryParse(dados[4], out DateTime dt)) dt = DateTime.MinValue;

                    var mov = new Movimento
                    {
                        movimentoId = movId,
                        produtoId = prodId,
                        movimentoTipo = dados[2],
                        movimentoQuantidade = qtd,
                        movimentoData = dt,
                        movimentoObservacao = dados[5]
                    };

                    movimentos.Add(mov);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar movimentos: " + ex.Message);
            }

            return movimentos;
        }
        public static void SalvarMovimentos(string caminhoMovimentos, List<Movimento> lista)
        {
            try
            {
                using (var writer = new StreamWriter(caminhoMovimentos, false))
                {
                    writer.WriteLine("movimentoId;produtoId;movimentoTipo;movimentoQuantidade;movimentoData;movimentoObservacao");
                    foreach (var m in lista)
                    {
                        writer.WriteLine($"{m.movimentoId};{m.produtoId};{m.movimentoTipo};{m.movimentoQuantidade};{m.movimentoData:yyyy-MM-dd HH:mm:ss};{m.movimentoObservacao}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar movimentos: " + ex.Message);
            }
        }
    }
}