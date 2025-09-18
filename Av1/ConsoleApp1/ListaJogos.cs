using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TrabalhoV1
{
    public class ListaJogos
    {
        public readonly string caminhoArquivo = "../../../biblioteca.json";

        public void Listar()
        {
            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine("Não foi possível localizar o arquivo, por favor cadastre algum jogo.");
                return;
            }

            string existeJson = File.ReadAllText(caminhoArquivo);

            if (string.IsNullOrEmpty(existeJson))
            {
                Console.WriteLine("Nenhum jogo cadastrado ainda.");
                return;
            }

            List<Jogo> jogosLista = JsonSerializer.Deserialize<List<Jogo>>(existeJson);

            if (jogosLista == null || jogosLista.Count == 0)
            {
                Console.WriteLine("Nenhum jogo cadastrado ainda.");
                return;
            }

            Console.WriteLine("Jogos na biblioteca:");
            foreach (var jogo in jogosLista)
            {
                Console.WriteLine($"Id: {jogo.Id}");
                Console.WriteLine($"Nome: {jogo.Nome}");
                Console.WriteLine($"Gênero: {jogo.Genero}");
                Console.WriteLine($"Ano: {jogo.Ano}");
                Console.WriteLine($"Emprestado a: {jogo.emprestadoha}");
                Console.WriteLine($"Estado: {(jogo.estado ? "Disponível" : "Indisponível")}");
                Console.WriteLine("");

            }
        }
    }
}
