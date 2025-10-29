void MostrarMenu()
{
    Console.WriteLine("Trabalho feito por: José - Bianca - Thais - Gabriel\n");//lembrar de colocar os nomes dos integrantes do grupo
    Console.WriteLine("SISTEMA PARA CADASTRAMENTO DE PRODUTOS");
    Console.WriteLine("|-------------------------------------------------|");
    Console.WriteLine("| 1 - Cadastrar Produto                           |");
    Console.WriteLine("| 2 - Listar Produtos                             |");
    Console.WriteLine("| 3 - Editar Produto                              |");
    Console.WriteLine("| 4 - Excluir Produto                             |");
    Console.WriteLine("| 5 - Dar ENTRADA em estoque                      |");
    Console.WriteLine("| 6 - Dar SAÍDA em estoque                        |");
    Console.WriteLine("| 7 - Relatório: Estoque abaixo do mínimo         |");
    Console.WriteLine("| 8 - Relatório: Extrato de movimento por produto |");
    Console.WriteLine("| 9 - Salvar                                      |");
    Console.WriteLine("| 0 - Sair                                        |");
    Console.WriteLine("|-------------------------------------------------|");

    SelecionaOpcao();
}

void SelecionaOpcao()
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
        MostrarMenu();

    }
}

void ChamaFuncaoEscolhida(int opcaovalida)
{
    switch (opcaovalida)
    {
        case 1:
            //CadastrarProduto();
            Console.WriteLine("testee");
            break;
        case 2:
            //ListarProdutos();
            break;
        case 3:
            //EditarProduto();
            break;
        case 4:
            //ExcluirProduto();
            break;
        case 5:
            //DarEntradaEstoque();
            break;
        case 6:
            //DarSaidaEstoque();
            break;
        case 7:
            //RelatorioEstoqueAbaixoMinimo();
            break;
        case 8:
            //RelatorioExtratoMovimentoPorProduto();
            break;
        case 9:
            //Salvar();
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }

}

MostrarMenu();