
using TrabalhoV1;
using TrabalhoV1.modelo;

namespace program 
{



    public class Program
    {

        static void Main(string[] args)
        {
            string opcao;
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
                opcao = Console.ReadLine();

                Emprestar emprestimo = new Emprestar();
                ListaJogos Lista = new ListaJogos();
                CadastroJogo cadastro = new CadastroJogo();
                CadastroMembros cadastromem = new CadastroMembros();
           
                    switch (opcao)
                {
                    case "1":
                        cadastro.Cadastrar(); // [AV1-4-Cadastrar]
                        break;
                    case "2":
                        cadastro.Excluir(); // [AV1-4-Excluir]
                        break;
                    case "3":
                        cadastromem.CadastroMembr(); // [AV1-4-CadastroMembro]
                        break;
                    case "4":
                        cadastromem.ExcluirCad(); // [AV1-4-ExcluirMembro]
                        break;
                    case "5":
                        Lista.Listar(); // [AV1-4-Listar]
                        break;
                    case "6":
                        emprestimo.EmprestarJogo(); // [AV1-4-Emprestar]
                        break;
                    case "7":
                        emprestimo.DevolverJogo(); // [AV1-4-Devolver]
                        break;
                    case "8":
                        GerarRelatorio relatorio = new GerarRelatorio(); // [AV1-4-Relatorio]
                        relatorio.RelatorioEmprestimos();
                        break;
                    default:
                        Console.WriteLine("Opção indisponive, tente novamente");
                        break;
                    case "0":
                        return; // [AV1-4-Sair]

                    
                }

                Console.WriteLine("pressione Enter para continuar");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}





