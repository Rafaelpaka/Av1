using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Diagnostics;

namespace TrabalhoV1
{
    public class Emprestar
    {
        private readonly string caminhoJogos;
        private readonly string caminhoMembros;

        public Emprestar()
        {
            string pastaData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data");
            if (!Directory.Exists(pastaData))
                Directory.CreateDirectory(pastaData);

            caminhoJogos = Path.Combine(pastaData, "biblioteca.json");
            caminhoMembros = Path.Combine(pastaData, "Membros.json");
        }

        private List<Jogo> CarregarJogos() { return Carregar<Jogo>(caminhoJogos); }
        private List<Membros> CarregarMembros() { return Carregar<Membros>(caminhoMembros); }

        private List<T> Carregar<T>(string caminho)
        {
            try // [AV1-5]
            {
                if (!File.Exists(caminho)) return new List<T>();
                string json = File.ReadAllText(caminho);
                if (string.IsNullOrEmpty(json)) return new List<T>();
                return JsonSerializer.Deserialize<List<T>>(json); // [AV1-3]
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar: {ex.Message}");
                Debug.WriteLine($"[{DateTime.Now}] Erro Carregar: {ex.Message}"); // [AV2-5]
                return new List<T>();
            }
        }

        private void Salvar<T>(string caminho, List<T> lista)
        {
            try // [AV1-5]
            {
                string json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }); // [AV1-3]
                File.WriteAllText(caminho, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar: {ex.Message}");
                Debug.WriteLine($"[{DateTime.Now}] Erro Salvar: {ex.Message}"); // [AV2-5]
            }
        }

        public void EmprestarJogo()
        {
            List<Membros> membros = CarregarMembros();
            List<Jogo> jogos = CarregarJogos();

            if (membros.Count == 0 || jogos.Count == 0) { Console.WriteLine("Dados insuficientes."); return; }

            Console.WriteLine("Membros:");
            membros.ForEach(m => Console.WriteLine($"ID: {m.IdMem} - {m.NomeMem}"));

            Console.Write("ID do membro: ");
            if (!int.TryParse(Console.ReadLine(), out int idMembro)) return;

            Membros membro = membros.FirstOrDefault(m => m.IdMem == idMembro);
            if (membro == null) { Console.WriteLine("Membro não encontrado."); return; }

            

            int emprestimosAtuais = jogos.Count(j => j.EmprestadoHa == membro.NomeMem);
            if (emprestimosAtuais >= 2) { Console.WriteLine("Limite de 2 jogos atingido."); return; }

            List<Jogo> disponiveis = jogos.Where(j => j.Estado).ToList();
            if (disponiveis.Count == 0) { Console.WriteLine("Nenhum jogo disponível."); return; }

            Console.WriteLine("Jogos disponíveis:");
            disponiveis.ForEach(j => Console.WriteLine($"ID: {j.Id} - {j.Nome}"));

            Console.Write("ID do jogo: ");
            if (!int.TryParse(Console.ReadLine(), out int idJogo)) return;

            Jogo jogo = jogos.FirstOrDefault(j => j.Id == idJogo && j.Estado);
            if (jogo == null) { Console.WriteLine("Jogo indisponível."); return; }

            jogo.Estado = false;
            if (!string.IsNullOrWhiteSpace(jogo.EmprestadoHa) && jogo.EmprestadoHa.ToLower() != "ninguem")
            {
                jogo.EmprestadoAnteriormente = jogo.EmprestadoHa;
            }

            jogo.EmprestadoHa = membro.NomeMem;
            jogo.DataEmprestimo = DateTime.Now;
            jogo.DataDevolucao = null;



            Salvar(caminhoJogos, jogos);

            Console.WriteLine($"Jogo '{jogo.Nome}' emprestado para {membro.NomeMem} em {jogo.DataEmprestimo.Value:dd/MM/yyyy}");
        }

        public void DevolverJogo()
        {
            List<Membros> membros = CarregarMembros();
            List<Jogo> jogos = CarregarJogos();

            List<Jogo> emprestados = jogos.Where(j => !j.Estado).ToList();
            if (emprestados.Count == 0) { Console.WriteLine("Nenhum jogo emprestado."); return; }

            Console.WriteLine("Membros com jogos emprestados:");
            List<Membros> membrosComJogos = membros.Where(m => emprestados.Any(j => j.EmprestadoHa == m.NomeMem)).ToList();
            membrosComJogos.ForEach(m => Console.WriteLine($"ID: {m.IdMem} - {m.NomeMem}"));

            Console.Write("ID do membro: ");
            if (!int.TryParse(Console.ReadLine(), out int idMembro)) return;
            Membros membro = membrosComJogos.FirstOrDefault(m => m.IdMem == idMembro);
            if (membro == null) return;

            List<Jogo> jogosDoMembro = emprestados.Where(j => j.EmprestadoHa == membro.NomeMem).ToList();
            jogosDoMembro.ForEach(j => Console.WriteLine($"ID: {j.Id} - {j.Nome}"));

            Console.Write("ID do jogo: ");
            if (!int.TryParse(Console.ReadLine(), out int idJogo)) return;
            Jogo jogo = jogosDoMembro.FirstOrDefault(j => j.Id == idJogo);
            if (jogo == null) return;

            jogo.Estado = true;
            jogo.EmprestadoAnteriormente = membro.NomeMem;
            jogo.EmprestadoHa = "ninguem";


            Console.Write("Data da devolução (ENTER = hoje): ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) jogo.DataDevolucao = DateTime.Now;
            else
            {
                DateTime data;
                while (!DateTime.TryParse(input, out data) || data < jogo.DataEmprestimo)
                {
                    Console.Write("Data inválida. Digite novamente: ");
                    input = Console.ReadLine();
                }
                jogo.DataDevolucao = data;
            }

            decimal valor = 10;
            int dias = (jogo.DataDevolucao.Value - jogo.DataEmprestimo.Value).Days + 1;
            if (dias > 7) valor += (dias - 7) * 5;

            Console.WriteLine($"Valor total: R$ {valor:F2}");
            Console.Write("Forma de pagamento (1-Dinheiro / 2-Pix): ");
            string opc = Console.ReadLine();
            jogo.FormaPagamento = opc == "1" ? "Dinheiro" : "Pix";

            Salvar(caminhoJogos, jogos);

            Console.WriteLine($"Jogo '{jogo.Nome}' devolvido com sucesso.");
        }
    }
}
