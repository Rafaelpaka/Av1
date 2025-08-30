using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TrabalhoV1
{
    public class Emprestar
    {
        private readonly string caminhoJogos = "../../../biblioteca.json";
        private readonly string caminhoMembros = "../../../Membros.json";

        // Carregar lista de jogos
        private List<Jogo> CarregarJogos()
        {
            if (!File.Exists(caminhoJogos)) return new List<Jogo>();
            string json = File.ReadAllText(caminhoJogos);
            if (string.IsNullOrEmpty(json)) return new List<Jogo>();
            return JsonSerializer.Deserialize<List<Jogo>>(json);
        }

        // Carregar lista de membros
        private List<Membros> CarregarMembros()
        {
            if (!File.Exists(caminhoMembros)) return new List<Membros>();
            string json = File.ReadAllText(caminhoMembros);
            if (string.IsNullOrEmpty(json)) return new List<Membros>();
            return JsonSerializer.Deserialize<List<Membros>>(json);
        }

        // Salvar jogos atualizados no JSON
        private void SalvarJogos(List<Jogo> jogos)
        {
            File.WriteAllText(caminhoJogos, JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true }));
        }

        // 1️⃣ Emprestar jogo
        public void EmprestarJogo()
        {
            var membrosLista = CarregarMembros();
            if (membrosLista.Count == 0) { Console.WriteLine("Nenhum membro cadastrado."); return; }

            Console.WriteLine("Membros cadastrados:");
            foreach (var m in membrosLista) Console.WriteLine($"ID: {m.IdMem} - Nome: {m.NomeMem}");

            Console.Write("Digite o ID do membro: ");
            if (!int.TryParse(Console.ReadLine(), out int idMembro)) { Console.WriteLine("Entrada inválida."); return; }

            var membroEscolhido = membrosLista.FirstOrDefault(m => m.IdMem == idMembro);
            if (membroEscolhido == null) { Console.WriteLine("Membro não encontrado."); return; }

            var jogosLista = CarregarJogos();
            int emprestimosAtuais = jogosLista.Count(j => j.emprestadoha == membroEscolhido.NomeMem);
            if (emprestimosAtuais >= 2) { Console.WriteLine("Este membro já possui 2 jogos emprestados."); return; }

            var jogosDisponiveis = jogosLista.Where(j => j.estado).ToList();
            if (jogosDisponiveis.Count == 0) { Console.WriteLine("Nenhum jogo disponível."); return; }

            Console.WriteLine("Jogos disponíveis:");
            foreach (var j in jogosDisponiveis) Console.WriteLine($"ID: {j.Id} - Nome: {j.Nome}");

            Console.Write("Digite o ID do jogo para emprestar: ");
            if (!int.TryParse(Console.ReadLine(), out int idJogo)) { Console.WriteLine("Entrada inválida."); return; }

            var jogoEscolhido = jogosLista.FirstOrDefault(j => j.Id == idJogo && j.estado);
            if (jogoEscolhido == null) { Console.WriteLine("Jogo indisponível."); return; }

            // Atualiza status
            jogoEscolhido.estado = false;
            jogoEscolhido.emprestadoha = membroEscolhido.NomeMem;

            SalvarJogos(jogosLista);
            Console.WriteLine($"Jogo '{jogoEscolhido.Nome}' emprestado para {membroEscolhido.NomeMem} com sucesso!");
        }

        // 2️⃣ Devolver jogo
        public void DevolverJogo()
        {
            var jogosLista = CarregarJogos();
            var membrosLista = CarregarMembros();

            if (jogosLista.Count == 0) { Console.WriteLine("Nenhum jogo cadastrado."); return; }
            if (membrosLista.Count == 0) { Console.WriteLine("Nenhum membro cadastrado."); return; }

            Console.WriteLine("Membros com jogos emprestados:");
            var membrosComJogos = jogosLista.Where(j => !j.estado).Select(j => j.emprestadoha).Distinct().ToList();
            foreach (var nome in membrosComJogos) Console.WriteLine($"- {nome}");

            Console.Write("Digite o nome do membro que vai devolver: ");
            string nomeMembro = Console.ReadLine();
            var jogosDoMembro = jogosLista.Where(j => j.emprestadoha == nomeMembro && !j.estado).ToList();

            if (jogosDoMembro.Count == 0) { Console.WriteLine("Este membro não possui jogos emprestados."); return; }

            Console.WriteLine("Jogos emprestados a este membro:");
            foreach (var j in jogosDoMembro) Console.WriteLine($"ID: {j.Id} - Nome: {j.Nome}");

            Console.Write("Digite o ID do jogo para devolver: ");
            if (!int.TryParse(Console.ReadLine(), out int idJogo)) { Console.WriteLine("Entrada inválida."); return; }

            var jogoEscolhido = jogosDoMembro.FirstOrDefault(j => j.Id == idJogo);
            if (jogoEscolhido == null) { Console.WriteLine("Jogo não encontrado."); return; }

            // Atualiza status
            jogoEscolhido.estado = true;
            jogoEscolhido.emprestadoha = "ninguem";

            SalvarJogos(jogosLista);
            Console.WriteLine($"Jogo '{jogoEscolhido.Nome}' devolvido com sucesso!");
        }
    }
}
