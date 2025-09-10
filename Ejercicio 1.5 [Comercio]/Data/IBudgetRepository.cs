using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio_1._5__Comercio_.Domain;

namespace Ejercicio_1._5__Comercio_.Data
{
    public interface IBudgetRepository
    {
        bool Save(Budget budget);// Guarda un presupuesto en la base de datos
        List<Budget> GetAll();// Devuelve todos los presupuestos de la base de datos
        Budget GetById(int id);// Devuelve un presupuesto de la base de datos segun su id
        bool Delete(int id);// Elimina un presupuesto de la base de datos segun su id
        bool Update(Budget budget);// Actualiza un presupuesto en la base de datos
    }
}
