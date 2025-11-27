namespace EstoqueConsole.src.Modelo
{
    public class Movimento 
    {
        public int movimentoId { get; set; }
        public int produtoId { get; set; }
        public string movimentoTipo { get; set; }
        public int movimentoQuantidade { get; set; }
        public DateTime movimentoData { get; set; }
        public string movimentoObservacao { get; set; }
    }
}