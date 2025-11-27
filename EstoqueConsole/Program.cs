using EstoqueConsole;
using EstoqueConsole.src.Armazenamento;
using EstoqueConsole.src.Inventario;

CsvArmazenamento csvArmazenamento = new CsvArmazenamento();
InventarioServico inventarioServico = new InventarioServico();
// COMANDO PARA GERAR O ARQUIVO NO NOTEBOOK:
/* dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=true -o C:\Users\User\Desktop\testePublish */
string caminhoArquivo = @"C:\Users\TREVOTECH\source\repos\estoqueConsole\EstoqueConsole\EstoqueConsole\data\produtos.csv";

CsvArmazenamento.CarregarProdutos(caminhoArquivo);

while (true)
{
    Console.Clear();
    InventarioServico.MostrarMenu();
}