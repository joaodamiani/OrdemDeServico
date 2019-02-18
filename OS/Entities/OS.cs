using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdemDeServico.Entities;
using System.Threading;
using OrdemDeServico.Entities.Exceptions;

namespace OrdemDeServico.Entities
{
    class OS
    {
        public int Numero { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataEncerramento { get; set; }
        public string Nome { get; set; }
        public List<Area> Area { get; set; } = new List<Area>();

        // variavel static para incrementar a cada novo objeto
        public static int globalOSNumero;

        public OS(DateTime dataAbertura, string nome)
        {
            // Interlocked.Increment função de auto increment e o ref referencia a variavel static
            Numero = Interlocked.Increment(ref globalOSNumero);
            DataAbertura = dataAbertura;
            Nome = nome;
        }

        public OS(DateTime dataAbertura, DateTime dataEncerramento, string nome)
        {
            // Interlocked.Increment função de auto increment e o ref referencia a variavel static
            Numero = Interlocked.Increment(ref globalOSNumero);
            DataAbertura = dataAbertura;
            DataEncerramento = dataEncerramento;
            Nome = nome;
        }

        // Metodo para adicionar areas
        public void AddArea(Area area)
        {
            foreach (Area a in Area)
            {
                if (a.Codigo == area.Codigo)
                {
                    throw new OSException($"Erro! o codigo {area.Codigo} já esta em uso em uma area");
                }
            }

            Area.Add(area);
        }

        // Metodo para calcular a soma das areas do objeto
        public double AreaOS()
        {
            double soma = 0;

            foreach (Area ar in Area)
            {
                soma = soma + ar.TamanhoArea;
            }
            return soma;
        }

        // Metodo para alterar a data de termino da Os e assim encerra-la
        public void EncerrarOS(DateTime termino)
        {
            int result = DateTime.Compare(termino, DataAbertura);

            if (result < 0)
            {
                throw new OSException("A data de termino deve ser posterior a de abertura");
            }
            else
            {
                DataEncerramento = termino;
            }

        }

        // verifica se a OS ja foi encerrada ou não
        public void OSEncerrada(int numero)
        {
            if(DataEncerramento != new DateTime())
            {
                throw new OSException($"A OS ({numero}) ja está encerrada!");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Numero: ");
            sb.AppendLine(Convert.ToString(Numero));
            sb.Append("Nome do responsavel: ");
            sb.AppendLine(Convert.ToString(Nome));
            sb.Append("Data de Abertura: ");
            sb.AppendLine(DataAbertura.ToString("dd/MM/yyyy"));

            if (DataEncerramento != new DateTime())
            {
                sb.Append("Data de Encerramento: ");
                sb.AppendLine(DataEncerramento.ToString("dd/MM/yyyy"));
            }
            
            int i = 1;
            foreach (Area ar in Area)
            {
                sb.AppendLine($"Dados Area #{i}: ");
                sb.Append("\tCodigo da area : ");
                sb.AppendLine(Convert.ToString(ar.Codigo));
                sb.Append("\tTamanho da area : ");
                sb.AppendLine(Convert.ToString(ar.TamanhoArea));
                i++;
            }
            sb.AppendLine();
            sb.Append("Area da OS: ");
            sb.AppendLine(Convert.ToString(AreaOS()));

            return sb.ToString();
        }
    }
}
