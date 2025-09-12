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

        public List<BudgetDetail> details { get; set; }//Esto va privado porque no queremos que nadie pueda modificar la lista details directamente, sino a traves de los metodos que nosotros definamos

        public List<BudgetDetail> GetDetails() => details; // Esto es un metodo que devuelve la lista details, para que nadie pueda modificarla directamente
        public Budget()// Este constructor sirve para inicializar la lista details y evitar errores de referencia nula
        {
            Id = 0;
            Date = DateTime.Now;
            details = new List<BudgetDetail>();
        }


        // Devuelve la lista details
      
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
