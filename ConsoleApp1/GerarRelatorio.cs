using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;

namespace TrabalhoV1
{
    public class GerarRelatorio
    {
        private readonly string caminhoJogos;
        private readonly string caminhoRelatorio;

        public GerarRelatorio()
        {
            string pastaData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data");
            if (!Directory.Exists(pastaData))
                Directory.CreateDirectory(pastaData);

            caminhoJogos = Path.Combine(pastaData, "biblioteca.json");
            caminhoRelatorio = Path.Combine(pastaData, "relatorio.txt");
        }

        public void RelatorioEmprestimos()
        {
            try // [AV1-5]
            {
                if (!File.Exists(caminhoJogos)) { Console.WriteLine("Nenhum dado para relatório."); return; }

                string json = File.ReadAllText(caminhoJogos);
                List<Jogo> jogos = JsonSerializer.Deserialize<List<Jogo>>(json); // [AV1-3]

                if (jogos == null || jogos.Count == 0)
                {
                    Console.WriteLine("Nenhum jogo cadastrado.");
                    return;
                }

                using (StreamWriter sw = new StreamWriter(caminhoRelatorio, true))
                {
                    sw.WriteLine("===== RELATÓRIO DE EMPRÉSTIMOS =====");
                    sw.WriteLine($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");

                    foreach (Jogo j in jogos.Where(j => j.DataEmprestimo.HasValue))
                    {
                        sw.WriteLine($"Jogo: {j.Nome}");
                        sw.WriteLine($"Emprestado a: {j.EmprestadoHa}");
                        sw.WriteLine($"Data Empréstimo: {j.DataEmprestimo.Value:dd/MM/yyyy}");

                        if (j.DataDevolucao.HasValue)
                        {
                            sw.WriteLine($"Data Devolução: {j.DataDevolucao.Value:dd/MM/yyyy}");
                            sw.WriteLine($"Forma de Pagamento: {j.FormaPagamento}");
                        }
                        else
                        {
                            sw.WriteLine("Ainda não foi devolvido.");
                        }

                        // cálculo valor
                        decimal valor = 10;
                        if (j.DataDevolucao.HasValue)
                        {
                            int dias = (j.DataDevolucao.Value - j.DataEmprestimo.Value).Days + 1;
                            if (dias > 7)
                            {
                                int diasAtraso = dias - 7;
                                valor += diasAtraso * 5;
                            }
                        }

                        sw.WriteLine($"Valor a pagar: R$ {valor:F2}");
                        sw.WriteLine("-----------------------------------");
                    }

                    sw.WriteLine();
                    sw.WriteLine("===================================");
                    sw.WriteLine();
                }

                Console.WriteLine($"Relatório acrescentado em: {Path.GetFullPath(caminhoRelatorio)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar relatório: {ex.Message}");
                Debug.WriteLine($"[{DateTime.Now}] Erro Relatorio: {ex.Message}"); // [AV2-5]
            }
        }
    }
}
