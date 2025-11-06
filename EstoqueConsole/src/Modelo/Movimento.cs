namespace EstoqueConsole.src.Modelo
{
    public class Movimento // Deixei os nomes das propriedades redundantes pra gente não se confundir pelo menos agora no começo
    {
        public int movimentoId { get; set; }
        public string produtoId { get; set; }
        public string movimentoTipo { get; set; } // O tipo deve ser "entrada" ou "saida"
        public int movimentoQuantidade { get; set; }
        public DateTime movimentoData { get; set; }
        public string movimentoObservacao { get; set; }
    }
}