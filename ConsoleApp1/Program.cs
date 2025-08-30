
using TrabalhoV1;
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
                Console.WriteLine("3 Cadastrar membro");
                Console.WriteLine("4 Excluir membro");
                Console.WriteLine("5 Listar jogos");
                Console.WriteLine("6 Emprestar jogo");
                Console.WriteLine("7 Devolver jogo");
                Console.WriteLine("8 Gerar relatório");
                Console.WriteLine("0 Sair");
                Console.Write("opção: ");
                A = Console.ReadLine();

                Emprestar emprestimo = new Emprestar();
                ListaJogos Lista = new ListaJogos();
                CadastroJogo cadastro = new CadastroJogo();
                CadastroMembros cadastromem = new CadastroMembros();
                switch (A)
                {
                    case "1":
                       
                        cadastro.Cadastrar();
                        break;
                    case "2":
                        cadastro.Excluir();
                        break;
                    case "3":
                        cadastromem.CadastroMembr();
                        break;
                    case "4":
                        cadastromem.excluirCad();
                        break;
                    case "5":
                        Lista.Listar();
                        break;
                    case "6":
                        emprestimo.EmprestarJogo();
                        break;
                    case "7":
                        emprestimo.DevolverJogo();
                        break;
                    case "8":
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





