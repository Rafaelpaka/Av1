using System.Text.Json;


namespace TrabalhoV1.modelo
{
    public class CadastroJogo
    {
        public readonly string caminhoArquivo = "../../../biblioteca.json";
        public void Excluir()
        {
            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine("arquivo biblioteca não existe"); return;
            }

            string ExisteJson = File.ReadAllText(caminhoArquivo);
            List<Jogo> jogos = JsonSerializer.Deserialize<List<Jogo>>(ExisteJson); 
            Console.WriteLine("Jogos na Biblioteca");
            foreach (var jogo in jogos)
            {
                Console.WriteLine($"ID: {jogo.Id} - Nome: {jogo.Nome} - Gênero: {jogo.Genero} - Ano: {jogo.Ano}");
            }

            Console.WriteLine("digite o ID do jogo que deseja excluir: ");
            if (int.TryParse(Console.ReadLine(), out int idexcluir))
            {

                var JogoExcluir = jogos.FirstOrDefault(j => j.Id == idexcluir);

                if (JogoExcluir != null)
                {
                    jogos.Remove(JogoExcluir);

                    string novoJson = JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(caminhoArquivo, novoJson);

                    Console.WriteLine($"\nJogo '{JogoExcluir.Nome}' (ID: {idexcluir}) excluído com sucesso.");
                }
                else
                {
                    Console.WriteLine("ID do jogo não encontrado. Nenhuma alteração foi feita.");
                }
            }
            else
            {
                Console.WriteLine("entrada invalida");
            }
        }



        public void Cadastrar()
        {


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
                do
                {

                    Console.WriteLine("Digite o nome do jogo: ");
                    novoJogo.Nome = Console.ReadLine();
                    if (string.IsNullOrEmpty(novoJogo.Nome))
                    {
                        Console.WriteLine("O nome não pode ser vazio. Porfavor, digite novamente");
                    }
                } while (string.IsNullOrEmpty(novoJogo.Nome));


                bool validaGen = false;
                while (!validaGen)
                {
                    Console.WriteLine("1 - Ação ");
                    Console.WriteLine("2 - Fantasia");
                    Console.WriteLine("3 - Corrida");
                    Console.WriteLine("4 - Terror");
                    Console.WriteLine("5 - Contrução");
                    Console.Write("Digite o Genero: ");
                    string GeneroJogo = Console.ReadLine();
                    switch (GeneroJogo)
                    {
                        case "1":
                            novoJogo.Genero = "ação";
                            validaGen = true;
                            break;
                        case "2":
                            novoJogo.Genero = "Fantasia";
                            validaGen = true;
                            break;
                        case "3":
                            novoJogo.Genero = "Corrida";
                            validaGen = true;
                            break;
                        case "4":
                            novoJogo.Genero = "Terror";
                            validaGen = true;
                            break;
                        case "5":
                            novoJogo.Genero = "Construção";
                            validaGen = true;
                            break;
                        default:
                            Console.WriteLine("Opção indisponivel, tente novamente");
                            validaGen = false;
                            break;
                    }
                }

                int anoJogo;
                Console.WriteLine("Ano do jogo: ");
                while (!int.TryParse(Console.ReadLine(), out anoJogo) || anoJogo < 1950)
                {
                    Console.WriteLine("Ano inválido. Euu rafael acredito não ter video games anteriores a 1950.");
                    Console.Write("Ano do jogo: ");
                }
                novoJogo.Ano = anoJogo;

                novoJogo.Id = jogos.Count + 1;

                jogos.Add(novoJogo);

                string novoJson = JsonSerializer.Serialize(jogos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, novoJson);


                Console.WriteLine($"Jogo '{novoJogo.Nome}'adicionado à biblioteca");




            }
        }

    }
