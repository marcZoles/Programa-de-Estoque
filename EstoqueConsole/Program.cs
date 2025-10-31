using System;
using EstoqueConsole.src.Modelo;
using EstoqueConsole.src.Servico;


namespace EstoqueConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Código principal aqui

        }

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
            if (opcaovalida > 0 || opcaovalida < 10)
            {
                Console.WriteLine($"Você escolheu a opção {opcaovalida}");
                ChamaFuncaoEscolhida(opcaovalida);
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

            try
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
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }



            static void CadastrarProduto()
            {
                Console.WriteLine("Nome do produto: ");
                string nomeProduto = Console.ReadLine()!;
                Console.WriteLine("Id do produto: ");
                int idProduto = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Categoria do produto: ");
                string categoriaProduto = Console.ReadLine()!;
                Console.WriteLine("Digite a quantidade de estoque minímo do produto");
                int estoqueMinimoProduto = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Saldo do produto: ");
                int saldoProduto = int.Parse(Console.ReadLine()!);

                var produto1 = new Produto(idProduto, nomeProduto, categoriaProduto, estoqueMinimoProduto, saldoProduto);
                Console.WriteLine($"Produto {produto1.NomeProduto} cadastrado com sucesso!");
            }
            static void AtualizarProduto()
            {
                Console.Write("Id: ");
                if (!int.TryParse(Console.ReadLine(), out int idProduto))
                {
                    Console.WriteLine("Id inválido. Operação cancelada.");
                    return;
                }
              
            }
        }
    }
}