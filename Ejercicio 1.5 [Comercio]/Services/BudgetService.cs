using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio_1._5__Comercio_.Domain;
using Ejercicio_1._5__Comercio_.Data;

namespace Ejercicio_1._5__Comercio_.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly string _cnnString;
        public BudgetService(string cnnString)
        {
            _cnnString = cnnString;
        }
        public List<Budget> GetAllBudgets()
        {
            using(var uow = new UnitOfWork(_cnnString))
            {
                return uow.BudgetRepository.GetAll();
            }
        }
        public Budget GetBudgetById(int id)
        {
            using(var uow = new UnitOfWork(_cnnString))
            {
                return uow.BudgetRepository.GetById(id);
            }
        }

        public bool SaveBudget(Budget budget)
        {
            using(var uow = new UnitOfWork(_cnnString))
            {
                try
                {
                    var repository = uow.BudgetRepository;
                    bool result = repository.Save(budget);
                    if(!result)
                    { return false; }
                    uow.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    //log
                    return false;
                }
            }
        }
        public bool UpdateBudget(Budget budget)
        {
            using(var uow = new UnitOfWork(_cnnString))
            {
                try
                {
                    var repository = uow.BudgetRepository;
                    bool result = repository.Update(budget);
                    if(!result)
                    { return false; }
                    uow.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    //log
                    return false;
                }
            }
        }

    }
}
