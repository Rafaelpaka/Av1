using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;

namespace TrabalhoV1
{
    public class CadastroMembros
    {
        private readonly string caminhoArquivo;

        public CadastroMembros()
        {
            string pastaData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data");
            if (!Directory.Exists(pastaData))
                Directory.CreateDirectory(pastaData);

            caminhoArquivo = Path.Combine(pastaData, "Membros.json");
        }

        private void Salvar(List<Membros> membros)
        {
            try // [AV1-5]
            {
                string json = JsonSerializer.Serialize(membros, new JsonSerializerOptions { WriteIndented = true }); // [AV1-3]
                File.WriteAllText(caminhoArquivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar membros: {ex.Message}");
                Debug.WriteLine($"[{DateTime.Now}] Erro Salvar Membros: {ex.Message}"); // [AV2-5]
            }
        }

        private List<Membros> Carregar()
        {
            try // [AV1-5]
            {
                if (!File.Exists(caminhoArquivo)) return new List<Membros>();
                string json = File.ReadAllText(caminhoArquivo);
                if (string.IsNullOrEmpty(json)) return new List<Membros>();
                return JsonSerializer.Deserialize<List<Membros>>(json); // [AV1-3]
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar membros: {ex.Message}");
                Debug.WriteLine($"[{DateTime.Now}] Erro Carregar Membros: {ex.Message}"); // [AV2-5]
                return new List<Membros>();
            }
        }

        public void CadastroMembr()
        {
            var membros = Carregar();
            Membros novo = new();
            do
            {
                Console.Write("Nome do membro: ");
                novo.NomeMem = Console.ReadLine();
            } while (string.IsNullOrEmpty(novo.NomeMem));

            novo.IdMem = membros.Count + 1;
            membros.Add(novo);
            Salvar(membros);

            Console.WriteLine($"Membro '{novo.NomeMem}' adicionado.");
        }

        public void ExcluirCad()
        {
            var membros = Carregar();
            if (membros.Count == 0) { Console.WriteLine("Nenhum membro cadastrado."); return; }

            Console.WriteLine("Membros:");
            membros.ForEach(m => Console.WriteLine($"ID: {m.IdMem} - {m.NomeMem}"));

            Console.Write("ID do membro para excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var excluir = membros.FirstOrDefault(m => m.IdMem == id);
            if (excluir == null) { Console.WriteLine("Membro não encontrado."); return; }

            membros.Remove(excluir);
            Salvar(membros);

            Console.WriteLine($"Membro '{excluir.NomeMem}' excluído.");
        }
    }
}
