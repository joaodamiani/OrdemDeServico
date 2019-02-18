using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdemDeServico.Entities;

namespace OrdemDeServico.Entities.Exceptions
{
    class OSException : Exception
    {
        public OSException(string msg) : base(msg)
        {

        }

    }
}
