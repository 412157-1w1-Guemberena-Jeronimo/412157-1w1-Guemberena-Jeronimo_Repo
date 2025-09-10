using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Domain
{
    public class BudgetDetail //Esta clase sirve para "conectar los articulos con la factura/presupuesto(budget)"
    {
        public int Id { get; set; }
        public Article Article { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Id + " " + Article.Nombre + " " + Count;
        }

        
    }
}
