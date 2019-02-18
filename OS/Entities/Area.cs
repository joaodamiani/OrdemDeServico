using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemDeServico.Entities
{
    class Area
    {
        
        public int Codigo { get; set; }
        public double TamanhoArea { get; set; }
        public Area(int codigo, double tamanhoArea)
        {
            Codigo = codigo;
            TamanhoArea = tamanhoArea;
        }


    }
}
