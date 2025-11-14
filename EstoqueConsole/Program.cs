// Depois que conferirmos tudo vou dar push com o menu principal e os métodos de CRUD para Produto e Movimento.
// - marcZ
// Depois que conferirmos tudo vou dar push com o menu principal e os métodos de CRUD para Produto e Movimento.
// prestar atenção em quem for  apresentar no dia, pois o arquivo csv deve estar com o caminho
// da pessoa com o computador local
using EstoqueConsole.src.Modelo;
using System.Security.Cryptography;
string caminhoArquivo = @"C:\Users\thais\OneDrive\Área de Trabalho\produto.csv";
ProcessaArquivoCSV(caminhoArquivo);
List<Produto> produtos = new List<Produto>();
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
            EscreverArquivo(caminhoArquivo, new List<Produto>());
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
    Console.WriteLine("Digite o ID do produto: ");
    p1.produtoId = int.Parse(Console.ReadLine()!);
    Console.Write("Quantidade do produto: ");
    p1.produtoSaldo = int.Parse(Console.ReadLine()!);
    produtos.Add(p1);
    Console.WriteLine($"Produto {p1.produtoNome} com quantidade {p1.produtoSaldo} adicionado com sucesso!");

    EscreverArquivo(caminhoArquivo, new List<Produto> { p1 });
    Console.ReadKey();
} //metodo 100% funcional para adicionar um produto | resolvi a quesão de aparecer a mensagem com readkey
// porem agora quando aperta qualquer tecla ele retorna para o menu principal
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
            string linha = $"{p1.produtoNome};{p1.produtoSaldo};{p1.produtoId}";
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

/*

void AlterarEntradaProduto(string caminhoArquivo, List<Produto> produtos) // Método para editar os arquivos ao realizar uma ENTRADA DE PRODUTOS
{
    Produto p1 = new Produto();
    try
    {
        List<string> linhas = new List<string>();

        if (p1.produtoId == ) {

            foreach (var p1 in produtos)
            {
                string linha = $"{p1.produtoId};{p1.produtoNome};{p1.produtoSaldo}";
                linhas.Add(linha);
            }

            File.AppendAllLines(caminhoArquivo, linhas);
            Console.WriteLine("Arquivo salvo com sucesso!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao salvar o arquivo: " + ex);
    }
}

*/

void editarProduto()
{

    Console.Write("Digite o ID do produto que deseja editar: ");
    int id = int.Parse(Console.ReadLine()!);

    Produto? encontrado = produtos.FirstOrDefault(p => p.produtoId == id);
    //está percorrendo a lista produtos e procuta o primeiro produto cujo produtoId corresponde ao id fornecido pelo usuário.

    if (encontrado == null)
    {
        Console.WriteLine("Produto não encontrado!");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"Produto encontrado: {encontrado.produtoNome} (Saldo: {encontrado.produtoSaldo})");
    Console.WriteLine();

  
    Console.Write("Novo nome (ou ENTER para manter): ");
    string novoNome = Console.ReadLine()!;
    if (!string.IsNullOrWhiteSpace(novoNome))
        encontrado.produtoNome = novoNome;

    Console.Write("Nova quantidade (ou ENTER para manter): ");
    string novaQtd = Console.ReadLine()!;
    if (!string.IsNullOrWhiteSpace(novaQtd))
        // verificação mais geral: Se o campo não estiver vazio ou nulo e nao contem so espaços em branco
        encontrado.produtoSaldo = int.Parse(novaQtd);

    SalvarProdutos(caminhoArquivo, produtos);

    Console.WriteLine("Produto editado com sucesso!");
    Console.ReadKey();
} // 100%
void ExcluirProduto(string caminhoArquivo)
{
    // Implementar a lógica para excluir produtos
    File.WriteAllLines(caminhoArquivo, new string[0]);
    Console.WriteLine("Arquivo excluído com sucesso!");
    Console.ReadLine();
} //ta excluindo o arquivo todo, tem que rever a logica

void DarEntradaEstoque()
{

    Console.Write("Informe o ID do produto: ");
    int id = int.Parse(Console.ReadLine()!);
    foreach (var p1 in produtos)
    {
        if (p1.produtoId == id)
        {
            Console.Write("Informe a quantidade de entrada: ");
            int qtd = int.Parse(Console.ReadLine()!);
            p1.produtoSaldo += qtd;
            Console.WriteLine("Entrada registrada com sucesso!");
            SalvarProdutos(caminhoArquivo, produtos);
            return;
        }
    }
} // 100%
void DarSaidaEstoque()
{
    Console.Write("Informe o ID do produto: ");
    int id = int.Parse(Console.ReadLine()!);
    foreach (var p1 in produtos)
    {
        if (p1.produtoId == id)
        {
            Console.Write("Informe a quantidade de retirada: ");
            int qtd = int.Parse(Console.ReadLine()!);
            p1.produtoSaldo -= qtd;
            Console.WriteLine("Quantidade retirada com sucesso!");
            SalvarProdutos(caminhoArquivo, produtos);
            return;
        }
    }
}//100%

void SalvarProdutos(string caminho, List<Produto> lista)
{
    using (var writer = new StreamWriter(caminho, false))
    //StreamWitter escreve no arquivo, o false indica que ele vai sobrescrever o arquivo toda vez que salvar
    {
        writer.WriteLine("saldo;nome;");

        foreach (var p in lista)
        {
            writer.WriteLine($"{p.produtoSaldo};{p.produtoNome}");
        }
    }
}//Método apoio editar produto (100%)