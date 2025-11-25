namespace EstoqueConsole.src.Modelo
{
    public class Produto
    {
        public int produtoId { get; set; }
        public string produtoNome { get; set; }
        public string produtoCategoria { get; set; }
        public string produtoObservacao { get; set; }
        public int produtoEstoqueMinimo { get; set; }
        public int produtoSaldo { get; set; }
    }
}