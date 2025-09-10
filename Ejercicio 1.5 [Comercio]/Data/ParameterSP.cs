using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Data
{
    public class ParameterSP
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public ParameterDirection Direction { get; set; } = ParameterDirection.Input; 


    }
}
