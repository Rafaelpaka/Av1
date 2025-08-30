
using TrabalhoV1.modelo;

namespace program 
{



    public class Program
    {

        static void Main(string[] args)
        {
            string A;
            while (true)
            {
                Console.WriteLine("=== LUDOTECA .NET ===");
                Console.WriteLine("1 Cadastrar jogo");
                Console.WriteLine("2 Excluir Jogo");
                Console.WriteLine("2 Cadastrar membro");
                Console.WriteLine("3 Listar jogos");
                Console.WriteLine("4 Emprestar jogo");
                Console.WriteLine("5 Devolver jogo");
                Console.WriteLine("6 Gerar relatório");
                Console.WriteLine("0 Sair");
                Console.Write("opção: ");
                A = Console.ReadLine();

                CadastroJogo cadastro = new CadastroJogo();
                switch (A)
                {
                    case "1":
                       
                        cadastro.Cadastrar();
                        break;
                    case "2":
                        cadastro.Excluir();
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
                    case "0":
                        return;
                }
            }
        }
    }
}





