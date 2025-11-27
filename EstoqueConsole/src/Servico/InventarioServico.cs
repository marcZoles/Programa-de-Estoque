using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EstoqueConsole.src.Modelo;
using EstoqueConsole.src.Armazenamento;

namespace EstoqueConsole.src.Inventario
{
    public class InventarioServico
    {
        static List<Produto> listaProdutos = new List<Produto>();
        static List<Movimento> listaMovimentos = new List<Movimento>();
        static string caminhoArquivo = @"C:\Users\TREVOTECH\source\repos\estoqueConsole\EstoqueConsole\EstoqueConsole\data\produtos.csv";
        static string caminhoMovimentos = @"C:\Users\TREVOTECH\source\repos\estoqueConsole\EstoqueConsole\EstoqueConsole\data\movimentos.csv";

        public static void MostrarMenu()
        {
            Console.Clear();
            listaProdutos = CsvArmazenamento.CarregarProdutos(caminhoArquivo);

            Console.WriteLine("SISTEMA PARA CADASTRAMENTO DE PRODUTOS");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("José - Bianca - Thais - Gabriel");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Listar Produtos");
            Console.WriteLine("3 - Editar Produto");
            Console.WriteLine("4 - Excluir Produto");
            Console.WriteLine("5 - Dar ENTRADA em estoque");
            Console.WriteLine("6 - Dar SAÍDA em estoque");
            Console.WriteLine("7 - Relatório: Estoque abaixo do mínimo");
            Console.WriteLine("8 - Relatório: Extrato de movimento por produto");
            Console.WriteLine("9 - Salvar");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("------------------------------------------------");

            SelecionaOpcao();
        }

        public static void SelecionaOpcao()
        {
            try
            {
                string opcao = string.Empty;
                int opcaovalida = -1;

                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine()!;
                int.TryParse(opcao, out opcaovalida);

                if (opcaovalida > 0 && opcaovalida < 10)
                {
                    Console.WriteLine($"Você escolheu a opção {opcaovalida}");
                    ChamaFuncaoEscolhida(opcaovalida);
                }

                else if (opcaovalida == 0)
                {
                    Console.WriteLine("Saindo do sistema...");
                    Environment.Exit(0);
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Opção inválida. Tente novamente.\n\n");
                    return;

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao selecionar opção: \n\n" + ex.Message);
            }
        }
        public static void ChamaFuncaoEscolhida(int opcaovalida)
        {
            try
            {
                switch (opcaovalida)
                {
                    case 1:
                        CriarProduto();
                        break;
                    case 2:
                        ListarProdutos();
                        break;
                    case 3:
                        EditarProduto();
                        break;
                    case 4:
                        ExcluirProduto();
                        break;
                    case 5:
                        DarEntradaEstoque();
                        break;
                    case 6:
                        DarSaidaEstoque();
                        break;
                    case 7:
                        RelatorioEstoqueAbaixoMinimo();
                        break;
                    case 8:
                        RelatorioExtratoMovimentoPorProduto();
                        break;
                    case 9:
                        CsvArmazenamento.SalvarProdutos(caminhoArquivo, listaProdutos);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao chamar função escolhida: \n\n" + ex.Message);
            }
        }

        //FUNCOES DE PRODUTOS
        public static void CriarProduto()
        {
            try
            {
                Produto p1 = new Produto();

                Console.WriteLine("=== ADICIONAR PRODUTO ===");
                string entrada;
                do
                {
                    Console.Write("Nome do produto: ");
                    entrada = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(entrada) && !int.TryParse(entrada, out _))
                        break;

                    Console.WriteLine("Digite apenas TEXTO para o nome do produto.");
                }
                while (true);
                p1.produtoNome = entrada;

                int tempInt;
                while (true)
                {
                    Console.Write("Digite o ID do produto: ");
                    entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out tempInt))
                        break;

                    Console.WriteLine("Digite apenas NÚMEROS para o ID.");
                }
                p1.produtoId = tempInt;

                do
                {
                    Console.Write("Digite a categoria do produto: ");
                    entrada = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(entrada) && !int.TryParse(entrada, out _))
                        break;

                    Console.WriteLine("Digite apenas TEXTO para a categoria.");
                }
                while (true);
                p1.produtoCategoria = entrada;

