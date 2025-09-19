using ComercioApiRest.Models;
namespace ComercioApiRest.Data
{
    public interface IBudgetRepository
    {
        List<Factura> GetAll();
        bool Save(Factura factura);
        bool Delete(int id);
    }
}
