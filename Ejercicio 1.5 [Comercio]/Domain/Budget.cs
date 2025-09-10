using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_1._5__Comercio_.Domain
{
    public class Budget
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Client { get; set; }
        
        public PayMethod? PayMethod { get; set; }

        private List<BudgetDetail> details;//Esto va privado porque no queremos que nadie pueda modificar la lista details directamente, sino a traves de los metodos que nosotros definamos

        public Budget()// Este constructor sirve para inicializar la lista details y evitar errores de referencia nula
        {
            details = new List<BudgetDetail>();
        }

        public List<BudgetDetail> GetDetails()// Devuelve la lista details
        {
            return details;
        }
        public void AddDetail(BudgetDetail detail)//
        {
            if (detail != null)

            {
                details.Add(detail);
            }
        }
        public void RemoveDetail(int index)// Elimina un detalle de la lista details en la posicion index (es como hacer un delete pasando el id del detalle)
        {   
            details.RemoveAt(index);
        }
        public override string ToString()
        {
            return Id + " " + Date +" " + Client; 
        }






    }
}
