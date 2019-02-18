using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdemDeServico.Entities;
using OrdemDeServico.Entities.Exceptions;
using System.Globalization;

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

                try
                {
                    if (op == 1)
                    {
                        Console.Write("Digite a data de abertura: ");
                        DateTime dataDeABertura = Convert.ToDateTime(Console.ReadLine());

                        /*if(!(dataDeABertura.CompareTo(DateTime.Now) == 1)){
                            throw new OSException("Data de abertura deve ser superior a data atual");
                        }*/

                        Console.WriteLine();

                        Console.Write("Digite o nome do resposavel: ");
                        string nome = Console.ReadLine();

                        OS os = new OS(dataDeABertura, nome);

                        Console.WriteLine();
                        
                        Console.Write("Quantas areas pretende cadastrar: ");
                        int numAreas = int.Parse(Console.ReadLine());

                        CadastrarArea(numAreas, os);

                        Console.Clear();

                        listOS.Add(os);

                        Console.WriteLine(os.ToString());

                        Console.ReadKey();
                        Console.Clear();
                    }

                    else if (op == 2)
                    {
                        if (listOS.Count < 1)
                        {
                            throw new OSException("Nada cadastrado!");
                        }

                        foreach (OS ordem in listOS)
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

                        if (encerrarOS == null)
                        {
                            throw new OSException($"Desculpe!, nada foi encontrado com o numero {numero} digitado");
                        }

                        Console.Clear();
                        encerrarOS.ToString();

                        Console.Write("Digite a data de encerramento: ");
                        DateTime dataEncerramento = Convert.ToDateTime(Console.ReadLine());

                        encerrarOS.EncerrarOS(dataEncerramento);

                        Console.Clear();
                        Console.WriteLine(encerrarOS.ToString());
                        Console.ReadKey();
                    }

                    else if (op == 4)
                    {
                        Console.Write("Qual numero de OS que deseja incluir uma nova area?: ");
                        int numero = Convert.ToInt32(Console.ReadLine());
                        var editOS = listOS.SingleOrDefault(x => x.Numero == numero);

                        if (editOS == null)
                        {
                            throw new OSException($"Desculpe!, nada foi encontrado com o  numero {numero} digitado");
                        }

                        Console.Write("Quantas novas areas pretende cadastrar: ");
                        int numAreas = int.Parse(Console.ReadLine());

                        CadastrarArea(numAreas, editOS);

                        Console.Clear();

                        Console.WriteLine(editOS.ToString());

                        Console.ReadKey();
                        Console.Clear();

                    }
                }
                catch (OSException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (op != 0);
        }

        static void Menu(ref int op)
        {
            try
            {
                Console.WriteLine("0 . Sair do programa.");
                Console.WriteLine("1 . Criar uma nova OS (solicitar data de abertura).");
                Console.WriteLine("2 . Listar todas as OS cadastradas.");
                Console.WriteLine("3 . Encerrar uma OS");
                Console.WriteLine("4 . Incluir uma nova área na OS.");
                Console.WriteLine("-------------------------------------------");
                Console.Write("INFORME QUAL TIPO DE OPERACAO PRETENDE FAZER: ");

                op = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Console.Clear();
            }
            

            Console.Clear();
        }

        static void CadastrarArea(int numAreas, OS os)
        {
            for (int i = 1; i <= numAreas; i++)
            {
                Console.WriteLine($"Digite os dados da Area #{i}");

                Console.Write("Digite o codigo da area: ");
                int codigoArea = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite o tamanho da area: ");
                double tamanhoDaArea = double.Parse(Console.ReadLine().ToString(CultureInfo.InvariantCulture));

                Area a = new Area(codigoArea, tamanhoDaArea);

                os.AddArea(a);

            }
        }
    }
}
