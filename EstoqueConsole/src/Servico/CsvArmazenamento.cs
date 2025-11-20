using EstoqueConsole.src.Modelo;
using System;
using System.Collections.Generic;
using System.IO;


namespace EstoqueConsole.src.Armazenamento
{
    public class CsvArmazenamento
    {
        string caminhoArquivo = @"C:\Users\guide\Desktop\Nova pasta\produtos.csv";
        public static void ProcessaArquivoCSV(string caminhoArquivo)
        {
            List<Produto> produtos = new List<Produto>();
            produtos = CarregarProdutos(caminhoArquivo);

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

            // Garante que o arquivo exista (sua função)
            ProcessaArquivoCSV(caminhoArquivo);

            // Agora lê o arquivo
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
                    produtoSaldo = int.Parse(dados[1]),
                    produtoNome = dados[2]
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
                    writer.WriteLine("id;nome;saldo");

                    foreach (var p in lista)
                    {
                        writer.WriteLine($"{p.produtoId};{p.produtoNome};{p.produtoSaldo}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao salvar produtos: \n\n" + ex.Message);
            }
        }
    }
}