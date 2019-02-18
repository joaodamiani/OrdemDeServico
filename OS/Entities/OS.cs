using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdemDeServico.Entities;
using System.Threading;

namespace OrdemDeServico.Entities
{
    class OS
    {
        
        public int Numero { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataEncerramento { get; set; }
        public string Nome { get; set; }
        public List<Area> Area { get; set; } = new List<Area>();

        public static int globalOSNumero;

        public OS(DateTime dataAbertura, string nome)
        {
            Numero = Interlocked.Increment(ref globalOSNumero);
            DataAbertura = dataAbertura;
            Nome = nome;
        }

        public OS(DateTime dataAbertura, DateTime dataEncerramento, string nome)
        {
            Numero = Interlocked.Increment(ref globalOSNumero);
            DataAbertura = dataAbertura;
            DataEncerramento = dataEncerramento;
            Nome = nome;
        }

        public void AddArea(Area area)
        {
            Area.Add(area);
        }

        public double AreaOS()
        {
            double soma = 0;

            foreach (Area ar in Area)
            {
                soma = soma + ar.TamanhoArea;
            }
            return soma;
        }

        public void EncerrarOS(DateTime termino)
        {
            DataEncerramento = termino;
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


            int i=1;
            foreach (Area ar in Area)
            {
                sb.AppendLine($"Dados Area #{i}: ");
                sb.Append("\tCodigo da area : ");
                sb.AppendLine(Convert.ToString(ar.Codigo));
                sb.Append("\tTamanho da area : ");
                sb.AppendLine(Convert.ToString(ar.TamanhoArea));
                i++;
            }

            sb.Append("\tArea da OS: ");
            sb.AppendLine(Convert.ToString(AreaOS()));

            return sb.ToString();
        }
    }
}
