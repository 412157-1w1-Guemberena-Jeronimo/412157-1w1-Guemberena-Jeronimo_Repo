using ComercioApiRest.Data;
using ComercioApiRest.Models;

namespace ComercioApiRest.Services
{
    public class BudgetService : IBudgetService
    {
        IBudgetRepository _repo;
        public BudgetService(IBudgetRepository repo)
        {
            _repo = repo;
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public List<Factura> GetAll()
        {
            return _repo.GetAll();
        }

        public bool Save(Factura factura)
        {
            return _repo.Save(factura);
        }
    }
}