                while (true)
                {
                    Console.Write("Digite o estoque mínimo do produto: ");
                    entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out tempInt))
                        break;

                    Console.WriteLine("Digite apenas NÚMEROS para o estoque mínimo.");
                }
                p1.produtoEstoqueMinimo = tempInt;

                while (true)
                {
                    Console.Write("Quantidade do produto: ");
                    entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out tempInt))
                        break;

                    Console.WriteLine("Digite apenas NÚMEROS para a quantidade.");
                }
                p1.produtoSaldo = tempInt;

                do
                {
                    Console.Write("Observação: ");
                    entrada = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(entrada) && !int.TryParse(entrada, out _))
                        break;

                    Console.WriteLine("Digite apenas TEXTO para a observação.");
                }
                while (true);
                p1.produtoObservacao = entrada;


                if (p1.produtoSaldo < 0 || p1.produtoSaldo < p1.produtoEstoqueMinimo)
                {
                    Console.WriteLine("Quantidade inválida!");
                    return;
                }

                listaProdutos.Add(p1);

                CsvArmazenamento.SalvarProdutos(caminhoArquivo, listaProdutos);
                Console.WriteLine($"Produto adicionado com sucesso!");
                Console.ReadKey();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar produto: \n\n" + ex.Message);
            }
        }
        public static void ListarProdutos()
        {
            Console.WriteLine("=== LISTA DE PRODUTOS ===\n");
            try
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                if (linhas.Length <= 1)
                {
                    Console.WriteLine("O arquivo está vazio. Nenhum produto cadastrado.");
                    Console.ReadKey();
                    return;
                }

                bool primeiraLinha = true;

                foreach (var linha in linhas)
                {
                    if (string.IsNullOrWhiteSpace(linha))
                        continue;

                    if (primeiraLinha)
                    {
                        primeiraLinha = false;
                        continue;
                    }

                    string[] dados = linha.Split(';');

                    if (dados.Length >= 3)
                    {
                        string id = dados[0];
                        string nome = dados[1];
                        string categoria = dados[2];
                        string estoqueM = dados[3];
                        string saldo = dados[4];
                        string obs = dados[5];




                        Console.WriteLine($"║ID: {id} ║NOME: {nome} ║CATEGORIA: {categoria} ║ESTOQUEMIN: {estoqueM} ║SALDO: {saldo} ║OBS: {obs} ║");
                    }
                }

                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar produtos: " + ex.Message);
            }
        }
        public static void EditarProduto()
        {
            try
            {
                Console.Write("Digite o ID do produto que deseja editar: ");
                string? inputId = Console.ReadLine();

                if (!int.TryParse(inputId, out int id) || id < 0)
                {
                    Console.WriteLine("ID inválido! Digite um número inteiro não negativo.");
                    Console.ReadKey();
                    return;
                }

                Produto? encontrado = listaProdutos.FirstOrDefault(p => p.produtoId == id);

                if (encontrado == null)
                {
                    Console.WriteLine("Produto não encontrado!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Produto encontrado: {encontrado.produtoNome}");
                Console.WriteLine();

                Console.Write("Novo nome (ou ENTER para manter): ");
                string? novoNome = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    encontrado.produtoNome = novoNome;
                }

                Console.Write("Nova categoria (ou ENTER para manter): ");
                string? novaCategoria = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(novaCategoria))
                {
                    encontrado.produtoCategoria = novaCategoria;
                }

                Console.Write("Novo estoque mínimo (ou ENTER para manter): ");
                string? novoEstqMinStr = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(novoEstqMinStr))
                {
                    if (int.TryParse(novoEstqMinStr, out int novoEstqMin))
                    {
                        if (novoEstqMin >= 0)
                        {
                            encontrado.produtoEstoqueMinimo = novoEstqMin;
                        }
                        else
                        {
                            Console.WriteLine("Estoque mínimo inválido! Valor não alterado.");
                        }
                    }
                }

                Console.Write("Nova quantidade (ou ENTER para manter): ");
                string? novaQtd = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(novaQtd))
                {
                    if (int.TryParse(novaQtd, out int saldo))
                    {
                        if (saldo >= 0)
                        {
                            encontrado.produtoSaldo = saldo;
                        }
                        else
                        {
                            Console.WriteLine("Quantidade não pode ser negativa! Valor não alterado.");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Quantidade inválida! Valor não alterado.");
                        Console.ReadKey();
                    }
                }

                CsvArmazenamento.SalvarProdutos(caminhoArquivo, listaProdutos);

                Console.WriteLine("Produto editado com sucesso!");
                Console.WriteLine("Produto atualizado: " + $"Nome: {encontrado.produtoNome} | Saldo: {encontrado.produtoSaldo} | Categoria: {encontrado.produtoCategoria} | {encontrado.produtoEstoqueMinimo}\n");
                Console.WriteLine("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();

                MostrarMenu();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao editar produto: \n\n" + ex.Message);
            }
        }

        public static void ExcluirProduto()
        {
            try
            {
                Console.Write("Digite o ID do produto que deseja excluir: ");
                string? inputId = Console.ReadLine();

                if (!int.TryParse(inputId, out int id) || id < 0)
                {
                    Console.WriteLine("ID inválido! Digite um número inteiro não negativo.");
                    Console.ReadKey();
                    return;
                }

                Produto? encontrado = listaProdutos.FirstOrDefault(p => p.produtoId == id);
                // Está percorrendo a lista produtos e procura o primeiro produto cujo produtoId corresponde ao id fornecido pelo usuário.

                if (encontrado == null)
                {
                    Console.WriteLine("Produto não encontrado!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Produto encontrado: {encontrado.produtoNome} (Saldo: {encontrado.produtoSaldo})");
                Console.WriteLine();

                Console.WriteLine($"Tem certeza que deseja excluir este produto!? (S/N): ");
                string? escolhaExcluir = Console.ReadLine().ToUpper();

                if (escolhaExcluir == "S")
                {
                    listaProdutos.Remove(encontrado);

                    CsvArmazenamento.SalvarProdutos(caminhoArquivo, listaProdutos); // Salva a lista atualizada no arquivo CSV após a exclusão.
                    Console.WriteLine("Produto excluido com sucesso!");
                    Console.ReadKey();
                    return;
                }

                else if (escolhaExcluir == "N")
                {
                    Console.WriteLine("Exclusão cancelada");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Opção inválida! Exclusão cancelada.");
                    Console.ReadKey();
                    return;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao editar produto: \n\n" + ex.Message);
            }

        }

        public static void DarEntradaEstoque()
        {
            try
            {
                Console.Write("Informe o ID do produto: ");
                string? inputId = Console.ReadLine();
                if (!int.TryParse(inputId, out int id) || id < 0)
                {
                    Console.WriteLine("ID inválido! Informe um número inteiro não negativo.");
                    Console.ReadKey();
                    return;
                }

                Produto? p1 = listaProdutos.FirstOrDefault(p => p.produtoId == id);

                if (p1 == null)
                {
                    Console.WriteLine("Produto não encontrado!");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Informe a quantidade de entrada: ");
                string? inputQtd = Console.ReadLine();
                if (!int.TryParse(inputQtd, out int qtd) || qtd <= 0)
                {
                    Console.WriteLine("Quantidade inválida! Informe um número inteiro positivo.");
                    Console.ReadKey();
                    return;
                }

                int quantidadeInicial = p1.produtoSaldo;

                p1.produtoSaldo += qtd;

                var novoMov = new Movimento
                {
                    movimentoId = listaMovimentos.Any() ? listaMovimentos.Max(m => m.movimentoId) + 1 : 1,
                    produtoId = p1.produtoId,
                    movimentoTipo = "ENTRADA",
                    movimentoQuantidade = qtd,
                    movimentoData = DateTime.Now,
                    movimentoObservacao = ""
                };

                listaMovimentos.Add(novoMov);

                CsvArmazenamento.SalvarProdutos(caminhoArquivo, listaProdutos);
                CsvArmazenamento.SalvarMovimentos(caminhoMovimentos, listaMovimentos);

                Console.WriteLine("Entrada registrada com sucesso!");
                Console.WriteLine($"Nome: {p1.produtoNome} | Inicial: {quantidadeInicial} | Entrada: {qtd} | Final: {p1.produtoSaldo}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao dar entrada no estoque: \n\n" + ex.Message);
            }
        }

        public static void DarSaidaEstoque()
        {
            try
            {
                Console.Write("Informe o ID do produto: ");
                string? inputId = Console.ReadLine();
                if (!int.TryParse(inputId, out int id) || id < 0)
                {
                    Console.WriteLine("ID inválido! Informe um número inteiro não negativo.");
                    Console.ReadKey();
                    return;
                }

                Produto? p1 = listaProdutos.FirstOrDefault(p => p.produtoId == id);
                if (p1 == null)
                {
                    Console.WriteLine("Produto não encontrado!");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Informe a quantidade de retirada: ");
                string? inputQtd = Console.ReadLine();
                if (!int.TryParse(inputQtd, out int qtd) || qtd <= 0)
                {
                    Console.WriteLine("Quantidade inválida! Informe um número inteiro positivo.");
                    Console.ReadKey();
                    return;
                }

                if (qtd > p1.produtoSaldo)
                {
                    Console.WriteLine($"Saldo insuficiente! Saldo atual: {p1.produtoSaldo}");
                    Console.ReadKey();
                    return;
                }

                int quantidadeInicial = p1.produtoSaldo;

                p1.produtoSaldo -= qtd;

                var novoMov = new Movimento
                {
                    movimentoId = listaMovimentos.Any() ? listaMovimentos.Max(m => m.movimentoId) + 1 : 1,
                    produtoId = p1.produtoId,
                    movimentoTipo = "SAIDA",
                    movimentoQuantidade = qtd,
                    movimentoData = DateTime.Now,
                    movimentoObservacao = ""
                };

                listaMovimentos.Add(novoMov);

                CsvArmazenamento.SalvarProdutos(caminhoArquivo, listaProdutos);
                CsvArmazenamento.SalvarMovimentos(caminhoMovimentos, listaMovimentos);

                Console.WriteLine("Quantidade retirada com sucesso!");
                Console.WriteLine($"Nome: {p1.produtoNome} | Inicial: {quantidadeInicial} | Saída: {qtd} | Final: {p1.produtoSaldo}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao dar saída no estoque: \n\n" + ex.Message);
            }
        }


        public static void RelatorioEstoqueAbaixoMinimo()
        {
            try
            {
                Console.Clear();

                var abaixo = listaProdutos
                    .Where(p => p.produtoSaldo < p.produtoEstoqueMinimo)
                    .ToList();

                if (abaixo.Count == 0)
                {
                    Console.WriteLine("Nenhum produto está abaixo do estoque mínimo.");
                    Console.WriteLine("Pressione ENTER para voltar ao menu...");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine("Produtos abaixo do estoque mínimo:\n");

                foreach (var p in abaixo)
                {
                    Console.WriteLine($"ID: {p.produtoId}");
                    Console.WriteLine($"Nome: {p.produtoNome}");
                    Console.WriteLine($"Quantidade: {p.produtoSaldo}");
                    Console.WriteLine($"Mínimo: {p.produtoEstoqueMinimo}");
                    Console.WriteLine("-------------------------");
                }

                Console.WriteLine("Pressione ENTER para voltar ao menu...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao gerar relatório de estoque abaixo do mínimo:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione ENTER para retornar ao menu...");
                Console.ReadLine();
            }
        }

        public static void RelatorioProdutosAbaixoDoEstoque()
        {
            Console.Clear();
            Console.WriteLine("RELATÓRIO - Produtos Abaixo do Estoque Mínimo");
            Console.WriteLine("--------------------------------------------------");

            var produtosCriticos = listaProdutos
                .Where(p => p.produtoSaldo < p.produtoEstoqueMinimo)
                .OrderBy(p => p.produtoNome)
                .ToList();

            if (produtosCriticos.Count == 0)
            {
                Console.WriteLine("Nenhum produto está abaixo do estoque mínimo!");
            }
            else
            {
                foreach (var p in produtosCriticos)
                {
                    Console.WriteLine(
                        $"ID: {p.produtoId} | {p.produtoNome} | " +
                        $"Saldo: {p.produtoSaldo} | Mínimo: {p.produtoEstoqueMinimo}"
                    );
                }
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Pressione ENTER para voltar ao menu...");
            Console.ReadLine();
        }

        public static void RelatorioExtratoMovimentoPorProduto()
        {
            Console.Clear();

            Console.Write("Informe o ID do produto: ");
            string idDigitado = Console.ReadLine();

            if (!int.TryParse(idDigitado, out int id))
            {
                Console.WriteLine("ID inválido.");
                Console.ReadLine();
                return;
            }

            var produto = listaProdutos.FirstOrDefault(p => p.produtoId == id);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                Console.ReadLine();
                return;
            }

            var movimentos = listaMovimentos
                .Where(m => m.produtoId == id)
                .OrderBy(m => m.movimentoData)
                .ToList();

            Console.Clear();
            Console.WriteLine($"RELATÓRIO DE ENTRADAS E SAÍDAS DO PRODUTO: {produto.produtoNome}");
            Console.WriteLine("-------------------------------------------------------------");

            if (movimentos.Count == 0)
            {
                Console.WriteLine("Nenhuma movimentação encontrada.");
                Console.ReadLine();
                return;
            }

            int quantidadeAtual = produto.produtoSaldo; // saldo atual no sistema
            int quantidadeInicial;                      // antes da operação
            int quantidadeFinal;                        // depois da operação

            // IMPORTANTE:
            // Para reconstruir o histórico corretamente, voltamos o saldo ao início.
            // saldoFinal = saldoAtual, então saldoInicial é recalculado de trás pra frente.

            quantidadeAtual = 0;
            foreach (var mov in movimentos)
            {
                if (mov.movimentoTipo == "ENTRADA")
                    quantidadeAtual += mov.movimentoQuantidade;
                else if (mov.movimentoTipo == "SAIDA")
                    quantidadeAtual -= mov.movimentoQuantidade;
            }

            int saldoReconstruido = 0;

            foreach (var mov in movimentos)
            {
                quantidadeInicial = saldoReconstruido;

                if (mov.movimentoTipo == "ENTRADA")
                    saldoReconstruido += mov.movimentoQuantidade;
                else if (mov.movimentoTipo == "SAIDA")
                    saldoReconstruido -= mov.movimentoQuantidade;

                quantidadeFinal = saldoReconstruido;

                if (mov.movimentoTipo == "ENTRADA")
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (mov.movimentoTipo == "SAIDA")
                    Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(
                    $"{mov.movimentoData:dd/MM/yyyy HH:mm} | {mov.movimentoTipo,-7} | " +
                    $"Inicial: {quantidadeInicial,-4} | " +
                    $"Operação: {mov.movimentoQuantidade,-4} | " +
                    $"Final: {quantidadeFinal,-4}"
                );

                Console.ResetColor();
            }

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Pressione ENTER para voltar ao menu...");
            Console.ReadLine();
        }

    }
}