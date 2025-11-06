namespace EstoqueConsole.src.Modelo
{
    public class Produto // Deixei os nomes das propriedades redundantes pra gente não se confundir pelo menos agora no começo
    {
        public int produtoId { get; set; } // Depois estar propriedades vão ficar só como "id" ou "nome"
        public string produtoNome { get; set; }
        public string produtoCategoria { get; set; }
        public string produtoObservacao { get; set; }
        public int produtoEstoqueMinimo { get; set; }
        public int produtoSaldo { get; set; }
    }
}