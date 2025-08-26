using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace av1
{

    public class Cadastro
    {
        public const string CaminhoArquivo = "biblioteca.json";
        public static async Task<List<Jogo>> CarregarJogosAsinc()
        {
            if (File.Exists(CaminhoArquivo))
            {
                var JsonString = await File.ReadAllTextAsync(CaminhoArquivo);
                return JsonSerializer.Deserialize<List<Jogo>>(JsonString) ?? new List<Jogo>();


            }
            return new List<Jogo>();

        }

        public static async Task SalvarJogosAsync(List<Jogo> Jogos)
        {
            var opcao = new JsonSerializerOptions { WriteIndented = true };
            var JsonString = JsonSerializer.Serialize(Jogos, opcao);
            await File.WriteAllTextAsync(CaminhoArquivo, JsonString);


        }

        public async Task CadastrarNovoJogoAsync()
        {
            Console.WriteLine("--- Cadastrar Jogo ---");

            var JogosExistentes = await CarregarJogosAsync();

            Console.WriteLine("Digite o nome do jogo: ");

        }
    }
}

            
      