using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Domain
{
    public class Article
    {
        public int Cod_articulo { get; set; }
        public string Nombre { get; set; }
        public decimal Pre_unitario { get; set; }

        public override string ToString()
        {
            return Cod_articulo + " " + Nombre + " "+ Pre_unitario;
        }
    }
}
