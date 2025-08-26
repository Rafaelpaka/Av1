using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace TrabalhoV1.modelo
{
    public class CadastroJogo
    {
        public void cadastrar()
        {
            string caminhoArquivo = "biblioteca.json";

            List<Jogo> jogos = new List<Jogo>();

            if (File.Exists(caminhoArquivo))
            {
                string JsonExistente = File.ReadAllText(caminhoArquivo);
                if (!string.IsNullOrEmpty(JsonExistente))
                {
                    jogos = JsonSerializer.Deserialize<List<Jogo>>(JsonExistente);
                }
            }

            Jogo novoJogo = new();

            Console.WriteLine("Digite o nome do jogo: ");
            novoJogo.Nome = Console.ReadLine();


            Console.WriteLine("Digite o Genero: ");
            novoJogo.Genero = Console.ReadLine();

            Console.WriteLine("Ano do jogo: ");
            novoJogo.Ano = int.Parse(Console.ReadLine());

            novoJogo.Id = jogos.Count + 1;

            jogos.Add(novoJogo);

            string novoJson = JsonSerializer.Serialize(jogos, new JsonSerializerOptions {WriteIndented = true });
            File.WriteAllText(caminhoArquivo, novoJson);


            Console.WriteLine($"Jogo '{novoJogo.Nome}'adicionado à biblioteca");




        }
    }

}