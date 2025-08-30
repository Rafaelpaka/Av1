using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TrabalhoV1
{
    public class CadastroMembros
    {
        public readonly string caminhoArquivo = "../../../Membros.json";

        public void excluirCad()
        {
            if (!File.Exists(caminhoArquivo)) {
                Console.WriteLine("Esse membro não existe "); return;
            
            }
            string ExisteJson = File.ReadAllText(caminhoArquivo);
            List<Membros> membrosLista = JsonSerializer.Deserialize<List<Membros>>(ExisteJson);
            Console.WriteLine("membros");
            foreach (var membros in membrosLista)
            {
                    Console.WriteLine($"ID: {membros.IdMem} \n Nome: {membros.NomeMem}\n Emprestado: {membros.emprestimo}");
            }
            Console.WriteLine("\nDigite o ID do membro que deseja excluir: ");
            if (int.TryParse(Console.ReadLine(), out int idExcluir))
            {
                var membroExcluir = membrosLista.FirstOrDefault(m => m.IdMem == idExcluir);

                if (membroExcluir != null)
                {
                    membrosLista.Remove(membroExcluir);

                    
                    string novoJson = JsonSerializer.Serialize(membrosLista, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(caminhoArquivo, novoJson);

                    Console.WriteLine($"\nMembro '{membroExcluir.NomeMem}' (ID: {idExcluir}) excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("ID não encontrado. Nenhuma exclusão realizada.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Digite apenas números.");
            }

        }

        public void CadastroMembr()
        {
            
            List<Membros> membrosLista = new List<Membros>();

            
            if (File.Exists(caminhoArquivo))
            {
                string JsonsExiste = File.ReadAllText(caminhoArquivo);
                if (!string.IsNullOrEmpty(JsonsExiste))
                {
                    membrosLista = JsonSerializer.Deserialize<List<Membros>>(JsonsExiste);
                }
            }

            
            Membros novoMembro = new Membros();

            
            do
            {
                Console.WriteLine("Digite o nome do membro: ");
                novoMembro.NomeMem = Console.ReadLine();

                if (string.IsNullOrEmpty(novoMembro.NomeMem))
                {
                    Console.WriteLine("O nome não pode ser vazio, por favor escreva algo");
                }

            } while (string.IsNullOrEmpty(novoMembro.NomeMem));

           
            novoMembro.IdMem = membrosLista.Count + 1;

            
            membrosLista.Add(novoMembro);

            
            string novoJson = JsonSerializer.Serialize(membrosLista, new JsonSerializerOptions { WriteIndented = true });

            
            File.WriteAllText(caminhoArquivo, novoJson);

            Console.WriteLine($"Membro '{novoMembro.NomeMem}' adicionado com sucesso!");
        }
    }
}
