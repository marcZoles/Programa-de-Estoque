using EstoqueConsole;
using EstoqueConsole.src.Armazenamento;
using EstoqueConsole.src.Inventario;

CsvArmazenamento csvArmazenamento = new CsvArmazenamento();
InventarioServico inventarioServico = new InventarioServico();
string caminhoArquivo = @"C:\Users\guide\Desktop\Nova pasta\produtos.csv";

CsvArmazenamento.CarregarProdutos(caminhoArquivo);

while (true)
{
    Console.Clear();
    InventarioServico.MostrarMenu();
}



