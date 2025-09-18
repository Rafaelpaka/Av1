namespace TrabalhoV1
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public int Ano { get; set; }
        public bool estado { get; set; } = true;
        public string emprestadoha { get; set; } = "ninguem";

        public DateTime? DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public string FormaPagamento { get; set; } 
    }
}
