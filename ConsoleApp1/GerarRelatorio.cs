using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TrabalhoV1
{
    public class GerarRelatorio
    {
        private readonly string caminhoJogos = "../../../biblioteca.json";

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

            Console.WriteLine("===== RELATÓRIO DE EMPRÉSTIMOS =====");

            foreach (var jogo in jogos.Where(j => j.DataEmprestimo.HasValue))
            {
                Console.WriteLine($"Jogo: {jogo.Nome}");
                Console.WriteLine($"Emprestado a: {jogo.emprestadoha}");
                Console.WriteLine($"Data de Empréstimo: {jogo.DataEmprestimo.Value:dd/MM/yyyy}");

                if (jogo.DataDevolucao.HasValue)
                {
                    Console.WriteLine($"Data de Devolução: {jogo.DataDevolucao.Value:dd/MM/yyyy}");
                }
                else
                {
                    Console.WriteLine("Ainda não foi devolvido.");
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
                

                Console.WriteLine($"Valor a pagar: R$ {valor:F2}");
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}
