using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TrabalhoV1;

namespace TrabalhoV1
{
    public class GerarRelatorio
    {
        private readonly string caminhoJogos = "../../../biblioteca.json";
        private readonly string caminhoRelatorio = "../../../relatorio.txt";

        public void RelatorioEmprestimos()
        {
            if (!File.Exists(caminhoJogos))
            {
                Console.WriteLine("Nenhum dado encontrado para gerar relatório.");
                return;
            }

            string json = File.ReadAllText(caminhoJogos);
            List<Jogo> jogos = JsonSerializer.Deserialize<List<Jogo>>(json);

            if (jogos == null || jogos.Count == 0)
            {
                Console.WriteLine("Nenhum jogo cadastrado.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(caminhoRelatorio, true)) 
            {
                writer.WriteLine("===== RELATÓRIO DE EMPRÉSTIMOS =====");
                writer.WriteLine($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                writer.WriteLine();

                foreach (var jogo in jogos.Where(j => j.DataEmprestimo.HasValue))
                {
                    writer.WriteLine($"Jogo: {jogo.Nome}");
                    writer.WriteLine($"Emprestado a: {jogo.emprestadoha}");
                    writer.WriteLine($"Data de Empréstimo: {jogo.DataEmprestimo.Value:dd/MM/yyyy}");

                    if (jogo.DataDevolucao.HasValue)
                    {
                        writer.WriteLine($"Data de Devolução: {jogo.DataDevolucao.Value:dd/MM/yyyy}");
                        writer.WriteLine($"Forma de Pagamento: {jogo.FormaPagamento}");
                    }
                    else
                    {
                        writer.WriteLine("Ainda não foi devolvido.");
                    }

                    decimal valor = 10;
                    if (jogo.DataDevolucao.HasValue)
                    {
                        int dias = (jogo.DataDevolucao.Value - jogo.DataEmprestimo.Value).Days + 1;
                        if (dias > 7)
                        {
                            int diasAtraso = dias - 7;
                            valor += diasAtraso * 5;
                        }
                    }

                    writer.WriteLine($"Valor a pagar: R$ {valor:F2}");
                    writer.WriteLine("-----------------------------------");
                }

                writer.WriteLine();
                writer.WriteLine("===================================");
                writer.WriteLine();
            }

            Console.WriteLine($"Relatório acrescentado em: {Path.GetFullPath(caminhoRelatorio)}");
        }
    }
}

