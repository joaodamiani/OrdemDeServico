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

                        // estanciação de uma nova OS
                        OS os = new OS(dataDeABertura, nome);

                        Console.WriteLine();

                        Console.Write("Quantas areas pretende cadastrar: ");
                        int numAreas = int.Parse(Console.ReadLine());

                        // chamada da função para cadastramento de novas areas
                        CadastrarArea(numAreas, os);

                        Console.Clear();

                        // Adicionar na lista de OSs
                        listOS.Add(os);

                        Console.WriteLine(os.ToString());

                        ToBeContinue();
                    }

                    else if (op == 2)
                    {
                        // caso nao tenha nenhum registro ainda cadastrado
                        if (listOS.Count < 1)
                        {
                            throw new OSException("Nada cadastrado!");
                        }

                        // percorre a lista e mostra todos os resultados
                        foreach (OS ordem in listOS)
                        {
                            Console.WriteLine(ordem.ToString());
                            Console.WriteLine("--------------------------------------------");
                        }

                        ToBeContinue();
                    }

                    else if (op == 3)
                    {
                        Console.Write("Qual numero de OS que deseja encerrar: ");
                        int numero = Convert.ToInt32(Console.ReadLine());

                        var encerrarOS = listOS.SingleOrDefault(x => x.Numero == numero);

                        // verifica se encontrou nao encontrou nenhum resultado
                        if (encerrarOS == null)
                        {
                            throw new OSException($"Desculpe!, nada foi encontrado com o numero {numero} digitado");
                        }

                        // Função para verificar se a OS já esta encerrada ou nao
                        encerrarOS.OSEncerrada(numero);

                        Console.Clear();
                        encerrarOS.ToString();

                        Console.Write("Digite a data de encerramento: ");
                        DateTime dataEncerramento = Convert.ToDateTime(Console.ReadLine());

                        // chama a função para alterar a data de encerramento
                        encerrarOS.EncerrarOS(dataEncerramento);

                        Console.Clear();
                        Console.WriteLine(encerrarOS.ToString());

                        ToBeContinue();
                    }

                    else if (op == 4)
                    {
                        Console.Write("Qual numero de OS que deseja incluir novas areas: ");
                        int numero = Convert.ToInt32(Console.ReadLine());
                        var editOS = listOS.SingleOrDefault(x => x.Numero == numero);

                        // verifica se encontrou nao encontrou nenhum resultado
                        if (editOS == null)
                        {
                            throw new OSException($"Desculpe!, nada foi encontrado com o  numero {numero} digitado");
                        }

                        // Função para verificar se a OS já esta encerrada ou nao
                        editOS.OSEncerrada(numero);

                        Console.Write("Quantas novas areas pretende cadastrar: ");
                        int numAreas = int.Parse(Console.ReadLine());

                        // chamada da função para cadastramento de novas areas
                        CadastrarArea(numAreas, editOS);

                        Console.Clear();

                        Console.WriteLine(editOS.ToString());

                        ToBeContinue();
                    }
                    else if (op == 5)
                    {
                        try
                        {
                            Console.WriteLine("Entre com o intarvalo de datas para a filtragem");
                            Console.Write("Data inicio: ");
                            string mesAnoEntrada = Console.ReadLine();

                            Console.Write("Data Fim: ");
                            string mesAnoFim = Console.ReadLine();

                            // separa as datas por variavies de mes e ano(data de inicio)
                            int mesEntrada = int.Parse(mesAnoEntrada.Substring(0, 2));
                            int anoEntrada = int.Parse(mesAnoEntrada.Substring(3));

                            // separa as datas por variavies de mes e ano(data de fim)
                            int mesFim = int.Parse(mesAnoFim.Substring(0, 2));
                            int anoFim = int.Parse(mesAnoFim.Substring(3));

                            // variavel para verificar se encontrou pelo menos um regitro
                            bool encontrouResultado = false;

                            Console.Clear();

                            foreach (OS os in listOS)
                            {
                                // verifica se as datas digitadas estao no intervalo definido
                                if (os.DataAbertura.Year >= anoEntrada && os.DataAbertura.Year <= anoFim
                                    && os.DataAbertura.Month >= mesEntrada && os.DataAbertura.Month <= mesFim)
                                {
                                    Console.WriteLine(os.ToString());
                                    Console.WriteLine("------------------------------------");
                                    encontrouResultado = true; // muda a variavel para true, ja que achou um registro
                                }
                            }

                            // caso nao tenha encontrado nenhum resultado
                            if (encontrouResultado == false)
                            {
                                Console.WriteLine($"Nenhuma OS encontrada no intervalo {mesEntrada}/{anoEntrada} e {mesFim}/{anoFim}");
                            }
                        }
                        // tratamento caso a data esteja digitado de maneira errada
                        catch (FormatException)
                        {
                            Console.WriteLine("Data digitada de maneira errada!");
                        }
                        // tratamento de exceção caso o usuario nao tenha digitado nada
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Nenhuma dada digitada!");
                        }
                        
                        ToBeContinue();
                    }
                }
                // Tratamento de exceção personalisadas da aplicação Ordem de Serviço
                catch (OSException e)
                {
                    Console.WriteLine(e.Message);
                    ToBeContinue();
                }
                // Tratamento de exceção genericas
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ToBeContinue();
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
                Console.WriteLine("5 . Listar OSs por intervalo de datas");
                Console.WriteLine("-------------------------------------------");
                Console.Write("INFORME QUAL TIPO DE OPERACAO PRETENDE FAZER: ");

                op = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ToBeContinue();
            }

            Console.Clear();
        }

        /* função para cadastrar areas que recebe como parametros o numero de novas Areas e o objeto OS*/
        static void CadastrarArea(int numAreas, OS os)
        {
            for (int i = 1; i <= numAreas; i++)
            {
                Console.WriteLine($"Digite os dados da Area #{i}");

                Console.Write("Digite o codigo da area: ");
                int codigoArea = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite o tamanho da area: ");
                double tamanhoDaArea = double.Parse(Console.ReadLine().ToString(CultureInfo.InvariantCulture));

                // Estanciação de uma nova area
                Area a = new Area(codigoArea, tamanhoDaArea);

                // função de adicionar area na classe OS
                os.AddArea(a);
            }
        }

        // Função que espera uma tecla para a aplicação continuar
        static void ToBeContinue()
        {
            Console.WriteLine("Tecle para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
