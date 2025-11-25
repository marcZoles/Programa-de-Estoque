using EstoqueConsole;
using EstoqueConsole.src.Armazenamento;
using EstoqueConsole.src.Inventario;

CsvArmazenamento csvArmazenamento = new CsvArmazenamento();
InventarioServico inventarioServico = new InventarioServico();
string caminhoArquivo = @"C:\Users\TREVOTECH\source\repos\estoqueConsole\EstoqueConsole\EstoqueConsole\data\produtos.csv";

// LEMBRAR DE TROCAR CAMINHO DO ARQUIVO AQUI E NO "InventarioServico.cs"

CsvArmazenamento.CarregarProdutos(caminhoArquivo);

while (true)
{
    Console.Clear();
    InventarioServico.MostrarMenu();
}