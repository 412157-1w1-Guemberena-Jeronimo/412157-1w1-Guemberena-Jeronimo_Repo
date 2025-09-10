using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio_1._5__Comercio_.Domain;

namespace Ejercicio_1._5__Comercio_.Services
{
    public interface IBudgetService
    {
        List<Budget> GetAllBudgets();// Devuelve todos los presupuestos
        Budget GetBudgetById(int id);// Devuelve un presupuesto por su id
        bool SaveBudget(Budget budget);// Guarda un presupuesto
        bool UpdateBudget(Budget budget);// Actualiza un presupuesto


    }
    
}
