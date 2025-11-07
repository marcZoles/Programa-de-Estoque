// Depois que conferirmos tudo vou dar push com o menu principal e os métodos de CRUD para Produto e Movimento.
// - marcZ
// Depois que conferirmos tudo vou dar push com o menu principal e os métodos de CRUD para Produto e Movimento.
// prestar atenção em quem for  apresentar no dia, pois o arquivo csv deve estar com o caminho
// da pessoa com o computador local
using EstoqueConsole.src.Modelo;

while (true)
{
    Console.Clear();
    MostrarMenu();
}

void MostrarMenu()
{

    Console.WriteLine("SISTEMA PARA CADASTRAMENTO DE PRODUTOS");
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("José - Bianca - Thais - Gabriel");//lembrar de colocar os nomes dos integrantes do grupo
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
            CriarProduto();
            break;
        case 2:
            ListarProdutos();
            break;
        case 3:
            editarProduto();
            break;
        case 4:
            ExcluirProduto(@"C:\Users\thais\OneDrive\Área de Trabalho\arquivo_alunos.csv");
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
void CriarProduto()
{
    Produto p1 = new Produto();
    Console.WriteLine("=== ADICIONAR PRODUTO ===");
    Console.Write("Nome do produto: ");
    p1.produtoNome = Console.ReadLine()!;
    Console.Write("Quantidade do produto: ");
    p1.produtoSaldo = int.Parse(Console.ReadLine()!);
    Console.WriteLine($"Produto {p1.produtoNome} com quantidade {p1.produtoSaldo} adicionado com sucesso!");
    EscreverArquivo(@"C:\Users\thais\OneDrive\Área de Trabalho\arquivo_alunos.csv", new List<Produto> { p1 });
    
}

void ListarProdutos()
{
    Console.WriteLine("=== LISTA DE PRODUTOS ===");
    // Implementar a lógica para listar produtos
    ProcessarArquivoCSV();


}

void ProcessarArquivoCSV()
{

    try
    {
        string caminhoArquivo = @"C:\Users\thais\OneDrive\Área de Trabalho\arquivo_alunos.csv";
        string[] linhas = LerArquivoCSV(caminhoArquivo);


        foreach (var line in linhas)
        {


            Console.WriteLine(line);
        }


    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao ler arquivo aoooooooooba" + ex.ToString());
    }


    Console.ReadLine();
}

string[] LerArquivoCSV(string caminhoArquivo)
{
    try
    {
        string[] linhas = File.ReadAllLines(caminhoArquivo);
        return linhas;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao escrever arquivo  baaaaaaaaoo" + ex.ToString());
        return new string[] { };
    }

}

void EscreverArquivo(string caminhoArquivo, List<Produto> produtos)
{
    try
    {
        List<string> linhas = new List<string>();

        foreach (var p1 in produtos)
        {
            string linha = $"{p1.produtoNome};{p1.produtoSaldo}";
            linhas.Add(linha);
        }

        File.WriteAllLines(caminhoArquivo, linhas);
        Console.WriteLine("Arquivo salvo com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao salvar o arquivo: " + ex);
    }
} 
void editarProduto() {
    Console.WriteLine("Edite o nome do produto: ");
    Produto p1 = new Produto();
    p1.produtoNome += Console.ReadLine();

    Console.WriteLine("Edite a quantidade do produto: ");
    p1.produtoSaldo += int.Parse(Console.ReadLine()!);

    EscreverArquivo(@"C:\Users\thais\OneDrive\Área de Trabalho\arquivo_alunos.csv", new List<Produto> { p1 });

}

void ExcluirProduto(string caminhoArquivo)
{
    // Implementar a lógica para excluir produtos
    File.WriteAllLines(caminhoArquivo, new string[0]);
    Console.WriteLine("Arquivo excluído com sucesso!");
    Console.ReadLine();
}
