using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TrabalhoV1;

namespace TrabalhoV1
{
    public class Emprestar
    {
        private readonly string caminhoJogos = "../../../biblioteca.json";
        private readonly string caminhoMembros = "../../../Membros.json";

        
        private List<Jogo> CarregarJogos()
        {
            if (!File.Exists(caminhoJogos)) return new List<Jogo>();
            string json = File.ReadAllText(caminhoJogos);
            if (string.IsNullOrEmpty(json)) return new List<Jogo>();
            return JsonSerializer.Deserialize<List<Jogo>>(json);
        }

        
        private List<Membros> CarregarMembros()
        {
            if (!File.Exists(caminhoMembros)) return new List<Membros>();
            string json = File.ReadAllText(caminhoMembros);
            if (string.IsNullOrEmpty(json)) return new List<Membros>();
            return JsonSerializer.Deserialize<List<Membros>>(json);
        }

        
        private void SalvarJogos(List<Jogo> jogos)
        {
            File.WriteAllText(caminhoJogos, JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true }));
        }

        
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

           
            jogoEscolhido.estado = false;
            jogoEscolhido.emprestadoha = membroEscolhido.NomeMem;
            jogoEscolhido.DataEmprestimo = DateTime.Now; // 

            SalvarJogos(jogosLista);
            Console.WriteLine($"Jogo '{jogoEscolhido.Nome}' emprestado para {membroEscolhido.NomeMem} em {jogoEscolhido.DataEmprestimo.Value:dd/MM/yyyy}!");
        }

        
        public void DevolverJogo()
        {
            var jogosLista = CarregarJogos();
            var membrosLista = CarregarMembros();

            if (jogosLista.Count == 0) { Console.WriteLine("Nenhum jogo cadastrado."); return; }
            if (membrosLista.Count == 0) { Console.WriteLine("Nenhum membro cadastrado."); return; }

            
            var membrosComJogos = membrosLista
                .Where(m => jogosLista.Any(j => j.emprestadoha == m.NomeMem && !j.estado))
                .ToList();

            if (membrosComJogos.Count == 0) { Console.WriteLine("Nenhum membro possui jogos emprestados."); return; }

            Console.WriteLine("Membros com jogos emprestados:");
            foreach (var m in membrosComJogos)
                Console.WriteLine($"ID: {m.IdMem} - Nome: {m.NomeMem}");

            Console.Write("Digite o ID do membro que vai devolver: ");
            if (!int.TryParse(Console.ReadLine(), out int idMembro)) { Console.WriteLine("Entrada inválida."); return; }

            var membroEscolhido = membrosComJogos.FirstOrDefault(m => m.IdMem == idMembro);
            if (membroEscolhido == null) { Console.WriteLine("Membro não encontrado ou não possui jogos emprestados."); return; }

            var jogosDoMembro = jogosLista.Where(j => j.emprestadoha == membroEscolhido.NomeMem && !j.estado).ToList();

            Console.WriteLine($"Jogos emprestados a {membroEscolhido.NomeMem}:");
            foreach (var j in jogosDoMembro)
                Console.WriteLine($"ID: {j.Id} - Nome: {j.Nome}");

            Console.Write("Digite o ID do jogo para devolver: ");
            if (!int.TryParse(Console.ReadLine(), out int idJogo)) { Console.WriteLine("Entrada inválida."); return; }

            var jogoEscolhido = jogosDoMembro.FirstOrDefault(j => j.Id == idJogo);
            if (jogoEscolhido == null) { Console.WriteLine("Jogo não encontrado."); return; }


            jogoEscolhido.estado = true;
            jogoEscolhido.emprestadoha = "ninguem";


            Console.Write("Digite a data da devolução (dd/MM/yyyy) ou pressione ENTER para usar a data de hoje: ");
            string inputData = Console.ReadLine();

            DateTime dataDevolucao;
            if (string.IsNullOrWhiteSpace(inputData))
            {
                dataDevolucao = DateTime.Now;
            }
            else
            {
                while (!DateTime.TryParseExact(inputData, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataDevolucao)
                       || dataDevolucao < jogoEscolhido.DataEmprestimo)
                {
                    if (dataDevolucao < jogoEscolhido.DataEmprestimo)
                    {
                        Console.WriteLine($"A data de devolução não pode ser anterior à data de empréstimo ({jogoEscolhido.DataEmprestimo.Value:dd/MM/yyyy}).");
                    }
                    else
                    {
                        Console.WriteLine("Data inválida. Digite novamente no formato dd/MM/yyyy.");
                    }

                    Console.Write("Digite a data da devolução: ");
                    inputData = Console.ReadLine();
                }
            }

            jogoEscolhido.DataDevolucao = dataDevolucao;


            decimal valor = 10;
            int dias = (jogoEscolhido.DataDevolucao.Value - jogoEscolhido.DataEmprestimo.Value).Days + 1;
            if (dias > 7)
            {
                int diasAtraso = dias - 7;
                valor += diasAtraso * 5;
            }

            Console.WriteLine($"💰 Valor total a pagar: R$ {valor:F2}");


            Console.WriteLine("Escolha a forma de pagamento:");
            Console.WriteLine("1 - Dinheiro");
            Console.WriteLine("2 - Pix");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":            
                    Console.WriteLine("Pagamento realizado em dinheiro.");
                    jogoEscolhido.FormaPagamento = "Dinheiro";
                    break;
                case "2":                 
                    Console.WriteLine("Pagamento realizado via Pix.");
                    jogoEscolhido.FormaPagamento = "Pix";

                    break;
            }

            SalvarJogos(jogosLista);
            Console.WriteLine($" Jogo '{jogoEscolhido.Nome}' devolvido com sucesso em {jogoEscolhido.DataDevolucao.Value:dd/MM/yyyy}!");
        }
    }
}
