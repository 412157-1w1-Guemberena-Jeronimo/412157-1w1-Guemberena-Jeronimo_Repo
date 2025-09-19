using ComercioApiRest.Models;
namespace ComercioApiRest.Services
{
    public interface IBudgetService
    {
        List<Factura> GetAll();
        bool Save(Factura factura);
        bool Delete(int id);

    }
}
