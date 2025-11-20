
using EstoqueConsole.src.Modelo;
using System;
using System.Collections.Generic;
using System.IO;

List<Produto> produtos = new List<Produto>();
string caminhoArquivo = @"C:\Users\guide\Desktop\Nova pasta\produtos.csv";
void MostrarMenu()
{
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
} // Aqui não precisa de tratamento de erros, pois é só exibir o menu
void SelecionaOpcao()
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
} // Função OK | Tratamento de erros OK
void ChamaFuncaoEscolhida(int opcaovalida)
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
                //RelatorioEstoqueAbaixoMinimo();
                break;
            case 8:
                //RelatorioExtratoMovimentoPorProduto();
                break;
            case 9:
                SalvarProdutos(caminhoArquivo, produtos);
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
} // Função OK | Tratamento de erros OK



//FUNCOES DE PRODUTOS
void CriarProduto()
{
    try
    {
        Produto p1 = new Produto();

        Console.WriteLine("=== ADICIONAR PRODUTO ===");
        string entrada;
        do // Fica no loop até o usuário digitar um nome válido
        {
            Console.Write("Nome do produto: ");
            entrada = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(entrada) && !int.TryParse(entrada, out _)) // Este "_" é só para indicar que não vamos usar o valor retornado pelo TryParse
                break;

            Console.WriteLine("Digite apenas TEXTO para o nome do produto.");
        }
        while (true);
        p1.produtoNome = entrada; // Atribui o nome válido somente após sair do loop, ou seja, quando o nome for válido (Texto e não nulo)

        int tempInt; // Variável temporária para armazenar valores inteiros convertidos
        while (true)
        {
            Console.Write("Digite o ID do produto: ");
            entrada = Console.ReadLine();

            if (int.TryParse(entrada, out tempInt))
                break;

            Console.WriteLine("Digite apenas NÚMEROS para o ID.");
        }
        p1.produtoId = tempInt; // Atribui o ID válido somente após sair do loop, ou seja, quando o ID for válido (Número)

        do // Mesma lógica do nome do produto
        {
            Console.Write("Digite a categoria do produto: ");
            entrada = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(entrada) && !int.TryParse(entrada, out _))
                break;

            Console.WriteLine("Digite apenas TEXTO para a categoria.");
        }
        while (true);
        p1.produtoCategoria = entrada;

        while (true) // Mesma lógica do ID do produto
        {
            Console.Write("Digite o estoque mínimo do produto: ");
            entrada = Console.ReadLine();

            if (int.TryParse(entrada, out tempInt))
                break;

            Console.WriteLine("Digite apenas NÚMEROS para o estoque mínimo.");
        }
        p1.produtoEstoqueMinimo = tempInt;

        while (true) // Mesma lógica do ID do produto
        {
            Console.Write("Quantidade do produto: ");
            entrada = Console.ReadLine();

            if (int.TryParse(entrada, out tempInt))
                break;

            Console.WriteLine("Digite apenas NÚMEROS para a quantidade.");
        }
        p1.produtoSaldo = tempInt;

        if (p1.produtoSaldo < 0 || p1.produtoSaldo < p1.produtoEstoqueMinimo)
        {
            Console.WriteLine("Quantidade inválida!");
            return;
        }

        produtos.Add(p1);
        Console.WriteLine($"Produto: {p1.produtoNome} | Quantidade: {p1.produtoSaldo} | Estoque minímo: {p1.produtoEstoqueMinimo} adicionado com sucesso!");


        Console.ReadKey(); // Pausa para o usuário ver a mensagem antes de voltar ao menu
    }

    catch (Exception ex)
    {
        Console.WriteLine("Erro ao criar produto: \n\n" + ex.Message);
    }
} // Função OK | Tratamento de erros OK
void ListarProdutos()
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

        Console.WriteLine("╔══════════════════════════════════╦════════════╗");
        Console.WriteLine("║ Nome do Produto                  ║ Quantidade ║");
        Console.WriteLine("╠══════════════════════════════════╬════════════╣");

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
                string saldo = dados[2];


                Console.WriteLine($"║ {id,-2} ║ {nome,-30} ║ {saldo,10} ║");
            }
        }

        Console.WriteLine("╚══════════════════════════════════╩════════════╝");
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadLine();
    }

    catch (Exception ex)
    {
        Console.WriteLine("Erro ao listar produtos: " + ex.Message);
    }
}  // Função OK | Tratamento de erros OK


void EditarProduto()
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

        Produto? encontrado = produtos.FirstOrDefault(p => p.produtoId == id);
        // Está percorrendo a lista produtos e procura o primeiro produto cujo produtoId corresponde ao id fornecido pelo usuário.

        if (encontrado == null)
        {
            Console.WriteLine("Produto não encontrado!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Produto encontrado: {encontrado.produtoNome} (Saldo: {encontrado.produtoSaldo})");
        Console.WriteLine();

        Console.Write("Novo nome (ou ENTER para manter): ");
        string? novoNome = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(novoNome))
        {
            encontrado.produtoNome = novoNome;
        }

        else if (novoNome != null && novoNome.Length > 0) // apenas espaços
        {
            Console.WriteLine("Nome inválido! O nome do produto não foi alterado.");
            Console.ReadKey();
        }

        Console.Write("Nova quantidade (ou ENTER para manter): ");
        string? novaQtd = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(novaQtd))
        {
            if (int.TryParse(novaQtd, out int saldo))
            {
                if (saldo < 0)
                {
                    Console.WriteLine("Quantidade não pode ser negativa! Valor não alterado.");
                    Console.ReadKey();
                }

                else
                {
                    encontrado.produtoSaldo = saldo;
                }
            }

            else
            {
                Console.WriteLine("Quantidade inválida! Valor não alterado.");
                Console.ReadKey();
            }
        }

        Console.WriteLine("Produto editado com sucesso!");
        Console.ReadKey();
    }

    catch (Exception ex)
    {
        Console.WriteLine("Erro ao editar produto: \n\n" + ex.Message);
    }
} // Função OK | Tratamento de erros OK
void ExcluirProduto()
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

        Produto? encontrado = produtos.FirstOrDefault(p => p.produtoId == id);
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
            produtos.Remove(encontrado);

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

} //Função OK | Tratamento de erros OK
void DarEntradaEstoque()
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

        Produto? p1 = produtos.FirstOrDefault(p => p.produtoId == id);

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

        p1.produtoSaldo += qtd;

        Console.WriteLine("Entrada registrada com sucesso!");

        Console.WriteLine($"Novo produto: Nome: {p1.produtoNome} | Saldo: {p1.produtoSaldo}");
        Console.ReadKey();
    }

    catch (Exception ex)
    {
        Console.WriteLine("Erro ao dar entrada no estoque: \n\n" + ex.Message);
    }
} // Função OK | Tratamento de erros OK
void DarSaidaEstoque()
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

        Produto? p1 = produtos.FirstOrDefault(p => p.produtoId == id);
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

        p1.produtoSaldo -= qtd;

        Console.WriteLine("Quantidade retirada com sucesso!");
        Console.WriteLine($"Novo saldo do produto {p1.produtoNome}: {p1.produtoSaldo}");
        Console.ReadKey();
    }

    catch (Exception ex)
    {
        Console.WriteLine("Erro ao dar saída no estoque: \n\n" + ex.Message);
    }
} // Função OK | Tratamento de erros OK
