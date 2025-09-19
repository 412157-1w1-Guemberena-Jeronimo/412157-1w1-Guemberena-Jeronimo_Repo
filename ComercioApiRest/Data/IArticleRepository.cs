using ComercioApiRest.Models;
namespace ComercioApiRest.Data
{
    public interface IArticleRepository
    {
        List<Articulo> GetAll();
        bool Save(Articulo articulo);
        bool Delete(int id);
    }
}
