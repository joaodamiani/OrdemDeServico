using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdemDeServico.Entities;

namespace OrdemDeServico
{
    class Program
    {
        static void Main(string[] args)
        {
            int op = 0;
            List<OS> listOS = new List<OS>();

            do
            {
                Menu(ref op);

                if (op == 1)
                {
                    Console.Write("Digite a data de abertura: ");
                    DateTime dataDeABertura =  Convert.ToDateTime(Console.ReadLine());

                    Console.WriteLine();

                    Console.Write("Digite o nome do resposavel: ");
                    string nome = Console.ReadLine();

                    OS os = new OS(dataDeABertura, nome);

                    Console.WriteLine();


                    Console.Write("Quantas areas pretende cadastrar: ");
                    int numAreas = int.Parse(Console.ReadLine());

                    for (int i = 1; i <= numAreas; i++)
                    {
                        Console.WriteLine($"Digite os dados da Area #{i}");

                        Console.Write("Digite o codigo da area: ");
                        int codigoArea = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Digite o tamanho da area: ");
                        double tamanhoDaArea = double.Parse(Console.ReadLine());

                        Area a = new Area(codigoArea, tamanhoDaArea);

                        os.AddArea(a);
                    }

                    Console.Clear();

                    listOS.Add(os);

                    Console.WriteLine(os.ToString()); 

                    Console.ReadKey();
                    Console.Clear();
                }

                else if (op == 2)
                {
                    foreach(OS ordem in listOS)
                    {
                        Console.WriteLine(ordem.ToString());
                        Console.WriteLine("--------------------------------------------");
                    }

                    Console.ReadKey();
                    Console.Clear();
                }

                else if (op == 3)
                {
                    Console.Write("Qual numero de OS que deseja encerrar: ");
                    int numero = Convert.ToInt32(Console.ReadLine());

                    var encerrarOS = listOS.SingleOrDefault(x => x.Numero == numero);

                    Console.Clear();
                    encerrarOS.ToString();

                    Console.Write("Digite a data de encerramento: ");
                    DateTime dataEncerramento = Convert.ToDateTime(Console.ReadLine());

                    encerrarOS.EncerrarOS(dataEncerramento);

                    Console.Clear();
                    encerrarOS.ToString();
                    Console.ReadKey();
                }

                else if (op == 4)
                {
                    Console.Write("Qual numero de OS que deseja incluir uma nova area?: ");
                    int numero = Convert.ToInt32(Console.ReadLine());
                    var editOS = listOS.SingleOrDefault(x => x.Numero == numero);

                    Console.Write("Quantas novas areas pretende cadastrar: ");
                    int numAreas = int.Parse(Console.ReadLine());

                    for (int i = 1; i <= numAreas; i++)
                    {
                        Console.WriteLine($"Digite os dados da Area #{i}");

                        Console.Write("Digite o codigo da area: ");
                        int codigoArea = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Digite o tamanho da area: ");
                        double tamanhoDaArea = double.Parse(Console.ReadLine());

                        Area a = new Area(codigoArea, tamanhoDaArea);

                        editOS.AddArea(a);
                    }

                    Console.Clear();

                    listOS.Add(editOS);

                    Console.WriteLine(editOS.ToString());

                    Console.ReadKey();
                    Console.Clear();

                }

            } while (op != 0);
        }

        static void Menu(ref int op)
        {
            Console.WriteLine("0 . Sair do programa.");
            Console.WriteLine("1 . Criar uma nova OS (solicitar data de abertura).");
            Console.WriteLine("2 . Listar todas as OS cadastradas.");
            Console.WriteLine("3 . Encerrar uma OS");
            Console.WriteLine("4 . Incluir uma nova área na OS.");
            Console.WriteLine("-------------------------------------------");
            Console.Write("INFORME QUAL TIPO DE OPERACAO PRETENDE FAZER: ");

            op = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
        }
    }
}
