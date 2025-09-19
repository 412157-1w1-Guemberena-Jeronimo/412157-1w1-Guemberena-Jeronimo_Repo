using ComercioApiRest.Models;
namespace ComercioApiRest.Services

{
    public interface IArticleService
    {
        List<Articulo> GetAll();
        bool Save(Articulo articulo);
        bool Delete(int id);
    }
}
