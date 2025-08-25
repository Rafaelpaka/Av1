using System


namespace program 
{



    public class Program
    {

        static void Main(string[] args)
        {
            string a;
            while (true)
            {
                Console.WriteLine("=== LUDOTECA .NET ===");
                Console.WriteLine("1 Cadastrar jogo");
                Console.WriteLine("2 Cadastrar membro");
                Console.WriteLine("3 Listar jogos");
                Console.WriteLine("4 Emprestar jogo");
                Console.WriteLine("5 Devolver jogo");
                Console.WriteLine("6 Gerar relatório");
                Console.WriteLine("0 Sair");
                Console.Write("opção:");
                a = Console.ReadLine();

                if (a == "0")
                {
                    Console.WriteLine("encerrando o programa...");
                    break;
                }
                switch (a)
                {
                    case "1":
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Opção indisponive, tente novamente");
                        break;
                }
            }
        }
    }
}





