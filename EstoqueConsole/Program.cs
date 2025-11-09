// Depois que conferirmos tudo vou dar push com o menu principal e os métodos de CRUD para Produto e Movimento.
// - marcZ
// Depois que conferirmos tudo vou dar push com o menu principal e os métodos de CRUD para Produto e Movimento.
// prestar atenção em quem for  apresentar no dia, pois o arquivo csv deve estar com o caminho
// da pessoa com o computador local
using EstoqueConsole.src.Modelo;
string caminhoArquivo = @"C:\Users\guide\Desktop\EstoqueConsole2.0\Dados.csv";
ProcessaArquivoCSV(caminhoArquivo);

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
        return;

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
            ExcluirProduto(caminhoArquivo);
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
            EscreverArquivo(caminhoArquivo, new List<Produto>()); //Incompleto, pois so conseguimos criar um produto
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

    EscreverArquivo(caminhoArquivo, new List<Produto> { p1 });

} //metodo 100% funcional para adicionar um produto
void ListarProdutos()
{
    Console.WriteLine("=== LISTA DE PRODUTOS ===");

    try
    {

        string[] linhas = File.ReadAllLines(caminhoArquivo);

        if (linhas.Length == 0)
        {
            Console.WriteLine("O arquivo está vazio. Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("\nNome do Produto\t\tQuantidade");
        Console.WriteLine("-------------------------------------");

        foreach (var linha in linhas)
        {
            if (string.IsNullOrWhiteSpace(linha))
                continue;

            string[] dados = linha.Split(';');

            if (dados.Length >= 2)
            {
                string nome = dados[0];
                string saldo = dados[1];
                Console.WriteLine($"{nome,-20}\t{saldo}");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao listar produtos: " + ex.Message);
    }


} //metodo 100% funcional para listar produtos
void ProcessaArquivoCSV(string caminhoArquivo)
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
        Console.WriteLine("Erro ao processar o arquivo: " + ex.ToString());

    }
} //metodo 100% funcional para processar o arquivo csv
void CriaArquivoCSV(string caminhoArquivo)
{
    try
    {
        using (File.Create(caminhoArquivo)) { }
        Console.WriteLine("Arquivo criado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao criar o arquivo: " + ex.ToString());
    }
} //metodo 100% funcional para criar o arquivo csv
string[] LerArquivoCSV(string caminhoArquivo) //por enquanto não estamos usando esse método pois o metodo de ler aqruivo funciona melhor
{
    try
    {
        string[] linhas = File.ReadAllLines(caminhoArquivo);
        return linhas;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao ler arquivo CSV \n\n" + ex.ToString());
        return new string[] { };
    }

} //nao estamos usando (ver com a galera)
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

        File.AppendAllLines(caminhoArquivo, linhas);
        Console.WriteLine("Arquivo salvo com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao salvar o arquivo: " + ex);
    }
} //metodo 100% funcional para escrever no arquivo csv
void editarProduto()
{
    Console.WriteLine("Edite o nome do produto: ");
    Produto p1 = new Produto();
    p1.produtoNome = Console.ReadLine();

    Console.WriteLine("Edite a quantidade do produto: ");
    p1.produtoSaldo = int.Parse(Console.ReadLine()!);

    EscreverArquivo(caminhoArquivo, new List<Produto> { p1 });

} // depois que consertei o de criar esse estragou, (ver com a galera)
void ExcluirProduto(string caminhoArquivo)
{
    // Implementar a lógica para excluir produtos
    File.WriteAllLines(caminhoArquivo, new string[0]);
    Console.WriteLine("Arquivo excluído com sucesso!");
    Console.ReadLine();
} //ta excluindo o arquivo todo, tem que rever a logica
